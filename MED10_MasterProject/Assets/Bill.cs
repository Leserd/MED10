using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bill : MonoBehaviour {
    public Text BillName;
    public InputField BillAmount;
    public Dropdown Category;
    public ToggleGroup Frequency;

    private BetalingsServiceData BPS;




	// Use this for initialization
	void Start () {
        BPS = BetalingsServiceData.Instance;
        Frequency.allowSwitchOff = true;
       

		
	}

    private void Update()
    {
        foreach (var toogle in Frequency.ActiveToggles())
        {
            Debug.Log(toogle.name);
        }
        Debug.Log(Frequency.ActiveToggles().ToString());
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Test();
        }
    }

    private void Test()
    {
        Debug.Log("Pressed Space");
        BPS.AddToCurrentExpenses(BillName.text, int.Parse(BillAmount.text), 12, Category.captionText.text);
        Debug.Log(BPS.GetPaymentservices(0).info());
    }
}
