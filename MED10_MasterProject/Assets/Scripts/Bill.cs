using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bill : MonoBehaviour {

    public delegate void D_LastBill();
    public static event D_LastBill LastBill;

    public Text BillName;
    public InputField BillAmount;
    public Dropdown Category;
    public Dropdown SubCategory;
    public ToggleGroup Frequency;
    public Button Finished;

    private bool _isActive, _choseCategory,_choseSubCategory, _choseFrequency;
    private BetalingsServiceData BPS;
    private string _toggleNumber;
    public List<Toggle> Toggles;


    private void Awake()
    {
        BPS = BetalingsServiceData.Instance;
        Frequency.allowSwitchOff = true;
        Finished.onClick.AddListener(() => AddToBPS());
        Category.onValueChanged.AddListener(delegate { CategoryChosen(); });
        SubCategory.onValueChanged.AddListener(delegate { SubCategoryChosen(); });
        foreach (var togle in Toggles)
        {
            togle.onValueChanged.AddListener(delegate { GetActiveToggle(togle.isOn, togle); });
        }
    }

    private void SubCategoryChosen()
    {
        if (SubCategory.value != 0)
        {
            _choseSubCategory = true;
            FinishedBill();
            return;
        }
        _choseSubCategory = false;
    }

    
    private void CategoryChosen()
    {
        if (Category.value != 0)
        {
            _choseCategory = true;
            FinishedBill();
            return;
        }
        _choseCategory = false;

    }

    void GetActiveToggle(bool toggleOn, Toggle changedToggle)
    {
        if (toggleOn)
        {
            _choseFrequency = true;
            FinishedBill();
            _toggleNumber = changedToggle.name;
        }
        if (!Frequency.AnyTogglesOn() )
        {
            _choseFrequency = false;
            FinishedBill();
        }

    }



    private void AddToBPS()
    {
        BPS.AddToCurrentExpenses(BillName.text, float.Parse(BillAmount.text), int.Parse(_toggleNumber), Category.captionText.text, SubCategory.captionText.text);
        gameObject.SetActive(false);


        var bills = GameObject.FindGameObjectsWithTag("Bill");
        if (bills.Length > 1)
        {
            foreach (var bill in bills)
            {
                if (bill.name == BillName.text)
                {
                    Destroy(bill);
                    break;
                }
            }
        }
        else
        {
            Destroy(bills[0]);
            LastBill();
            ActivateGameobject.Instance.Interactable(false); 
        }

        transform.parent.gameObject.SetActive(false);

        Destroy(this);
        Debug.Log(BPS.GetPaymentservices(0).Info());

    }

    void FinishedBill()
    {
        if (_choseFrequency && _choseCategory &&_choseSubCategory)
        {
            Finished.interactable = true;
            return;
        }
        Finished.interactable = false;
    }
}
