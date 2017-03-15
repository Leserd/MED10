using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IntroductionMenu : MonoBehaviour {

    public EventSystem eventSystem;
    public Image imagePanel;                    //The direct parent of the images that are scrolled between
    public Image currentImage;
    public Sprite dotActive, dotInactive;
    public int currentImageIndex = 0;
    public List<Image> images = new List<Image>();
    public List<Vector2> imagePositions = new List<Vector2>();
    public List<Image> imageDots = new List<Image>();
    public Vector2 startTouchPos = Vector2.zero;
    public Vector2 lastTouchPos = Vector2.zero;
    public Vector2 curTouchPos = Vector2.zero;
    public bool dragging = false;
    public bool canSwipe = true;
    public float swipeDistanceThreshold = 45f;  //How far the user has to swipe before it counts as turning a page
    public float timeToMoveImage = 0.5f;        //Time for the image to move to the new position 

    private Vector2 _panelStartPos;
    private int _imageWidth = 175;
    private int _imageSpacing = 10;
    

	// Use this for initialization
	void Start () {
        if (eventSystem == null)
            eventSystem = GameObject.FindObjectOfType<EventSystem>();
        _panelStartPos = imagePanel.rectTransform.localPosition;

        for(int i = 0; i < images.Count; i++)
        {
            imagePositions.Add(_panelStartPos + new Vector2(-i * (_imageSpacing + _imageWidth), 0));
        }
    }



    public void BeginDrag()
    {
        if (canSwipe)
        {
            AssignStartTouchPosition();
            AssignCurrentTouchPosition();
            AssignLastTouchPosition();
            dragging = true;
        }
        
    }



    public void Drag()
    {
        if (canSwipe)
        {
            AssignCurrentTouchPosition();

            float distance = curTouchPos.x - lastTouchPos.x;

            int dir = -1;
            if (distance < 0)
                dir = 1;



            if(dir == -1 && currentImageIndex == 0 && (Vector2)imagePanel.rectTransform.localPosition == imagePositions[currentImageIndex]) //if trying to swipe to a side that has no additional image
            {
                return;
            }
            else if(dir == 1 && currentImageIndex == images.Count - 1 && (Vector2)imagePanel.rectTransform.localPosition == imagePositions[currentImageIndex])
            {
                return;
            }
            else
            {
                Vector3 newPosition = Vector3.Lerp(imagePanel.rectTransform.position,
                    imagePanel.rectTransform.position + new Vector3(distance, 0, 0),
                    Time.deltaTime * 1f);

                //Clamp x value of new position so it does not extend too far from what is possible. DOES NOT WORK
                //float clampedX = Mathf.Clamp(newPosition.x, imagePositions[currentImageIndex].x - _imageSpacing, imagePositions[currentImageIndex].x + _imageSpacing);
                //newPosition = new Vector3(clampedX, newPosition.y, newPosition.z);

                imagePanel.rectTransform.position = newPosition;
            }
            

            AssignLastTouchPosition();
        }
    }



    public void EndDrag()
    {
        if (canSwipe)
        {
            AssignCurrentTouchPosition();

            float distance = curTouchPos.x - startTouchPos.x;

            if (Mathf.Abs(distance) > swipeDistanceThreshold)
            {
                int dir = -1;  //-1 = defaults to swiping to the right
                if (distance < 0) //swiping to the  left
                    dir = 1;

                if (currentImageIndex + dir < images.Count && currentImageIndex + dir >= 0)    
                {
                    StartCoroutine(MoveImageToNewPosition(dir));
                    
                }
                else
                {
                    StartCoroutine(ReturnImageToPosition());
                }
                
            }
            else
            {
                StartCoroutine(ReturnImageToPosition());
            }

            dragging = false;
        }
    }



    private IEnumerator MoveImageToNewPosition(int direction)
    {
        canSwipe = false;
        Vector2 startPosition = imagePanel.rectTransform.localPosition;
        Vector2 endPosition = imagePositions[currentImageIndex + direction];

        float currentLerpTime = 0f;

        while (Vector2.Distance(imagePanel.rectTransform.localPosition, endPosition) > 1f)
        {

            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > timeToMoveImage)
            {
                currentLerpTime = timeToMoveImage;
            }

            float perc = currentLerpTime / timeToMoveImage;
            imagePanel.rectTransform.localPosition = Vector2.Lerp(imagePanel.rectTransform.localPosition, endPosition, perc);
            
            yield return new WaitForEndOfFrame();
        }

        imagePanel.rectTransform.localPosition = endPosition;

        //Disable currentImage
        currentImage.GetComponent<EventTrigger>().enabled = false;
        imageDots[currentImageIndex].sprite = dotInactive;

        //Update currentImage
        currentImageIndex += direction;
        currentImage = images[currentImageIndex];

        //Enable new currentImage
        currentImage.GetComponent<EventTrigger>().enabled = true;
        imageDots[currentImageIndex].sprite = dotActive;

        //enable swiping
        canSwipe = true;


    }


    private IEnumerator ReturnImageToPosition()
    {
        canSwipe = false;

        Vector2 startPosition = imagePanel.rectTransform.localPosition;
        Vector2 endPosition = imagePositions[currentImageIndex];

        float currentLerpTime = 0f;

        while (Vector2.Distance(imagePanel.rectTransform.localPosition, endPosition) > 1f)
        {

            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > timeToMoveImage)
            {
                currentLerpTime = timeToMoveImage;
            }

            float perc = currentLerpTime / timeToMoveImage;
            imagePanel.rectTransform.localPosition = Vector2.Lerp(imagePanel.rectTransform.localPosition, endPosition, perc);

            yield return new WaitForEndOfFrame();
        }

        imagePanel.rectTransform.localPosition = endPosition;
        //enable swiping
        canSwipe = true;
    }



    public void StartGame()
    {
        gameObject.SetActive(false);
    }


    private void AssignStartTouchPosition()
    {
        if (GameManager.IsApp)
        {
            if (Input.touchCount > 0)
            {
                startTouchPos = Input.GetTouch(0).position;

            }
        }
        else
        {
            startTouchPos = Input.mousePosition;
        }
    }


    private void AssignCurrentTouchPosition()
    {
        if (GameManager.IsApp)
        {
            if(Input.touchCount > 0)
            {
                curTouchPos = Input.GetTouch(0).position;
                
            }
        }
        else
        {
            curTouchPos = Input.mousePosition;
        }
    }

    private void AssignLastTouchPosition()
    {
        if (GameManager.IsApp)
        {
            if (Input.touchCount > 0)
            {
                lastTouchPos = curTouchPos;

            }
        }
        else
        {
            lastTouchPos = curTouchPos;
        }
    }
}
