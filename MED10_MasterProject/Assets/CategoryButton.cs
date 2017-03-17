using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CategoryButton : MonoBehaviour {
    Button _button;
    public delegate void Categorys(CategoryName name);
    public static event Categorys CategoryPress;
    



	// Use this for initialization
	void Awake () {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => SendUpdate());
	}


    void SendUpdate()
    {

        var temp = new CategoryName();
        temp.Category = GetCategoryName();
        temp.SubCategory = GetSubCategoryName();
        if (CategoryPress != null)
        {
            CategoryPress(temp);

        }
        transform.parent.gameObject.SetActive(false);
    }

    string GetCategoryName()
    {
        return transform.parent.name;
    }
    string GetSubCategoryName()
    {
        return GetComponentInChildren<Text>().text;
    }
    public class CategoryName
    {
        public string Category;
        public string SubCategory;
    }
}
