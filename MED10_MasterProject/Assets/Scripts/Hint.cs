using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hint {

    private Sprite _sprite;
    private Button _hintObj;
   
    //Mangler mulighed for at sige hvad beskeden er

    public Hint(string spritePath, Vector3 position)
    {
        //instantiate hint prefab
        GameObject newBtn = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Hint"), GameObject.Find("HintCanvas").transform);
        _hintObj = newBtn.GetComponent<Button>();

        //Assign sprite to hint
        if ( Resources.Load<Sprite>(spritePath))
        {
            Debug.Log("loaded sprite");
        }
        Sprite = Resources.Load<Sprite>(spritePath);

        //AddListener to button
        //Burde der ikke bare være en der lytter efter touch input her og laver destroy når der bliver trykket et eller andet sted?
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
            {
                _hintObj.GetComponent<Image>().sprite = _sprite;
                var recttrans = _hintObj.transform as RectTransform;
                recttrans.sizeDelta = new Vector2(_sprite.textureRect.width, _sprite.textureRect.height);
            }
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

