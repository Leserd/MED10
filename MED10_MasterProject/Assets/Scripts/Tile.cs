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

	void Awake() {
        TileStatus = E_TileStatus.EMPTY;
        colorStatus = colorEmpty;
        //highlightPlane = transform.FindChild("HighlightPlane").GetComponent<MeshRenderer>();
        sprite = GetComponent<SpriteRenderer>();

        TileManager.ToggleTileStatus += ToggleHighlight;
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
        //highlightPlane.enabled = showTileStatus;
        if (showTileStatus)
            sprite.color = colorStatus;
        else
            sprite.color = colorDefault;
    }

    public void ToggleHighlight(bool show)
    {
        showTileStatus = show;
        //highlightPlane.enabled = showTileStatus;
        if (showTileStatus)
            sprite.color = colorStatus;
        else
            sprite.color = colorDefault;
    }

    public void BuildOnTile()
    {
        attachedBuilding = Instantiate(BuildManager.BuildingToBuild, transform.position + BuildManager.buildingOffset, Quaternion.identity).GetComponent<Building>();
        attachedBuilding.transform.SetParent(CreateBuilding.Instance.transform);
        attachedBuilding.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1;
        ChangeTileStatus(E_TileStatus.FULL);
        attachedBuilding.GetComponent<BuyBuilding>().ButtonPress(attachedBuilding.transform);

    }
}

public enum E_TileStatus
{
    EMPTY,
    FULL
}
