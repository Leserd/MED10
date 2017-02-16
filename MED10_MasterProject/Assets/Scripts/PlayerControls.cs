using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerControls : MonoBehaviour {

    public static PlayerControls instance;
    public GameObject testSprite;
    public E_TouchStatus TouchStatus { get; set; }
    public delegate void D_ChangeTouchStatus(E_TouchStatus status);
    public static event D_ChangeTouchStatus TouchStatusChange;
    private Tile selectedTile;
    private Camera cam;
    private Vector2 touchPosBegin, touchPosCurrent, touchPosLast;
    private const float TOUCH_MOVE_DIST_THRESHOLD = 0.9f;
    private bool swiping = false;

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
            print("Moving");
            
            if (swiping == true)
            {
                Vector2 dist = touchPosLast - touchPosCurrent;
                cam.transform.position = Vector3.Lerp(cam.transform.position, cam.transform.position + new Vector3(dist.x, dist.y, 0), Time.deltaTime * 2);
            }
            swiping = true;

        }
        touchPosLast = touchPosCurrent;
    }



    private void TouchEnd()
    {
        Vector2 touchPos;
#if UNITY_EDITOR
        touchPos = Input.mousePosition;
#elif UNITY_ANDROID
        touchPos = Input.GetTouch(0).position;
#endif

        if (Vector2.Distance(touchPosBegin, touchPos) < TOUCH_MOVE_DIST_THRESHOLD && swiping != true)
        {
            Ray mouseRay = cam.ScreenPointToRay(touchPos);
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
                    BuildManager.instance.BuildOnTile(tile);

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
