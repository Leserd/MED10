using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercentageBar : MonoBehaviour {
    public  float Size;
    public  float BillAmount;

    private Vector2 _curentSize;
    private Vector2 _endSize;
    private bool _resize = false;
    private float _timeStartLerp;

    private RectTransform RectSize;


    public void Resize()
    {
        _curentSize = RectSize.sizeDelta;
        _timeStartLerp = Time.time;
        _endSize = new Vector2(Size, RectSize.rect.height);
        _resize = true;
    }

	// Use this for initialization
	void Awake () {
        RectSize = transform as RectTransform;
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (_resize)
        {
            RunTest();
        }
		
	}
    void RunTest()
    {
        var timeSinceStart = Time.time - _timeStartLerp;
        var percentageComplete = timeSinceStart / 1f;

        RectSize.sizeDelta = Vector2.Lerp(_curentSize, _endSize, percentageComplete);

        if (percentageComplete >= 1f)
        {
            _resize = false;
        }
        /*
        var time = 0f;
        while (time < 1f)
        {
            //Debug.Log(time);
            time += Time.deltaTime;
            RectSize.sizeDelta = Vector2.Lerp(_curentSize, new Vector2(Size, RectSize.rect.height), time);
        }*/
        //Resized = false;
    }
}
