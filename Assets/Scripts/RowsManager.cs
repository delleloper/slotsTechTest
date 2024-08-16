using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RowsManager : MonoBehaviour
{
    [SerializeField] private List<RowController> rows;
    [SerializeField] private Button button;
    [SerializeField] private float rollWait = 0.8f;
    [SerializeField] private float spinSpeed;
    [SerializeField] private float startPosition;
    [SerializeField] private float downLimit;

    const float RANDOM_SECS_MIN = 2;
    const float RANDOM_SECS_MAX = 2;
    private string[] lines = new string[3];

    private PatternChecker patternChecker;
    [SerializeField] private ResultsDisplay resultsDisplay;


    Action onRollingStopped;

    void Awake()
    {
        foreach (RowController row in rows)
        {
            row.Init(spinSpeed, startPosition, downLimit);
        }
        onRollingStopped += OnSpinStopped;

        patternChecker = new PatternChecker();

        // string[] linesTest = {
        //     "21382",
        //     "11111",
        //     "14578"
        // };
        // Debug.Log(patternChecker.CheckLinePattern(linesTest, 0));
        // Debug.Log(patternChecker.CheckLinePattern(linesTest, 1));
        // Debug.Log(patternChecker.CheckLinePattern(linesTest, 2));


    }

    IEnumerator StartSpinning()
    {
        foreach (RowController row in rows)
        {
            row.StartSpinning();
            yield return new WaitForSeconds(rollWait);
        }
        yield return new WaitForSeconds(rollWait);
        foreach (RowController row in rows)
        {
            yield return new WaitForSeconds(Random.Range(RANDOM_SECS_MIN, RANDOM_SECS_MAX));
            row.StopSpin();
        }
        onRollingStopped.Invoke();
    }

    public void Spin()
    {
        resultsDisplay.Clear();
        StartCoroutine(nameof(StartSpinning));
        button.interactable = false;

    }


    private void OnSpinStopped()
    {
        GetResults();
        button.interactable = true;
    }


    public void GetResults()
    {
        List<string> results = new List<string>();
        foreach (RowController row in rows)
        {
            results.Add(row.GetVisibleSymbols());
        }
        StringBuilder lineBuilder1 = new StringBuilder();
        StringBuilder lineBuilder2 = new StringBuilder();
        StringBuilder lineBuilder3 = new StringBuilder();
        for (int i = 0; i < results.Count; i++)
        {
            lineBuilder1.Append(results[i][0]);
            lineBuilder2.Append(results[i][1]);
            lineBuilder3.Append(results[i][2]);
        }
        lines[0] = lineBuilder1.ToString();
        lines[1] = lineBuilder2.ToString();
        lines[2] = lineBuilder3.ToString();

        List<Result> matches = patternChecker.CheckLines(lines);
        foreach (Result item in matches)
        {
            resultsDisplay.ShowResults(item.pattern);
        }
    }

    public bool CheckLinePattern(string PatternToCheck)
    {
        return false;
    }

}
