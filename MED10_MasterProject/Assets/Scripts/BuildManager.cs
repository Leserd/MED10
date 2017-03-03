using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;
	public static GameObject BuildingToBuild { get; set; }
    public static Vector3 buildingOffset = new Vector3(0, 0.55f, 0);
    public List<Button> availableBuildings = new List<Button>();
    public GameObject buildingBtnPrefab;
    public Transform buildingBtnParent;             //The Transform to which all building buttons will be a child
    public Canvas buildMenu;                        

    private void Awake()
    {
        instance = this;
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddBuildingButton("Prefabs/House2D");
        }
    }



    public void SetBuildingToBuild(GameObject building)
    {
        Plane plane = new Plane(Vector3.back, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 spawnPos = Vector3.zero;
        float hitdist = 0.0f;

        if (plane.Raycast(ray, out hitdist))
        {
            spawnPos = ray.GetPoint(hitdist);
        }
        
        BuildingToBuild = Instantiate(building, spawnPos, Quaternion.identity);
        PlayerControls.instance.ChangeTouchStatus(E_TouchStatus.BUILD);
    }



    public void CancelBuild()
    {
        Destroy(BuildingToBuild.gameObject);
        PlayerControls.instance.ChangeTouchStatus(E_TouchStatus.IDLE);
        //TODO: replace button on the build menu
    }



    public void AddBuildingButton(string buildingPath)
    {
        GameObject building = Resources.Load<GameObject>(buildingPath);
        if(building == null)
        {
            Debug.Log("The path: " + buildingPath + " returns no object");
            return;
        }

        //Instantiate the new button
        GameObject newBuildingBtn = Instantiate(buildingBtnPrefab);
        newBuildingBtn.transform.SetParent(buildingBtnParent);
        newBuildingBtn.transform.localScale = Vector3.one;

        //Add new button to List
        availableBuildings.Add(newBuildingBtn.GetComponent<Button>());

        //Assign referenced building to build
        newBuildingBtn.GetComponent<BuildButton>().building = building;

        //Create BeginDrag trigger event
        EventTrigger trigger = newBuildingBtn.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.BeginDrag;
        entry.callback.AddListener((eventData) => { SetBuildingToBuild(building); });
        trigger.triggers.Add(entry);

        MenuManager.instance.ToggleBuildMenu();
    }


    
}

public enum E_BuildingType
{
    HOUSE,
    FACTORY,
    MEDIA

}
