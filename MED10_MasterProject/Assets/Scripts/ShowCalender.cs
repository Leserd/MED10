using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCalender : MonoBehaviour {

    private Button _showCalenderButton;
    public GameObject Calender;
    
    
    private void Awake()
    {
        _showCalenderButton = gameObject.AddComponent<Button>();
        _showCalenderButton.onClick.AddListener(() => OpenCalender());
    } 
    private void OpenCalender()
    {
        Calender.SetActive(true);
    }
}
