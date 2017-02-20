using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour {

    private Button btn;
    public GameObject building;

    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() => SetBuildingToBuild());
       
    }

    public void SetBuildingToBuild()
    {
        BuildManager.BuildingToBuild = building;
        PlayerControls.instance.ChangeTouchStatus(E_TouchStatus.BUILD);
        MenuManager.instance.CloseMenues();
    }
}
