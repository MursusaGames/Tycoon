using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public enum FishProductType
{
    Fish,
    UnpackStake,
    Stake,
    UnpackFarsh,
    Farsh,
    UnpackFile,
    File,
    None
}
public class OutDepository : MonoBehaviour
{
    [SerializeField] private GameObject ordersMenu;
    [SerializeField] private GameObject ordersPopUp;
    [SerializeField] public List<BoxInOutWar> boxes;
    [SerializeField] private AppData data;
    [SerializeField] private OrdersSystem ordersSystem;
    public bool full;
    public bool empty;
    public List<int> productBoxCount;
    private int fishId = 0;
    private int unpStakeId = 1;
    private int stakeId = 2;
    private int farshId = 3;
    private int unpFarshId = 4;
    private int fileId = 5;
    private int unpFileId = 6;
    private bool orderOn;  
    
    private void Start()
    {
        empty = true;
        productBoxCount = new List<int>() { 0,0,0,0,0,0,0};
        
        //Invoke(nameof(InitDepo), 3f);
    }
    private void SetOrdersBtn()
    {
        orderOn = PlayerPrefs.GetInt(SaveDataSystem.Instance.firstOrder) == 1;
        ordersMenu.SetActive(orderOn);
        if(orderOn) data.userData.getOrder = false;
    }

    private void InitDepo()
    {
        for (int i = 0; i < data.userData.fishInWarehouse; i++)
        {
            AddBox(fishId,true);
        }
        for (int i = 0; i < data.userData.unpSteakInWarehouse; i++)
        {
            AddBox(unpStakeId, true);
        }
        for (int i = 0; i < data.userData.steakInWarehouse; i++)
        {
            AddBox(stakeId, true);
        }
        for (int i = 0; i < data.userData.farshInWarehouse; i++)
        {
            AddBox(farshId, true);
        }
        for (int i = 0; i < data.userData.unpFarshInWarehouse; i++)
        {
            AddBox(unpFarshId, true);
        }
        for (int i = 0; i < data.userData.fileInWarehouse; i++)
        {
            AddBox(fileId, true);
        }
        for (int i = 0; i < data.userData.unpFileInWarehouse; i++)
        {
            AddBox(unpFileId, true);
        }
        SetOrdersBtn();
    }

    public bool AddBox(int id, bool inData = false)
    {
        var index = GetFirstFreeBox();
        if(index == int.MaxValue) return false;
        boxes[index].SetBox(id);
        var temp = productBoxCount[id];
        temp++;
        productBoxCount[id] = temp;
        if (!inData)
        {
            if (id == fishId) data.userData.fishInWarehouse++;
            else if (id == unpStakeId) data.userData.unpSteakInWarehouse++;
            else if (id == stakeId) data.userData.steakInWarehouse++;
            else if (id == farshId) data.userData.farshInWarehouse++;
            else if (id == unpFarshId) data.userData.unpFarshInWarehouse++;
            else if (id == fileId) data.userData.fileInWarehouse++;
            else if (id == unpFileId) data.userData.unpFileInWarehouse++;
        }        
        empty = false;
        if (!orderOn)
        {
            data.userData.getOrder = true;
            orderOn = true;            
            ordersMenu.SetActive(orderOn);
        }
        

        //if (id == fishId) ordersSystem.FishAdd();
        //if (id == unpStakeId) ordersSystem.UnpackedStakeAdd();
        //if (id == stakeId) ordersSystem.StakeAdd();
        return true;
    }
    public void RemoveBox(FishProductType product, int count, int id)
    {
        for (int i = 0; i < boxes.Count; i++)
        {
            if (boxes[i].product == product)
            {
                boxes[i].ResetBox();
                productBoxCount[id]--;
                count--;
                if (count == 0) return;
            }
        }
    }
    private int GetFirstFreeBox()
    {
        for (int i = 0; i < boxes.Count; i++)
        {
            if (!boxes[i].busy)
            {
                return i;   
            }
            
        }
        return int.MaxValue;
    }

    public int FindActivPaleteForProduct( FishProductType product)
    {
        for (int i = 0; i < boxes.Count; i++)
        {
            if (boxes[i].gameObject.activeInHierarchy && boxes[i].product == product)
            {
                return i;
            }
        }
        return int.MaxValue;
    }
}
