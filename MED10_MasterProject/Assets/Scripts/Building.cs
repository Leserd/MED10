using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

    public int buildingSize;
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
    private Tile hoveredTile;



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
            UpdatePosition();
        }
    }



    private void UpdatePosition()
    {
        if (PlayerControls.hoveredTile)
        {
            if (PlayerControls.hoveredTile != hoveredTile)
            {
                //Update hoveredTile
                hoveredTile = PlayerControls.hoveredTile;

                //Find touching tiles (touchingTiles must be set before calculating position)
                touchingTiles = GetTouchingTiles();

                //Update position (After finding the touching tiles of the new position)
                transform.position = CalculatePosition();

                //Check whether the building can be built here
                SetPossibleToBuild(CalculateBuildability());
            }
        }
        else
        {
            Plane plane = new Plane(Vector3.back, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitdist = 0.0f;

            if (plane.Raycast(ray, out hitdist))
            {
                transform.position = ray.GetPoint(hitdist);
            }

            SetPossibleToBuild(false);
            hoveredTile = null;
        }

        sprite.sortingOrder = CalculateSortingOrder();
    }



    private Vector3 CalculatePosition()
    {
        Vector3 newPos = Vector3.zero;

        if(hoveredTile != null)
        {
            if (touchingTiles.Count > 0)
            {
                if (buildingSize == 1)
                {
                    newPos = hoveredTile.transform.position + BuildManager.buildingOffset;
                }
                else
                {
                    foreach (Tile tile in touchingTiles)
                    {
                        newPos += tile.transform.position + BuildManager.buildingOffset;
                    }
                    newPos /= touchingTiles.Count;
                }
            }
        }
       
        return newPos;
    }



    public int CalculateSortingOrder()
    {
        if(touchingTiles.Count == 0)
        {
            return 10; //Arbitrarily high number to place building on top of any other tiles or buildings 
        }
        else
        {
            int sortingOrder = -999;    //arbitrary number lower than the lowest sortingOrder value in the tiles.

            foreach (Tile tile in touchingTiles)
            {
                if (tile.GetComponent<SpriteRenderer>().sortingOrder > sortingOrder)
                {
                    sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder;
                }
            }
            return sortingOrder + 1;
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



    public void ChangeStatus(E_BuildingStatus s)
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



    public List<Tile> GetTouchingTiles()
    {
        List<Tile> tilesHit = new List<Tile>();

        if (hoveredTile)
        {

            List<Vector2> tileCoordsToCheck = new List<Vector2>();

            Vector2 curTileCoords = new Vector2(hoveredTile.x, hoveredTile.y);

            //The hoveredTile is always being touched
            tileCoordsToCheck.Add(Vector2.zero);

            switch (buildingSize)
            {
                case 1:
                    //Add nothing
                    break;
                case 2:
                    tileCoordsToCheck.Add(new Vector2(0, 1)); //Up
                    break;
                case 4:
                    tileCoordsToCheck.Add(new Vector2(1, 0));  //Right
                    tileCoordsToCheck.Add(new Vector2(1, 1)); //Up to the right
                    tileCoordsToCheck.Add(new Vector2(0, 1)); //Up
                    break;
                default:
                    //Add nothing
                    break;
            }

            for(int i = 0; i < tileCoordsToCheck.Count; i++)
            {
                int x = (int)curTileCoords.x + (int)tileCoordsToCheck[i].x;
                int y = (int)curTileCoords.y + (int)tileCoordsToCheck[i].y;

                //If the coords point to a non-existant tile, dont try to add it
                if (x > TileManager.instance.tiles.GetLength(0) - 1 || x < 0 
                    || y > TileManager.instance.tiles.GetLength(1) - 1 || y < 0)
                {
                    continue;
                }
                    

                tilesHit.Add(TileManager.instance.tiles[x, y]);
            }

        }

        return tilesHit;
    }



    public bool CalculateBuildability()
    {
        if (touchingTiles.Count == 0 || touchingTiles.Count != buildingSize)
            return false;

        bool buildability = true;

        foreach(Tile tile in touchingTiles)
        {
            if (tile.TileStatus == E_TileStatus.FULL)
                buildability = false;
        }

        return buildability;
    }



    public void BuildOnTile()
    {
        foreach(Tile tile in touchingTiles)
        {
            tile.AssignBuilding(BuildManager.buildingToBuild.GetComponent<Building>());
            tile.ChangeTileStatus(E_TileStatus.FULL);
        }

        //Update position and sortingOrder, to be sure it is placed properly
        transform.position = CalculatePosition();
        sprite.sortingOrder = CalculateSortingOrder();

        //Remove the (disabled) button for this building in the build bar
        BuildManager.instance.RemoveActiveBuildButton();

        ChangeStatus(E_BuildingStatus.BEING_BUILT);
    }
}



public enum E_BuildingStatus
{
    CHOOSE_LOCATION,
    BEING_BUILT,
    COMPLETE
}