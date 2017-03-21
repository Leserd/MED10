using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenStub : MonoBehaviour {
    public GameObject Open;


    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => OpenSelectedStub());
    }

    void OpenSelectedStub()
    {
        Instantiate(Open, GameObject.FindGameObjectWithTag("Category").transform, false);
    }


}
