using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadBill : MonoBehaviour {
    public PretendData Data;
    public GameObject BillTextObject;
    public GameObject ParentTransform;
    public GameObject Bill;


    // Use this for initialization
    void Awake () {
        foreach (var Name in Data.LasseTestData)
        {
            var bill = Instantiate(BillTextObject, ParentTransform.transform);
            bill.transform.localScale = bill.transform.localScale*1.3f;
            bill.tag = "Bill";
            bill.name = Name.BSDataName;
            bill.GetComponentInChildren<Text>().text = Name.BSDataName;
            bill.SetActive(true);
            bill.AddComponent<Button>().onClick.AddListener(() => LookAtBill(Name));
        }
        EditBill.Edit += EditBillMethod;
    }


    void LookAtBill(DataInputTest testBill)
    {
        var billStart = Instantiate(Bill, Vector3.zero, Bill.transform.rotation);
        billStart.name = testBill.BSDataName;
        billStart.transform.SetParent(transform, false);

        var bill = billStart.GetComponent<Bill>();
        bill.BillName.text = testBill.BSDataName;
        bill.BillAmount.text = testBill.BSDataAmount;


        //Look at the bill!!
    }
    void EditBillMethod(int ID)
    {
        var BSinstance = BetalingsServiceData.Instance;
        var billInfo = BSinstance.GetPaymentservices(ID);

        var billStart = Instantiate(Bill, Vector3.zero, Bill.transform.rotation);
        billStart.name = billInfo.TransactionName;
        billStart.transform.SetParent(transform, false);

        var bill = billStart.GetComponent<Bill>();
        bill.BillName.text = billInfo.TransactionName;
        bill.BillAmount.text = billInfo.Expense.ToString();
        bill.Toggles[SetActiveToggle(billInfo.PaymentsPerYear)].isOn = true;
        bill.CategoryText.text = billInfo.SubCategory;
        bill.EditBill(billInfo.Category, billInfo.SubCategory);
        bill.IDnum = ID;



    }

    int SetActiveToggle(int num)
    {
        switch (num)
        {
            case 12:
                return 0;
            case 4:
                return 1;
            case 2:
                return 2;
            case 1:
                return 3;
            default:
                return num ;
        }
    }
	

}
