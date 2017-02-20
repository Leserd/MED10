using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BuyElectricityBuilding : MonoBehaviour {

    public Sprite ElectricitySprite;


    private const int _waterCost = 100;
    private const int _accountCost = 400;

    int electricityIncome = 300;

    public void ButtonPress()
    {
        var uiBar = UIDataChangeBuildings.Instance;
        uiBar.Account = -_accountCost;
        uiBar.Water = -_waterCost;
        var test = CreateBuilding.Instance;
        test.SetupBuilding("ElectricityBuilding", electricityIncome, ElectricitySprite, 2);
    }


}
