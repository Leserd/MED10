using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavingsAccessor : MonoBehaviour {
    public   string SavingsCategory;
    public   int SavingsAmount;
    public  int SavingsDate;


    [SerializeField]
    private Dropdown _categoryDrop;
    

    public static SavingsAccessor Instance = null;
void Awake()
    {
        Instance = this;
        _categoryDrop.onValueChanged.AddListener(delegate { savingsCat(); });


    }
    public void savings(string amount)
    {
        int.TryParse(amount, out SavingsAmount);
    }

    private void savingsCat ()
    {
        SavingsCategory = _categoryDrop.options[_categoryDrop.value].text;
    }

}
