using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerControls : MonoBehaviour
{

    public static PlayerControls instance;
    public static Tile hoveredTile { get; set; }       //Tile that the player is currently touching while dragging finger/mouse on screen


    //****Event variables
    public delegate void D_ChangeTouchStatus(E_TouchStatus status);
    public static event D_ChangeTouchStatus TouchStatusChange;

    public delegate void D_SelectObject(GameObject obj);
    public static event D_SelectObject SelectObject;

    public delegate void D_ClearSelection();
    public static event D_ClearSelection ClearSelection;

    //****Touch variables
    public E_TouchStatus TouchStatus { get; set; }
    public Vector2[] touchPosBegin, touchPosCurrent, touchPosLast;
    private const float TOUCH_MOVE_DIST_THRESHOLD = 0.9f;
    private bool swiping = false;

    //****Camera variables
    private Camera cam;
    private const float CAM_MAX_Y = 2;
    private const float CAM_MIN_Y = -2;
    private const float CAM_MAX_X = 2;
    private const float CAM_MIN_X = -2;
    private const float CAM_ZOOM_MIN = 2.5f;
    private const float CAM_ZOOM_MAX = 7.5f;
    private const float CAM_DRAG_SPEED = 150;
    private const float CAM_ZOOM_SPEED = 75f;

    private void Awake()
    {
        instance = this;
        TouchStatus = E_TouchStatus.IDLE;
        cam = FindObjectOfType<Camera>();

        touchPosBegin = new Vector2[2];
        touchPosCurrent = new Vector2[2];
        touchPosLast = new Vector2[2];
    }


    private void Update()
    {
        DetectTouch();
    }


    private void DetectTouch()
    {
        //only call touch functions if pointer is not over a UI object
        if (!IsPointerOverUIObject())
        {
            //If app, look for touch inputs
            if (GameManager.IsApp)
            {
                //Only call these functions if two or less touch inputs at the same time
                if (Input.touchCount <= 2)
                {
                    for (int i = 0; i < Input.touchCount; i++)
                    {
                        if (Input.GetTouch(i).phase == TouchPhase.Began)
                        {
                            TouchBegin();
                        }
                        else if (Input.GetTouch(i).phase == TouchPhase.Ended)
                        {
                            TouchEnd();
                        }
                        else if (Input.GetTouch(i).phase == TouchPhase.Moved)
                        {
                            TouchSwipe();
                        }
                    }
                }
            }
            //if editor, look for mouse input
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    TouchBegin();
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    TouchEnd();
                }

                else if (Input.GetMouseButton(0))
                {
                    TouchSwipe();
                }
            }
        }
        //If pointer is above UI object, reset stored pointer position so it does not create errors next time user touches screen
        else
        {
            Vector2 pos = Vector2.zero;
            if (GameManager.IsApp)
            {
                pos = Input.GetTouch(0).position;
            }
            else
            {
                pos = Input.mousePosition;
            }


            touchPosBegin[0] = pos;
            touchPosCurrent[0] = pos;
            touchPosLast[0] = pos;

            //Cancel build in case player attempts to place a building on top of a UI button
            if((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0)){
                BuildManager.instance.CancelBuild();
            }
        }
    }


    //User touches screen with finger/mouse 
    private void TouchBegin()
    {
        if (GameManager.IsApp)
        {
            //store position of all touch inputs
            for (int i = 0; i < Input.touchCount; i++)
            {
                touchPosBegin[i] = Input.GetTouch(i).position;
                touchPosCurrent[i] = Input.GetTouch(i).position;
                touchPosLast[i] = touchPosBegin[i];
            }
        }
        else
        {
            //store position of mouse input
            touchPosBegin[0] = Input.mousePosition;
            touchPosCurrent[0] = Input.mousePosition;
            touchPosLast[0] = touchPosBegin[0];
        }

        //Touch input just began, so we are not (yet) swiping
        swiping = false;
    }


    //User moves mouse/finger on screen. Figure out if user is trying to drag camera or zoom 
    private void TouchSwipe()
    {
        if (GameManager.IsApp)
        {
            //Pan camera
            for (int i = 0; i < Input.touchCount; i++)
            {
                touchPosCurrent[i] = Input.GetTouch(i).position;
            }
            if (Input.touchCount == 1)
            {
                //Drag the camera
                TouchDrag();
            }
            else if (Input.touchCount == 2)
            {
                //Zoom the camera
                TouchZoom();
            }
        }
        else //if Unity Editor
        {
            //pan camera
            touchPosCurrent[0] = Input.mousePosition;
            TouchDrag();
        }
    }


    //User lifts finger/mouse
    private void TouchEnd()
    {
        hoveredTile = null;

        //Raycast at current touch position
        Ray mouseRay = cam.ScreenPointToRay(touchPosCurrent[0]);
        RaycastHit2D mouseHit = Physics2D.GetRayIntersection(mouseRay);

        if (TouchStatus == E_TouchStatus.IDLE)
        {
            //If it is a click and not released after dragging
            if (Vector2.Distance(touchPosBegin[0], touchPosCurrent[0]) < TOUCH_MOVE_DIST_THRESHOLD && swiping != true)
            {
                if (mouseHit.collider != null)
                {
                    TouchObject(mouseHit.transform.gameObject);
                }
                else
                {
                    ClearTileSelection();
                }
            }
        }
        else //if user is about to build
        {
            if (mouseHit.collider != null)
            {
                if (mouseHit.transform.tag == "Tile")
                {
                    TouchObject(mouseHit.transform.gameObject);
                }
                else
                {
                    BuildManager.instance.CancelBuild();
                }
            }
            else
            {
                BuildManager.instance.CancelBuild();
            }
        }
    }


    //User is attempting to drag camera
    private void TouchDrag()
    {
        //Find the tile currently hovered over
        Ray mouseRay = cam.ScreenPointToRay(touchPosCurrent[0]);
        RaycastHit2D mouseHit = Physics2D.GetRayIntersection(mouseRay);
        if (mouseHit.collider != null)
        {
            if (mouseHit.transform.tag == "Tile")
            {
                hoveredTile = mouseHit.transform.GetComponent<Tile>();
            }
        }
        else
        {
            hoveredTile = null;
        }

        //Move camera (only if touchStatus idle)
        if (TouchStatus == E_TouchStatus.IDLE)
        {
            //Only attempt to drag camera if user has moved touch a little bit
            if (Vector2.Distance(touchPosBegin[0], touchPosCurrent[0]) > TOUCH_MOVE_DIST_THRESHOLD)
            {
                if (swiping == true)
                {
                    Vector3 lastTouch = cam.ScreenToWorldPoint(touchPosLast[0]);
                    Vector3 curTouch = cam.ScreenToWorldPoint(touchPosCurrent[0]);

                    Vector3 dist = lastTouch - curTouch;
                    dist.z = 0;

                    //Clamp camera movement 
                    if (dist.y > 0 && cam.transform.position.y >= CAM_MAX_Y) //dragging up
                    {
                        dist.y = 0;
                        cam.transform.position = new Vector3(cam.transform.position.x, CAM_MAX_Y, cam.transform.position.z);
                    }
                    if (dist.x > 0 && cam.transform.position.x >= CAM_MAX_X) //dragging right
                    {
                        dist.x = 0;
                        cam.transform.position = new Vector3(CAM_MAX_X, cam.transform.position.y, cam.transform.position.z);
                    }
                    if (dist.y < 0 && cam.transform.position.y <= CAM_MIN_Y) //dragging down
                    {
                        dist.y = 0;
                        cam.transform.position = new Vector3(cam.transform.position.x, CAM_MIN_Y, cam.transform.position.z);
                    }
                    if (dist.x < 0 && cam.transform.position.x <= CAM_MIN_X) //dragging left
                    {
                        dist.x = 0;
                        cam.transform.position = new Vector3(CAM_MIN_X, cam.transform.position.y, cam.transform.position.z);
                    }

                    cam.transform.position = Vector3.Lerp(cam.transform.position, cam.transform.position + dist, Time.deltaTime * CAM_DRAG_SPEED);
                }

                swiping = true;
                touchPosLast[0] = touchPosCurrent[0];
            }
        }
    }


    private void TouchZoom()
    {
        Vector3[] lastTouch = new Vector3[2];
        Vector3[] curTouch = new Vector3[2];

        curTouch[0] = cam.ScreenToWorldPoint(touchPosCurrent[0]);
        curTouch[1] = cam.ScreenToWorldPoint(touchPosCurrent[1]);
        lastTouch[0] = cam.ScreenToWorldPoint(touchPosLast[0]);
        lastTouch[1] = cam.ScreenToWorldPoint(touchPosLast[1]);

        float distCur = Vector3.Distance(curTouch[0], curTouch[1]);
        float distLast = Vector3.Distance(lastTouch[0], lastTouch[1]);
        float dist = distLast - distCur;
       
        cam.orthographicSize += dist * Time.deltaTime * CAM_ZOOM_SPEED;

        if (cam.orthographicSize > CAM_ZOOM_MAX)
        {
            cam.orthographicSize = CAM_ZOOM_MAX;
        }
        else if (cam.orthographicSize < CAM_ZOOM_MIN)
        {
            cam.orthographicSize = CAM_ZOOM_MIN;
        }

        touchPosLast[0] = touchPosCurrent[0];
        touchPosLast[1] = touchPosCurrent[1];
    }



    public void TouchObject(GameObject obj)
    {
        switch (TouchStatus)
        {
            case E_TouchStatus.IDLE:
                SelectObject(obj);      //if it is null (raycast hit nothing), then everything will be deselected.
                break;
            case E_TouchStatus.BUILD:
                if (obj.GetComponent<Tile>() && obj.GetComponent<Tile>().TileStatus == E_TileStatus.EMPTY)
                {
                    BuildManager.buildingToBuild.GetComponent<Building>().BuildOnTile();

                    ChangeTouchStatus(E_TouchStatus.IDLE);
                }
                else
                {
                    BuildManager.instance.CancelBuild();
                }
                break;
        }
    }


    //NO LONGER USED! Saved for now, just in case
    public void TouchTile(Tile tile)
    {
        switch (TouchStatus)
        {
            case E_TouchStatus.IDLE:
                if(SelectObject != null)
                    SelectObject(tile.gameObject);
                break;
            case E_TouchStatus.BUILD:
                if (tile.TileStatus == E_TileStatus.EMPTY)
                {
                    BuildManager.buildingToBuild.GetComponent<Building>().BuildOnTile();

                    ChangeTouchStatus(E_TouchStatus.IDLE);
                }
                else
                {
                    BuildManager.instance.CancelBuild();
                }
                break;
        }
    }



    public void ClearTileSelection()
    {
        if(SelectObject != null)
        {
            SelectObject(null);
        }
    }


    public void ChangeTouchStatus(E_TouchStatus status)
    {
        TouchStatus = status;
        if (TouchStatusChange != null)
            TouchStatusChange(status);

        switch (status)
        {
            case E_TouchStatus.IDLE:
                TileManager.instance.ToggleTileAvailability(false);
                break;
            case E_TouchStatus.BUILD:
                TileManager.instance.ToggleFullTileAvailability(true);
                break;
        }
    }


    //Substitute function for IsPointerOverGameObject, as it does not work with touch
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}

public enum E_TouchStatus
{
    IDLE,
    BUILD
}
