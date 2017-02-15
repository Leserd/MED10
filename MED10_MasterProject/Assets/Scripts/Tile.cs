﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour {

    public E_TileStatus TileStatus { get; set; }
    public bool showTileStatus = false;

    private MeshRenderer highlightPlane;
    private Color colorEmpty = Color.green;
    private Color colorFull = Color.red;
    private Color colorCurrent;

	void Awake() {
        TileStatus = E_TileStatus.EMPTY;
        colorCurrent = colorEmpty;
        highlightPlane = transform.FindChild("HighlightPlane").GetComponent<MeshRenderer>();
        if (TileStatus == E_TileStatus.EMPTY)
            colorCurrent = colorEmpty;
        else
            colorCurrent = colorFull;
        ChangeHighlightColor();

        TileManager.ToggleTileStatus += ToggleShowStatus;
    }

	//void OnMouseDown() {
 //       //Dont detect clicks if user taps button
 //       if (!EventSystem.current.IsPointerOverGameObject())
 //       {
 //           PlayerControls.instance.TouchTile(this);
 //       }
        
 //   }

    public void ChangeTileStatus(E_TileStatus status)
    {
        switch (status)
        {
            case E_TileStatus.EMPTY:
                colorCurrent = colorEmpty;
                TileStatus = E_TileStatus.EMPTY;
                break;
            case E_TileStatus.FULL:
                colorCurrent = colorFull;
                TileStatus = E_TileStatus.FULL;
                break;
        }
        ChangeHighlightColor();
    }

    public void ChangeHighlightColor()
    {
        highlightPlane.material.color = colorCurrent;
    }

    public void ToggleShowStatus()
    {
        showTileStatus = !showTileStatus;
        highlightPlane.enabled = showTileStatus;
    }

    public void ToggleShowStatus(bool show)
    {
        showTileStatus = show;
        highlightPlane.enabled = showTileStatus;
    }
}

public enum E_TileStatus
{
    EMPTY,
    FULL
}
