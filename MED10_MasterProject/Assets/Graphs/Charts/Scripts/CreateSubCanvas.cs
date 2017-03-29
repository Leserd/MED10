using UnityEngine;
using UnityEngine.UI;


public class CreateSubCanvas : MonoBehaviour {
    public void CreateCharts(string whichChart, Vector3 worldPosition, GameObject bar, GameObject pie){

        var canvasSpawnAtLoc = new GameObject();

        canvasSpawnAtLoc.transform.SetParent(transform, false);
        canvasSpawnAtLoc.name = whichChart;
        canvasSpawnAtLoc.AddComponent<Canvas>().renderMode = RenderMode.WorldSpace;
        canvasSpawnAtLoc.AddComponent<CanvasScaler>();
        canvasSpawnAtLoc.AddComponent<ChartTester>().barChartPrefab = bar;
        canvasSpawnAtLoc.GetComponent<ChartTester>().pieChartPrefab = pie;
        canvasSpawnAtLoc.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 500);
        canvasSpawnAtLoc.GetComponent<RectTransform>().localPosition = worldPosition;
        if (whichChart == "testPieChart")
        {
            canvasSpawnAtLoc.GetComponent<ChartTester>().testPie = true;
        }
        else if (whichChart == "testBarChart")
        {
            canvasSpawnAtLoc.GetComponent<ChartTester>().testBar = true;
        }
    }
}
