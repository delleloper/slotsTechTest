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
    [SerializeField] private float limit;
    [SerializeField] private ResultsDisplay resultsDisplay;

    const float RANDOM_SECS_MIN = 2;
    const float RANDOM_SECS_MAX = 4;
    private string[] lines = new string[3];
    private PatternChecker patternChecker;

    Action onRollingStopped;

    void Awake()
    {
        patternChecker = new PatternChecker();
        foreach (RowController row in rows)
        {
            row.Init(spinSpeed, startPosition, limit);
        }
        onRollingStopped += OnSpinStopped;
    }

    IEnumerator StartSpinning()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        float time = Random.Range(RANDOM_SECS_MIN, RANDOM_SECS_MAX);
        foreach (RowController row in rows)
        {
            row.StartSpinning();
            yield return new WaitForSeconds(rollWait);
        }
        yield return new WaitForSeconds(time);
        foreach (RowController row in rows)
        {
            row.StopSpin();
            yield return new WaitForSeconds(rollWait);
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
        StartCoroutine(GetResults());
    }

    public IEnumerator GetResults()
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

        List<Result> matches = patternChecker.CheckAllPatterns(lines);
        foreach (Result item in matches)
        {
            StartCoroutine(resultsDisplay.ShowResults(item.pattern));
            StartCoroutine(resultsDisplay.AddCredits(item.score));
            yield return new WaitForSeconds(2);
        }
        resultsDisplay.Clear();
        button.interactable = true;
    }

}
