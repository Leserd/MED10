﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;
	public static GameObject buildingToBuild { get; set; }              //Building (prefab) the player is currenty trying to place 
    public static GameObject activeBuildButton;                         //Building (button) the player is currently trying to place
    public static Vector3 buildingOffset = new Vector3(-0.03f, 0.4f, 0);    //Offset for buildings so they are correctly shown on tiles
    public List<Button> availableBuildings = new List<Button>();        //List of buttons available on the Build bar
    public Building[] placedBuildings;
    public GameObject buildingBtnPrefab;                                //Prefab of a building button
    public Transform buildingBtnParent;                                 //The Transform to which all building buttons will be a child
    public GameObject cancelArea;                                       //The area in which the player can drop a building if he does not want to place it anyway
    private bool _firstBuildingButton = true;                           //Should we show the hint when button is created
    public static bool firstBuildingToBePlaced = true;                 //Should we show a hint when the first building is created ("There are more bills")
    public static bool billsDone = false;

    public delegate void D_LastBuildingPlaced();
    public static event D_LastBuildingPlaced LastBuildingPlaced;

    public delegate void D_NewBuildingPlaced(int id);
    public static event D_NewBuildingPlaced NewBuildingPlaced;

    public delegate void D_IgnoreRaycast();
    public static event D_IgnoreRaycast StartIgnoreRaycast;
    public static event D_IgnoreRaycast StopIgnoreRaycast;

    private void Awake()
    {
        instance = this;
        BetalingsServiceData.newBill += CreateBuilding;
        Bill.LastBill += BillsDone;
        BetalingsServiceData.changedBill += ChangeBuilding;

        //for(int i = 0; i < PretendData.instance.LasseTestData.Length; )
        //Debug.Log(PretendData.instance.LasseTestData.Length);
    }
    private void Start()
    {
        placedBuildings = new Building[PretendData.instance.LasseTestData.Length];
    }



    void CreateBuilding(BetalingsServiceData.BSData data)
    {
        AddBuildingButton("Prefabs/Lasse/" + data.SubCategory, data.ID);
    }


    void BillsDone()
    {
        billsDone = true;

    }

    public void BuildingHasBeenPlaced(Building building, int id)
    {
        //Make all buildings opaque and raycastable
        if (StopIgnoreRaycast != null)
        {
            StopIgnoreRaycast();
        }

        if(NewBuildingPlaced != null)
        {
            NewBuildingPlaced(id);
        }

        //Add building to building list
        
        placedBuildings[id] = building;

        if (billsDone && availableBuildings.Count == 0)
        {
            AnnounceLastBuilding();
        }
    }


    public void ChangeBuilding(BetalingsServiceData.BSData bsData)
    {
        Building building = placedBuildings[bsData.ID];
        building.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Buildings/" + bsData.SubCategory);
        building.transform.name = bsData.SubCategory;
        //TODO: Change event script on building
    }


    public void AnnounceLastBuilding()
    {
        if(LastBuildingPlaced != null)
        {
            LastBuildingPlaced();
        }
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TestBuildings();
        }
    }


    public void TestBuildings()
    {
        AddBuildingButton("Prefabs/Lasse/Husleje", 0);
        AddBuildingButton("Prefabs/Lasse/El", 0);
        AddBuildingButton("Prefabs/Lasse/Husleje", 0);
    }


    public void StartPlacingBuilding(GameObject buildingButton, int id)
    {
        //Store the button for the building being built:
        SetActiveBuildButton(buildingButton);

        //Store the gameobject of the building being built:
        SetBuildingToBuild(buildingButton.GetComponent<BuildButton>().building, id);

        PlayerControls.instance.ChangeTouchStatus(E_TouchStatus.BUILD);

        //Make all buildings transparent
        if(StartIgnoreRaycast != null)
        {
            StartIgnoreRaycast();
        }
    }



    public void SetBuildingToBuild(GameObject building, int id)
    {
        Plane plane = new Plane(Vector3.back, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 spawnPos = Vector3.zero;
        float hitdist = 0.0f;

        if (plane.Raycast(ray, out hitdist))
        {
            spawnPos = ray.GetPoint(hitdist);
        }

        buildingToBuild = Instantiate(building, spawnPos, Quaternion.identity);
        buildingToBuild.GetComponent<Building>().ID = id;
    }



    public void CancelBuild()
    {
        if (buildingToBuild)
        {
            Destroy(buildingToBuild.gameObject);
            buildingToBuild = null;
        }
        PlayerControls.instance.ChangeTouchStatus(E_TouchStatus.IDLE);

        //Make all buildings opaque
        if (StopIgnoreRaycast != null)
        {
            StopIgnoreRaycast();
        }

        //Replace button on the build menu
        ReactivateBuildButton();
    }



    //This function is called when the player has begun dragging a build button, temporarily disabling the button until the player cancels or finishes building
    public void SetActiveBuildButton(GameObject btn)
    {
        activeBuildButton = btn;
        activeBuildButton.SetActive(false);
        cancelArea.SetActive(true);
    }


    public void ReactivateBuildButton()
    {
        if (activeBuildButton)
        {
            activeBuildButton.SetActive(true);
            activeBuildButton = null;
        }
        cancelArea.SetActive(false);
    }


    //This function is called when a building has been successfully placed, thereby finally destroying the (currently disabled) button
    public void RemoveActiveBuildButton()
    {
        availableBuildings.Remove(activeBuildButton.GetComponent<Button>());
        Destroy(activeBuildButton);
        activeBuildButton = null;
        cancelArea.SetActive(false);
        buildingToBuild = null;
    }


    public void AddBuildingButton(string buildingPath, int id)
    {
        GameObject building = Resources.Load<GameObject>(buildingPath);
        if(building == null)
        {
            Debug.Log("The path: " + buildingPath + " returns no object");
            return;
        }

        if (_firstBuildingButton)
        {
            new Hint("Sprites/Hints/Hint3AfterFirstBill", new Vector3(0f, -350f)); //(-100,-350f)
            _firstBuildingButton = false;
        }

        //Instantiate the new button
        GameObject newBuildingBtn = Instantiate(buildingBtnPrefab);
        newBuildingBtn.transform.SetParent(buildingBtnParent);
        newBuildingBtn.transform.localScale = Vector3.one;
        newBuildingBtn.GetComponent<Image>().sprite = building.GetComponent<SpriteRenderer>().sprite;

        //Add new button to List
        availableBuildings.Add(newBuildingBtn.GetComponent<Button>());

        //Assign referenced building to build
        newBuildingBtn.GetComponent<BuildButton>().building = building;
        newBuildingBtn.GetComponent<BuildButton>().ID = id;

        //Create BeginDrag trigger event
        EventTrigger trigger = newBuildingBtn.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.BeginDrag;
        entry.callback.AddListener((eventData) => { StartPlacingBuilding(newBuildingBtn, id); });
        trigger.triggers.Add(entry);
    }  
}