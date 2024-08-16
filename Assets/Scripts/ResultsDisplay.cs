using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ResultsDisplay : MonoBehaviour
{
    private List<Image> Images;
    const int width = 5;
    const int height = 3;
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
                    yield return new WaitForSeconds(0.1f);
                    Images[index].color = Color.green;
                }
                else
                {
                    Images[index].color = Color.clear;
                }
            }
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
