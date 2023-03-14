using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Blincked : MonoBehaviour
{
    private Image hand;
    private bool isBlincked;
    private int index;
    void Start()
    {
        hand = gameObject.GetComponent<Image>();
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
                hand.enabled = false;
                yield return new WaitForSeconds(1);
            }
            else
            {
                hand.enabled = true;
                yield return new WaitForSeconds(1);
            }
            index++;

        }
    }
}
