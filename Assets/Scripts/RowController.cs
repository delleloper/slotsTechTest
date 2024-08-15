using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class RowController : MonoBehaviour
{
    private bool spinning = false;
    private float spinSpeed;
    private float startPosition;
    private float downLimit;
    private float symbolHeight = 115;
    private RectTransform rectTransform;
    private float stepHeight;
    private float xPosition;
    private VerticalLayoutGroup verticalLayoutGroup;

    public void Awake()
    {
        verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
        rectTransform = (RectTransform)transform;
        stepHeight = symbolHeight + verticalLayoutGroup.spacing;
        xPosition = rectTransform.anchoredPosition.x;
    }

    public void Init(float speed, float startPos, float YLimit)
    {
        spinSpeed = speed;
        startPosition = startPos;
        downLimit = YLimit;
    }

    public void StartSpinning()
    {
        spinning = true;
    }

    public void StopSpin()
    {
        spinning = false;
        float snappedY = Mathf.Round(rectTransform.anchoredPosition.y / stepHeight) * stepHeight;
        StartCoroutine(SmoothStop(snappedY, 0.5f));
    }

    void Update()
    {
        if (spinning)
        {
            rectTransform.anchoredPosition -= spinSpeed * Time.deltaTime * Vector2.down;
        }
        if (transform.position.y >= downLimit)
        {
            rectTransform.anchoredPosition = new Vector2(xPosition, startPosition);
        }
    }


    IEnumerator SmoothStop(float targetY, float duration)
    {
        float timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float newY = Mathf.Lerp(rectTransform.anchoredPosition.y, targetY, timeElapsed / duration);
            rectTransform.anchoredPosition = new Vector2(xPosition, newY);
            yield return null;
        }
        rectTransform.anchoredPosition = new Vector2(xPosition, targetY);
    }
}
