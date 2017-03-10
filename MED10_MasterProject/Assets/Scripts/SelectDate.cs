using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SelectDate : MonoBehaviour {

    private Button _dateSelected;
    private string[] _months = new string[12] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
    [SerializeField]
    private GameObject Parent;
    [SerializeField]
    private InputField _date, _year,_month;

    private void Awake()
    {
        _dateSelected = gameObject.GetComponent<Button>();
        _dateSelected.onClick.AddListener(() => DateSelected());
    }

    private void DateSelected()
    {
        Debug.Log("shutdown");
        var pos = Array.IndexOf(_months, _month.text);
        pos++;
        var monthNumbers = pos.ToString();
        if (pos < 10)
        {
            monthNumbers = "0" + pos.ToString();
        }


        _date.text = "01" + monthNumbers + _year.text ;
        //var dataText = "01" + monthNumbers + _year.text;
        var accessCalender = CalenderDate.Instance;
        accessCalender.OutsideUpdate( new string[]{ "01", monthNumbers, _year.text});

        Parent.SetActive(false);


    }
}
