using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RowsManager : MonoBehaviour
{
    [SerializeField] private List<RowController> rows;
    [SerializeField] private Button button;
    [SerializeField] private float rollWait = 0.8f;
    [SerializeField] private float spinSpeed;
    [SerializeField] private float startPosition;
    [SerializeField] private float downLimit;

    void Awake()
    {
        foreach (RowController row in rows)
        {
            row.Init(spinSpeed,startPosition,downLimit);
        }
    }

    IEnumerator StartSpinning()
    {
        foreach (RowController row in rows)
        {
            row.StartSpinning();
            yield return new WaitForSeconds(rollWait);
        }
        float randomSecs = Random.Range(2, 4);
        yield return new WaitForSeconds(randomSecs);
        foreach (RowController row in rows)
        {
            row.StopSpin();
            yield return new WaitForSeconds(rollWait);
        }
        button.interactable = true;

    }

    public void Spin()
    {
        StartCoroutine(nameof(StartSpinning));
        button.interactable = false;
    }

}
