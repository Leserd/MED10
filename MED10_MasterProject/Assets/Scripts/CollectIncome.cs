using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectIncome : MonoBehaviour {
    public int income;
    public int targetSource = 2;

    private void Update()
    {
        gameObject.GetComponent<Image>().fillAmount += Time.deltaTime;
    }


    public void OnButtonClick()
    {
        Debug.Log(income);
        if(gameObject.GetComponent<Image>().fillAmount > 0.99f)
        {
            var UIBar = UIDataChangeBuildings.Instance;

            switch (targetSource)
            {

                case 2:
                    //food
                    UIBar.Food = income;
                    break;
                case 1:
                    //electricity
                    UIBar.Electricity = income;
                    break;

                default:
                    Debug.Log(targetSource + " is not a possible target");
                    break;
            }


            //empty fillamount
            gameObject.GetComponent<Image>().fillAmount = 0;
        }
    }
}
