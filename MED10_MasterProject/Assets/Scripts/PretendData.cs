using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PretendData : MonoBehaviour {
    public DataInputTest[] LasseTestData;
    public static PretendData instance;

    private void Awake()
    {
        instance = this;
    }
}
[System.Serializable]
public class DataInputTest
{
    // public string BSdataName;

    public string BSDataName;
    public string BSDataAmount;
}



