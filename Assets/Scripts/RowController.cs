using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RowController : MonoBehaviour
{
    private bool spinning = false;
    private float spinSpeed;
    private float startPosition;
    private float limit;
    private float symbolHeight = 115;
    private RectTransform rectTransform;
    private float stepHeight;
    private float xPosition;
    private VerticalLayoutGroup verticalLayoutGroup;
    private string symbols;
    public bool debug = false;

    public void Awake()
    {
        verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
        rectTransform = (RectTransform)transform;
        stepHeight = symbolHeight + verticalLayoutGroup.spacing;
        xPosition = rectTransform.anchoredPosition.x;
        foreach (Image item in GetComponentsInChildren<Image>())
        {
            if (item.gameObject.activeInHierarchy && item.sprite != null)
            {
                symbols += item.sprite.name;
            }
            else
            {
                symbols += "0";
            }
        };
        // float randomStartPos = Random.Range(0, 14) * stepHeight;
        // rectTransform.anchoredPosition = new Vector2(xPosition, randomStartPos);
    }

    public void Init(float speed, float startPos, float YLimit)
    {
        spinSpeed = speed;
        startPosition = startPos;
        limit = YLimit;
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
            rectTransform.anchoredPosition -= spinSpeed * Vector2.up;
        }
        if (rectTransform.anchoredPosition.y <= limit)
        {
            rectTransform.anchoredPosition = new Vector2(xPosition, startPosition);
        }
        if (debug)
        {
            Debug.Log(GetVisibleSymbols());
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
    }

    public string GetVisibleSymbols()
    {
        int stoppedPosition = GetSnappedPosId();
        string visibleSymbols = WrappedSubstring(symbols, stoppedPosition);
        return visibleSymbols;
    }

    public string WrappedSubstring(string symbols, int startIndex)
    {
        int totalLength = symbols.Length;

        string wrappedSymbols = symbols + symbols;
        startIndex = startIndex % totalLength;
        return wrappedSymbols.Substring(startIndex, 3);
    }


    private int GetSnappedPosId()
    {
        return Mathf.RoundToInt(rectTransform.anchoredPosition.y / stepHeight);
    }
}
