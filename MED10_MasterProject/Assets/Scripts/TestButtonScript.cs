using UnityEngine;
using UnityEngine.UI;

public class TestButtonScript : MonoBehaviour {
    public Text inputField;




    public void ButtonPress()
    {
        var test = UIDataChangeBuildings.Instance;
        Debug.Log(test.AccountBalance);
        test.AccountBalance = int.Parse(inputField.text);

    }

	
}
