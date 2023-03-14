using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField] private Logger logger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tree"))
        {
            logger.PlaySound();
        }
    }
}
