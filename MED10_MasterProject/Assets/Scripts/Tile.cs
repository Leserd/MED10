using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour {

    public E_TileStatus TileStatus;
    private E_TileStatus _tileStatus;
    private Building attachedBuilding;
    public bool showTileStatus = false;
    private SpriteRenderer sprite;
    private MeshRenderer highlightPlane;
    private Color colorEmpty = Color.green;
    private Color colorFull = Color.red;
    private Color colorDefault = Color.white;
    private Color colorStatus;
    private bool selected = false;
    public int x, y;

    

    void Awake() {
        if (TileStatus == E_TileStatus.EMPTY)
            colorStatus = colorEmpty;
        else
            colorStatus = colorFull;

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
        ////Deselect if object is null or if obj != go while ob
        //if ((obj == null ||
        //    (obj != gameObject && obj.GetComponent<Building>() != null && obj.GetComponent<Building>() != attachedBuilding))
        //    && selected)
        //{
        //    Deselect();
        //}
        //else if ((obj == gameObject || (obj.GetComponent<Building>() != null && obj.GetComponent<Building>() == attachedBuilding)) && !selected)
        //{
        //    //Highlight
        //    ToggleHighlight(true);
        //    selected = true;
        //    //Debug.Log("Selected " + gameObject.name);
        //    //if building placed, select building instead?
        //    if (attachedBuilding)
        //        attachedBuilding.Select(attachedBuilding.gameObject);
        //}

        if ((obj == null || obj != gameObject) && selected)
        {
            Deselect();
        }
        else if (obj == gameObject && !selected)
        {
            //Debug.Log("Selected " + gameObject.name);
            selected = true;
            //Highlight
            ToggleHighlight(true);
            if (attachedBuilding)
                attachedBuilding.Select(attachedBuilding.gameObject);
        }
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
                TileStatus = E_TileStatus.EMPTY;
                break;
            case E_TileStatus.FULL:
                colorStatus = colorFull;
                TileStatus = E_TileStatus.FULL;
                break;
        }
    }



    public void ToggleHighlight()
    {
        showTileStatus = !showTileStatus;

        if (showTileStatus)
            sprite.color = colorStatus;
        else
            sprite.color = colorDefault;
    }



    public void ToggleHighlight(bool show)
    {
        showTileStatus = show;
        if (showTileStatus)
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
