using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeYear : MonoBehaviour {


    [SerializeField]
    private GameObject DecreaseYear, IncreaseYear;

    void Awake()
    {
        GetComponent<InputField>().text = "2017";
        DecreaseYear.AddComponent<Button>().onClick.AddListener(() => DeacreaseVisualYear());
        IncreaseYear.AddComponent<Button>().onClick.AddListener(() => IncreaseVisualYear());
    }

    void DeacreaseVisualYear()
    {
        var text = gameObject.GetComponent<InputField>();
        var currentYear = int.Parse(text.text);
        currentYear--;
        text.text = currentYear.ToString();

    }
    void IncreaseVisualYear()
    {
        var text = gameObject.GetComponent<InputField>();
        var currentYear = int.Parse(text.text);
        currentYear++;
        text.text = currentYear.ToString();
    }
}
