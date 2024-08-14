using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowController : MonoBehaviour
{
    [SerializeField] private bool spinning = true;
    [SerializeField] private float spinSpeed;
    [SerializeField] private float startPosition = 417.4f;
    [SerializeField] private float downLimit = 2466f;



    // private RectTransform transform;
    void Awake()
    {
        // transform = GetComponent<RectTransform>();
        StartSpinning();
        // transform.position = new Vector3(transform.position.x, startPosition, transform.position.z);

    }

    void StartSpinning()
    {
        spinning = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (spinning)
        {
            transform.position -= spinSpeed * Time.deltaTime * Vector3.down;
        }
        if (transform.position.y >= downLimit)
        {
            transform.position = new Vector3(transform.position.x, startPosition, transform.position.z);
        }
    }
}
