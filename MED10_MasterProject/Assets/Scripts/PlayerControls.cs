using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerControls : MonoBehaviour {

    public static PlayerControls instance;
    private Tile selectedTile;

    //****Event variables
    public delegate void D_ChangeTouchStatus(E_TouchStatus status);
    public static event D_ChangeTouchStatus TouchStatusChange;

    //****Touch variables
    public E_TouchStatus TouchStatus { get; set; }
    private Vector2 touchPosBegin, touchPosCurrent, touchPosLast;
    private const float TOUCH_MOVE_DIST_THRESHOLD = 0.9f;
    private bool swiping = false;

    //****Camera variables
    private Camera cam;
    private const float CAM_MAX_Y = 4;
    private const float CAM_MIN_Y = -3;
    private const float CAM_MAX_X = 18;
    private const float CAM_MIN_X = 2;
    private const float CAM_DRAG_SPEED = 150;


    private void Awake()
    {
        instance = this;
        TouchStatus = E_TouchStatus.IDLE;
        cam = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        DetectTouch();
    }

    private void DetectTouch()
    {


        if (!EventSystem.current.IsPointerOverGameObject())
        {
#if UNITY_EDITOR
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

#elif UNITY_ANDROID
            if (Input.touchCount > 2)
            {
                //Zoom the camera
            }
            else if (Input.touchCount == 1)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    TouchBegin();
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    TouchEnd();
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    TouchSwipe();
                }
            }

#endif
        }
    }


    private void TouchBegin()
    {
#if UNITY_EDITOR     
        touchPosBegin = Input.mousePosition;
#elif UNITY_ANDROID
        touchPosBegin = Input.GetTouch(0).position;
#endif
        touchPosLast = touchPosBegin;
        swiping = false;
    }


    private void TouchSwipe()
    {
#if UNITY_EDITOR
        //pan camera
        touchPosCurrent = Input.mousePosition;

#elif UNITY_ANDROID
        //Pan camera
        touchPosCurrent = Input.GetTouch(0).position;
#endif

        if (Vector2.Distance(touchPosBegin, touchPosCurrent) > TOUCH_MOVE_DIST_THRESHOLD)
        {
            //print("Moving");
            
            if (swiping == true)
            {
                Vector3 lastTouch = cam.ScreenToWorldPoint(touchPosLast);
                Vector3 curTouch = cam.ScreenToWorldPoint(touchPosCurrent);

                Vector3 dist = lastTouch - curTouch;
                dist.z = 0;

                //Clamp camera movement 
                if(dist.y > 0 && cam.transform.position.y >= CAM_MAX_Y) //dragging up
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

        }
        touchPosLast = touchPosCurrent;
    }



    private void TouchEnd()
    {

#if UNITY_EDITOR
        touchPosCurrent = Input.mousePosition;
#elif UNITY_ANDROID
        touchPosCurrent = Input.GetTouch(0).position;
#endif

        if (Vector2.Distance(touchPosBegin, touchPosCurrent) < TOUCH_MOVE_DIST_THRESHOLD && swiping != true)
        {
            Ray mouseRay = cam.ScreenPointToRay(touchPosCurrent);
            RaycastHit2D mouseHit = Physics2D.GetRayIntersection(mouseRay);
            if (mouseHit.collider != null)
            {
                if (mouseHit.transform.tag == "Tile")
                {
                    TouchTile(mouseHit.transform.GetComponent<Tile>());
                }
            }
            else
                print("NOTHING WAS HIT!");
        }
    }


    private void TouchZoom()
    {
#if UNITY_ANDROID
        //Zoom the camera
#endif
    }

    public void TouchTile(Tile tile)
    {
        switch (TouchStatus)
        {
            case E_TouchStatus.IDLE:
                if (selectedTile != null && selectedTile != tile)
                {
                    selectedTile.ToggleHighlight(false);
                }
                selectedTile = tile;
                selectedTile.ToggleHighlight(true);
                break;
            case E_TouchStatus.BUILD:
                if(tile.TileStatus == E_TileStatus.EMPTY)
                {
                    tile.BuildOnTile();

                    ChangeTouchStatus(E_TouchStatus.IDLE);
                }
                break;
        }
    }

    public void ChangeTouchStatus(E_TouchStatus status)
    {
        TouchStatus = status;
        if(TouchStatusChange != null)
            TouchStatusChange(status);

        switch (status)
        {
            case E_TouchStatus.IDLE:
                TileManager.instance.ToggleTileAvailability(false);
                break;
            case E_TouchStatus.BUILD:
                TileManager.instance.ToggleTileAvailability(true);
                break;
        }
    }
}

public enum E_TouchStatus
{
    IDLE,
    BUILD
}
