using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement {

    private string _title;                       //Achievement name
    private string _description;                //Achievement description
    private string _spritePath;                 //Resource folder path to achievement sprite
    private Sprite _sprite;                 
    private int _points;                        //Points awarded for completion
    private bool _unlocked;                     //Has achievement been unlocked yet
    private AchievementObject _achievementRef;  //Referenced UI object in the scene
    private string _category;                   //Name of the achievement category this belongs to, as well as the name of the parent in the scene
    private List<Achievement> dependencies;     //Other achievements that must be completed before this



    public Achievement(string category, AchievementObject achievementRef, string title, string description, string spritePath, int points)
    {
        Category = category;
        AchievementRef = achievementRef;
        Title = title;
        Description = description;
        SpritePath = spritePath;
        Points = points;
        Unlocked = false;
    }


    public void ClickAchievement()
    {
        Transform achievementLocation = AchievementManager.instance.achievementInfo.transform.GetChild(0);
        achievementLocation.GetChild(0).GetComponent<Image>().sprite = Sprite;
        achievementLocation.GetChild(1).GetComponent<Text>().text = Title;
        achievementLocation.GetChild(2).GetComponent<Text>().text = Points.ToString();
        achievementLocation.GetChild(3).GetComponent<Text>().text = Description;

        AchievementManager.instance.OpenAchievementInfo();
        //TODO: Disable interaction with achievement buttons
    }



    public void ProgressAchievement()
    {
        //Add progress to achievement
        //Eventually call UnlockAchievement
    }



    public void UnlockAchievement()
    {
        //Change sprite to fulfilled
        //Award points
        //Instantiate object in scene
        AchievementUnlock unlockedAchievement = UnityEngine.MonoBehaviour.Instantiate(AchievementManager.instance.achievementUnlockedPrefab).GetComponent<AchievementUnlock>();
        unlockedAchievement.transform.SetParent(AchievementManager.instance.achievementUnlockLocation);
        
    }


    public void AddDependency(Achievement dependency)
    {
        dependencies.Add(dependency);
    }


    //******Getters and setters******

    public string Title
    {
        get
        {
            return _title;
        }

        set
        {
            _title = value;
            _achievementRef.titleText.text = _title;
        }
    }

    public string Description
    {
        get
        {
            return _description;
        }

        set
        {
            _description = value;
        }
    }

    public string SpritePath
    {
        get
        {
            return _spritePath;
        }

        set
        {
            _spritePath = value;
            if(Resources.Load<Sprite>(_spritePath))
                Sprite= Resources.Load<Sprite>(_spritePath);
        }
    }

    public int Points
    {
        get
        {
            return _points;
        }

        set
        {
            _points = value;
            _achievementRef.pointText.text = _points.ToString();
        }
    }

    public bool Unlocked
    {
        get
        {
            return _unlocked;
        }

        set
        {
            _unlocked = value;
        }
    }

    public AchievementObject AchievementRef
    {
        get
        {
            return _achievementRef;
        }

        set
        {
            _achievementRef = value;
            _achievementRef.transform.SetParent(GameObject.Find(_category).transform);
            _achievementRef.transform.localScale = Vector3.one;
            _achievementRef.GetComponent<Button>().onClick.AddListener(() => ClickAchievement());
        }
    }

    public string Category
    {
        get
        {
            return _category;
        }

        set
        {
            _category = value;
        }
    }

    public Sprite Sprite
    {
        get
        {
            return _sprite;
        }

        set
        {
            _sprite = value;
            _achievementRef.icon.sprite = _sprite;
        }
    }
}
