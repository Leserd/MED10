using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenCategories : MonoBehaviour {
    public GameObject[] Categories;
    public Text CategoryText;


	// Use this for initialization
	void Awake () {
        GetComponent<Button>().onClick.AddListener(() =>OpenCategory2());
        CategoryButton.CategoryPress += CloseAll;

	}
    void CloseAll(string name, string subName)
    {
        foreach (var Object in Categories)
        {
            if (Object != null)
            {
                if (Object.activeSelf)
                {
                    Object.SetActive(false);
                }
            }

        }
        if (CategoryText != null)
        {
        CategoryText.text = subName;

        }

    }
    public void SetActive(int num)
    {
        Categories[num].SetActive(true);
    }
    void OpenCategory2()
    {

        Categories[0].SetActive(true);
    }
}
