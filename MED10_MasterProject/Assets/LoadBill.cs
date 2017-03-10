using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadBill : MonoBehaviour {
    public PretendData Data;
    public GameObject BillTextObject;
    public GameObject ParentTransform;
    public GameObject Bill;

    List<GameObject> _bills = new List<GameObject>();


    // Use this for initialization
    void Start () {
        foreach (var Name in Data.LasseTestData)
        {
            var bill = Instantiate(BillTextObject, ParentTransform.transform);
            bill.name = Name.BSDataName;
            bill.GetComponent<Text>().text = Name.BSDataName;
            bill.SetActive(true);
            bill.AddComponent<Button>().onClick.AddListener(() => LookAtBill(Name));
            
            _bills.Add(bill);
        }
    }


    void LookAtBill(DataInputTest testBill)
    {
        Debug.Log("Clicked on bill for " + testBill.BSDataName);
        var billStart = Instantiate(Bill, Vector3.zero, Bill.transform.rotation);
        billStart.transform.SetParent(transform, false);

        var bill = billStart.GetComponent<Bill>();
        bill.BillName.text = testBill.BSDataName;
        bill.BillAmount.text = testBill.BSDataAmount;

        //Look at the bill!!
    }
	

}
