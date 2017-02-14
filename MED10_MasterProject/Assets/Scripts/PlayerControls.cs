using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour {

    public static PlayerControls instance;
    public E_TouchStatus TouchStatus { get; set; }
    public delegate void D_ChangeTouchStatus(E_TouchStatus status);
    public static event D_ChangeTouchStatus TouchStatusChange;
    private Tile selectedTile;

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
                if (selectedTile != null && selectedTile != tile)
                {
                    selectedTile.ToggleShowStatus(false);
                }
                selectedTile = tile;
                selectedTile.ToggleShowStatus(true);
                break;
            case E_TouchStatus.BUILD:
                if(tile.TileStatus == E_TileStatus.EMPTY)
                {
                    //Instantiate(BuildManager.BuildingToBuild, tile.transform.position, Quaternion.identity);
                    BuildManager.instance.BuildOnTile(tile);

                    ChangeTouchStatus(E_TouchStatus.IDLE);
                    
                }
                break;
        }
    }

    public void ChangeTouchStatus(E_TouchStatus status)
    {
        TouchStatus = status;
        if(TouchStatusChange != null)
            TouchStatusChange(status);

        switch (status)
        {
            case E_TouchStatus.IDLE:
                TileManager.instance.ToggleTileAvailability(false);
                break;
            case E_TouchStatus.BUILD:
                TileManager.instance.ToggleTileAvailability(true);
                break;
        }
    }

}

public enum E_TouchStatus
{
    IDLE,
    BUILD
}
