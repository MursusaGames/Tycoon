using System.Collections.Generic;
using UnityEngine;

public class BoxInOutWar : MonoBehaviour
{
    public List<GameObject> palettes;
    public bool busy;    
    public FishProductType product;
    private void Start()
    {
        product = FishProductType.None;
    }

    public void SetBox(int index)
    {
        for (int i = 0; i < palettes.Count; i++)
        {
            palettes[i].SetActive(i == index);
        }
        busy = true;
        product = index == 0 ? FishProductType.Fish : index == 1 ? FishProductType.UnpackStake : index == 2 ? FishProductType.Stake 
            : index == 3 ? FishProductType.Farsh : index == 4 ? FishProductType.UnpackFarsh : index == 5 ? FishProductType.File :
            index == 6 ? FishProductType.UnpackFile : FishProductType.None;
    }
    public void ResetBox()
    {
        for (int i = 0; i < palettes.Count; i++)
        {
            palettes[i].SetActive(false);
        }
        busy = false;
        product = FishProductType.None;
    }


}
