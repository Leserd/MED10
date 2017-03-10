using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

    public GameObject tilePrefab2D;
    public GameObject tileGridPrefab2D;
    public Tile[,] tiles;
    public static TileManager instance;
    public delegate void BuildingLocations(bool show);
    public static event BuildingLocations ToggleTileStatus;

    private const float TILE_GRID_X_DIST = 1.95f;
    private const float TILE_GRID_Y_DIST = -0.99f;
    private const int TILE_GRID_X_SIZE = 12;
    private const int TILE_GRID_Y_SIZE = 8;

    private void Awake()
    {
        instance = this;
        tiles = new Tile[TILE_GRID_X_SIZE, TILE_GRID_Y_SIZE];
    }

    private void Start()
    {
        SetUpTileGrid();                              //Spawn tile grid instantly
        //StartCoroutine(SetUpTileGridCoroutine());     //Spawn tile grid over time
        //SetUpTilesByPrefab();                           //Spawn tile grid instantly via prefab
    }

    private void SetUpTilesByPrefab()
    {
        Instantiate(tileGridPrefab2D);
    }

    private void SetUpTileGrid()
    {
        Vector3 startPos = new Vector3(0, 0, 0);
        int renderOrder = (TILE_GRID_X_SIZE * TILE_GRID_Y_SIZE) * (-1);
        Transform tileParent = new GameObject("Tiles").transform;
       
        for (int y = 0; y < TILE_GRID_Y_SIZE; y++)
        {
            for (int x = TILE_GRID_X_SIZE; x > 0; x--)
            //for (int x = 0; x < TILE_GRID_X_SIZE; x++)    //This spawns tiles in a more logical order (but messes up rendering order)
            {
                Vector3 pos = startPos + new Vector3((x * TILE_GRID_X_DIST / 2) + (y * TILE_GRID_X_DIST / 2), 
                    (y * TILE_GRID_Y_DIST / 2) - (x * TILE_GRID_Y_DIST / 2), 0);
                renderOrder++;
                SpriteRenderer tile = Instantiate(tilePrefab2D, pos, Quaternion.identity).GetComponent<SpriteRenderer>();
                tile.transform.name = "Tile(" + x + "/" + (TILE_GRID_Y_SIZE - y) + ")"; 
                tile.sortingOrder = renderOrder;
                tiles[x-1, TILE_GRID_Y_SIZE - y - 1] = tile.GetComponent<Tile>();
                tile.GetComponent<Tile>().SetTileCoordinates(x - 1, TILE_GRID_Y_SIZE - y - 1);
                tile.transform.parent = tileParent;
            }
        }
    }

    //If we want the grid to be placed over time instead of instantly
    private IEnumerator SetUpTileGridCoroutine()
    {
        Vector3 startPos = new Vector3(0, 0, 0);
        int renderOrder = (TILE_GRID_X_SIZE * TILE_GRID_Y_SIZE) * (-1);
        Transform tileParent = new GameObject("Tiles").transform;

        for (int y = 0; y < TILE_GRID_Y_SIZE; y++)
        {
            for (int x = TILE_GRID_X_SIZE; x > 0; x--)
            //for (int x = 0; x < TILE_GRID_X_SIZE; x++)    //This spawns tiles in a more logical order (but messes up rendering order)
            {
                Vector3 pos = startPos + new Vector3((x * TILE_GRID_X_DIST / 2) + (y * TILE_GRID_X_DIST / 2),
                    (y * TILE_GRID_Y_DIST / 2) - (x * TILE_GRID_Y_DIST / 2), 0);
                renderOrder++;
                SpriteRenderer tile = Instantiate(tilePrefab2D, pos, Quaternion.identity).GetComponent<SpriteRenderer>();
                tile.transform.name = "Tile(" + x + "/" + (TILE_GRID_Y_SIZE - y) + ")";
                tile.sortingOrder = renderOrder;
                tiles[x - 1, TILE_GRID_Y_SIZE - y - 1] = tile.GetComponent<Tile>();
                tile.GetComponent<Tile>().SetTileCoordinates(x - 1, TILE_GRID_Y_SIZE - y - 1);
                tile.transform.parent = tileParent;
                yield return new WaitForSeconds(Time.deltaTime / (TILE_GRID_X_SIZE * TILE_GRID_Y_SIZE) / 3);
            }
        }
    }

    public void ToggleTileAvailability(bool b)
    {
        if (ToggleTileStatus != null)
        {
            ToggleTileStatus(b);
        }
    }
}
