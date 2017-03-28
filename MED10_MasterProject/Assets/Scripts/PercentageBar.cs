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

    void Awake () {
        RectSize = transform as RectTransform;		
	}
	
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
    }
}
