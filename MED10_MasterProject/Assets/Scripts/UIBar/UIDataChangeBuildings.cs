using UnityEngine;
using UnityEngine.UI;

public class UIDataChangeBuildings : MonoBehaviour {

    private static UIDataChangeBuildings instance = null;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
                
        }
        else
        {
            instance = this;
        }
    }
    public static UIDataChangeBuildings Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<UIDataChangeBuildings>();
                if (instance == null)
                {
                    var instanceObject = new GameObject();
                    instanceObject.name = "UIDataSingleton";
                    instance = instanceObject.AddComponent<UIDataChangeBuildings>();
                    DontDestroyOnLoad(instanceObject);
                }

            }
            return instance;
        }
    }

    public Text AccountData;
    public Text ElectricityData;
    public Text ClothingData;
    public Text EntertainmentData;
    public Text WaterData;
    public Text FoodData;




    private static int _accountBalance = 0;
    private static int _electricity = 0;
    private static int _clothing = 0;
    private static int _entertainment = 0;
    private static int _water = 0;
    private static int _food = 0;


    private void Start()
    {
        _accountBalance = int.Parse(AccountData.text);
        _electricity = int.Parse(ElectricityData.text);
        _entertainment = int.Parse(EntertainmentData.text);
        _food = int.Parse(FoodData.text);
        _water = int.Parse(WaterData.text);
        _clothing = int.Parse(ClothingData.text);
    }

    public int Account
    {
        get
        {
            int.TryParse(AccountData.text, out _accountBalance);
            return _accountBalance;

        }
        set
        {
           
           _accountBalance += value;
           AccountData.text = _accountBalance.ToString();
        }

    }
    public int Electricity
    {
        get
        {
            int.TryParse(ElectricityData.text, out _electricity);
            return _electricity;

        }
        set
        {
            _electricity += value;
            ElectricityData.text = _electricity.ToString();
        }

    }
    public int Entertainment
    {
        get
        {
            int.TryParse(EntertainmentData.text, out _entertainment);
            return _entertainment;

        }
        set
        {
            _entertainment += value;
            EntertainmentData.text = _entertainment.ToString();
        }

    }
    public int Food
    {
        get
        {
            int.TryParse(FoodData.text, out _food);
            return _food;

        }
        set
        {
            _food += value;
            FoodData.text = _food.ToString();
        }

    }
    public int Water
    {
        get
        {
            int.TryParse(WaterData.text, out _water);
            return _water;

        }
        set
        {
            _water += value;
            WaterData.text = _water.ToString();
        }

    }
    public int Clothing
    {
        get
        {
            int.TryParse(ClothingData.text, out _clothing);
            return _clothing;

        }
        set
        {
            _clothing += value;
            ClothingData.text = _clothing.ToString();
        }

    }
}
