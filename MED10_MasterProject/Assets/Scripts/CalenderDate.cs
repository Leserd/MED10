using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalenderDate : MonoBehaviour {
    public Text OutputField;
    public Image HideBackground;
    public static CalenderDate Instance = null;

    private string _currentInput;
    private InputField _calenderField;
    private string _month = "MM", _year = "YYYY", _seperator = " / ";
    private string _calenderString;

    public string[] CalenderAttempt = { "DD", "MM", "YYYY" };

    public void OutsideUpdate(string[] arrayString )
    {
        if (_currentInput == "")
        {
            foreach (var date in arrayString)
            {
                _currentInput += date;
            }
        }
        CalenderAttempt = arrayString;
        OutputField.text = OutputString();
        Background();

    }


    private void Start()
    {
        Instance = this;
        _calenderField = GetComponent<InputField>();
        _calenderField.onValueChanged.AddListener( delegate { TestOutput(); });


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
            CalenderAttempt[0] = _currentInput;
            CalenderAttempt[1] = "MM";
            CalenderAttempt[2] = "YYYY";
        }
        if (_currentInput.ToString().Length <= 4 && _currentInput.ToString().Length >=3)
        {

            var month = _currentInput;
            month = month.Substring(2);
            CalenderAttempt[1] = month;
            CalenderAttempt[2] = "YYYY";
        }
        if (_currentInput.ToString().Length <= 8 && _currentInput.ToString().Length >= 5)
        {
            var year = _currentInput;
            year = year.Substring(4);
            CalenderAttempt[2] = year;
        }
        else return;

    }



    private string OutputString()
    {
        if (CalenderAttempt[1] == "MM")
        {
            return CalenderAttempt[0].ToString() + " / MM / YYYY";
        }
        if (CalenderAttempt[2] == "YYYY")
        {
            return CalenderAttempt[0].ToString() + " / " + CalenderAttempt[1].ToString() + " / YYYY";
        }
        return CalenderAttempt[0].ToString() + _seperator + CalenderAttempt[1] + _seperator + CalenderAttempt[2];
    }


    private void Background()
    {
        if (_currentInput != "")
        {
            Debug.Log("Active");
            HideBackground.gameObject.SetActive(true);
            OutputField.gameObject.SetActive(true);
            var accessor = SavingsAccessor.Instance;
            Debug.Log(_currentInput);
             int.TryParse(_currentInput,out accessor.SavingsDate);

        }
        else
        {
            Debug.Log("Not active");
            HideBackground.gameObject.SetActive(false);
            OutputField.gameObject.SetActive(false);



        }


    }
}
