using TMPro;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public enum ValuteType
{
    money,
    gold,
    crystall
}
public class OrdersSystem : BaseMonoSystem
{
    [SerializeField] private TextMeshProUGUI capacity;    
    [SerializeField] private AdmobRewardedSystem admobRewardedSystem;
    [SerializeField] private DeliveryPanel deliveryPanel;
    [SerializeField] private Image deliveryBar;
    [SerializeField] private Image productImg;
    [SerializeField] private List<GameObject> arrows;
    [SerializeField] private List<Button> buttons;
    [SerializeField] private GameObject noOrdersPanel;
    [SerializeField] private GameObject ordersPanel;
    [SerializeField] private GameObject ordersPanelInDelivery;
    [SerializeField] private GameObject orderDeliversPanel;
    [SerializeField] private GameObject orderPanelInTimer;
    [SerializeField] private TextMeshProUGUI timerCount;
    [SerializeField] private TextMeshProUGUI fishBoxCount;
    [SerializeField] private GameObject fishGO;
    [SerializeField] private TextMeshProUGUI unpSteakBoxCount;
    [SerializeField] private TextMeshProUGUI allBoxCount;
    [SerializeField] private TextMeshProUGUI productNumber;
    [SerializeField] private TextMeshProUGUI productName;
    [SerializeField] private TextMeshProUGUI productCost;
    [SerializeField] private TextMeshProUGUI productCostInGold;
    [SerializeField] private GameObject unpSteakGO;
    [SerializeField] private TextMeshProUGUI unpFarshBoxCount;
    [SerializeField] private GameObject unpFarshGO;
    [SerializeField] private TextMeshProUGUI unpFileBoxCount;
    [SerializeField] private GameObject unpFileGO;
    [SerializeField] private TextMeshProUGUI steakBoxCount;
    [SerializeField] private GameObject steakGO;
    [SerializeField] private TextMeshProUGUI farshBoxCount;
    [SerializeField] private GameObject farshGO;
    [SerializeField] private TextMeshProUGUI fileBoxCount;
    [SerializeField] private GameObject fileGO;
    [SerializeField] private OutDepository outDepo_1;    
    [SerializeField] private OutDepository outDepo_2;
    [SerializeField] private OutDepository startOutDepo;
    [SerializeField] private OutDepository outDepo_3;
    [SerializeField] private List<Sprite> productsSprites;    
    public List<Image> btnImgs;
    public OutDepository currentDepo;
    [SerializeField] private BigCar bigCar;
    [SerializeField] private Barier barier;
    [SerializeField] private List<string> ordersName;    

    //TODO изменить на private

    public List<int> ordersProductNumber;
    public List<double> ordersCost;
    public List<int> ordersProductId;
    public List<ValuteType> ordersValute;    
    
    public bool orderFishOn;
    public bool orderUnpackedStakeOn;
    public bool orderStakeOn;
    private int fishId = 0;
    private int unpackedSteakId = 1;
    private int steakId = 2;
    private int farshId = 3;
    private int unpackedFarshId = 4;
    private int fileId = 5;
    private int unpackedFileId = 6;

    private float timer = 120f;
    private bool startTimer;
    public int activBtn;
    private int ordersCount = 3;
    private bool firstOrderClose;
    private bool acceptForAD;
    int orderCountInCikl = 0;
    private void Start()
    {
        data.userData.currentProductCount = 0;
        data.userData.steakInWarehouse = 0;
        data.userData.unpSteakInWarehouse = 0;
        data.userData.unpFileInWarehouse = 0;
        data.userData.fileInWarehouse = 0;
        data.userData.unpFarshInWarehouse = 0;
        data.userData.farshInWarehouse = 0;
        data.userData.fishInWarehouse = 0;
    }
    private double GetCurrentAllIncome()
    {
        var result = data.userData.headCutingMashinePromice;
        if (data.userData.fishCleaningMashine > 0) result += data.userData.fishCleaningMashinePromice;
        if (data.userData.fishCleaningMashine > 1) result += data.userData.fishCleaningMashine2Promice;
        if (data.userData.fishCleaningMashine > 2) result += data.userData.fishCleaningMashine3Promice;
        if (data.userData.headCutingMashine > 1) result += data.userData.headCutingMashine2Promice;
        if (data.userData.steakMashine > 0) result += data.userData.steakMashinePromice;
        if (data.userData.farshMashine > 0) result += data.userData.farshMashinePromice;
        if (data.userData.fileMashine > 0) result += data.userData.fileMashinePromice;
        if (data.userData.packingMashine > 0) result += data.userData.steakPackingMashinePromice;
        if (data.userData.packingMashine > 1) result += data.userData.farshPackingMashinePromice;
        if (data.userData.packingMashine > 2) result += data.userData.filePackingMashinePromice;
        return result;
    }

