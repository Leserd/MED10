using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;
	public static GameObject BuildingToBuild { get; set; }
   

    private void Awake()
    {
        instance = this;
    }

   

    public void BuildOnTile(Tile tile)
    {
        Instantiate(BuildingToBuild, tile.transform.position, Quaternion.identity);
        tile.ChangeTileStatus(E_TileStatus.FULL);
    }
}
