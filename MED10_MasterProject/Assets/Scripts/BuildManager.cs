using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;
	public static GameObject BuildingToBuild { get; set; }
    public static Vector3 buildingOffset = new Vector3(0, 0.55f, 0);

    private void Awake()
    {
        instance = this;
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
        MenuManager.instance.CloseMenues();
    }

    public void CancelBuild()
    {
        Destroy(BuildingToBuild.gameObject);
        PlayerControls.instance.ChangeTouchStatus(E_TouchStatus.IDLE);
    }

    public void Test()
    {
        print("up");
    }
}
