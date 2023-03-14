using UnityEngine;
using System.Collections.Generic;

public class MachineControlSystem : BaseMonoSystem
{
    [SerializeField] private AdjustEventsSystem adjustEvents;
    [SerializeField] private GameObject headCutingMashine1;
    [SerializeField] private GameObject headCutingMashine2;
    [SerializeField] private List<GameObject> fishCleaningMashines;
    [SerializeField] private GameObject steakMashine;
    [SerializeField] private GameObject farshMashine;
    [SerializeField] private GameObject fileMashine;
    [SerializeField] private List<GameObject> packingMashines;
    [SerializeField] private Transform cameraPort;
    [SerializeField] private Transform cameraWork;
    [SerializeField] private Transform cameraMain;
    [SerializeField] private MoneySystem moneySystem;
    [SerializeField] private FueSystem fueSystem;
    [SerializeField] private GameObject moneyBlockerWindow;
    [SerializeField] private GameObject levelBlockerWindow;
    
    private float hiLevel = 25f;
    private int noMashine = 0;
    private int firstMashine = 1;
    private int secondMashine = 2;

    public override void Init(AppData data)
    {
        base.Init(data);        
    }

    
    public void StartInit()
    {
        if (data.userData.headCutingMashine == noMashine)
        {
            data.userData.headCutingMashine = firstMashine;
        }           
        headCutingMashine2.SetActive(data.userData.headCutingMashine >= secondMashine);        
        for(int i = 0; i < fishCleaningMashines.Count; i++)
        {
            fishCleaningMashines[i].SetActive(i < data.userData.fishCleaningMashine);
        }
        if (data.userData.steakMashine > noMashine)
        {
            steakMashine.SetActive(true);
        }
        if (data.userData.farshMashine > noMashine)
        {
            farshMashine.SetActive(true);
        }
        if (data.userData.fileMashine > noMashine)
        {
            fileMashine.SetActive(true);
        }
        if (data.userData.packingMashine > noMashine)
        {
            packingMashines[0].SetActive(true);
        }
        if (data.userData.packingMashine > firstMashine)
        {
            packingMashines[0].SetActive(true);
            packingMashines[1].SetActive(true);
        }
        if (data.userData.packingMashine > secondMashine)
        {
            packingMashines[0].SetActive(true);
            packingMashines[1].SetActive(true);
            packingMashines[2].SetActive(true);
        }


    }

