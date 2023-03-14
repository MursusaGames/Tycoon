using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggsDepositories : MonoBehaviour
{
    [SerializeField] private GameObject orderOpenWindow;
    [SerializeField] private AppData data;
    //[SerializeField] private GameObject moneyBoom;
    
    [SerializeField] private bool depo_2;
    [SerializeField] private GameObject depo_2_Off;
    [SerializeField] private bool depo_3;
    [SerializeField] private GameObject depo_3_Off;
    private OrdersSystem ordersSystem;
    public int currentBox = 0;
    public List<GameObject> boxes = new List<GameObject>();
    public float maxBoxesValue;
    public bool isFull;
    public bool isEmpty;
    public bool headCuttingMashine;
    public bool port;
    public bool stakePalet;
    //public bool out_1_Depo;
    
    public bool war_1 ;
    public bool endDepo;
    public bool onLoading;
    private float timeToCloseWindow = 2f;
    [SerializeField] private float timeToBoxOff = 1.5f;
    void Awake()
    {
        ordersSystem = FindObjectOfType<OrdersSystem>();
        for(int i = 1; i < transform.childCount; i++)
        {
            boxes.Add(transform.GetChild(i).gameObject);
            if(!stakePalet)
                transform.GetChild(i).gameObject.SetActive(false);
        }
        isEmpty = true;
        maxBoxesValue = boxes.Count;
    }
    private void Start()
    {
        onLoading = true;
        if (depo_2 && depo_2_Off.activeInHierarchy) onLoading = false;
        if (depo_3 && depo_3_Off.activeInHierarchy) onLoading = false;
    }
    private int CheckFreeGO()
    {
        for(int i = 0;i < boxes.Count; i++)
        {
            if (!boxes[i].gameObject.activeInHierarchy)
            {
                return i;
            }
        }
        return currentBox;
    }

    public int GetFreeGO()
    {
        for (int i = 0; i < boxes.Count; i++)
        {
            if (!boxes[i].gameObject.activeInHierarchy)
            {
                return i;
            }
        }
        return int.MaxValue;
    }
    public int GetFullGO()
    {
        for (int i = 0; i < boxes.Count; i++)
        {
            if (boxes[i].gameObject.activeInHierarchy && boxes[i].GetComponent<Palette>().isFull)
            {
                return i;
            }
        }
        return int.MaxValue;
    }

    public bool AddBox()
    {
        if (isFull) return false;
        if (!port)
            BoxShow();
        if (endDepo)
        {
            orderOpenWindow.SetActive(true);
            Invoke(nameof(ResetOrderOpenWindow), timeToCloseWindow);
        }
        return true;
    }
    private void ResetOrderOpenWindow()
    {
        orderOpenWindow.SetActive(false);
    }
    private void CheckFull()
    {
        foreach (var box in boxes)
        {
            if (!box.activeInHierarchy)
            {
                isFull = false;
                return;
            }
        }
        isFull = true;        
    }
    private void CheckEmpty()
    {
        foreach (var box in boxes)
        {
            if (box.activeInHierarchy)
            {
                return;
            }
        }
        isEmpty = true;
    }

    public void BoxShow()
    {
        if (headCuttingMashine)
        {
            var index = CheckFreeGO();
            boxes[index].SetActive(true);
            boxes[index].GetComponent<Palette>().currentBox = 0;
            boxes[index].GetComponent<Palette>().isEmpty = false;
        }
        else
        {
            boxes[currentBox].SetActive(true);
            boxes[currentBox].GetComponent<Palette>().isEmpty = false;   
        }
            
        
        //moneyBoom.SetActive(true);
        currentBox++;
        isEmpty = false;
        CheckFull();
        if (currentBox == maxBoxesValue)
        {
            isFull = true;                          
        }
    }
    public bool RemoveBox(int id = int.MaxValue)
    {
        if(id == int.MaxValue)
        {
            if (currentBox == 0) return false;
            currentBox--;
            if (war_1)
            {
                Invoke(nameof(BoxOff),timeToBoxOff);
            }
            else
            {
                boxes[currentBox].SetActive(false);
            }            
            boxes[currentBox].GetComponent<Palette>().isFull = false;            
            isFull = false;
            CheckEmpty();
            return true;
        }
        else
        {
            currentBox--;
            boxes[id].GetComponent<Palette>().isFull = false;
            boxes[id].GetComponent<Palette>().currentReversBox = boxes[id].GetComponent<Palette>().boxes.Length;
            boxes[id].SetActive(false);
            isFull = false;
            CheckEmpty();
            return true;
        }
        
    }
    private void BoxOff()
    {
        boxes[currentBox].SetActive(false);
    }

    public void RemoveAnyBoxes(int count)
    {
        for (int i = 0; i < count; i++)
        {
            boxes[currentBox].SetActive(false);
            boxes[currentBox].GetComponent<Palette>().isFull = false;
            currentBox--;
        }
        CheckEmpty();
    }
    public int GetFullGoCount()
    {
        int result = 0;
        for (int i = 0; i < boxes.Count; i++)
        {
            if (boxes[i].activeInHierarchy)
            {
                result++;
            }
        }
        return result;
    }
    public void AddAnyBoxes(int count)
    {
        for (int i = 0; i < count; i++)
        {
            boxes[currentBox].SetActive(true);
            boxes[currentBox].GetComponent<Palette>().isFull = false;
            currentBox++;
        }        
    }
}
