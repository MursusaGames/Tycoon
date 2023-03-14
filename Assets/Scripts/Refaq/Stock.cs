using System.Collections.Generic;
using UnityEngine;

public class Stock : MonoBehaviour
{
    [SerializeField] private List<GameObject> boxes;
    [SerializeField] private GameObject machine;
    public int currentBox = -1;
    public bool loading;
    public bool isBuzy;
    private void Start()
    {
        if(machine.activeInHierarchy)
            loading = true;
        foreach(var box in boxes)
        {
            box.SetActive(false);
        }
    }
    public void AddBox(int count=1)
    {
        for (int i = 0; i < count; i++)
        {
            currentBox++;
            boxes[currentBox].SetActive(true);
        }
                   
    }
    public void RemovBox(int count=1)
    {
        for (int i = 0; i < count; i++)
        {
            boxes[currentBox].SetActive(false);
            currentBox--;
        }
        
    }


}
