using System;
using UnityEngine;
using System.Collections.Generic;
using DataVisualisation.Utilities;


public class PieGraph : MonoBehaviour
{
    public Pie PiePrefab;

    public void SetData(List<BetalingsServiceData.BSData> dataCollection)
    {
       

        var total = 0f;
        var zRotation = 0f;

        for (var i = 0; i < dataCollection.Count; i++)
        {
            total += dataCollection[i].MonthlyExpence;
        }

        var colors = ColorGenerator.GetColorsGoldenRatio(dataCollection.Count);
        for (var i = 0; i < dataCollection.Count; i++)
        {
            var entry = dataCollection[i];
            
            //Create the pie
            Pie newWedge = Instantiate(PiePrefab) as Pie;
            newWedge.transform.SetParent(transform);
            newWedge.pie.transform.SetParent(transform, false);
            newWedge.pie.GetComponent<RectTransform>().sizeDelta = transform.root.GetComponent<RectTransform>().sizeDelta;
            newWedge.pie.color = colors[i];
            newWedge.pie.fillAmount = entry.MonthlyExpence / total;
            newWedge.pie.transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRotation));
            zRotation -= newWedge.pie.fillAmount * 360f;

            //Create the correct labels
            newWedge.label.text = entry.TransactionName;
            newWedge.label.transform.localEulerAngles = new Vector3(0, 0, -newWedge.pie.GetComponent<RectTransform>().localEulerAngles.z);

            //Create the correct percentage values
            newWedge.pieValue.text = (Math.Round(entry.MonthlyExpence / total,3)).ToString();
            newWedge.pieValue.transform.localEulerAngles = new Vector3 (0,0,-newWedge.pie.GetComponent<RectTransform>().localEulerAngles.z);
        }
        
    }
}
