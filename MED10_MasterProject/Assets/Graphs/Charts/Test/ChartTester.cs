using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ChartTester : MonoBehaviour
{
    public string database = "flowers";
    public string databaseTable = "floweroriginal";
    public string phpURL = "http://localhost/Cool_YT_RPG/ItemsDataQuery.php";

    public GameObject pieChartPrefab;
    public GameObject barChartPrefab;
    private float _chartHeight;
    public bool testPie;
    public bool testBar;

    private void Start()
    {
        _chartHeight = GetComponent<RectTransform>().rect.height;
    }

    private void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Return))
        {
            try
            {
                StartCoroutine(LoadDataBase(database, databaseTable));
            }
            catch (System.Exception meh)
            {
                Debug.Log("Failed because of " + meh);
                
            } 
            Debug.Log("pressed Enter");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var data = BetalingsServiceData.Instance.GetAllPaymentServices();



            if (testPie)
            {
                var piechart = Instantiate(pieChartPrefab);
                piechart.transform.SetParent(transform, false);
                piechart.GetComponent<PieGraph>().SetData(data);
            }

            if (testBar)
            {
                var barchart = Instantiate(barChartPrefab);
                barchart.transform.SetParent(transform, false);

                var chart = barchart.GetComponent<BarChart>();
                chart.Chartheight = _chartHeight;
                chart.SetData(data);
            }

            Debug.Log("pressed Space");
        }
    }

    private IEnumerator LoadDataBase(string db, string dbtable)
    {
        var flowerInfo = new List<string>();
        var thisForm = new WWWForm();
        thisForm.AddField("dbname", db);
        thisForm.AddField("dbtable", dbtable);
        var flowers = new WWW(phpURL, thisForm);
        yield return flowers;
        var flowerString = flowers.text;

        flowerInfo.AddRange(flowerString.Split('\n'));
        flowerInfo.Remove(flowerInfo[flowerInfo.Count - 1]);

        //CreateBarChart(Sort(flowerInfo));
    }

  /*  private void CreateBarChart(List<FlowerDatabaseEntry> listInput)
    {
        //function for getting the labels, counts and colors to the display barchart function
        var label = new List<string>();
        var counts = new List<float>();


        List<DataPoint> dataEntries = new List<DataPoint>();


        label.Add(listInput[0].Species);
        for (var i = 0; i < listInput.Count; i++)
        {
            if (!CheckMatch(label, listInput[i].Species))
            {
                label.Add(listInput[i].Species);
            }
        }

        counts.AddRange(new float[label.Count]);
        for (var i = 0; i < listInput.Count; i++)
        {

            if (label[0] == listInput[i].Species)
            {
                counts[0]++;
            }
            else if (label[1] == listInput[i].Species)
            {
                counts[1]++;
            }
            else
            {
                counts[2]++;
            }
        }

        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        //Convert the data to DataEntry objects.
        for (int i = 0; i < label.Count; i++)
        {
            dataEntries.Add(new DataPoint(counts[i], label[i]));
        }

        var dataCollection = new DataPointCollection(dataEntries, "no title");

        if (testPie)
        {

            var piechart = Instantiate(pieChartPrefab);
            piechart.transform.SetParent(transform, false);
            piechart.GetComponent<PieGraph>().SetData(dataCollection);
        }

        if (testBar)
        {
            var barchart = Instantiate(barChartPrefab);
            barchart.transform.SetParent(transform, false);
            barchart.GetComponent<BarChart>().Chartheight = _chartHeight;
            barchart.GetComponent<BarChart>().SetData(dataCollection);
        }

        if (!testPie && !testBar)
            Debug.Log("No test options selected2");


    }*/

    private bool CheckMatch(List<string> thisbool, string stringtoCheckfor)
    {
        var boolvalue = false;
        for (var i = 0; i < thisbool.Count; i++)
        {
            if (stringtoCheckfor == thisbool[i])
            {
                boolvalue = true;
            }

            else
            {
                boolvalue = false;
            }

        }
        return boolvalue;
    }

    private static List<FlowerDatabaseEntry> Sort(List<string> sortList)
    {
        //sort the data to a list
        var organizedData = new List<FlowerDatabaseEntry>();
        for (var i = 0; i < sortList.Count; i++)
        {
            var entry = new FlowerDatabaseEntry();
            var tempList = new List<string>();
            tempList.AddRange(sortList[i].Split(','));

            for (var j = 0; j < tempList.Count; j++)
            {
                tempList[j] = tempList[j].Remove(0, tempList[j].IndexOf(":") + 1);
            }

            entry.SepalWidth = float.Parse(tempList[1]);
            entry.SepalLength = float.Parse(tempList[2]);
            entry.PetalWidth = float.Parse(tempList[3]);
            entry.PetalLength = float.Parse(tempList[4]);
            entry.Species = (tempList[5]);
            organizedData.Add(entry);
        }

        return organizedData;
    }
}
