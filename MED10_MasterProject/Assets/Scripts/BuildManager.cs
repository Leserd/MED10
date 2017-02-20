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
    // skal udvides med det der ligger i BuyBuilding
    /*
     *     public Sprite BuildingSprite;
    public string Name;



    [SerializeField]
    int IncomeAmount=0,Target=0, AccountCost=0, ElectricityCost=0, ClothingCost=0, EntertainmentCost=0, WaterCost=0,FoodCost=0;
*/

   

    
}
