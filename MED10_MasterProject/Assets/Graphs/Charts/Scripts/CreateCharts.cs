using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CreateCharts : MonoBehaviour {

    public string ChooseChart = "testBarChart";
    public Vector3 ChooseStartLocation;
    public GameObject Bars;
    public GameObject Pies;
    public Dropdown DropdownChooser;
    
    void Start()
    {
        ChooseChart = "testBarChart";
    }    	
	void Update () {
        if (Input.GetKeyDown(KeyCode.M))
        {
            var newChart = new GameObject(ChooseChart);
            newChart.transform.SetParent(transform, false);
            newChart.AddComponent<CreateSubCanvas>().CreateCharts(ChooseChart, ChooseStartLocation,Bars,Pies );
            Debug.Log("Pressed M"); 
        }	
	}
    public void changeChart()
    {
        if (DropdownChooser.value == 0)
        {
            ChooseChart = "testBarChart";
        }
        else if (DropdownChooser.value == 1)
        {
            ChooseChart = "testPieChart";
        }
    }
}
