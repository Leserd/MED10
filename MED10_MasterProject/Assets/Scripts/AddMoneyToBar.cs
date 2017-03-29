using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddMoneyToBar : MonoBehaviour
{

    public GameObject PercentageBarPrefab;
    [SerializeField]
    private Text avgMonthly, ApartmentPercentage = null, MediaPercentage = null, OtherPercentage = null;
    private const float MAX_VALUE = 950f;
    private float _totalMoneyAdded = 0;
    private List<GameObject> _addedBills = new List<GameObject>();
    private float _totalApartment = 0, _totalMedia = 0, _totalOther = 0;

    public static Dictionary<string, Color> BarColors = new Dictionary<string, Color>()
            {
                { "Abonnement", new Color(63f/255,115f/255,178f/255)},
                { "Lån",  new Color(63f/255,115f/255,178f/255)},
                { "Forsikring", new Color(63f/255,115f/255,178f/255)},
                { "Fritid",  new Color(63f/255,115f/255,178f/255)},
                { "Transport",  new Color(63f/255,115f/255,178f/255)},
                { "Personlig Pleje", new Color(63f/255,115f/255,178f/255)},
                { "Andet",  new Color(63f/255,115f/255,178f/255)},
                { "Internet", new Color(207f/255,52f/255,52f/255)},
                { "TV", new Color(207f/255,52f/255,52f/255)},
                { "Mobil", new Color(207f/255,52f/255,52f/255)},
                { "Licens", new Color(207f/255,52f/255,52f/255)},
                { "Husleje", new Color(117f/255,180f/255,61f/255)},
                { "Varme", new Color(117f/255,180f/255,61f/255)},
                { "El", new Color(117f/255,180f/255,61f/255)},
                { "Vand",new Color(117f/255,180f/255,61f/255)},
                { "Gas", new Color(117f/255,180f/255,61f/255)}
            };

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
        ApartmentPercentage.text = (_totalApartment / _totalMoneyAdded * 100).ToString("F0") + "%|";
        MediaPercentage.text = (_totalMedia / _totalMoneyAdded * 100).ToString("F0") + "%|";
        OtherPercentage.text = (_totalOther / _totalMoneyAdded * 100).ToString("F0") + "%|";

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
        avgMonthly.text = _totalMoneyAdded.ToString("F0") + " kr";

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
