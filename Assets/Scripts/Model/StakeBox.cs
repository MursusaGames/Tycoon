using System.Collections.Generic;
using UnityEngine;

public class StakeBox : MonoBehaviour
{
    [SerializeField] private List<GameObject> fishes;
    private int currentFish;
    private void OnEnable()
    {
        currentFish = fishes.Count;
    }
    public void HideFish()
    {
        fishes[0].SetActive(false);
    }
    private void OnDisable()
    {
        foreach(var fish in fishes)
        {
            fish.SetActive(true);  
        }
    }
}
