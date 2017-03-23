using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddMoneyToBar : MonoBehaviour
{

    public GameObject PercentageBarPrefab;
    private const float MAX_VALUE = 950f;
    private float _totalMoneyAdded = 0;
    private List<GameObject> _addedBills = new List<GameObject>();
    Dictionary<string, Color> BarColors = new Dictionary<string, Color>()
            {
                { "Abonnement", Color.green },
                { "Lån", Color.green},
                { "Forsikring", Color.green },
                { "Fritid", Color.green},
                { "Transport", Color.green},
                { "Personlig Pleje", Color.green },
                { "Andet", Color.green},
                { "Internet", Color.red},
                { "TV", Color.red},
                { "Mobil", Color.red},
                { "Licens", Color.red},
                { "Husleje", Color.blue},
                { "Varme", Color.blue},
                { "El", Color.blue},
                { "Vand", Color.blue},
                { "Gas", Color.blue}
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
    }

    void ResizePercentageBar()
    {
        foreach (var percentageBar in _addedBills)
        {
            var bar = percentageBar.GetComponent<PercentageBar>();
            bar.Size = (bar.BillAmount / _totalMoneyAdded) * MAX_VALUE;
            bar.Resize();
        }
    }
    private void ChangeBill(BetalingsServiceData.BSData changedData)
    {
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
