using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenCategories : MonoBehaviour {
    public GameObject[] Categories;

	// Use this for initialization
	void Awake () {
        GetComponentInChildren<Button>().onClick.AddListener(() =>OpenCategory());
        CategoryButton.CategoryPress += DestroyAll;
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
    void DestroyAll(CategoryButton.CategoryName meh)
    {
        foreach (var Cat in Categories)
        {
            Destroy(Cat);
        }
    }

}