    private void SetNewOrder(int i, int id, double cost, int count)
    {
        var rand = UnityEngine.Random.Range(0.1f, 0.3f);
        ordersProductId[i] = id;
        ordersProductNumber[i] = UnityEngine.Random.Range(1, count + 1);
        if (rand > 0.285f)
        {
            ordersValute[i] = ValuteType.gold;
            ordersCost[i] = 10;
        }
        else
        {
            ordersValute[i] = ValuteType.money;
            int randId = UnityEngine.Random.Range(0,2);
            double result = randId == 0 ? ordersProductNumber[i] * (cost * (1 - rand)) :
                ordersProductNumber[i] * (cost * (1 + rand));
            ordersCost[i] = Math.Round((double)result, 1);
        }
    }
    private void InitOrders()
    {
        var allIncome = GetCurrentAllIncome();
        data.userData.fishPaletCost = allIncome / 7;
        data.userData.unpSteakPaletCost = allIncome / 6;
        data.userData.unpFarshPaletCost = allIncome / 5;
        data.userData.unpFilePaletCost = allIncome / 4;
        data.userData.steakPaletCost = allIncome / 3;
        data.userData.farshPaletCost = allIncome / 2;
        data.userData.filePaletCost = allIncome;
        for (int i = 0; i < ordersCount; i++)
        {
            noOrdersPanel.SetActive(false);
            ordersPanel.SetActive(true);            
            if (data.userData.fileInWarehouse > 0)
            {
                SetNewOrder(orderCountInCikl, fileId, data.userData.filePaletCost, data.userData.fileInWarehouse);                
                orderCountInCikl++;
                if(orderCountInCikl  == ordersCount)
                {
                    orderCountInCikl = 0;
                    BtnPressed(0);
                    return;
                }
            }
            if (data.userData.farshInWarehouse > 0)
            {
                SetNewOrder(orderCountInCikl, farshId, data.userData.farshPaletCost, data.userData.farshInWarehouse);                
                orderCountInCikl++;
                if (orderCountInCikl == ordersCount)
                {
                    orderCountInCikl = 0;
                    BtnPressed(0);
                    return;
                }
            }
            if (data.userData.steakInWarehouse > 0)
            {
                SetNewOrder(orderCountInCikl, steakId, data.userData.steakPaletCost, data.userData.steakInWarehouse);                
                orderCountInCikl++;
                if (orderCountInCikl == ordersCount)
                {
                    orderCountInCikl = 0;
                    BtnPressed(0);
                    return;
                }
            }
            if (data.userData.unpFileInWarehouse > 0)
            {
                SetNewOrder(orderCountInCikl, unpackedFileId, data.userData.unpFilePaletCost, data.userData.unpFileInWarehouse);                
                orderCountInCikl++;
                if (orderCountInCikl == ordersCount)
                {
                    orderCountInCikl = 0;
                    BtnPressed(0);
                    return;
                }
            }
            if (data.userData.unpFarshInWarehouse > 0)
            {
                SetNewOrder(orderCountInCikl, unpackedFarshId, data.userData.unpFarshPaletCost, data.userData.unpFarshInWarehouse);                
                orderCountInCikl++;
                if (orderCountInCikl == ordersCount)
                {
                    orderCountInCikl = 0;
                    BtnPressed(0);
                    return;
                }
            }
            if (data.userData.unpSteakInWarehouse > 0)
            {
                SetNewOrder(orderCountInCikl, unpackedSteakId, data.userData.unpSteakPaletCost, data.userData.unpSteakInWarehouse);                
                orderCountInCikl++;
                if (orderCountInCikl == ordersCount)
                {
                    orderCountInCikl = 0;
                    BtnPressed(0);
                    return;
                }
            }
            if ( data.userData.fishInWarehouse > 0)
            {
                SetNewOrder(orderCountInCikl, fishId, data.userData.fishPaletCost, data.userData.fishInWarehouse);                
                orderCountInCikl++;
                if (orderCountInCikl == ordersCount)
                {
                    orderCountInCikl = 0;
                    BtnPressed(0);
                    return;
                }
            }

        }        
        
    }

