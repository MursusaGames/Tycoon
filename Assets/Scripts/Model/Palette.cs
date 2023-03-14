using UnityEngine;

public class Palette : MonoBehaviour
{
    [SerializeField] private EggsDepositories depo;
    public GameObject[] boxes;
    public int id;
    public int currentBox = 0;
    public int currentReversBox;
    public bool isFull;
    public bool isEmpty;
    public bool stakePalet;
    public bool isBuzy;
    
    private void OnEnable()
    {
        isBuzy = false;   
        currentReversBox = boxes.Length;
        currentBox = 0;
        MakeFull(true);
    }
    public void MakeFull(bool second)
    {
        if (stakePalet&&second)
        {
            Empty();
            return;
        }
        foreach (var box in boxes)
        {
            box.SetActive(true);
        }
        isFull = true;
        currentBox = 0;
    }
     
    public void TakeBox(int index = int.MaxValue)
    {
        if (isEmpty) return ;
        if(index == int.MaxValue)
        {
            boxes[currentBox].gameObject.SetActive(false);
            currentBox++;
            if (currentBox == boxes.Length)
            {
                if (!stakePalet)
                {
                    currentBox = 0;
                    depo.RemoveBox(id);                    
                }
                isEmpty = true;
                isFull = false;
            }
        }
        else
        {
            boxes[index].gameObject.SetActive(false);
            currentReversBox++;
            CheckEmpty();
        }
        
    }
    public int GetBox()
    {
        for (int i = 0; i < boxes.Length; i++)
        {
            if (boxes[i].activeInHierarchy)
            {
                return i;
            }
        }
        return int.MaxValue;
    }
    public int GetFreeBox()
    {
        for (int i = 0; i < boxes.Length; i++)
        {
            if (!boxes[i].activeInHierarchy)
            {
                return i;
            }
        }
        return int.MaxValue;
    }

    public bool GiveBox(int id1 = int.MaxValue)
    {
        if (isFull) return false;
        if(id1 == int.MaxValue)
        {
            boxes[currentReversBox - 1].gameObject.SetActive(true);
            currentReversBox--;
            if (currentReversBox == 0)
            {
                depo.boxes[id].SetActive(true);
                depo.currentBox++;
                depo.isEmpty = false;
                isFull = true;
            }
        }
        else
        {
            boxes[id1].SetActive(true);
            currentReversBox--;//TODO temp
        }
        
        isEmpty = false;
        CheckEmpty();
        CheckFull();
        
        return true;
    }

    public void Empty()
    {
        foreach (var box in boxes)
        {
            box.SetActive(false);
        }
        isFull = false;
        isEmpty = true;
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
        isEmpty = false;
    }
    private void CheckEmpty()
    {
        foreach (var box in boxes)
        {
            if (box.activeInHierarchy)
            {
                isEmpty = false;
                return;
            }
                
        }
        isEmpty = true;
        isFull = false;        
    }
}
