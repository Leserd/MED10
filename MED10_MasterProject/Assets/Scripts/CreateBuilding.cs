using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateBuilding : MonoBehaviour {

    public GameObject BuildingPrefab;



    private static CreateBuilding instance = null;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);    
        }
        else
        {
            instance = this;
        }
    }

    public static CreateBuilding Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CreateBuilding>();
            }
            return instance;
        }
    }


//TODO is missing a way to diffirentiate between different income targets
    public  void SetupBuilding(string buildingName, int incomeAmount, Sprite buildingImage, int target, Transform parentTransform , float timeToUpdate)
    {
        var buildingClone = Instantiate(BuildingPrefab, gameObject.transform, false);
        buildingClone.transform.SetParent(parentTransform);
        buildingClone.transform.position = parentTransform.position;
        buildingClone.name = buildingName;
        var texts = buildingClone.GetComponentsInChildren<Text>();
        texts[1].text = incomeAmount.ToString(); 
        var images = buildingClone.GetComponentsInChildren<Image>();
        images[1].sprite = buildingImage;
        var collectIncomeScript = buildingClone.GetComponentInChildren<CollectIncome>();
        collectIncomeScript.income = incomeAmount;
        collectIncomeScript.targetSource = target;
        collectIncomeScript.timeToUpdate = timeToUpdate;



    }
}