    public void HireHeadCutingMashine()
    {
        if (data.userData.headCutingMashine == firstMashine && data.userData.fishCleaningMashine3Level>=hiLevel)
        {
            if (moneySystem.Buy(data.upgradesData.headCutingMashineCost))
            {
                //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.headCutingMashineCost,
                   // "Buy", "BuyCarvingMachine_2");
                //var softPurchaseParameters = new Dictionary<string, object>();
                //softPurchaseParameters["mygame_purchased_item"] = "BuyCarvingMachine_2";
                //FB.LogAppEvent(AppEventName.SpentCredits,(float)data.upgradesData.headCutingMashineCost, softPurchaseParameters);
                headCutingMashine2.SetActive(true);
                data.userData.headCutingMashine = secondMashine;
                SaveDataSystem.Instance.SaveCarvingMachineCount();
                adjustEvents.AdustEventForProgres("Carving Machine_2");
                data.userData.levelUser = "Carving Machine_2";
                SaveDataSystem.Instance.SaveUserLevel();
                return;
            }
            else
            {
                moneyBlockerWindow.SetActive(true);                
            }
        }
        else
        {
            levelBlockerWindow.SetActive(true);            
        }
    }
    public void HireFishCleaningMashine(int index)
    {
        if (data.userData.fishCleaningMashine == noMashine)
        {
            if(data.userData.headCutingMashineLevel >= hiLevel)
            {
                if (moneySystem.Buy(data.upgradesData.fishCleaning1MashineCost))
                {
                    //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.fishCleaning1MashineCost,
                    //"Buy", "BuySlicingMachine_1");
                    //var softPurchaseParameters = new Dictionary<string, object>();
                    //softPurchaseParameters["mygame_purchased_item"] = "BuySlicingMachine_1";
                    //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.fishCleaning1MashineCost, softPurchaseParameters);
                    fishCleaningMashines[data.userData.fishCleaningMashine].SetActive(true);
                    data.userData.fishCleaningMashine++;
                    SaveDataSystem.Instance.SaveScalingMachineCount();
                    adjustEvents.AdustEventForProgres("Scaling Machine_1");
                    data.userData.levelUser = "Scaling Machine_1";
                    SaveDataSystem.Instance.SaveUserLevel();
                    return;
                }
                else
                {
                    moneyBlockerWindow.SetActive(true);
                }
            }
            else
            {
                levelBlockerWindow.SetActive(true);                
            }

        }

        if (data.userData.fishCleaningMashine == firstMashine)
        {
            if (data.userData.fishCleaningMashineLevel >= hiLevel)
            {
                if (moneySystem.Buy(data.upgradesData.fishCleaning2MashineCost))
                {
                    //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.fishCleaning2MashineCost,
                    //"Buy", "BuySlicingMachine_2");
                    //var softPurchaseParameters = new Dictionary<string, object>();
                    //softPurchaseParameters["mygame_purchased_item"] = "BuySlicingMachine_2";
                    //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.fishCleaning2MashineCost, softPurchaseParameters);
                    fishCleaningMashines[data.userData.fishCleaningMashine].SetActive(true);
                    data.userData.fishCleaningMashine++;
                    SaveDataSystem.Instance.SaveScalingMachineCount();
                    adjustEvents.AdustEventForProgres("Scaling Machine_2");
                    data.userData.levelUser = "Scaling Machine_2";
                    SaveDataSystem.Instance.SaveUserLevel();
                    return;
                }
                else
                {
                    moneyBlockerWindow.SetActive(true);
                }
            }
            else
            {
                levelBlockerWindow.SetActive(true);                
            }

        }

        if (data.userData.fishCleaningMashine == secondMashine)
        {
            if (data.userData.fishCleaningMashine2Level >= hiLevel)
            {
                if (moneySystem.Buy(data.upgradesData.fishCleaning3MashineCost))
                {
                    //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.fishCleaning3MashineCost,
                    //"Buy", "BuySlicingMachine_3");
                    //var softPurchaseParameters = new Dictionary<string, object>();
                    //softPurchaseParameters["mygame_purchased_item"] = "BuySlicingMachine_3";
                    //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.fishCleaning3MashineCost, softPurchaseParameters);
                    fishCleaningMashines[data.userData.fishCleaningMashine].SetActive(true);
                    data.userData.fishCleaningMashine++;
                    SaveDataSystem.Instance.SaveScalingMachineCount();
                    adjustEvents.AdustEventForProgres("Scaling Machine_3");
                    data.userData.levelUser = "Scaling Machine_3";
                    SaveDataSystem.Instance.SaveUserLevel();
                    return;
                }
                else
                {
                    moneyBlockerWindow.SetActive(true);
                }
            }
            else
            {
                levelBlockerWindow.SetActive(true);                
            }

        }
    }
    public void HireSteakMashine()
    {
        if(data.userData.steakMashine == noMashine && data.userData.headCutingMashine2Level>=hiLevel)
        {
            if (moneySystem.Buy(data.upgradesData.steakMashineCost))
            {
                //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.steakMashineCost,
                    //"Buy", "BuySteakingMachine");
                //var softPurchaseParameters = new Dictionary<string, object>();
                //softPurchaseParameters["mygame_purchased_item"] = "BuySteakingMachine";
               // FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.steakMashineCost, softPurchaseParameters);
                steakMashine.SetActive(true);
                data.userData.steakMashine++;
                SaveDataSystem.Instance.SaveSteakingMachineCount();
                adjustEvents.AdustEventForProgres("Steaking Machine");
                data.userData.levelUser = "Steaking Machine";
                SaveDataSystem.Instance.SaveUserLevel();
                return;
            }
            else
            {
                moneyBlockerWindow.SetActive(true);                
            }
        }
        else
        {
            levelBlockerWindow.SetActive(true);
        }

    }
    public void HireFarshMashine()
    {
        if (data.userData.farshMashine == noMashine && data.userData.steakMashineLevel >= hiLevel)
        {
            if (moneySystem.Buy(data.upgradesData.farshMashineCost))
            {
                //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.farshMashineCost,
                //    "Buy", "BuyMincingMeatMachine");
                //var softPurchaseParameters = new Dictionary<string, object>();
                //softPurchaseParameters["mygame_purchased_item"] = "BuyMincingMeatMachine";
                //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.farshMashineCost, softPurchaseParameters);
                farshMashine.SetActive(true);
                data.userData.farshMashine++;
                SaveDataSystem.Instance.SaveFarshMachineCount();
                adjustEvents.AdustEventForProgres("MincingMeat Machine");
                data.userData.levelUser = "MincingMeat Machine";
                SaveDataSystem.Instance.SaveUserLevel();
                return;
            }
            else
            {
                moneyBlockerWindow.SetActive(true);
            }
        }
        else
        {
            levelBlockerWindow.SetActive(true);
        }

    }
    public void HireFileMashine()
    {
        if (data.userData.fileMashine == noMashine && data.userData.farshMashineLevel >= hiLevel)
        {
            if (moneySystem.Buy(data.upgradesData.fileMashineCost))
            {
                //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.fileMashineCost,
                //    "Buy", "BuyFileMachine");
                //var softPurchaseParameters = new Dictionary<string, object>();
                //softPurchaseParameters["mygame_purchased_item"] = "BuyFileMachine";
                //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.fileMashineCost, softPurchaseParameters);
                fileMashine.SetActive(true);
                data.userData.fileMashine++;
                SaveDataSystem.Instance.SaveFileMachineCount();
                adjustEvents.AdustEventForProgres("Fileting Machine");
                data.userData.levelUser = "Fileting Machine";
                SaveDataSystem.Instance.SaveUserLevel();
                return;
            }
            else
            {
                moneyBlockerWindow.SetActive(true);
            }
        }
        else
        {
            levelBlockerWindow.SetActive(true);
        }

    }
    public void HireEggPackingMashine()
    {
        if (data.userData.packingMashine == noMashine && data.userData.fileMashineLevel>=hiLevel)
        {
            if (moneySystem.Buy(data.upgradesData.steakPackingMashineCost))
            {
                //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.steakPackingMashineCost,
                //    "Buy", "BuyPackingMachine_1");
                //var softPurchaseParameters = new Dictionary<string, object>();
                //softPurchaseParameters["mygame_purchased_item"] = "BuyPackingMachine_1";
                //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.steakPackingMashineCost, softPurchaseParameters);
                packingMashines[data.userData.packingMashine].SetActive(true);
                data.userData.packingMashine++;
                SaveDataSystem.Instance.SavePackingMachineCount();
                adjustEvents.AdustEventForProgres("Packing Machine_1");
                data.userData.levelUser = "Packing Machine_1";
                SaveDataSystem.Instance.SaveUserLevel();
                return;
            }
            else
            {
                moneyBlockerWindow.SetActive(true);                
            }
        }
        else if (data.userData.packingMashine == firstMashine && data.userData.steakPackingMashineLevel >= hiLevel)
        {
            if (moneySystem.Buy(data.upgradesData.farshPackingMashineCost))
            {
                //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.farshPackingMashineCost,
                //    "Buy", "BuyPackingMachine_2");
                //var softPurchaseParameters = new Dictionary<string, object>();
                //softPurchaseParameters["mygame_purchased_item"] = "BuyPackingMachine_2";
                //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.farshPackingMashineCost, softPurchaseParameters);
                packingMashines[data.userData.packingMashine].SetActive(true);
                data.userData.packingMashine++;
                SaveDataSystem.Instance.SavePackingMachineCount();
                adjustEvents.AdustEventForProgres("Packing Machine_2");
                data.userData.levelUser = "Packing Machine_2";
                SaveDataSystem.Instance.SaveUserLevel();
                return;
            }
            else
            {
                moneyBlockerWindow.SetActive(true);
            }
        }
        else if (data.userData.packingMashine == secondMashine && data.userData.farshPackingMashineLevel >= hiLevel)
        {
            if (moneySystem.Buy(data.upgradesData.filePackingMashineCost))
            {
                //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.filePackingMashineCost,
                //    "Buy", "BuyPackingMachine_3");
                //var softPurchaseParameters = new Dictionary<string, object>();
                //softPurchaseParameters["mygame_purchased_item"] = "BuyPackingMachine_3";
                //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.filePackingMashineCost, softPurchaseParameters);
                packingMashines[data.userData.packingMashine].SetActive(true);
                data.userData.packingMashine++;
                SaveDataSystem.Instance.SavePackingMachineCount();
                adjustEvents.AdustEventForProgres("Packing Machine_3");
                data.userData.levelUser = "Packing Machine_3";
                SaveDataSystem.Instance.SaveUserLevel();
                return;
            }
            else
            {
                moneyBlockerWindow.SetActive(true);
            }
        }
        else
        {
            levelBlockerWindow.SetActive(true);
        }
    }
    
}

