using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour {
    private Button _button;
    public GameObject SettingsMenu;


    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => OpenSettings());
    }


    private void OpenSettings()
    {
        SettingsMenu.SetActive(true);
    }

}
