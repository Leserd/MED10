using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ActivateGameobject : MonoBehaviour {
    public GameObject ActivateObject;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => ActivateObject.SetActive(true));

    }
}
