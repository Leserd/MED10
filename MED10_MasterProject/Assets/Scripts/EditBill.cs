using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditBill : MonoBehaviour {
    int IDnum;
    public delegate void D_EditBill(int ID);
    public static event D_EditBill Edit;
    public GameObject EditBillBar;


	void Awake () {
        PlayerControls.SelectObject += EventStart;
        EditBillBar.GetComponent<Button>().onClick.AddListener(() => SendEvent());

	}

    void EventStart(GameObject building)
    {
        if (building != null && building.GetComponent<Building>())
        {
            EditBillBar.SetActive(true);
            IDnum = building.GetComponent<Building>().ID;
            var data = BetalingsServiceData.Instance.GetPaymentservices(IDnum);
            var textFields = GetComponentsInChildren<Text>();
            textFields[0].text = data.TransactionName;
            textFields[1].text = data.Expense.ToString();
            textFields[2].text = "Årlige betalinger: " +data.PaymentsPerYear.ToString();
            textFields[3].text = data.SubCategory;
            return;
        }
        EditBillBar.SetActive(false);
        
    }

    void SendEvent()
    {
        if (Edit != null)
        {
            Edit(IDnum);
            EditBillBar.SetActive(false);
        }
    }
	

}
