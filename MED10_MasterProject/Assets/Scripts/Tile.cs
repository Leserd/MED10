using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour {

    public E_TileStatus TileStatus { get; set; }
    private Building attachedBuilding;
    public bool showTileStatus = false;
    private SpriteRenderer sprite;
    private MeshRenderer highlightPlane;
    private Color colorEmpty = Color.green;
    private Color colorFull = Color.red;
    private Color colorDefault = Color.white;
    private Color colorStatus;
    public int x, y;



	void Awake() {
        TileStatus = E_TileStatus.EMPTY;
        colorStatus = colorEmpty;
        sprite = GetComponent<SpriteRenderer>();
        TileManager.ToggleTileStatus += ToggleHighlight;
        TileManager.ToggleFullTileStatus += ToggleHighlightFull;


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
                //TileManager.ToggleTileStatus += ToggleHighlight;
                break;
        }
    }

    //TODO: gør så tiles kan toggles hvis de ikke er full (tiles forbliver grønne efter fejlet bygning)

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
