using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public Button buildBtn, cancelBuildBtn;
    public Canvas menuMain, menuBuild, menuStatistics;
    public GameObject BuildMenu;
    public static MenuManager instance;
    public E_MenuType ActiveMenu { get; set; }

    private void Awake()
    {
        instance = this;
        buildBtn.onClick.AddListener(() => ChangeMenu(E_MenuType.BUILD));
        cancelBuildBtn.onClick.AddListener(() => PlayerControls.instance.ChangeTouchStatus(E_TouchStatus.IDLE));

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
                //menuBuild.enabled = true;
                BuildMenu.SetActive(true);
                break;
            case E_MenuType.STATISTICS:
                menuStatistics.enabled = true;
                break;
        }
    }

    public void ToggleCancelButton(E_TouchStatus status)
    {
        if (status == E_TouchStatus.IDLE)
            cancelBuildBtn.gameObject.SetActive(false);
        else if (status == E_TouchStatus.BUILD)
            cancelBuildBtn.gameObject.SetActive(true);
    }

    public void CloseMenues()
    {
        if(menuMain)
            menuMain.enabled = false;
        if (menuBuild)
        {
            //menuBuild.enabled = false;
            BuildMenu.SetActive(false);
        }
        if (menuStatistics)
            menuStatistics.enabled = false;
    }

}

public enum E_MenuType
{
    MAIN,
    BUILD,
    STATISTICS
}