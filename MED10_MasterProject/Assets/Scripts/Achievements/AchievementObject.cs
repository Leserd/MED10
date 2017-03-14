using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementObject : MonoBehaviour {

	public Text titleText;
    public Text descriptionText;
    public Text pointText;
    public Image icon;

    private void Awake()
    {
        if (titleText == null)
            titleText = transform.FindChild("TitleText").GetComponent<Text>();
        //if (descriptionText == null)
        //    descriptionText = transform.FindChild("DescriptionText").GetComponent<Text>();
        if (pointText == null)
            pointText = transform.FindChild("PointText").GetComponent<Text>();
        if (icon == null)
            icon = transform.FindChild("Icon").GetComponent<Image>();
    }
}
