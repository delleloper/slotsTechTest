using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class ResultsDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI creditsLabel;

    private List<Image> Images;
    const int width = 5;
    const int height = 3;
    int credits = 0;
    public void Awake()
    {
        Images = GetComponentsInChildren<Image>().ToList();
    }

    public IEnumerator ShowResults(string[] symbols)
    {
        Clear();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int index = (y * width) + x;
                if (symbols[y][x] == 'X')
                {
                    yield return new WaitForSeconds(0.01f);
                    Images[index].color = Color.green;
                }
                else
                {
                    Images[index].color = Color.clear;
                }
            }
        }
    }

    public IEnumerator AddCredits(int score)
    {
        int oldValue = credits;
        credits += score;
        while (oldValue != credits)
        {
            oldValue++;
            yield return new WaitForSeconds(0.01f);
            creditsLabel.text = oldValue.ToString().PadLeft(5, '0');
        }
    }

    public void Clear()
    {
        for (int i = 0; i < height; i++)
        {
            for (int y = 0; y < width; y++)
            {
                int index = (i * 5) + y;
                Images[index].color = Color.clear;
            }
        }
    }
}
