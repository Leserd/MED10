using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenCategories : MonoBehaviour {
    public GameObject[] Categories;


	// Use this for initialization
	void Awake () {
        GetComponent<Button>().onClick.AddListener(() =>OpenCategory2());
        CategoryButton.CategoryPress += CloseAll;

	}
    void CloseAll(string name, string subName)
    {
        GetComponentInChildren<Text>().text = subName;
        foreach (var Object in Categories)
        {
            Object.SetActive(false);
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

    void OpenCategory()
    {
        var categories = Instantiate(Categories[0], transform, false);
        var catbuttons = categories.GetComponentsInChildren<Button>();
        int number = 0;
        foreach (var button in catbuttons)
        {
            button.onClick.AddListener(() => SubCategory(number));
            number++;
        }
    }
    void SubCategory( int num)
    {
        if (num != 0)
        {
            Debug.Log(num);
            Instantiate(Categories[num], transform, false);

        }

    }


}
