using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static bool IsApp { get; set; }        //Is the game an app version or in Unity Editor
    //public Text touchCounter;

    public void Awake()
    {
#if UNITY_EDITOR
        IsApp = false;
#else
        IsApp = true;
#endif

        
    }


    
}
