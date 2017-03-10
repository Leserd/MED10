using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ActivateGameobject : MonoBehaviour {
    public GameObject ActivateObject;

    public static ActivateGameobject Instance = null;

    private Button _button;

    private void Awake()
    {
        Instance = this;
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => ActivateObject.SetActive(true));

    }
    public void Interactable(bool interact)
    {
        _button.interactable = interact;
    }

}
