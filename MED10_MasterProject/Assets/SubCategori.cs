using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubCategori : MonoBehaviour {
    public Dropdown SubDropdown;
    private Dropdown CategoryDropdown;

    private List<string[]> test = new List<string[]>() {    new string[] { "Vælg underkategori...", "Husleje", "Varme", "El", "Vand", "Gas" },
                                                            new string[] { "Vælg underkategori...", "Internet", "TV", "Mobil", "Licens" } ,
                                                            new string[] { "Vælg underkategori...", "Lån", "Forsikring", "Abonnement", "Fritid", "Transport", "Personlig Pleje", "Andet" } };
  

    void Awake () {
        CategoryDropdown = GetComponent<Dropdown>();
        CategoryDropdown.onValueChanged.AddListener(delegate { OnChangedValue(); });
    }


    void OnChangedValue()
    {
        if (CategoryDropdown.value !=0)
        {
            SubDropdown.options.Clear();
            foreach (var option in test[CategoryDropdown.value-1])
            {
                SubDropdown.options.Add(new Dropdown.OptionData(option));
            }
            SubDropdown.interactable = true;
        }
        else
        {
            SubDropdown.interactable = false;
        }
    }
}
