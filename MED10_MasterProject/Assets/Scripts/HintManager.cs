using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour {

    private List<Hint> activeHints = new List<Hint>();
    public static HintManager instance;

    private void Awake()
    {
        instance = this;
        PlayerControls.DestroyActiveHints += DestroyActiveHints;
    }
    

    public void AddActiveHint(Hint hint)
    {
        activeHints.Add(hint);
    }


    public void DestroyActiveHints()
    {
        foreach(Hint hint in activeHints)
        {
            hint.DestroyHint();
        }

        activeHints.Clear();
    }
}
