using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;
	public static GameObject BuildingToBuild { get; set; }
    private Vector3 buildingOffset = new Vector3(0, 0.55f, 0);

    private void Awake()
    {
        instance = this;
    }

   

    public void BuildOnTile(Tile tile)
    {
        Instantiate(BuildingToBuild, tile.transform.position + buildingOffset, Quaternion.identity);
        tile.ChangeTileStatus(E_TileStatus.FULL);
    }
}
