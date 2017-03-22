using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegretPressingBill : MonoBehaviour {

    private void Awake()
    {
        gameObject.AddComponent<Button>().onClick.AddListener(() => transform.parent.gameObject.SetActive(false));//Destroy(transform.parent.gameObject));
    }
}
