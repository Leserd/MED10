using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour {

    public E_TileStatus TileStatus;
    private E_TileStatus _tileStatus;
    [SerializeField] private Building attachedBuilding;
    public bool showTileStatus = false;
    private SpriteRenderer sprite;
    private MeshRenderer highlightPlane;
    private Color colorEmpty = Color.green;
    private Color colorFull = Color.red;
    private Color colorDefault = Color.white;
    private Color colorSelected = Color.cyan;
    private Color colorStatus;
    private bool selected = false;
    public int x, y;
    private PolygonCollider2D _collider;
    

    void Awake() {
        if (GetComponent<PolygonCollider2D>())
            _collider = GetComponent<PolygonCollider2D>();
        else
            Debug.LogWarning(transform.name + " has no PolygonCollider2D. Will cause errors.");

        if (TileStatus == E_TileStatus.EMPTY)
        {
            colorStatus = colorEmpty;
            _collider.enabled = true;
        }
        else
        {
            _collider.enabled = false;
            colorStatus = colorFull;
        }

        sprite = GetComponent<SpriteRenderer>();
        TileManager.ToggleTileStatus += ToggleHighlight;
        TileManager.ToggleFullTileStatus += ToggleHighlightFull;

        PlayerControls.SelectObject += Select;
    }



    private void Start()
    {
        TileManager.instance.SetTileListCoords(this, x, y);
    }



    //Called when user selects this tile
    public void Select(GameObject obj)
    {
        bool select = true; //Should this object be selected

        if (obj == null)
        {
            select = false;
        }   
        else if(obj != gameObject)
        {
            if(obj.tag == "Building")
            {
                if (obj.GetComponent<Building>() == attachedBuilding)
                {
                    select = true;
                }
                else
                {
                    select = false;
                }
            }
            else
            {
                select = false;
            }
        }

        if (select)
        {
            if (selected)   //was it already selected?
            {
                return;
            }
            else
            {
                //Debug.Log("Selected " + gameObject.name);
                selected = true;
                //Highlight
                ToggleHighlight(true);
                if (attachedBuilding)
                    attachedBuilding.Select(attachedBuilding.gameObject);
            }
        }
        else
        {
            if (selected)   //only deselect if it was currently selected
                Deselect();
        }



        //if ((obj == null || (obj != gameObject && (obj.tag == "Building" && attachedBuilding != obj.GetComponent<Building>())))  && selected)
        //{
        //    Deselect();
        //}
        //else if ((obj == gameObject || (obj.tag == "Building" && attachedBuilding == obj.GetComponent<Building>())) && !selected)
        //{
        //    Debug.Log("Selected " + gameObject.name);
        //    selected = true;
        //    //Highlight
        //    ToggleHighlight(true);
        //    if (attachedBuilding)
        //        attachedBuilding.Select(attachedBuilding.gameObject);
        //}
    }



    //Called when user select another tile or clears tile selection
    public void Deselect()
    {
        //Dehighlight
        ToggleHighlight(false);
        selected = false;
        //Debug.Log("Deselected " + gameObject.name);

    }



    public void ChangeTileStatus(E_TileStatus status)
    {
        switch (status)
        {
            case E_TileStatus.EMPTY:
                colorStatus = colorEmpty;
                _collider.enabled = true;
                TileStatus = E_TileStatus.EMPTY;
                break;
            case E_TileStatus.FULL:
                colorStatus = colorFull;
                _collider.enabled = false;
                TileStatus = E_TileStatus.FULL;
                break;
        }
    }



    public void ToggleHighlight()
    {
        showTileStatus = !showTileStatus;

        if (showTileStatus)
        {
            if (selected)
                sprite.color = colorSelected;
            else
                sprite.color = colorStatus;
        }
            
        else
            sprite.color = colorDefault;
    }



    public void ToggleHighlight(bool show)
    {
        showTileStatus = show;
        if (showTileStatus)
            if (selected)
                sprite.color = colorSelected;
            else
                sprite.color = colorStatus;
        else
            sprite.color = colorDefault;
    }



    public void ToggleHighlightFull(bool show)
    {
        if(TileStatus == E_TileStatus.FULL)
        {
            showTileStatus = show;
            if (showTileStatus)
                sprite.color = colorStatus;
            else
                sprite.color = colorDefault;
        }
    }



    public void AssignBuilding(Building building)
    {
        attachedBuilding = building;
        _collider.enabled = false;
    }



    public void SetTileCoordinates(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}



public enum E_TileStatus
{
    EMPTY,
    FULL
}
