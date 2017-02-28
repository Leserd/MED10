using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectIncome : MonoBehaviour {
    public int income;
    public int targetSource = 2;
    public float timeToUpdate = 0f;

    private void Update()
    {
        gameObject.GetComponent<Image>().fillAmount += (Time.fixedDeltaTime/timeToUpdate);
    }



    public void OnButtonClick()
    {
        if(gameObject.GetComponent<Image>().fillAmount > 0.99f)
        {
            var UIBar = UIDataChangeBuildings.Instance;

            switch (targetSource)
            {

                case 1:
                    //account
                    UIBar.Account = income;
                    break;

                case 2:
                    //electricity
                    UIBar.Electricity = income;
                    break;

                case 3:
                    //clothing
                    UIBar.Clothing = income;
                    break;

                case 4:
                    //entertainment
                    UIBar.Entertainment = income;
                    break;

                case 5:
                    //water
                    UIBar.Water = income;
                    break;               

                case 6:
                    //food
                    UIBar.Food = income;
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

