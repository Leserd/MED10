using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ChangeMonth : MonoBehaviour {

    [SerializeField]
    private GameObject DecreaseYear, IncreaseYear;

    private string[] _months = new string[12] { "January", "February", "March","April","May","June","July","August","September", "October", "November","December" };

    void Awake()
    {
        GetComponent<InputField>().text = _months[2];
        DecreaseYear.AddComponent<Button>().onClick.AddListener(() => DecreaseVisualMonth());
        IncreaseYear.AddComponent<Button>().onClick.AddListener(() => IncreaseVisualMonth());
    }

    void DecreaseVisualMonth()
    {
        var text = gameObject.GetComponent<InputField>();
        var currentMonth = text.text;
        var pos = Array.IndexOf(_months, currentMonth);
        pos--;
        if (pos < 0)
        {
            pos += 12;
        }
        text.text = _months[pos];


    }
    void IncreaseVisualMonth()
    {
        var text = gameObject.GetComponent<InputField>();
        var currentMonth = text.text;
        var pos = Array.IndexOf(_months, currentMonth);
        pos++;
        if (pos >= 12)
        {
            pos -= 12;
        }
        text.text = _months[pos];
    }

    public enum Month
    {
        NotSet = 0,
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    };
}
