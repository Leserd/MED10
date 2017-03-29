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
                { "Abonnement", new Color(63f,115f,178f)},
                { "Lån", new Color(63f,115f,178f)},
                { "Forsikring", new Color(63f,115f,178f) },
                { "Fritid", new Color(63f,115f,178f)},
                { "Transport", new Color(63f,115f,178f)},
                { "Personlig Pleje", new Color(63f,115f,178f) },
                { "Andet", new Color(63f,115f,178f)},
                { "Internet", new Color(207f,52f,52f)},
                { "TV", new Color(207f,52f,52f)},
                { "Mobil", new Color(207f,52f,52f)},
                { "Licens", new Color(207f,52f,52f)},
                { "Husleje", new Color(117f,180f,61f)},
                { "Varme", new Color(117f,180f,61f)},
                { "El", new Color(117f,180f,61f)},
                { "Vand", new Color(117f,180f,61f)},
                { "Gas", new Color(117f,180f,61f)}
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
