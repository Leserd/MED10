using UnityEngine;
using DataVisualisation.Utilities;
using System.Collections.Generic;

public class BarChart : MonoBehaviour
{
    public Bar barPrefab;

    public float Chartheight { get; set; }

    public void SetData(List<BetalingsServiceData.BSData> dataCollection)
    {
        float maxValue = 0;

        foreach (var entry in dataCollection)
        {
            if (entry.MonthlyExpence > maxValue)
                maxValue = entry.MonthlyExpence;
        }

        var colors = ColorGenerator.GetColorsGoldenRatio(dataCollection.Count);
        for (int i = 0; i < dataCollection.Count; i++)
        {
            Bar newBar = Instantiate(barPrefab) as Bar;
            newBar.transform.SetParent(transform);

            var data = dataCollection[i];
            float value = data.MonthlyExpence;

            // set size of bar/s
            RectTransform rt = newBar.bar.GetComponent<RectTransform>();
            float normalizedValue = (value / maxValue) * 0.95f;
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, Chartheight * normalizedValue);
            newBar.bar.color = colors[i];

            newBar.label.text = data.TransactionName;

            //set value label
            newBar.barValue.text = value.ToString();
            if (rt.sizeDelta.y < 30f)
            {
                newBar.barValue.rectTransform.pivot = new Vector2(0.5f, 0f);
                newBar.barValue.rectTransform.anchoredPosition = Vector2.zero;
            }
        }
    }
}