    private void SetArrow(int id)
    {
        for (int i = 0; i < arrows.Count; i++)
        {
            if (i == id) arrows[i].SetActive(true);
            else arrows[i].SetActive(false);
        }
    }
    private void SetOrder(int id)
    {
        productNumber.text = ordersProductNumber[id].ToString();
        productImg.sprite = productsSprites[ordersProductId[id]];
        if (ordersValute[id] == ValuteType.gold)
        {
            productCost.text = "0,0";
            productCostInGold.text = Converter.instance.ConvertMoneyView(ordersCost[id]);
        }
        else
        {
            productCost.text = Converter.instance.ConvertMoneyView(ordersCost[id]);
            productCostInGold.text = "0";
        }
        productName.text = ordersName[ordersProductId[id]];

    }

    public void BtnPressed(int id)
    {
        SetArrow(id);
        SetOrder(id);
        activBtn = id;
        ButtonsSet();
    }
    public void ButtonsSet()
    {
        if (activBtn == 0)
        {
            buttons[0].interactable = false;
            buttons[1].interactable = true;
        }
        else if(activBtn == 1)
        {
            buttons[0].interactable = true;
            buttons[1].interactable = true;
        }
        else
        {
            buttons[0].interactable = true;
            buttons[1].interactable = false;
        }
        
        
    }

    public void AcceptOrder()
    {
        orderDeliversPanel.SetActive(true);
        ordersPanel.SetActive(false);
        ordersPanelInDelivery.SetActive(true);
        double gold;
        double money;
        if (ordersValute[activBtn] == ValuteType.gold)
        {
            money = 0;
            gold = ordersCost[activBtn];
        }
        else
        {
            money = ordersCost[activBtn];
            gold = 0;
        }
        if (!data.userData.inDeliver)
        {
            data.userData.currentDeliverID = activBtn;
            data.userData.currentProductID = ordersProductId[activBtn];
            data.userData.currentmoney = money;
            data.userData.currentgold = gold;
            data.userData.currentMaxProductCount = ordersProductNumber[activBtn];
        }
        data.userData.inDeliver = true;
        deliveryPanel.SetPanel(productsSprites[data.userData.currentProductID], btnImgs[data.userData.currentDeliverID].sprite,
            data.userData.currentmoney, data.userData.currentgold, data.userData.currentMaxProductCount);
    }

    public void ShowADOrderIncomex2()
    {
        admobRewardedSystem.GetPlacement(RewardedBtns.orderIncomex2);
        admobRewardedSystem.ShowRewardAd();
    }

