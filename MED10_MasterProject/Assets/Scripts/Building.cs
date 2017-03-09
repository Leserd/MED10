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
    private Vector3 buildingPosition = Vector3.zero;
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
            if (PlayerControls.hoveredTile)
            {
                if (PlayerControls.hoveredTile != hoveredTile)
                {
                    transform.position = PlayerControls.hoveredTile.transform.position + BuildManager.buildingOffset;
                    hoveredTile = PlayerControls.hoveredTile;
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
                    print("This happenned");
                }
                //print("not hitting anything");
                SetPossibleToBuild(false);
                hoveredTile = null;
            }

            //transform.position = CalculatePosition();
            sprite.sortingOrder = CalculateSortingOrder();
        }
    }


    private Vector3 CalculatePosition()
    {
        print(touchingTiles.Count);
        Vector3 newPos = Vector3.zero;

        if(hoveredTile != null)
        {
            if (touchingTiles.Count > 0)
            {
                if (buildingSize == E_BuildingSize.ONE)
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
                    print("Calculating tile pos");
                }

            }
        }
        
        else
        {
            Plane plane = new Plane(Vector3.back, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitdist = 0.0f;

            if (plane.Raycast(ray, out hitdist))
            {
                newPos = ray.GetPoint(hitdist);
                print("This happenned");
            }
            //print("not hitting anything");
            SetPossibleToBuild(false);         
        }




        ////If building is not hitting any tiles
        //if (touchingTiles.Count == 0 && hoveredTile == null)
        //{
            
        //}
        ////If building is hitting tiles
        //else 
        //{
        //    Ray mouseRay = Camera.main.ScreenPointToRay(PlayerControls.instance.touchPosCurrent[0]);
        //    RaycastHit2D mouseHit = Physics2D.GetRayIntersection(mouseRay);
        //    //if mouse is above tile that is not in the currently touched tiles
        //    if (mouseHit.collider != null)
        //    {
        //        if (mouseHit.transform.tag == "Tile")
        //        {
        //            if (!touchingTiles.Contains(mouseHit.transform.GetComponent<Tile>()))
        //            {
        //                print("Tile not in touchingTiles");
        //                newPos =  mouseHit.point;
        //            }
        //        }
        //    }
            
        //    else
        //    {
        //        foreach (Tile tile in touchingTiles)
        //        {
        //            newPos += tile.transform.position + BuildManager.buildingOffset;
        //        }
        //        newPos /= touchingTiles.Count;
        //        print("Calculating tile pos");

        //    }
        //}

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

            transform.position = CalculatePosition();
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
            
            if (fullCounter == 0)
            {
                SetPossibleToBuild(true);
                Debug.Log("Counter: " + fullCounter + ", " + possibleToBuild);

            }
            else
            {
                SetPossibleToBuild(false);
                Debug.Log("Counter: " + fullCounter + ", " + possibleToBuild);

            }


            transform.position = CalculatePosition();
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