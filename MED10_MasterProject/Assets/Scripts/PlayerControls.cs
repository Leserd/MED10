using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    public static PlayerControls instance;
    public E_TouchStatus TouchStatus { get; set; }

    private void Awake()
    {
        instance = this;
        TouchStatus = E_TouchStatus.IDLE;
    }

    public void TouchTile(Tile tile)
    {
        switch (TouchStatus)
        {
            case E_TouchStatus.IDLE:
                tile.showTileStatus = !tile.showTileStatus;
                break;
            case E_TouchStatus.BUILD:
                if(tile.TileStatus == E_TileStatus.EMPTY)
                {
                    //Instantiate(BuildManager.BuildingToBuild, tile.transform.position, Quaternion.identity);
                    BuildManager.instance.BuildOnTile(tile);
                    
                    TouchStatus = E_TouchStatus.IDLE;
                    TileManager.instance.ToggleTileAvailability(false);
                }
                break;
        }
    }

}

public enum E_TouchStatus
{
    IDLE,
    BUILD
}
