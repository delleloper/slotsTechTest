using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RowsManager : MonoBehaviour
{
    [SerializeField] private List<RowController> rows;
    [SerializeField] private Button button;
    [SerializeField] private float rollWait = 0.8f;


    // Start is called before the first frame update

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

    // public void Update()
    // {
    //     Debug.Log(((RectTransform)rows[0].transform).anchoredPosition);
    // }

    public void Spin()
    {
        StartCoroutine(nameof(StartSpinning));
        button.interactable = false;
    }

}
