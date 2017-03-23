using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bill : MonoBehaviour
{

    public delegate void D_Bill();
    public static event D_Bill LastBill;
    public static event D_Bill BillFinished;


    public Text BillName;
    public InputField BillAmount;
    // public Dropdown Category;
    // public Dropdown SubCategory;
    public ToggleGroup Frequency;
    public Button Finished;

    private bool _isActive, _choseCategory, _choseSubCategory, _choseFrequency;
    private BetalingsServiceData BPS;
    private string _toggleNumber, _categoryName, _subCategoryName;
    public List<Toggle> Toggles;
    public Text CategoryText;
    public int IDnum = -1;


    private void Awake()
    {
        CategoryButton.CategoryPress += CategoryChosen;
        BPS = BetalingsServiceData.Instance;
        Frequency.allowSwitchOff = true;
        Finished.onClick.AddListener(() => AddToBPS());
        //Category.onValueChanged.AddListener(delegate { CategoryChosen(); });
        //SubCategory.onValueChanged.AddListener(delegate { SubCategoryChosen(); });
        foreach (var togle in Toggles)
        {
            togle.onValueChanged.AddListener(delegate { GetActiveToggle(togle.isOn, togle); });
        }
    }

    public void EditBill(string category, string subCategory)
    {
        CategoryChosen(category, subCategory);

    }



    private void CategoryChosen(string category, string subCatogry)
    {
        _categoryName = category;
        _subCategoryName = subCatogry;
        _choseCategory = true;
        FinishedBill();
    }

    void GetActiveToggle(bool toggleOn, Toggle changedToggle)
    {
        if (toggleOn)
        {
            _choseFrequency = true;
            FinishedBill();
            _toggleNumber = changedToggle.name;
        }
        if (!Frequency.AnyTogglesOn())
        {
            _choseFrequency = false;
            FinishedBill();
        }

    }



    private void AddToBPS()
    {
        if (IDnum >= 0)
        {
            BPS.CorrectExpenses(IDnum, BillName.text, float.Parse(BillAmount.text), int.Parse(_toggleNumber), _categoryName, _subCategoryName);
        }
        else
        {
            BPS.AddToCurrentExpenses(BillName.text, float.Parse(BillAmount.text), int.Parse(_toggleNumber), _categoryName, _subCategoryName);
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
                if (LastBill != null)
                {
                    LastBill();

                }
                ActivateGameobject.Instance.BillsFinished(false);
            }
            if (BillFinished != null)
            {
                BillFinished();
            }
        }
       // gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void FinishedBill()
    {
        if (_choseFrequency && _choseCategory)
        {
            Finished.interactable = true;
            return;
        }
        Finished.interactable = false;
    }
}
