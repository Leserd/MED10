using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BuyBuilding : MonoBehaviour {

    public Sprite BuildingSprite;
    public string Name;




    [SerializeField]
    int IncomeAmount,Target, AccountCost, ElectricityCost, ClothingCost, EntertainmentCost, WaterCost,FoodCost;



    public void ButtonPress()
    {
        var uiBar = UIDataChangeBuildings.Instance;
        uiBar.Account = AccountCost;
        uiBar.Electricity = ElectricityCost;
        uiBar.Clothing = ClothingCost;
        uiBar.Entertainment = EntertainmentCost;
        uiBar.Water = WaterCost;
        uiBar.Food = FoodCost;
        

        var createBuilding = CreateBuilding.Instance;
        createBuilding.SetupBuilding(Name, IncomeAmount, BuildingSprite, Target);
    }


}
