using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour {

    public GameObject achievementPrefab;            
    public GameObject achievementInfo;              //Panel with achievement description, shown when achievement is clicked
    public Transform achievementUnlockLocation;     //Location where newly unlocked achievements pop up
    public GameObject achievementUnlockedPrefab;    
    public Dictionary<string, Achievement> achievements = new Dictionary<string, Achievement>();
    public static AchievementManager instance;



    private void Awake()
    {
        instance = this;
    }



    private void Start()
    {
        if (!achievementInfo)
            achievementInfo = GameObject.Find("AchievementInfo");
        if (!achievementUnlockLocation)
            achievementUnlockLocation = GameObject.Find("AchievementUnlockLocation").transform;

        CreateAchievement("Achievements", "Turn", "Make a turn", "Sprites/Achievements/Turn", 5);
        CreateAchievement("Achievements", "T", "Arrive at a T", "Sprites/Achievements/T", 8);
        CreateAchievement("Achievements", "WaterTurn", "Make a Water turn", "Sprites/Achievements/WaterTurn", 10);
    }



    public void CreateAchievement(string category, string title, string description, string spritePath, int points)
    {
        //Create the achievement UI in the scene
        GameObject newAchievementRef = Instantiate(achievementPrefab);
        //Create the achievement
        Achievement newAchievement = new Achievement(category, newAchievementRef.GetComponent<AchievementObject>(), title, description, spritePath, points);
        //Add achievement to the Achievements dictionary
        achievements.Add(title, newAchievement);
    }


    public void OpenAchievementInfo()
    {
        achievementInfo.SetActive(true);
        //TODO: Enable interaction with achievement buttons
    }


    public void CloseAchievementInfo()
    {
        achievementInfo.SetActive(false);
        //TODO: Enable interaction with achievement buttons
    }

}
