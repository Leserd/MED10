using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyFoodBuilding : MonoBehaviour
{
    public Sprite FoodSprite;

    /// <summary>
    /// Building cost
    /// </summary>
    private const int _electricityCost = 200;
    private const int _waterCost = 100;

    /// <summary>
    /// Income parameters
    /// </summary>
    int foodIncome = 100;



    public void ButtonPress()
    {
        var uiBar = UIDataChangeBuildings.Instance;
        uiBar.Electricity = -_electricityCost;
        uiBar.Water = -_waterCost;
        var test = CreateBuilding.Instance;
        test.SetupBuilding("FoodBuilding", foodIncome, FoodSprite, 2);
    }



}
