using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public Button buildBtn;
    public Canvas menuMain, menuBuild, menuStatistics;
    public static MenuManager instance;
    public E_MenuType ActiveMenu { get; set; }

    private void Awake()
    {
        instance = this;
        buildBtn.onClick.AddListener(() => ChangeMenu(E_MenuType.BUILD));
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
                break;
            case E_MenuType.STATISTICS:
                menuStatistics.enabled = true;
                break;
        }
    }


    public void CloseMenues()
    {
        if(menuMain)
            menuMain.enabled = false;
        if (menuBuild)
            menuBuild.enabled = false;
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