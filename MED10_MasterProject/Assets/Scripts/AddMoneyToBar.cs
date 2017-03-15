using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddMoneyToBar : MonoBehaviour {

    public GameObject PercentageBarPrefab;
    private const float MAX_VALUE = 950f;
    private float _totalMoneyAdded = 0;
    private List<GameObject> _addedBills = new List<GameObject>();

    //Det er muligt at man kan nøjes med expenseMonth
    List<BetalingsServiceData.BSData> _currentAddedBill = new List<BetalingsServiceData.BSData>();


    private void Awake()
    {

        BetalingsServiceData.newBill += AddBill;
        BetalingsServiceData.changedBill += ChangeBill;
    }

    private void AddBill(BetalingsServiceData.BSData addedData)
    {
        _currentAddedBill.Add(addedData);
        _totalMoneyAdded += addedData.MonthlyExpence;
        var bar = Instantiate(PercentageBarPrefab, transform, false);
        bar.GetComponent<Image>().color = Random.ColorHSV();
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
            bar.Size = (bar.BillAmount /_totalMoneyAdded) * MAX_VALUE;
            bar.Resize();
        }
    }
    private void ChangeBill(BetalingsServiceData.BSData changedData)
    {
        _currentAddedBill[changedData.ID] = changedData;
        ResizePercentageBar();
    }

    





}
