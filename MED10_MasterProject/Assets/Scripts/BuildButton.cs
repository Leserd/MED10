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

    private void SetBuildingToBuild()
    {
        BuildManager.BuildingToBuild = building;
        TileManager.instance.ToggleTileAvailability(true);
        PlayerControls.instance.TouchStatus = E_TouchStatus.BUILD;
        MenuManager.instance.CloseMenues();
    }
}
