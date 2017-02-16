using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;
	public static GameObject BuildingToBuild { get; set; }
    public static Vector3 buildingOffset = new Vector3(0, 0.55f, 0);

    private void Awake()
    {
        instance = this;
    }

   

    
}
