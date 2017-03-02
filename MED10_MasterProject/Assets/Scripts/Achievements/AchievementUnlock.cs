using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUnlock : MonoBehaviour {

    public Text titleText;
    public Text pointText;
    public Image icon;



    private void Awake()
    {
        if (titleText == null)
            titleText = transform.FindChild("Texts").FindChild("TitleText").GetComponent<Text>();
        if (pointText == null)
            pointText = transform.FindChild("Texts").FindChild("PointText").GetComponent<Text>();
        if (icon == null)
            icon = transform.FindChild("Icon").GetComponent<Image>();
    }



    public void DisplayAchievementInfo(Achievement achievement)
    {
        icon.sprite = achievement.Sprite;
        titleText.text = achievement.Title;
        pointText.text = achievement.Points.ToString();
    }

}
