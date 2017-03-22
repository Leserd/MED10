using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenCategories : MonoBehaviour {
    public GameObject[] Categories;
    private Text _categoryText;


	// Use this for initialization
	void Awake () {
        GetComponent<Button>().onClick.AddListener(() =>OpenCategory2());
        CategoryButton.CategoryPress += CloseAll;
        _categoryText = GetComponentInChildren<Text>();

	}
    void CloseAll(string name, string subName)
    {
        foreach (var Object in Categories)
        {
            if (Object != null)
            {
                if (Object.activeSelf)
                {
                    Debug.Log("This was active " + Object.name);
                    Object.SetActive(false);
                }
            }

        }
        _categoryText.text = subName;

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
