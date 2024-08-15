using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RowController : MonoBehaviour
{
    private bool spinning = false;
    [SerializeField] private float spinSpeed;
    [SerializeField] private float startPosition = 417.4f;
    [SerializeField] private float downLimit = 2466f;
    [SerializeField] private float symbolHeight;
    [SerializeField] private VerticalLayoutGroup verticalLayoutGroup;
    [SerializeField] private RectTransform rectTransform;

    private float stepHeight;
    private float xPosition;

    public void Awake()
    {
        rectTransform = (RectTransform)transform;
        stepHeight = symbolHeight + verticalLayoutGroup.spacing;
        xPosition = rectTransform.anchoredPosition.x;
    }

    public void StartSpinning()
    {
        spinning = true;
    }

    public void StopSpin()
    {
        spinning = false;
        Debug.Log(rectTransform.anchoredPosition.y);

        float snappedY = Mathf.Round(rectTransform.anchoredPosition.y / stepHeight) * stepHeight;
        Debug.Log(snappedY);
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
