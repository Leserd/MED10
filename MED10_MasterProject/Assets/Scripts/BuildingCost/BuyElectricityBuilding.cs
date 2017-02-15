using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyElectricityBuilding : MonoBehaviour {

    private const int _waterCost = 100;
    private const int _accountCost = 400;

    public void ButtonPress()
    {
        var uiBar = UIDataChangeBuildings.Instance;
        uiBar.AccountBalance = _accountCost;
        uiBar.Water = _waterCost;
    }


}
