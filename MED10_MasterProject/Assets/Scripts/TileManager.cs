using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

    public GameObject tilePrefab2D;
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
    }

    private void Start()
    {
        SetUpTileGrid();
    }

    private void SetUpTileGrid()
    {
        Vector3 startPos = new Vector3(0, 0, 0);
        int renderOrder = (TILE_GRID_X_SIZE * TILE_GRID_Y_SIZE) * (-1);
        
        for (int y = 0; y < TILE_GRID_Y_SIZE; y++)
        {
            for (int x = TILE_GRID_X_SIZE; x > 0; x--)
            {
                Vector3 pos = startPos + new Vector3((x * TILE_GRID_X_DIST / 2) + (y * TILE_GRID_X_DIST / 2), 
                    (y * TILE_GRID_Y_DIST / 2) - (x * TILE_GRID_Y_DIST / 2), 0);
                renderOrder++;
                SpriteRenderer tile = Instantiate(tilePrefab2D, pos, Quaternion.identity).GetComponent<SpriteRenderer>();
                tile.sortingOrder = renderOrder;
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
