using System.Collections.Generic;
using UnityEngine;

public class BoxInCleanMashine : MonoBehaviour
{
    [SerializeField] private AppData data;
    [SerializeField] private List<GameObject> fishes;    
    [SerializeField] private CleanMashineWorcer worcer;    
    [SerializeField] private GameObject mesh;
    public int fishCount;

    private void OnEnable()
    {
        fishCount = data.matchData.fisInBox-1;
    }    

    public void HideFish()
    {
        fishes[fishCount].SetActive(false);
        fishCount--;
        if (fishCount < 0)
        {
            fishCount = data.matchData.fisInBox - 1;
            SetFishes();
            mesh.SetActive(false);
        }
    }
    public void ShowMesh()
    {
        mesh.SetActive(true);
    }
    private void SetFishes()
    {
        foreach(var fish in fishes)
        {
            fish.SetActive(true);
        }
    }
}
