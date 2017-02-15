using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerControls : MonoBehaviour {

    public static PlayerControls instance;
    public E_TouchStatus TouchStatus { get; set; }
    public delegate void D_ChangeTouchStatus(E_TouchStatus status);
    public static event D_ChangeTouchStatus TouchStatusChange;
    private Tile selectedTile;
    private Camera cam;
    private Vector2 touchPosBegin, touchPosCurrent;
    private const float TOUCH_MOVE_DIST_THRESHOLD = 0.75f;
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

#if UNITY_ANDROID
        if(Input.touchCount > 2)
        {
            //Zoom the camera
        }
        else if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                swiping = false;
                touchPosBegin = Input.GetTouch(0).position;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (Vector2.Distance(touchPosBegin, Input.GetTouch(0).position) < TOUCH_MOVE_DIST_THRESHOLD && swiping != true)
                {
                    // Construct a ray from the current touch coordinates
                    Ray touchRay = cam.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit touchHit = new RaycastHit();
                    // Create a particle if hit
                    if (Physics.Raycast(touchRay, out touchHit))
                    {
                        if (touchHit.transform.tag == "Tile")
                        {
                            if (!EventSystem.current.IsPointerOverGameObject())
                                TouchTile(touchHit.transform.GetComponent<Tile>());
                        }
                    }
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                //Pan camera
                touchPosCurrent = Input.GetTouch(0).position;
                if (Vector2.Distance(touchPosBegin, touchPosCurrent) > TOUCH_MOVE_DIST_THRESHOLD)
                {
                    print("Moving");
                    swiping = true;
                }
            }
        }

#endif


#if UNITY_EDITOR

        if (Input.GetMouseButtonDown(0))
        {
            swiping = false;
            touchPosBegin = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0)) { 
            if(Vector2.Distance(touchPosBegin, Input.mousePosition) < TOUCH_MOVE_DIST_THRESHOLD && swiping != true)
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit mouseHit;

                if (Physics.Raycast(ray, out mouseHit))
                {
                    if (mouseHit.transform.tag == "Tile")
                    {
                        if (!EventSystem.current.IsPointerOverGameObject())
                            TouchTile(mouseHit.transform.GetComponent<Tile>());
                    }
                }
            }
        }

        else if (Input.GetMouseButton(0))
        {
            //pan camera
            touchPosCurrent = Input.mousePosition;
            if(Vector2.Distance(touchPosBegin, touchPosCurrent) > TOUCH_MOVE_DIST_THRESHOLD)
            {
                print("Moving");
                swiping = true;
            }
        }

#endif
    }

    public void TouchTile(Tile tile)
    {
        switch (TouchStatus)
        {
            case E_TouchStatus.IDLE:
                if (selectedTile != null && selectedTile != tile)
                {
                    selectedTile.ToggleShowStatus(false);
                }
                selectedTile = tile;
                selectedTile.ToggleShowStatus(true);
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