    public void AcceptForADOrder()
    {
        acceptForAD = true;        
    }
    private void AcceptAD()
    {
        if (!acceptForAD) return;
        orderDeliversPanel.SetActive(true);
        ordersPanel.SetActive(false);
        ordersPanelInDelivery.SetActive(true);
        double gold;
        double money;
        if (ordersValute[activBtn] == ValuteType.gold)
        {
            money = 0;
            ordersCost[activBtn] *= 2;
            gold = ordersCost[activBtn];
        }
        else
        {
            ordersCost[activBtn] *= 2;
            money = ordersCost[activBtn];
            gold = 0;
        }
        if (!data.userData.inDeliver)
        {
            data.userData.currentDeliverID = activBtn;
            data.userData.currentProductID = ordersProductId[activBtn];
            data.userData.currentmoney = money;
            data.userData.currentgold = gold;
            data.userData.currentMaxProductCount = ordersProductNumber[activBtn];
        }
        data.userData.inDeliver = true;
        deliveryPanel.SetPanel(productsSprites[data.userData.currentProductID], btnImgs[data.userData.currentDeliverID].sprite,
            data.userData.currentmoney, data.userData.currentgold, data.userData.currentMaxProductCount);
    }
    public void RightArrowBtnPressed()
    {
        activBtn++;
        BtnPressed(activBtn);
    }
    public void LeftArrowBtnPressed()
    {
        activBtn--;
        BtnPressed(activBtn);
    }
    public void GetInfo()
    {
        if (data.userData.inDeliver)
        {
            SetPanelInDelivery();
            return;
        }
        if (data.userData.inWaiting)
        {
            SetWaitingPanel();
            return;
        }
        SetArrow(0);
        SetTopPanel();
        if (data.userData.inDeliver)
        {
            ordersPanel.SetActive(false);
            orderDeliversPanel.SetActive(true);
            ordersPanelInDelivery.SetActive(true);
        }
        else
        {
            ordersPanel.SetActive(true);
            orderDeliversPanel.SetActive(false);
            ordersPanelInDelivery.SetActive(false);
        }
        InitOrders();
        SetOrder(0);
    }
    private void SetTopPanel()
    {
        GetCurrentWarehouse();        
        var allBoxesCount = data.userData.fishInWarehouse + data.userData.unpSteakInWarehouse + data.userData.steakInWarehouse+
            data.userData.farshInWarehouse+ data.userData.unpFarshInWarehouse+ data.userData.unpFileInWarehouse+ data.userData.fileInWarehouse;
        allBoxCount.text = allBoxesCount.ToString();
        var currentCapacity = allBoxesCount * 100 / currentDepo.boxes.Count;
        capacity.text = currentCapacity.ToString()+"%";
        if (data.userData.fishInWarehouse > 0)
        {
            fishGO.SetActive(true);
            fishBoxCount.text = data.userData.fishInWarehouse.ToString();
        }
        else
        {
            fishGO.SetActive(false);
        }
        if (data.userData.unpSteakInWarehouse > 0)
        {
            unpSteakGO.SetActive(true);
            unpSteakBoxCount.text = data.userData.unpSteakInWarehouse.ToString();
        }
        else
        {
            unpSteakGO.SetActive(false);
        }
        if (data.userData.steakInWarehouse > 0)
        {
            steakGO.SetActive(true);
            steakBoxCount.text = data.userData.steakInWarehouse.ToString();
        }
        else
        {
            steakGO.SetActive(false);
        }
        if (data.userData.unpFarshInWarehouse > 0)
        {
            unpFarshGO.SetActive(true);
            unpFarshBoxCount.text = data.userData.unpFarshInWarehouse.ToString();
        }
        else
        {
            unpFarshGO.SetActive(false);
        }
        if (data.userData.farshInWarehouse > 0)
        {
            farshGO.SetActive(true);
            farshBoxCount.text = data.userData.farshInWarehouse.ToString();
        }
        else
        {
            farshGO.SetActive(false);
        }
        if (data.userData.unpFileInWarehouse > 0)
        {
            unpFileGO.SetActive(true);
            unpFileBoxCount.text = data.userData.unpFileInWarehouse.ToString();
        }
        else
        {
            unpFileGO.SetActive(false);
        }
        if (data.userData.fileInWarehouse > 0)
        {
            fileGO.SetActive(true);
            fileBoxCount.text = data.userData.fileInWarehouse.ToString();
        }
        else
        {
            fileGO.SetActive(false);
        }
    }
    
    private void SetPanelInDelivery()
    {
        SetTopPanel();
        AcceptOrder();
    }
    
