using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegretPressingBill : MonoBehaviour {

    private void Awake()
    {
        gameObject.AddComponent<Button>().onClick.AddListener(() => Destroy(transform.parent.gameObject));
    }
}
