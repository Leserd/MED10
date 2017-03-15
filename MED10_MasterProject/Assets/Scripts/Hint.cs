using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hint {

    private Sprite _sprite;
    private Button _hintObj;
   

    public Hint(string spritePath, Vector3 position)
    {
        //instantiate hint prefab
        GameObject newBtn = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Hint"), GameObject.Find("HintCanvas").transform);
        _hintObj = newBtn.GetComponent<Button>();

        //Assign sprite to hint
        Sprite = Resources.Load<Sprite>(spritePath);

        //AddListener to button
        _hintObj.onClick.AddListener(() => DestroyHint());

        //Change position
        _hintObj.GetComponent<RectTransform>().localPosition = position;

        //Enable hint
        ShowHint();

    }



    public void ShowHint()
    {
        //TODO: Fancy måde hints popper frem på. 
        _hintObj.gameObject.SetActive(true);
    }



    public void DestroyHint()
    {
        //TODO: Fancy måde hints fjernes på. 
        Object.Destroy(_hintObj.gameObject);
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
            if (_sprite)
                _hintObj.GetComponent<Image>().sprite = _sprite;
            else
                return;
        }
    }


    public Button HintObj
    {
        get
        {
            return _hintObj;
        }

        set
        {
            _hintObj = value;
        }
    }
}


public enum E_HintDirection
{
    NONE,
    UP,
    RIGHT,
    DOWN,
    LEFT
}