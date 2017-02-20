using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateChild : MonoBehaviour {

    public GameObject BuildMenu;

    public void activate(bool activate)
    {
        BuildMenu.SetActive(activate);
    }


}
