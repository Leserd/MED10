using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalenderDate : MonoBehaviour {
    private int _currentInput;
    private InputField _calenderField;
    private string _date = "DD", _month = "MM", _year = "YYYY", _seperator = " / ";
    private string _calenderString;


    private void Awake()
    {
        _calenderField = GetComponent<InputField>();
        _calenderField.onValueChanged.AddListener( delegate { TestOutput(); });
    }

    public void TestOutput()
    {
        int.TryParse(_calenderField.text, out _currentInput);
        Debug.Log(_currentInput);
        //_calenderField.text = _date + _seperator + _month + _seperator + _year;
        Debug.Log("Value have been changed");
        Debug.Log(_calenderField.text);
    }

}
