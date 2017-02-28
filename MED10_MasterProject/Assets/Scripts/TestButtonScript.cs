using UnityEngine;
using UnityEngine.UI;

public class TestButtonScript : MonoBehaviour {
    public Text inputField;




    public void ButtonPress()
    {
        var test = UIDataChangeBuildings.Instance;
        Debug.Log(test.Account);
        test.Account = int.Parse(inputField.text);

    }

	
}
