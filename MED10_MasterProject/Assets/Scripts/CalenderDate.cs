﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalenderDate : MonoBehaviour {
    public Text OutputField;
    public Image HideBackground;
    private int _currentInput;
    private InputField _calenderField;
    private string _month = "MM", _year = "YYYY", _seperator = " / ";
    private string _calenderString;

    private int[] _calenderAttemt = { 0, 0, 0 };


    private void Awake()
    {
        _calenderField = GetComponent<InputField>();
        _calenderField.onValueChanged.AddListener( delegate { TestOutput(); });
        _calenderField.onValueChanged.AddListener(delegate { testInputNumbers(); });


    }

    public void TestOutput()
    {
        var time = new DateTime();
        time.AddDays(3);
        time.AddMonths(12);
        time.AddYears(2018);
        Debug.Log(time.ToString());
        int.TryParse(_calenderField.text, out _currentInput);
        AddToCalender();
        OutputField.text = OutputString();

        //_calenderField.text = _date + _seperator + _month + _seperator + _year;
        //OutputField.text = _currentInput.ToString();
        Background();

    }
    private void AddToCalender()
    {
        if (_currentInput.ToString().Length <=  2)
        {
            _calenderAttemt[0] = _currentInput;
            _calenderAttemt[1] = 0;
            _calenderAttemt[2] = 0;
        }
        if (_currentInput.ToString().Length <= 4 && _currentInput.ToString().Length >=3)
        {
            if (_currentInput.ToString().Length == 3)
            {
                var oneDigit = _currentInput % 10;
                _calenderAttemt[1] = oneDigit;
            }
            else
            {
                var twoDigits = _currentInput % 100;
                _calenderAttemt[1] = twoDigits;
            }
            _calenderAttemt[2] = 0;
        }
        if (_currentInput.ToString().Length <= 6 && _currentInput.ToString().Length >= 5)
        {
            var twoDigits = _currentInput % 10000;

            _calenderAttemt[2] = twoDigits;

        }

    }

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
    }


    private string OutputString()
    {
        if (_calenderAttemt[1] == 0)
        {
            return _calenderAttemt[0].ToString() + " / MM / YYYY";
        }
        if (_calenderAttemt[2] == 0)
        {
            return _calenderAttemt[0].ToString() + " / " + _calenderAttemt[1].ToString() + " / YY";
        }
        return _calenderAttemt[0].ToString() + _seperator + _calenderAttemt[1] + _seperator + _calenderAttemt[2];
    }


    private void Background()
    {
        if (_currentInput != 0)
        {
            HideBackground.gameObject.SetActive(true);
            OutputField.gameObject.SetActive(true);
        }
        else
        {
            HideBackground.gameObject.SetActive(false);
            OutputField.gameObject.SetActive(false);


        }


    }
}
