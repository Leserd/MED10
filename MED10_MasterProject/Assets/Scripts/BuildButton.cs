using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour {

    private Button btn;
    public GameObject building;
    public int ID;

    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() => BuildManager.instance.SetBuildingToBuild(building, ID));
       
    }

}
