using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ActivateGameobject : MonoBehaviour {
    public GameObject ActivateObject;
    private bool _firstTimePress = false;

    public static ActivateGameobject Instance = null;

    private Button _button;

    private void Awake()
    {
        Instance = this;
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => Pressed());
        _firstTimePress = true;

    }
    public void Interactable(bool interact)
    {
        _button.interactable = interact;
    }
    void Pressed()
    {
        ActivateObject.SetActive(true);
        if (_firstTimePress)
        {
            //new Hint("Sprites/Hints/test1", Vector3.zero);
            new Hint("Sprites/Hints/test2", new Vector3(0, 200, 200));
            _firstTimePress = false;
        }

    }

}
