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

    public void ShowResults(string[] symbols)
    {
        for (int i = 0; i < height; i++)
        {
            for (int y = 0; y < width; y++)
            {
                int index = (i * width) + y;
                if (symbols[i][y] == 'X')
                {
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
