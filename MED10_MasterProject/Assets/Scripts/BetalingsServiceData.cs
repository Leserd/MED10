using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetalingsServiceData : MonoBehaviour {

    public static BetalingsServiceData Instance = null;
    private void Awake()
    {
        Instance = this;
    }

    private static int IDnum =0;
    private List<BSData> _currentHouses = new List<BSData>();


    //method to assign data and add to list of data
    public void  AddToCurrentExpenses(string transactionName, float expense, int paymentsPerYear, string category, string startingMonth = "January")
    {        
        _currentHouses.Add(new BSData(transactionName, IDnum ,expense, paymentsPerYear, category, startingMonth));
        IDnum += 1;

    }

    // method to get data based on id
    public BSData GetPaymentservices (int ID)
    {
        return _currentHouses[ID];
    }


    //method to correct data;

    public void CorrectExpenses(int ID, string transactionName, float expense, int paymentsPerYear, string category ,string startingMonth = "January")
    {
        _currentHouses[ID] = new BSData(transactionName, _currentHouses[ID].ID, expense, paymentsPerYear, category, startingMonth);
    }


    public struct BSData
    {
        string TransactionName;
        public int ID;
        float Expense;
        int PaymentsPerYear;
        float MonthlyExpence;// { get {return monthlyExpence } set { monthlyExpence = Expense / PaymentsPerYear; } }
        string StartingMonth;
        string Category;


        public BSData (string transactionName, int id, float expense, int paymentsPerYear, string category, string startingMonth)
        {
            TransactionName = transactionName;
            Expense = expense;
            ID = id;
            PaymentsPerYear = paymentsPerYear;
            StartingMonth = startingMonth;
            Category = category;
            MonthlyExpence = expense / PaymentsPerYear;


        }
        public string info()
        {
            var info = "Transaction ID = " + TransactionName
                + "\nID = " + ID
                + "\nExpense = " + Expense
                + "\nPayments per year =  " + PaymentsPerYear
                + "\nMonthly Expenses = " + MonthlyExpence
                + "\nStarting Month =  " + StartingMonth
                + "\nCategory = " + Category;
            return info;
        }
    }
}
