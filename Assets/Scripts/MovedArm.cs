using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovedArm : MonoBehaviour
{
    private bool isBlincked;
    private int index;
    void Start()
    {
        isBlincked = true;
        StartCoroutine(nameof(Blinck));
    }
    private void OnDisable()
    {
        StopCoroutine(nameof(Blinck));
    }

    private IEnumerator Blinck()
    {
        while (isBlincked)
        {
            if (index % 2 == 0)
            {
                RotateUpIn10ToZ();
                yield return new WaitForSeconds(1);
            }
            else
            {
                RotateDownIn10ToZ();
                yield return new WaitForSeconds(1);
            }
            index++;

        }
    }
    private void RotateUpIn10ToZ()
    {
        for (int i = 0; i < 10; i++)
        {
            gameObject.transform.Rotate(Vector3.forward, 1f);
        }        
    }
    private void RotateDownIn10ToZ()
    {
        for (int i = 0; i < 10; i++)
        {
            gameObject.transform.Rotate(Vector3.forward, -1f);
        }
        
    }
}
