using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine;

public class BuyBuilding : MonoBehaviour {

    public Sprite BuildingSprite;
    public string Name;
    



    [SerializeField]
    int IncomeAmount=0,Target=0, AccountCost=0, ElectricityCost=0, ClothingCost=0, EntertainmentCost=0, WaterCost=0,FoodCost=0;
    [SerializeField]
    float TimeToUpdateInSeconds = 1f;





    public void ButtonPress(Transform parentTransform)
    {

            var uiBar = UIDataChangeBuildings.Instance;
            uiBar.Account = -AccountCost;
            uiBar.Electricity = -ElectricityCost;
            uiBar.Clothing = -ClothingCost;
            uiBar.Entertainment = -EntertainmentCost;
            uiBar.Water = -WaterCost;
            uiBar.Food = -FoodCost;


            var createBuilding = CreateBuilding.Instance;
            createBuilding.SetupBuilding(Name, IncomeAmount, BuildingSprite, Target, parentTransform, TimeToUpdateInSeconds);
        
    }


}
