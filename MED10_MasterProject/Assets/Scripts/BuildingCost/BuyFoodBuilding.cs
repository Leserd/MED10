using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyFoodBuilding : MonoBehaviour
{

    private const int _electricityCost = 200;
    private const int _waterCost = 100;

    public void ButtonPress()
    {
        var uiBar = UIDataChangeBuildings.Instance;
        uiBar.Electricity = _electricityCost;
        uiBar.Water = _waterCost;
    }


}
