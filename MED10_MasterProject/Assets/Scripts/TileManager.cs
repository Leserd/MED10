using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {
    
    public static TileManager instance;

    public delegate void BuildingLocations(bool show);
    public static event BuildingLocations ToggleTileStatus;

    private void Awake()
    {
        instance = this;
    }

    public void ToggleTileAvailability(bool b)
    {
        if (ToggleTileStatus != null)
        {
            ToggleTileStatus(b);
        }
    }
}
