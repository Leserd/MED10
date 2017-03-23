using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditBill : MonoBehaviour {
    string IDnum;

	void Awake () {
		
	}

    void EventStart(GameObject building)
    {
        if (building != null && building.GetComponent<Building>())
        {
            IDnum = building.GetComponent<Building>()
        }
    }
	

}
