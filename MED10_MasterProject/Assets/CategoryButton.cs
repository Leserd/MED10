using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CategoryButton : MonoBehaviour {
    Button _button;
    public delegate void Categorys(string name, string subName);
    public static event Categorys CategoryPress;
    
	// Use this for initialization
	void Awake () {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => SendUpdate());
	}


    void SendUpdate()
    {
        if (CategoryPress != null)
        {
            CategoryPress(GetCategoryName(), GetSubCategoryName());

        }
    }

    string GetCategoryName()
    {
        return transform.parent.name;
    }
    string GetSubCategoryName()
    {
        return GetComponentInChildren<Text>().text;
    }

}
