using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetalingsServiceData : MonoBehaviour {

    public static BetalingsServiceData Instance = null;
    public delegate void Bill(BSData data);
    public static event Bill newBill;
    public static event Bill changedBill;
    private void Awake()
    {
        Instance = this;
    }

    private static int IDnum =0;
    private List<BSData> _currentHouses = new List<BSData>();


    //method to assign data and add to list of data
    public void  AddToCurrentExpenses(string transactionName, float expense, int paymentsPerYear, string category, string subCategory = "None chosen", string startingMonth = "January")
    {        
        _currentHouses.Add(new BSData(transactionName, IDnum ,expense, paymentsPerYear, category, subCategory, startingMonth));
        if (newBill != null)
        {
            newBill(_currentHouses[IDnum]);
        }
        IDnum += 1;


    }
    public List<BSData> GetAllPaymentServices()
    {
        return _currentHouses;
    }

    // method to get data based on id
    public BSData GetPaymentservices (int ID)
    {
        return _currentHouses[ID];
    }


    //method to correct data;

    public void CorrectExpenses(int ID, string transactionName, float expense, int paymentsPerYear, string category , string subCategory = "None Chosen", string startingMonth = "January")
    {
        _currentHouses[ID] = new BSData(transactionName, _currentHouses[ID].ID, expense, paymentsPerYear, category,subCategory , startingMonth);
        if (changedBill != null)
        {
            changedBill(_currentHouses[ID]);
        }
    }


    public struct BSData
    {
        string TransactionName;
        public int ID;
        float Expense;
        int PaymentsPerYear;
        public float MonthlyExpence;// { get {return monthlyExpence } set { monthlyExpence = Expense / PaymentsPerYear; } }
        string StartingMonth;
        string Category;
        string SubCategory;


        public BSData (string transactionName, int id, float expense, int paymentsPerYear, string category, string subCategory, string startingMonth)
        {
            TransactionName = transactionName;
            Expense = expense;
            ID = id;
            PaymentsPerYear = paymentsPerYear;
            StartingMonth = startingMonth;
            Category = category;
            SubCategory = subCategory;
            MonthlyExpence = (expense * PaymentsPerYear)/12f;


        }
        public string Info()
        {
            var info = "Transaction ID = " + TransactionName
                + "\nID = " + ID
                + "\nExpense = " + Expense
                + "\nPayments per year =  " + PaymentsPerYear
                + "\nMonthly Expenses = " + MonthlyExpence
                + "\nStarting Month =  " + StartingMonth
                + "\nCategory = " + Category
                + "\nSub Category = " + SubCategory;
            return info;
        }
    }
}
