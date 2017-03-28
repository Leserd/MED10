using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddMoneyToBar : MonoBehaviour
{

    public GameObject PercentageBarPrefab;
    [SerializeField]
    private Text avgMonthly, ApartmentPercentage, MediaPercentage, OtherPercentage;
    private const float MAX_VALUE = 950f;
    private float _totalMoneyAdded = 0;
    private List<GameObject> _addedBills = new List<GameObject>();
    private float _totalApartment = 0, _totalMedia = 0, _totalOther = 0;


    public static Dictionary<string, Color> BarColors = new Dictionary<string, Color>()
            {
                { "Abonnement", Color.blue},
                { "Lån", Color.blue},
                { "Forsikring", Color.blue },
                { "Fritid", Color.blue},
                { "Transport", Color.blue},
                { "Personlig Pleje", Color.blue },
                { "Andet", Color.blue},
                { "Internet", Color.red},
                { "TV", Color.red},
                { "Mobil", Color.red},
                { "Licens", Color.red},
                { "Husleje", Color.green},
                { "Varme", Color.green},
                { "El", Color.green},
                { "Vand", Color.green},
                { "Gas", Color.green}
            };



    //BarColors.add("test",Color.Black);

    //Det er muligt at man kan nøjes med expenseMonth
    List<BetalingsServiceData.BSData> _currentAddedBill = new List<BetalingsServiceData.BSData>();


    private void Awake()
    {
        BuildManager.NewBuildingPlaced += AddBill;
        BetalingsServiceData.changedBill += ChangeBill;
    }

    private void AddBill(int IDnum)
    {
        var addedData = BetalingsServiceData.Instance.GetPaymentservices(IDnum);
        _currentAddedBill.Add(addedData);
        _totalMoneyAdded += addedData.MonthlyExpence;
        var bar = Instantiate(PercentageBarPrefab, transform, false);
        bar.GetComponent<Image>().color = BarColors[addedData.SubCategory];//Random.ColorHSV();
        var percentage = bar.GetComponent<PercentageBar>();
        percentage.BillAmount = addedData.MonthlyExpence;
        _addedBills.Add(bar);
        ResizePercentageBar();
        ChangePercentage(addedData.Category, addedData.MonthlyExpence);

    }

    void ChangePercentage(string category, float changeValue)
    {
        switch (category)
        {
            case "Bolig":
                _totalApartment += changeValue;
               
                break;
            case "Medier":
                _totalMedia += changeValue;
                
                break;
            case "Andet":
                _totalOther += changeValue;
                
                break;

            default:
                break;
        }
        ApartmentPercentage.text = (_totalApartment / _totalMoneyAdded * 100).ToString() + "%|";
        MediaPercentage.text = (_totalMedia / _totalMoneyAdded * 100).ToString() + "%|";
        OtherPercentage.text = (_totalOther / _totalMoneyAdded * 100).ToString() + "%|";

    }

    void ChangePercentage(string changedCategory, float changedValue, string newCategory,float newValue)
    {
        ChangePercentage(changedCategory, -changedValue);
        ChangePercentage(newCategory, newValue);
    }

    void ResizePercentageBar()
    {
        foreach (var percentageBar in _addedBills)
        {
            var bar = percentageBar.GetComponent<PercentageBar>();
            bar.Size = (bar.BillAmount / _totalMoneyAdded) * MAX_VALUE;
            bar.Resize();
        }
        avgMonthly.text = _totalMoneyAdded.ToString();

    }
    private void ChangeBill(BetalingsServiceData.BSData changedData)
    {
        ChangePercentage(_currentAddedBill[changedData.ID].Category, _currentAddedBill[changedData.ID].MonthlyExpence, changedData.Category, changedData.MonthlyExpence);            
        _totalMoneyAdded += changedData.MonthlyExpence - _currentAddedBill[changedData.ID].MonthlyExpence;
        _currentAddedBill[changedData.ID] = changedData;
        var bar = _addedBills[changedData.ID];
        bar.GetComponent<Image>().color = BarColors[changedData.SubCategory];
        var percentage = bar.GetComponent<PercentageBar>();
        percentage.BillAmount = changedData.MonthlyExpence;
        _addedBills[changedData.ID] = bar;

        ResizePercentageBar();
    }







}
