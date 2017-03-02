using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public Button buildBtn;
    public Button achievementBtn;
    public Button[] cancelBuildBtn;
    public Canvas menuMain, menuBuild, menuStatistics, menuAchievements;
    public GameObject BuildMenu;
    public static MenuManager instance;
    public E_MenuType ActiveMenu { get; set; }

    private void Awake()
    {
        instance = this;
        buildBtn.onClick.AddListener(() => ChangeMenu(E_MenuType.BUILD));
        achievementBtn.onClick.AddListener(() => ChangeMenu(E_MenuType.ACHIEVEMENTS));

        PlayerControls.TouchStatusChange += ToggleCancelButton;
    }


	public void ChangeMenu(E_MenuType newMenu)
    {
        CloseMenues();
        ActiveMenu = newMenu;

        switch (ActiveMenu)
        {
            case E_MenuType.MAIN:
                menuMain.enabled = true;
                break;
            case E_MenuType.BUILD:
                menuBuild.enabled = true;
                //BuildMenu.SetActive(true);
                break;
            case E_MenuType.STATISTICS:
                menuStatistics.enabled = true;
                break;
            case E_MenuType.ACHIEVEMENTS:
                menuAchievements.enabled = true;
                break;
        }
    }

    //Toggles the cancel button that appears while user is selecting the spot to place building. Must be at index 0 in cancelBuildBtn array
    //TODO: Make the Build button turn into a Cancel button when pressed. And put in front of the BuildMenu canvas
    public void ToggleCancelButton(E_TouchStatus status)
    {
        if (status == E_TouchStatus.IDLE)
            cancelBuildBtn[0].gameObject.SetActive(false);
        else if (status == E_TouchStatus.BUILD)
            cancelBuildBtn[0].gameObject.SetActive(true);
    }

    public void CloseMenues()
    {
        if(menuMain)
            menuMain.enabled = false;
        if (menuBuild)
        {
            menuBuild.enabled = false;
           // BuildMenu.SetActive(false);
        }
        if (menuStatistics)
            menuStatistics.enabled = false;
        if (menuAchievements)
            menuAchievements.enabled = false;
    }

}

public enum E_MenuType
{
    MAIN,
    BUILD,
    STATISTICS,
    ACHIEVEMENTS
}