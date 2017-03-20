using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BudgetOverview : MonoBehaviour {

    public Button budgetButton;
    public Button closeButton;
    public GameObject budgetOverview;

    private void Awake()
    {
        BuildManager.LastBuildingPlaced += ActivateBudgetButton;
        if(budgetButton)
            budgetButton.onClick.AddListener(() => ShowOverview());
        if (budgetButton)
            closeButton.onClick.AddListener(() => HideOverview());
    }


    public void ShowOverview()
    {
        budgetOverview.SetActive(true);
        closeButton.gameObject.SetActive(true);
    }


    public void HideOverview()
    {
        budgetOverview.SetActive(false);
        closeButton.gameObject.SetActive(false);
    }

    private void ActivateBudgetButton()
    {
        budgetButton.gameObject.SetActive(true);
    }
}
