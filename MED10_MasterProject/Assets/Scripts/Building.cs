using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

    public E_BuildingSize buildingSize;
    public float buildTime;
    public E_BuildingStatus status;
    public bool possibleToBuild = false;
    private SpriteRenderer sprite;
    private Color colorDefault = Color.white;
    private Color colorPossible = new Color(0, 1, 0, 0.5f);
    private Color colorImpossible = new Color(1, 0, 0, 0.5f);
    private Color halfTransparent = new Color(1, 1, 1, 0.4f);
    private Color fullTransparent = new Color(1, 1, 1, 0.0f);
    private List<Tile> touchingTiles;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sortingOrder = 10;   //Sorting order will be changed when the building is placed on a tile
        sprite.color = halfTransparent;
        touchingTiles = new List<Tile>();
    }

    private void Update()
    {
        if(status == E_BuildingStatus.CHOOSE_LOCATION)
        {
            if(PlayerControls.hoveredTile != null)
            {
                transform.position = PlayerControls.hoveredTile.transform.position + BuildManager.buildingOffset;
                if(PlayerControls.hoveredTile.TileStatus == E_TileStatus.EMPTY)
                {
                    SetPossibleToBuild(true);
                }
                else
                {
                    SetPossibleToBuild(false);
                }
            }
            else
            {
                Plane plane = new Plane(Vector3.back, Vector3.zero);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                Vector3 mousePos = Vector3.zero;
                float hitdist = 0.0f;

                if (plane.Raycast(ray, out hitdist))
                {
                    mousePos = ray.GetPoint(hitdist);
                }

                transform.position = mousePos;
                SetPossibleToBuild(false);
            }
            
        }
    }

    //For now it shows building completeness by changing alpha value of sprite
    public IEnumerator Build()
    {
        sprite.color = Color.white;
        GetComponent<EdgeCollider2D>().enabled = false;

        float startTime = Time.time;
        float endTime = startTime + buildTime;

        sprite.color = fullTransparent;
        while(endTime > Time.time)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a + (Time.deltaTime / buildTime));
            yield return new WaitForEndOfFrame();
        }
        sprite.color = colorDefault;
        ChangeStatus(E_BuildingStatus.COMPLETE);
    }

    public void SetPossibleToBuild(bool possibility)
    {
        possibleToBuild = possibility;
        if (possibleToBuild)
            sprite.color = colorPossible;
        else
            sprite.color = colorImpossible;
    }

    public  void ChangeStatus(E_BuildingStatus s)
    {
        status = s;
        switch (status)
        {
            case E_BuildingStatus.CHOOSE_LOCATION:
                break;
            case E_BuildingStatus.BEING_BUILT:
                StartCoroutine(Build());
                break;
            case E_BuildingStatus.COMPLETE:
                break;

        }
    }

    public void BuildOnTile()
    {
        int sortingOrder = -999;    //arbitrary number lower than the lowest sortingOrder value in the tiles.
        Vector3 buildingPos = Vector3.zero;

        foreach(Tile tile in touchingTiles)
        {
            tile.AssignBuilding(BuildManager.BuildingToBuild.GetComponent<Building>());
            tile.ChangeTileStatus(E_TileStatus.FULL);
            buildingPos += tile.transform.position + BuildManager.buildingOffset;
            if (tile.GetComponent<SpriteRenderer>().sortingOrder > sortingOrder)
            {
                sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder;
            }
        }
        buildingPos /= touchingTiles.Count;

        transform.position = buildingPos;

        GetComponent<SpriteRenderer>().sortingOrder = sortingOrder + 1;
        
        ChangeStatus(E_BuildingStatus.BEING_BUILT);
    }



    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Tile" && status == E_BuildingStatus.CHOOSE_LOCATION)
        {
            Tile tile = col.GetComponent<Tile>();
            touchingTiles.Add(tile);
            if(tile.TileStatus == E_TileStatus.FULL)
            {
                SetPossibleToBuild(false);
            }
            else
            {
                SetPossibleToBuild(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Tile" && status == E_BuildingStatus.CHOOSE_LOCATION)
        {
            if (touchingTiles.Contains(col.GetComponent<Tile>()))
            {
                touchingTiles.Remove(col.GetComponent<Tile>());
            }

            int fullCounter = 0;
            foreach (Tile tile in touchingTiles)
            {

                if(tile.TileStatus == E_TileStatus.FULL)
                {
                    fullCounter++;
                }
            }
            if(fullCounter == 0)
            {
                SetPossibleToBuild(true);
            }
            else
            {
                SetPossibleToBuild(false);
            }

        }
    }
}

public enum E_BuildingSize
{
    ONE,
    TWO,
    THREE_L,
    THREE_LINE,
    FOUR_SQUARE,
    FOUR_L
}

public enum E_BuildingStatus
{
    CHOOSE_LOCATION,
    BEING_BUILT,
    COMPLETE
}