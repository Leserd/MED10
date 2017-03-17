using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour {

	// Use this for initialization
	void Awake () {
       // GetComponent<Button>().onClick.AddListener(() => transform.parent.gameObject.SetActive(false));

        // alternativ
        GetComponent<Button>().onClick.AddListener(() => Destroy(transform.parent.gameObject));


    }

}
