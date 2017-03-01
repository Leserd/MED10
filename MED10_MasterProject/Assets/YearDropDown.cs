using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YearDropDown : MonoBehaviour {
    private Dropdown _yearDrop;



    void Awake()
    {
        _yearDrop = gameObject.GetComponent<Dropdown>();
        SetupDropdown();

    }

    private void SetupDropdown()
    {
        var fiftyYears = new List<string>();
        var start = 2017;
        for (int i = 0; i < 50; i++)
        {
            fiftyYears.Add(start.ToString());
            start += 1;
        }
        _yearDrop.AddOptions(fiftyYears);
    }
}