    private void GetCurrentWarehouse()
    {
        if (data.userData.warehaus_updated == 0)
        {
            currentDepo = startOutDepo;
        }
        else if (data.userData.warehaus_updated == 1)
        {
            currentDepo = outDepo_1;
        }
        else if (data.userData.warehaus_updated == 2)
        {
            currentDepo = outDepo_2;
        }
        else if (data.userData.warehaus_updated == 3)
        {
            currentDepo = outDepo_3;
        }
        else
        {
            currentDepo = startOutDepo;
        }
    }
    public void FishAdd()
    {
        data.userData.fishInWarehouse--;
        data.userData.currentProductCount++;
        deliveryPanel.ShowBar(data.userData.currentProductCount);
    }
    public void UnpackedStakeAdd()
    {
        data.userData.unpSteakInWarehouse--;
        data.userData.currentProductCount++;
        deliveryPanel.ShowBar(data.userData.currentProductCount);
    }
    public void StakeAdd()
    {
        data.userData.steakInWarehouse--;
        data.userData.currentProductCount++;
        deliveryPanel.ShowBar(data.userData.currentProductCount);
    }
    public void UnpackedFarshAdd()
    {
        data.userData.unpFarshInWarehouse--;
        data.userData.currentProductCount++;
        deliveryPanel.ShowBar(data.userData.currentProductCount);
    }
    public void FarshAdd()
    {
        data.userData.farshInWarehouse--;
        data.userData.currentProductCount++;
        deliveryPanel.ShowBar(data.userData.currentProductCount);
    }
    public void UnpackedFileAdd()
    {
        data.userData.unpFileInWarehouse--;
        data.userData.currentProductCount++;
        deliveryPanel.ShowBar(data.userData.currentProductCount);
    }
    public void FileAdd()
    {
        data.userData.fileInWarehouse--;
        data.userData.currentProductCount++;
        deliveryPanel.ShowBar(data.userData.currentProductCount);
    }
    public void CloseOrder()
    {
        data.userData.coins.Value += data.userData.currentmoney;
        SaveDataSystem.Instance.SaveMoney();
        data.userData._coins.Value = Converter.instance.ConvertMoneyView(data.userData.coins.Value);
        data.userData.gold.Value += Convert.ToInt32(data.userData.currentgold);
        SaveDataSystem.Instance.SaveGold();
        data.userData.inDeliver = false;
        SetWaitingPanel();
        bigCar.Go();
        barier.Open();
    }
    public void ResetCurrentOrder()
    {
        data.userData.inDeliver = false;
        data.userData.currentMaxProductCount = 0;
        data.userData.currentProductCount = 0;        
        SetWaitingPanel();
    }

    public void ResetTimerForCrystalls(int count)
    {
        if(data.userData.cristalls.Value >= count)
        {
            data.userData.cristalls.Value -= count;
            ResetTimerForAD();
        }
    }
    public void ShowAdForResetTime()
    {
        admobRewardedSystem.GetPlacement(RewardedBtns.dontWaitNewOrders);
        admobRewardedSystem.ShowRewardAd();
    }
    public void ResetTimerForAD()
    {
        timer = 0;
    }

    public void RefuseAllOrders()
    {
        SetWaitingPanel();
    }
    private void SetWaitingPanel()
    {
        data.userData.inWaiting = true;
        deliveryPanel.ResetDeliverMode();
        ordersPanel.SetActive(false);
        orderDeliversPanel.SetActive(false);
        ordersPanelInDelivery.SetActive(false);
        orderPanelInTimer.SetActive(true);
        SetTopPanel();
        StartCoroutine(nameof(TimerOrder));
    }
    
    private IEnumerator TimerOrder()
    {
        startTimer = true;
        while (timer > 0)
        {
            timerCount.text = timer.ToString("F0");
            yield return new WaitForSeconds(1);
        }
        startTimer = false;
        timer = 120f;
        ordersPanel.gameObject.SetActive(true);
        orderPanelInTimer.SetActive(false);
        data.userData.inWaiting = false;
        data.userData.currentProductCount = 0;
        GetInfo();
        yield break;
    }
    private void Update()
    {
        if (startTimer)
        {
            timer -= Time.deltaTime;
        }
        if (acceptForAD)
        {
            AcceptAD();
            acceptForAD = false;
        }
    }    
} 
