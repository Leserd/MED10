using UnityEngine;
using UnityEngine.UI;
using Script.UIBar;

public class TestButtonScript : MonoBehaviour {
    public Text inputField;




    public void ButtonPress()
    {
        var test = UIData.Instance;
        Debug.Log(test.AccountBalance);
        test.AccountBalance = int.Parse(inputField.text);

    }

	
}
