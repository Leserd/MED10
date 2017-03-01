using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalenderDate : MonoBehaviour {
    public Text OutputField;
    public Image HideBackground;

    private string _currentInput;
    private InputField _calenderField;
    private string _month = "MM", _year = "YYYY", _seperator = " / ";
    private string _calenderString;

    private string[] _calenderAttemt = { "DD", "MM", "YYYY" };


    private void Awake()
    {
        _calenderField = GetComponent<InputField>();
        _calenderField.onValueChanged.AddListener( delegate { TestOutput(); });
      //  _calenderField.onValueChanged.AddListener(delegate { testInputNumbers(); });


    }

    public void TestOutput()
    {
        _currentInput = _calenderField.text;
        AddToCalender();
        OutputField.text = OutputString();
        Background();

    }

    private void AddToCalender()
    {
        if (_currentInput.ToString().Length <=  2)
        {
            _calenderAttemt[0] = _currentInput;
            _calenderAttemt[1] = "MM";
            _calenderAttemt[2] = "YYYY";
        }
        if (_currentInput.ToString().Length <= 4 && _currentInput.ToString().Length >=3)
        {

            var month = _currentInput;
            month.Substring(2);
            _calenderAttemt[1] = month;
            _calenderAttemt[2] = "YYYY";
        }
        if (_currentInput.ToString().Length <= 8 && _currentInput.ToString().Length >= 5)
        {
            var year = _currentInput;
            year.Substring(4);
            _calenderAttemt[2] = year;
        }
        else return;

    }
/*
    public int[] InputNumbers(int values)
    {
        var numbers = new Stack<int>();
        for (; values > 1; values /=6 )
        {
            numbers.Push(values % 10);
        }
        return numbers.ToArray();
    }

    private void testInputNumbers()
    {
        var numbers = InputNumbers(_currentInput);
        foreach (var numbs in numbers)
        {
            Debug.Log(numbs + " number >=> ");
        }
    }*/


    private string OutputString()
    {
        if (_calenderAttemt[1] == "MM")
        {
            return _calenderAttemt[0].ToString() + " / MM / YYYY";
        }
        if (_calenderAttemt[2] == "YYYY")
        {
            return _calenderAttemt[0].ToString() + " / " + _calenderAttemt[1].ToString() + " / YYYY";
        }
        return _calenderAttemt[0].ToString() + _seperator + _calenderAttemt[1] + _seperator + _calenderAttemt[2];
    }


    private void Background()
    {
        if (_currentInput != "")
        {
            Debug.Log("Active");
            HideBackground.gameObject.SetActive(true);
            OutputField.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Not active");
            HideBackground.gameObject.SetActive(false);
            OutputField.gameObject.SetActive(false);


        }


    }
}
