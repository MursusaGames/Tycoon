using UnityEngine;
using UniRx;
using System.Collections.Generic;
using System;

public class UpgradeSystem : BaseMonoSystem
{
    [SerializeField] private FueSystem fueSystem;
    [SerializeField] private VisualEffectSystem _effectSystem;
    [SerializeField] private GameObject noMoneyPopUp;
    [SerializeField] private GameObject noGoldPopUp;
    [SerializeField] private BalanceSystem balanceSystem;
    [SerializeField] private CarInsideWar carZone2;
    [SerializeField] private CarShipment carShipment;
    [SerializeField] private CarFinalZone carFinalZone;
    [SerializeField] private CarFinalZone carFinalZone2;
    [SerializeField] private CarHeadCutZone carHeadCutZone;
    [SerializeField] private CarAfterHeadCutZone carLine2;
    [SerializeField] private CarWorkZone carPort;
    [SerializeField] private Ship ship;
    [SerializeField] private CarPort carInWarZone;
    [SerializeField] private CarPort carInWarZone2;
    [SerializeField] private MoneySystem moneySystem;     
    [SerializeField] private int nextMashineOpenLevel = 25;
    [SerializeField] private ChangeWarSizeSystem changeWarSizeSystem;
    
    private List<EquipmentEnabled> equipmentEnableds = new List<EquipmentEnabled>();
    //private float increaseSpeedValue = 0.01f;
    private int shipCapacityChangeFirstPoint = 100;
    private int shipCapacityChangeSecondPoint = 300;
    private int shipCapacityChangeThirdPoint = 500;
    private int shipCapacityChangeFourthPoint = 750;
    private bool init;
    public override void Init(AppData data)
    {
        base.Init(data);
        //SetObservables();
    }

    private void SetObservables()
    {
        data.matchData.state
            .Where(x => x == MatchData.State.WorcersTime)
            .Subscribe(_ => StartInit());
    }
    private void StartInit()
    {
        equipmentEnableds.AddRange(FindObjectsOfType<EquipmentEnabled>());
        
    }
    public void InitMultiplier()
    {
        init = true;
        SetMultiplierHeadCut_1(data.userData.headCutingMashineLevel);
        SetMultiplierHeadCut_2(data.userData.headCutingMashine2Level);
        SetMultiplierFishClean_1(data.userData.fishCleaningMashineLevel);
        SetMultiplierFishClean_2(data.userData.fishCleaningMashine2Level);
        SetMultiplierFishClean_3(data.userData.fishCleaningMashine3Level);
        SetMultiplierSteakMashine(data.userData.steakMashineLevel);
        SetMultiplierFarshMashine(data.userData.farshMashineLevel);
        SetMultiplierFileMashine(data.userData.fileMashineLevel);
        SetMultiplierSteakPackingMashine(data.userData.steakPackingMashineLevel);
        SetMultiplierFarshPackingMashine(data.userData.farshPackingMashineLevel);
        SetMultiplierFilePackingMashine(data.userData.filePackingMashineLevel);
        init = false;
    }

    public void UpgradeAllSpeeds()
    {
        UpgradeShipSpeed();
        UpgradePortZoneCarSpeed();
        UpgradeFinishZoneCarSpeed();
        UpgradeZoneInWarSpeed();
        UpgradeZone1CarSpeed();
        UpgradeZone2CarSpeed();
        UpgradeZone3CarSpeed();
        UpgradeZoneOutWarSpeed();
    }
    public void UpgradeWarehouseOut()
    {
        if (data.userData.warehaus_updated==3) return;
        if (data.userData.gold.Value >= data.upgradesData.war_1_upgradeCost)
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Gold", (float)data.upgradesData.war_1_upgradeCost,
            //        "Upgrade", "UpgradeOutWar");
            //var softPurchaseParameters = new Dictionary<string, object>();
            //softPurchaseParameters["mygame_purchased_item"] = "UpgradeOutWar";
            //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.war_1_upgradeCost, softPurchaseParameters);
            data.userData.gold.Value -= Convert.ToInt32(data.upgradesData.war_1_upgradeCost);
            SaveDataSystem.Instance.SaveGold();
            data.userData.warehaus_updated ++;
            SaveDataSystem.Instance.SaveStockCapasity();
            changeWarSizeSystem.ChangeOutWarSize();
        }
        else
        {
            noGoldPopUp.SetActive(true);   
        }
    }

    public void UpgradeShip()
    {
        if (data.userData.shipLevel == Constant.maxSpeed) return;
        if (moneySystem.Buy(data.upgradesData.shipUpgradeCost))
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.shipUpgradeCost,
            //        "Upgrade", "UpgradeShipLevel");
            //var softPurchaseParameters = new Dictionary<string, object>();
            //softPurchaseParameters["mygame_purchased_item"] = "UpgradeShipLevel";
            //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.shipUpgradeCost, softPurchaseParameters);
            data.userData.shipLevel++;
            SaveDataSystem.Instance.SaveShipLevel();
            balanceSystem.Set_Ship_UpgradeCost();
            if (data.userData.shipLevel == shipCapacityChangeFirstPoint||
                data.userData.shipLevel == shipCapacityChangeSecondPoint ||
                data.userData.shipLevel == shipCapacityChangeThirdPoint ||
                data.userData.shipLevel == shipCapacityChangeFourthPoint) 
            {
                data.userData.shipCapasity++;
            }
            UpgradeShipSpeed();
        }
        else
        {
            noMoneyPopUp.SetActive(true);
        }
    }
    public void UpgradeShipSpeed()
    {
        if (data.userData.shipSpeed == Constant.maxSpeed) return;        
        ship.ChangeSpeed();
    }
    
    public void UpgradePortZoneCar()
    {
        if (data.userData.carPortLevel == Constant.maxSpeed) return;
        if (moneySystem.Buy(data.upgradesData.carPortUpgradeCost))
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.carPortUpgradeCost,
            //        "Upgrade", "UpgradeCarPortLevel");
            //var softPurchaseParameters = new Dictionary<string, object>();
            //softPurchaseParameters["mygame_purchased_item"] = "UpgradeCarPortLevel";
            //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.carPortUpgradeCost, softPurchaseParameters);
            data.userData.carPortLevel++;
            SaveDataSystem.Instance.SaveCarPortLevel();
            balanceSystem.SetCarInPort_UpgradeCost();
            UpgradePortZoneCarSpeed();
        }
        else
        {
            noMoneyPopUp.SetActive(true);
        }
    }
    public void UpgradePortZoneCarSpeed()
    {
        if (data.userData.carPortSpeed == Constant.maxSpeed) return;        
        carPort.ChangeSpeed();
    }
    public void UpgradeFinishZoneCar()
    {
        if (data.userData.finishZoneCarLevel == Constant.maxSpeed) return;
        if (moneySystem.Buy(data.upgradesData.finishZoneCarUpgradeCost))
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.finishZoneCarUpgradeCost,
            //        "Upgrade", "UpgradeFinishCarLevel");
            //var softPurchaseParameters = new Dictionary<string, object>();
            //softPurchaseParameters["mygame_purchased_item"] = "UpgradeFinishCarLevel";
            //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.finishZoneCarUpgradeCost, softPurchaseParameters);
            data.userData.finishZoneCarLevel++;
            SaveDataSystem.Instance.SaveCarFinishZoneLevel();
            balanceSystem.Set_FinishZoneCar_UpgradeCost();
            UpgradeFinishZoneCarSpeed();
        }
        else
        {
            noMoneyPopUp.SetActive(true);
        }
    }
    public void UpgradeFinishZoneCarSpeed()
    {
        if (data.userData.finishZoneCarSpeed == Constant.maxSpeed) return;        
        carShipment.ChangeSpeed();
    }
    public void UpgradeZoneInWar()
    {
        if (data.userData.zoneInWarLevel == Constant.maxSpeed) return;
        if (moneySystem.Buy(data.upgradesData.zoneInWarUpgradeCost))
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.zoneInWarUpgradeCost,
            //        "Upgrade", "UpgradeInWarCarLevel");
            //var softPurchaseParameters = new Dictionary<string, object>();
            //softPurchaseParameters["mygame_purchased_item"] = "UpgradeInWarCarLevel";
            //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.zoneInWarUpgradeCost, softPurchaseParameters);
            data.userData.zoneInWarLevel++;
            SaveDataSystem.Instance.SaveInWarLevel();
            balanceSystem.SetCarInWar_UpgradeCost();
            UpgradeZoneInWarSpeed();
        }
        else
        {
            noMoneyPopUp.SetActive(true);
        }
    }
    public void UpgradeZoneInWarSpeed()
    {
        if (data.userData.zoneInWarSpeed == Constant.maxSpeed) return;        
        carInWarZone.ChangeSpeed();
        if (carInWarZone2.gameObject.activeInHierarchy)
        {
            carInWarZone2.ChangeSpeed();
        }
    }
    public void UpgradeZoneOutWar()
    {
        if (data.userData.zoneOutWarLevel == Constant.maxSpeed) return;
        if (moneySystem.Buy(data.upgradesData.carOutUpgradeCost))
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.carOutUpgradeCost,
            //        "Upgrade", "UpgradeCarOutLevel");
            //var softPurchaseParameters = new Dictionary<string, object>();
            //softPurchaseParameters["mygame_purchased_item"] = "UpgradeOutWarCarLevel";
            //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.carOutUpgradeCost, softPurchaseParameters);
            data.userData.zoneOutWarLevel++;
            SaveDataSystem.Instance.SaveOutWarLevel();
            balanceSystem.Set_OutCar_UpgradeCost();
            UpgradeZoneOutWarSpeed();
        }
        else
        {
            noMoneyPopUp.SetActive(true);
        }
    }
    public void UpgradeZoneOutWarSpeed()
    {
        if (data.userData.zoneOutWarSpeed == Constant.maxSpeed) return;        
        carFinalZone.ChangeSpeed();
        if (carFinalZone2.gameObject.activeInHierarchy)
        {
            carFinalZone2.ChangeSpeed();
        }
    }
    public void UpgradeZone1Car()
    {
        if (data.userData.zone1CarLevel == Constant.maxSpeed) return;
        if (moneySystem.Buy(data.upgradesData.zone1CarUpgradeCost))
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.zone1CarUpgradeCost,
            //        "Upgrade", "UpgradeZone1CarLevel");
            //var softPurchaseParameters = new Dictionary<string, object>();
            //softPurchaseParameters["mygame_purchased_item"] = "UpgradeZone1CarLevel";
            //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.zone1CarUpgradeCost, softPurchaseParameters);
            data.userData.zone1CarLevel++;
            SaveDataSystem.Instance.SaveCar1Level();
            balanceSystem.SetZone_1Car_UpgradeCost();
            UpgradeZone1CarSpeed();
        }
        else
        {
            noMoneyPopUp.SetActive(true);
        }
    }
    public void UpgradeZone1CarSpeed()
    {
        if (data.userData.zone1CarSpeed == Constant.maxSpeed) return;        
        carHeadCutZone.ChangeSpeed();
    }
    public void UpgradeZone2Car()
    {
        if (data.userData.zone2CarLevel == Constant.maxSpeed) return;
        if (moneySystem.Buy(data.upgradesData.zone2CarUpgradeCost))
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.zone2CarUpgradeCost,
            //        "Upgrade", "UpgradeZone2CarLevel");
            //var softPurchaseParameters = new Dictionary<string, object>();
            //softPurchaseParameters["mygame_purchased_item"] = "UpgradeZone2CarLevel";
            //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.zone2CarUpgradeCost, softPurchaseParameters);
            data.userData.zone2CarLevel++;
            SaveDataSystem.Instance.SaveCar2Level();
            balanceSystem.SetZone_2Car_UpgradeCost();
            UpgradeZone2CarSpeed();
        }
        else
        {
            noMoneyPopUp.SetActive(true);
        }
    }
    public void UpgradeZone2CarSpeed()
    {
        if (data.userData.zone2CarSpeed == Constant.maxSpeed) return;        
        carZone2.ChangeSpeed();
    }
    public void UpgradeZone3Car()
    {
        if (data.userData.zone3CarLevel == Constant.maxSpeed) return;
        if (moneySystem.Buy(data.upgradesData.zone3CarUpgradeCost))
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.zone3CarUpgradeCost,
            //        "Upgrade", "UpgradeZone3CarLevel");
            //var softPurchaseParameters = new Dictionary<string, object>();
            //softPurchaseParameters["mygame_purchased_item"] = "UpgradeZone3CarLevel";
            //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.zone3CarUpgradeCost, softPurchaseParameters);
            data.userData.zone3CarLevel++;
            SaveDataSystem.Instance.SaveCar3Level();
            balanceSystem.SetZone_3Car_UpgradeCost();
            UpgradeZone3CarSpeed();
        }
        else
        {
            noMoneyPopUp.SetActive(true);
        }
    }
    public void UpgradeZone3CarSpeed()
    {
        if (data.userData.zone3CarSpeed == Constant.maxSpeed) return;        
        carLine2.ChangeSpeed();
    }
    public void UpgradeHeadCutingMashineLevel()
    {
        if (data.userData.headCutingMashineLevel >= Constant.maxLevel)
        {
            data.userData.headCutingMashineLevel = Constant.maxLevel;
            return;
        }
            
        if (moneySystem.Buy(data.upgradesData.headCutMashine_1_UpgradeCost.Value))
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.headCutMashine_1_UpgradeCost.Value,
            //        "Upgrade", "UpgradeCarvingMachine1Level");
            //var softPurchaseParameters = new Dictionary<string, object>();
            //softPurchaseParameters["mygame_purchased_item"] = "UpgradeCarvingMachine1Level";
            //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.headCutMashine_1_UpgradeCost.Value, softPurchaseParameters);
            data.userData.headCutingMashineLevel++;            
            _effectSystem.ShowHeadCut1Particle();
            SetMultiplierHeadCut_1(data.userData.headCutingMashineLevel);
            balanceSystem.SetHeadCut_1_Incomes();            
            if (data.userData.headCutingMashineLevel == nextMashineOpenLevel)
            {
                StartInit();
                foreach(var equipment in equipmentEnableds)
                {
                    if (equipment.headCut_1)
                    {
                        equipment.EnabledNextZone();
                    }
                }
                if (data.matchData.isFue) fueSystem.ElevnStepBtn();
            }
        }
        else
        {
            noMoneyPopUp.SetActive(true);
        }
    }
    private void SetMultiplierHeadCut_1(int level)
    {
        if (level % nextMashineOpenLevel == 0 && level != 0 || init)
        {
            if (level < 25)
            {
                data.userData.headCut_1_multiplier.Value = 1f;
                data.userData.headCut_1_multiplierCoef.Value = 2f;//1
                return;
            }
            if (level >= 25 && level < 50)
            {
                data.userData.headCut_1_multiplier.Value = 2f;
                data.userData.headCut_1_multiplierCoef.Value = 1.5f;//2
                return;
            }
            if (level >= 50 && level < 75)
            {
                data.userData.headCut_1_multiplier.Value = 3f;
                data.userData.headCut_1_multiplierCoef.Value = 1.5f;//3
                return;
            }
            if (level >= 75 && level < 100)
            {
                data.userData.headCut_1_multiplier.Value = 4.5f;
                data.userData.headCut_1_multiplierCoef.Value = 2f;//4.5
                return;
            }
            if (level >= 100 && level < 125)
            {
                data.userData.headCut_1_multiplier.Value = 9f;
                data.userData.headCut_1_multiplierCoef.Value = 1f;//9
                return;
            }
            if (level >= 125 && level < 150)
            {
                data.userData.headCut_1_multiplier.Value = 9f;
                data.userData.headCut_1_multiplierCoef.Value = 2f;//9
                return;
            }

            if (level >= 150 && level < 175)
            {
                data.userData.headCut_1_multiplier.Value = 18;
                data.userData.headCut_1_multiplierCoef.Value = 1f;//18
                return;
            }
            if (level >= 175 && level < 200)
            {
                data.userData.headCut_1_multiplier.Value = 18;
                data.userData.headCut_1_multiplierCoef.Value = 3f;//18
                return;
            }
            if (level >= 200 && level < 225)
            {
                data.userData.headCut_1_multiplier.Value = 54;
                data.userData.headCut_1_multiplierCoef.Value = 1f;//54
                return;
            }
            if (level >= 225 && level < 250)
            {
                data.userData.headCut_1_multiplier.Value = 54;
                data.userData.headCut_1_multiplierCoef.Value = 3f;//54
                return;
            }
            if (level >= 250 && level < 275)
            {
                data.userData.headCut_1_multiplier.Value = 162;
                data.userData.headCut_1_multiplierCoef.Value = 1f;//162
                return;
            }
            if (level >= 275 && level < 300)
            {
                data.userData.headCut_1_multiplier.Value = 162;
                data.userData.headCut_1_multiplierCoef.Value = 3f;//162
                return;
            }
            if (level >= 300 && level < 325)
            {
                data.userData.headCut_1_multiplier.Value = 486;
                data.userData.headCut_1_multiplierCoef.Value = 1f;//486
                return;
            }
            if (level >= 325 && level < 350)
            {
                data.userData.headCut_1_multiplier.Value = 486;
                data.userData.headCut_1_multiplierCoef.Value = 3f;//486
                return;
            }
            if (level >= 350 && level < 375)
            {
                data.userData.headCut_1_multiplier.Value = 972;
                data.userData.headCut_1_multiplierCoef.Value = 1f;//972
                return;
            }
            if (level >= 375 && level < 400)
            {
                data.userData.headCut_1_multiplier.Value = 972;
                data.userData.headCut_1_multiplierCoef.Value = 3f;//972
                return;
            }
            if (level >= 400 && level < 425)
            {
                data.userData.headCut_1_multiplier.Value = 2916;
                data.userData.headCut_1_multiplierCoef.Value = 1f;//2916
                return;
            }
            if (level >= 425 && level < 450)
            {
                data.userData.headCut_1_multiplier.Value = 2916;
                data.userData.headCut_1_multiplierCoef.Value = 2f;//2916
                return;
            }
            if (level >= 450 && level < 475)
            {
                data.userData.headCut_1_multiplier.Value = 5832;
                data.userData.headCut_1_multiplierCoef.Value = 1f;//5832
                return;
            }
            if (level >= 475 && level < 500)
            {
                data.userData.headCut_1_multiplier.Value = 5832;
                data.userData.headCut_1_multiplierCoef.Value = 2f;//5832
                return;
            }
            if (level >= 500 && level < 525)
            {
                data.userData.headCut_1_multiplier.Value = 11664;
                data.userData.headCut_1_multiplierCoef.Value = 1f;//11664
                return;
            }
            if (level >= 525 && level < 550)
            {
                data.userData.headCut_1_multiplier.Value = 11664;
                data.userData.headCut_1_multiplierCoef.Value = 2f;//11664
                return;
            }
            if (level >= 550 && level < 575)
            {
                data.userData.headCut_1_multiplier.Value = 23328;
                data.userData.headCut_1_multiplierCoef.Value = 1f;//23328
                return;
            }
            if (level >= 575 && level < 600)
            {
                data.userData.headCut_1_multiplier.Value = 23328;
                data.userData.headCut_1_multiplierCoef.Value = 2f;//23328
                return;
            }
            if (level >= 600 && level < 625)
            {
                data.userData.headCut_1_multiplier.Value = 46656;
                data.userData.headCut_1_multiplierCoef.Value = 1f;//46656
                return;
            }
            if (level >= 625 && level < 650)
            {
                data.userData.headCut_1_multiplier.Value = 46656;
                data.userData.headCut_1_multiplierCoef.Value = 2f;//46656
                return;
            }
            if (level >= 650 && level < 675)
            {
                data.userData.headCut_1_multiplier.Value = 93312;
                data.userData.headCut_1_multiplierCoef.Value = 1f;//93312
                return;
            }
            if (level >= 675 && level < 700)
            {
                data.userData.headCut_1_multiplier.Value = 93312;
                data.userData.headCut_1_multiplierCoef.Value = 2f;//93312
                return;
            }
            if (level >= 700 && level < 725)
            {
                data.userData.headCut_1_multiplier.Value = 186624;
                data.userData.headCut_1_multiplierCoef.Value = 1f;//186624
                return;
            }
            if (level >= 725 && level < 750)
            {
                data.userData.headCut_1_multiplier.Value = 186624;
                data.userData.headCut_1_multiplierCoef.Value = 2f;//186624
                return;
            }
            if (level >= 750 && level < 775)
            {
                data.userData.headCut_1_multiplier.Value = 373248;
                data.userData.headCut_1_multiplierCoef.Value = 1f;//373248
                return;
            }
            if (level >= 775 && level < 800)
            {
                data.userData.headCut_1_multiplier.Value = 373248;
                data.userData.headCut_1_multiplierCoef.Value = 2f;//373248
                return;
            }
            if (level >= 800 && level < 825)
            {
                data.userData.headCut_1_multiplier.Value = 746496;
                data.userData.headCut_1_multiplierCoef.Value = 1f;//746496
                return;
            }
            if (level >= 825 && level < 850)
            {
                data.userData.headCut_1_multiplier.Value = 746496;
                data.userData.headCut_1_multiplierCoef.Value = 2f;//746496
                return;
            }
            if (level >= 850 && level < 875)
            {
                data.userData.headCut_1_multiplier.Value = 1492992;
                data.userData.headCut_1_multiplierCoef.Value = 1f;//1492992
                return;
            }
            if (level >= 875 && level < 900)
            {
                data.userData.headCut_1_multiplier.Value = 1492992;
                data.userData.headCut_1_multiplierCoef.Value = 2f;//1492992
                return;
            }
            if (level >= 900 && level < 925)
            {
                data.userData.headCut_1_multiplier.Value = 2985984;
                data.userData.headCut_1_multiplierCoef.Value = 1f;//2985984
                return;
            }
            if (level >= 925 && level < 950)
            {
                data.userData.headCut_1_multiplier.Value = 2985984;
                data.userData.headCut_1_multiplierCoef.Value = 2f;//2985984
                return;
            }
            if (level >= 950 && level < 975)
            {
                data.userData.headCut_1_multiplier.Value = 5971968;
                data.userData.headCut_1_multiplierCoef.Value = 1f;//5971968
                return;
            }
            if (level >= 975 && level < 1000)
            {
                data.userData.headCut_1_multiplier.Value = 5971968;
                data.userData.headCut_1_multiplierCoef.Value = 3f;//5971968
                return;
            }
            if (level >= 1000)
            {
                data.userData.headCut_1_multiplier.Value = 17915904;
                data.userData.headCut_1_multiplierCoef.Value = 1f;//17915904
                return;
            }
        }
    }
    private void SetMultiplierHeadCut_2(int level)
    {
        if (level % nextMashineOpenLevel == 0 && level != 0|| init)
        {
            if (level < 25)
            {
                data.userData.headCut_2_multiplier.Value = 1f;
                data.userData.headCut_2_multiplierCoef.Value = 2f;//2
                return;
            }
            if (level >= 25 && level < 50)
            {
                data.userData.headCut_2_multiplier.Value = 2f;
                data.userData.headCut_2_multiplierCoef.Value = 1.5f;//2
                return;
            }
            if (level >= 50 && level < 75)
            {
                data.userData.headCut_2_multiplier.Value = 3f;
                data.userData.headCut_2_multiplierCoef.Value = 1.5f;//3
                return;
            }
            if (level >= 75 && level < 100)
            {
                data.userData.headCut_2_multiplier.Value = 4.5f;
                data.userData.headCut_2_multiplierCoef.Value = 2f;//4.5
                return;
            }
            if (level >= 100 && level < 125)
            {
                data.userData.headCut_2_multiplier.Value = 9;
                data.userData.headCut_2_multiplierCoef.Value = 1f;//9
                return;
            }
            if (level >= 125 && level < 150)
            {
                data.userData.headCut_2_multiplier.Value = 9;
                data.userData.headCut_2_multiplierCoef.Value = 2f;//9
                return;
            }

            if (level >= 150 && level < 175)
            {
                data.userData.headCut_2_multiplier.Value = 18;
                data.userData.headCut_2_multiplierCoef.Value = 1f;//18
                return;
            }
            if (level >= 175 && level < 200)
            {
                data.userData.headCut_2_multiplier.Value = 18;
                data.userData.headCut_2_multiplierCoef.Value = 3f;//18
                return;
            }
            if (level >= 200 && level < 225)
            {
                data.userData.headCut_2_multiplier.Value = 54;
                data.userData.headCut_2_multiplierCoef.Value = 1f;//54
                return;
            }
            if (level >= 225 && level < 250)
            {
                data.userData.headCut_2_multiplier.Value = 54;
                data.userData.headCut_2_multiplierCoef.Value = 3f;//54
                return;
            }
            if (level >= 250 && level < 275)
            {
                data.userData.headCut_2_multiplier.Value = 162;
                data.userData.headCut_2_multiplierCoef.Value = 1f;//162
                return;
            }
            if (level >= 275 && level < 300)
            {
                data.userData.headCut_2_multiplier.Value = 162;
                data.userData.headCut_2_multiplierCoef.Value = 3f;//162
                return;
            }
            if (level >= 300 && level < 325)
            {
                data.userData.headCut_2_multiplier.Value = 486;
                data.userData.headCut_2_multiplierCoef.Value = 1f;//486
                return;
            }
            if (level >= 325 && level < 350)
            {
                data.userData.headCut_2_multiplier.Value = 486;
                data.userData.headCut_2_multiplierCoef.Value = 3f;//486
                return;
            }
            if (level >= 350 && level < 375)
            {
                data.userData.headCut_2_multiplier.Value = 972;
                data.userData.headCut_2_multiplierCoef.Value = 1f;//972
                return;
            }
            if (level >= 375 && level < 400)
            {
                data.userData.headCut_2_multiplier.Value = 972;
                data.userData.headCut_2_multiplierCoef.Value = 3f;//972
                return;
            }
            if (level >= 400 && level < 425)
            {
                data.userData.headCut_2_multiplier.Value = 2916;
                data.userData.headCut_2_multiplierCoef.Value = 1f;//2916
                return;
            }
            if (level >= 425 && level < 450)
            {
                data.userData.headCut_2_multiplier.Value = 2916;
                data.userData.headCut_2_multiplierCoef.Value = 2f;//2916
                return;
            }
            if (level >= 450 && level < 475)
            {
                data.userData.headCut_2_multiplier.Value = 5832;
                data.userData.headCut_2_multiplierCoef.Value = 1f;//5832
                return;
            }
            if (level >= 475 && level < 500)
            {
                data.userData.headCut_2_multiplier.Value = 5832;
                data.userData.headCut_2_multiplierCoef.Value = 2f;//5832
                return;
            }
            if (level >= 500 && level < 525)
            {
                data.userData.headCut_2_multiplier.Value = 11664;
                data.userData.headCut_2_multiplierCoef.Value = 1f;//11664
                return;
            }
            if (level >= 525 && level < 550)
            {
                data.userData.headCut_2_multiplier.Value = 11664;
                data.userData.headCut_2_multiplierCoef.Value = 2f;//11664
                return;
            }
            if (level >= 550 && level < 575)
            {
                data.userData.headCut_2_multiplier.Value = 23328;
                data.userData.headCut_2_multiplierCoef.Value = 1f;//23328
                return;
            }
            if (level >= 575 && level < 600)
            {
                data.userData.headCut_2_multiplier.Value = 23328;
                data.userData.headCut_2_multiplierCoef.Value = 2f;//23328
                return;
            }
            if (level >= 600 && level < 625)
            {
                data.userData.headCut_2_multiplier.Value = 46656;
                data.userData.headCut_2_multiplierCoef.Value = 1f;//46656
                return;
            }
            if (level >= 625 && level < 650)
            {
                data.userData.headCut_2_multiplier.Value = 46656;
                data.userData.headCut_2_multiplierCoef.Value = 2f;//46656
                return;
            }
            if (level >= 650 && level < 675)
            {
                data.userData.headCut_2_multiplier.Value = 93312;
                data.userData.headCut_2_multiplierCoef.Value = 1f;//93312
                return;
            }
            if (level >= 675 && level < 700)
            {
                data.userData.headCut_2_multiplier.Value = 93312;
                data.userData.headCut_2_multiplierCoef.Value = 2f;//93312
                return;
            }
            if (level >= 700 && level < 725)
            {
                data.userData.headCut_2_multiplier.Value = 186624;
                data.userData.headCut_2_multiplierCoef.Value = 1f;//186624
                return;
            }
            if (level >= 725 && level < 750)
            {
                data.userData.headCut_2_multiplier.Value = 186624;
                data.userData.headCut_2_multiplierCoef.Value = 2f;//186624
                return;
            }
            if (level >= 750 && level < 775)
            {
                data.userData.headCut_2_multiplier.Value = 373248;
                data.userData.headCut_2_multiplierCoef.Value = 1f;//373248
                return;
            }
            if (level >= 775 && level < 800)
            {
                data.userData.headCut_2_multiplier.Value = 373248;
                data.userData.headCut_2_multiplierCoef.Value = 2f;//373248
                return;
            }
            if (level >= 800 && level < 825)
            {
                data.userData.headCut_2_multiplier.Value = 746496;
                data.userData.headCut_2_multiplierCoef.Value = 1f;//746496
                return;
            }
            if (level >= 825 && level < 850)
            {
                data.userData.headCut_2_multiplier.Value = 746496;
                data.userData.headCut_2_multiplierCoef.Value = 2f;//746496
                return;
            }
            if (level >= 850 && level < 875)
            {
                data.userData.headCut_2_multiplier.Value = 1492992;
                data.userData.headCut_2_multiplierCoef.Value = 1f;//1492992
                return;
            }
            if (level >= 875 && level < 900)
            {
                data.userData.headCut_2_multiplier.Value = 1492992;
                data.userData.headCut_2_multiplierCoef.Value = 2f;//1492992
                return;
            }
            if (level >= 900 && level < 925)
            {
                data.userData.headCut_2_multiplier.Value = 2985984;
                data.userData.headCut_2_multiplierCoef.Value = 1f;//2985984
                return;
            }
            if (level >= 925 && level < 950)
            {
                data.userData.headCut_2_multiplier.Value = 2985984;
                data.userData.headCut_2_multiplierCoef.Value = 2f;//2985984
                return;
            }
            if (level >= 950 && level < 975)
            {
                data.userData.headCut_2_multiplier.Value = 5971968;
                data.userData.headCut_2_multiplierCoef.Value = 1f;//5971968
                return;
            }
            if (level >= 975 && level < 1000)
            {
                data.userData.headCut_2_multiplier.Value = 5971968;
                data.userData.headCut_2_multiplierCoef.Value = 3f;//5971968
                return;
            }
            if (level >= 1000)
            {
                data.userData.headCut_2_multiplier.Value = 17915904;
                data.userData.headCut_2_multiplierCoef.Value = 1f;//17915904
                return;
            }




        }
    }
    private void SetMultiplierFishClean_1(int level)
    {
        if (level % nextMashineOpenLevel == 0 && level != 0||init)
        {
            if (level < 25)
            {
                data.userData.fishClean_1_multiplier.Value = 1;
                data.userData.fishClean_1_multiplierCoef.Value = 2f;//1
                return;
            }
            if (level >= 25 && level < 50)
            {
                data.userData.fishClean_1_multiplier.Value = 2;
                data.userData.fishClean_1_multiplierCoef.Value = 1.5f;//2
                return;
            }
            if (level >= 50 && level < 75)
            {
                data.userData.fishClean_1_multiplier.Value = 3;
                data.userData.fishClean_1_multiplierCoef.Value = 1.5f;//3
                return;
            }
            if (level >= 75 && level < 100)
            {
                data.userData.fishClean_1_multiplier.Value = 4.5f;
                data.userData.fishClean_1_multiplierCoef.Value = 2f;//4.5
                return;
            }
            if (level >= 100 && level < 125)
            {
                data.userData.fishClean_1_multiplier.Value = 9;
                data.userData.fishClean_1_multiplierCoef.Value = 1f;//9
                return;
            }
            if (level >= 125 && level < 150)
            {
                data.userData.fishClean_1_multiplier.Value = 9;
                data.userData.fishClean_1_multiplierCoef.Value = 2f;//9
                return;
            }

            if (level >= 150 && level < 175)
            {
                data.userData.fishClean_1_multiplier.Value = 18;
                data.userData.fishClean_1_multiplierCoef.Value = 1f;//18
                return;
            }
            if (level >= 175 && level < 200)
            {
                data.userData.fishClean_1_multiplier.Value = 18;
                data.userData.fishClean_1_multiplierCoef.Value = 3f;//18
                return;
            }
            if (level >= 200 && level < 225)
            {
                data.userData.fishClean_1_multiplier.Value = 54;
                data.userData.fishClean_1_multiplierCoef.Value = 1f;//54
                return;
            }
            if (level >= 225 && level < 250)
            {
                data.userData.fishClean_1_multiplier.Value = 54;
                data.userData.fishClean_1_multiplierCoef.Value = 3f;//54
                return;
            }
            if (level >= 250 && level < 275)
            {
                data.userData.fishClean_1_multiplier.Value = 162;
                data.userData.fishClean_1_multiplierCoef.Value = 1f;//162
                return;
            }
            if (level >= 275 && level < 300)
            {
                data.userData.fishClean_1_multiplier.Value = 162;
                data.userData.fishClean_1_multiplierCoef.Value = 3f;//162
                return;
            }
            if (level >= 300 && level < 325)
            {
                data.userData.fishClean_1_multiplier.Value = 486;
                data.userData.fishClean_1_multiplierCoef.Value = 1f;//486
                return;
            }
            if (level >= 325 && level < 350)
            {
                data.userData.fishClean_1_multiplier.Value = 486;
                data.userData.fishClean_1_multiplierCoef.Value = 3f;//486
                return;
            }
            if (level >= 350 && level < 375)
            {
                data.userData.fishClean_1_multiplier.Value = 972;
                data.userData.fishClean_1_multiplierCoef.Value = 1f;//972
                return;
            }
            if (level >= 375 && level < 400)
            {
                data.userData.fishClean_1_multiplier.Value = 972;
                data.userData.fishClean_1_multiplierCoef.Value = 3f;//972
                return;
            }
            if (level >= 400 && level < 425)
            {
                data.userData.fishClean_1_multiplier.Value = 2916;
                data.userData.fishClean_1_multiplierCoef.Value = 1f;//2916
                return;
            }
            if (level >= 425 && level < 450)
            {
                data.userData.fishClean_1_multiplier.Value = 2916;
                data.userData.fishClean_1_multiplierCoef.Value = 2f;//2916
                return;
            }
            if (level >= 450 && level < 475)
            {
                data.userData.fishClean_1_multiplier.Value = 5832;
                data.userData.fishClean_1_multiplierCoef.Value = 1f;//5832
                return;
            }
            if (level >= 475 && level < 500)
            {
                data.userData.fishClean_1_multiplier.Value = 5832;
                data.userData.fishClean_1_multiplierCoef.Value = 2f;//5832
                return;
            }
            if (level >= 500 && level < 525)
            {
                data.userData.fishClean_1_multiplier.Value = 11664;
                data.userData.fishClean_1_multiplierCoef.Value = 1f;//11664
                return;
            }
            if (level >= 525 && level < 550)
            {
                data.userData.fishClean_1_multiplier.Value = 11664;
                data.userData.fishClean_1_multiplierCoef.Value = 2f;//11664
                return;
            }
            if (level >= 550 && level < 575)
            {
                data.userData.fishClean_1_multiplier.Value = 23328;
                data.userData.fishClean_1_multiplierCoef.Value = 1f;//23328
                return;
            }
            if (level >= 575 && level < 600)
            {
                data.userData.fishClean_1_multiplier.Value = 23328;
                data.userData.fishClean_1_multiplierCoef.Value = 2f;//23328
                return;
            }
            if (level >= 600 && level < 625)
            {
                data.userData.fishClean_1_multiplier.Value = 46656;
                data.userData.fishClean_1_multiplierCoef.Value = 1f;//46656
                return;
            }
            if (level >= 625 && level < 650)
            {
                data.userData.fishClean_1_multiplier.Value = 46656;
                data.userData.fishClean_1_multiplierCoef.Value = 2f;//46656
                return;
            }
            if (level >= 650 && level < 675)
            {
                data.userData.fishClean_1_multiplier.Value = 93312;
                data.userData.fishClean_1_multiplierCoef.Value = 1f;//93312
                return;
            }
            if (level >= 675 && level < 700)
            {
                data.userData.fishClean_1_multiplier.Value = 93312;
                data.userData.fishClean_1_multiplierCoef.Value = 2f;//93312
                return;
            }
            if (level >= 700 && level < 725)
            {
                data.userData.fishClean_1_multiplier.Value = 186624;
                data.userData.fishClean_1_multiplierCoef.Value = 1f;//186624
                return;
            }
            if (level >= 725 && level < 750)
            {
                data.userData.fishClean_1_multiplier.Value = 186624;
                data.userData.fishClean_1_multiplierCoef.Value = 2f;//186624
                return;
            }
            if (level >= 750 && level < 775)
            {
                data.userData.fishClean_1_multiplier.Value = 373248;
                data.userData.fishClean_1_multiplierCoef.Value = 1f;//373248
                return;
            }
            if (level >= 775 && level < 800)
            {
                data.userData.fishClean_1_multiplier.Value = 373248;
                data.userData.fishClean_1_multiplierCoef.Value = 2f;//373248
                return;
            }
            if (level >= 800 && level < 825)
            {
                data.userData.fishClean_1_multiplier.Value = 746496;
                data.userData.fishClean_1_multiplierCoef.Value = 1f;//746496
                return;
            }
            if (level >= 825 && level < 850)
            {
                data.userData.fishClean_1_multiplier.Value = 746496;
                data.userData.fishClean_1_multiplierCoef.Value = 2f;//746496
                return;
            }
            if (level >= 850 && level < 875)
            {
                data.userData.fishClean_1_multiplier.Value = 1492992;
                data.userData.fishClean_1_multiplierCoef.Value = 1f;//1492992
                return;
            }
            if (level >= 875 && level < 900)
            {
                data.userData.fishClean_1_multiplier.Value = 1492992;
                data.userData.fishClean_1_multiplierCoef.Value = 2f;//1492992
                return;
            }
            if (level >= 900 && level < 925)
            {
                data.userData.fishClean_1_multiplier.Value = 2985984;
                data.userData.fishClean_1_multiplierCoef.Value = 1f;//2985984
                return;
            }
            if (level >= 925 && level < 950)
            {
                data.userData.fishClean_1_multiplier.Value = 2985984;
                data.userData.fishClean_1_multiplierCoef.Value = 2f;//2985984
                return;
            }
            if (level >= 950 && level < 975)
            {
                data.userData.fishClean_1_multiplier.Value = 5971968;
                data.userData.fishClean_1_multiplierCoef.Value = 1f;//5971968
                return;
            }
            if (level >= 975 && level < 1000)
            {
                data.userData.fishClean_1_multiplier.Value = 5971968;
                data.userData.fishClean_1_multiplierCoef.Value = 3f;//5971968
                return;
            }
            if (level >= 1000)
            {
                data.userData.fishClean_1_multiplier.Value = 17915904;
                data.userData.fishClean_1_multiplierCoef.Value = 1f;//17915904
                return;
            }
        }
    }
    private void SetMultiplierFishClean_2(int level)
    {
        if (level % nextMashineOpenLevel == 0 && level != 0 || init)
        {
            if (level < 25)
            {
                data.userData.fishClean_2_multiplier.Value = 1;
                data.userData.fishClean_2_multiplierCoef.Value = 2f;//1
                return;
            }
            if (level >= 25 && level < 50)
            {
                data.userData.fishClean_2_multiplier.Value = 2;
                data.userData.fishClean_2_multiplierCoef.Value = 1.5f;//2
                return;
            }
            if (level >= 50 && level < 75)
            {
                data.userData.fishClean_2_multiplier.Value = 3;
                data.userData.fishClean_2_multiplierCoef.Value = 1.5f;//3
                return;
            }
            if (level >= 75 && level < 100)
            {
                data.userData.fishClean_2_multiplier.Value = 4.5f;
                data.userData.fishClean_2_multiplierCoef.Value = 2f;//4.5
                return;
            }
            if (level >= 100 && level < 125)
            {
                data.userData.fishClean_2_multiplier.Value = 9;
                data.userData.fishClean_2_multiplierCoef.Value = 1f;//9
                return;
            }
            if (level >= 125 && level < 150)
            {
                data.userData.fishClean_2_multiplier.Value = 9;
                data.userData.fishClean_2_multiplierCoef.Value = 2f;//9
                return;
            }

            if (level >= 150 && level < 175)
            {
                data.userData.fishClean_2_multiplier.Value = 18;
                data.userData.fishClean_2_multiplierCoef.Value = 1f;//18
                return;
            }
            if (level >= 175 && level < 200)
            {
                data.userData.fishClean_2_multiplier.Value = 18;
                data.userData.fishClean_2_multiplierCoef.Value = 3f;//18
                return;
            }
            if (level >= 200 && level < 225)
            {
                data.userData.fishClean_2_multiplier.Value = 54;
                data.userData.fishClean_2_multiplierCoef.Value = 1f;//54
                return;
            }
            if (level >= 225 && level < 250)
            {
                data.userData.fishClean_2_multiplier.Value = 54;
                data.userData.fishClean_2_multiplierCoef.Value = 3f;//54
                return;
            }
            if (level >= 250 && level < 275)
            {
                data.userData.fishClean_2_multiplier.Value = 162;
                data.userData.fishClean_2_multiplierCoef.Value = 1f;//162
                return;
            }
            if (level >= 275 && level < 300)
            {
                data.userData.fishClean_2_multiplier.Value = 162;
                data.userData.fishClean_2_multiplierCoef.Value = 3f;//162
                return;
            }
            if (level >= 300 && level < 325)
            {
                data.userData.fishClean_2_multiplier.Value = 486;
                data.userData.fishClean_2_multiplierCoef.Value = 1f;//486
                return;
            }
            if (level >= 325 && level < 350)
            {
                data.userData.fishClean_2_multiplier.Value = 486;
                data.userData.fishClean_2_multiplierCoef.Value = 3f;//486
                return;
            }
            if (level >= 350 && level < 375)
            {
                data.userData.fishClean_2_multiplier.Value = 972;
                data.userData.fishClean_2_multiplierCoef.Value = 1f;//972
                return;
            }
            if (level >= 375 && level < 400)
            {
                data.userData.fishClean_2_multiplier.Value = 972;
                data.userData.fishClean_2_multiplierCoef.Value = 3f;//972
                return;
            }
            if (level >= 400 && level < 425)
            {
                data.userData.fishClean_2_multiplier.Value = 2916;
                data.userData.fishClean_2_multiplierCoef.Value = 1f;//2916
                return;
            }
            if (level >= 425 && level < 450)
            {
                data.userData.fishClean_2_multiplier.Value = 2916;
                data.userData.fishClean_2_multiplierCoef.Value = 2f;//2916
                return;
            }
            if (level >= 450 && level < 475)
            {
                data.userData.fishClean_2_multiplier.Value = 5832;
                data.userData.fishClean_2_multiplierCoef.Value = 1f;//5832
                return;
            }
            if (level >= 475 && level < 500)
            {
                data.userData.fishClean_2_multiplier.Value = 5832;
                data.userData.fishClean_2_multiplierCoef.Value = 2f;//5832
                return;
            }
            if (level >= 500 && level < 525)
            {
                data.userData.fishClean_2_multiplier.Value = 11664;
                data.userData.fishClean_2_multiplierCoef.Value = 1f;//11664
                return;
            }
            if (level >= 525 && level < 550)
            {
                data.userData.fishClean_2_multiplier.Value = 11664;
                data.userData.fishClean_2_multiplierCoef.Value = 2f;//11664
                return;
            }
            if (level >= 550 && level < 575)
            {
                data.userData.fishClean_2_multiplier.Value = 23328;
                data.userData.fishClean_2_multiplierCoef.Value = 1f;//23328
                return;
            }
            if (level >= 575 && level < 600)
            {
                data.userData.fishClean_2_multiplier.Value = 23328;
                data.userData.fishClean_2_multiplierCoef.Value = 2f;//23328
                return;
            }
            if (level >= 600 && level < 625)
            {
                data.userData.fishClean_2_multiplier.Value = 46656;
                data.userData.fishClean_2_multiplierCoef.Value = 1f;//46656
                return;
            }
            if (level >= 625 && level < 650)
            {
                data.userData.fishClean_2_multiplier.Value = 46656;
                data.userData.fishClean_2_multiplierCoef.Value = 2f;//46656
                return;
            }
            if (level >= 650 && level < 675)
            {
                data.userData.fishClean_2_multiplier.Value = 93312;
                data.userData.fishClean_2_multiplierCoef.Value = 1f;//93312
                return;
            }
            if (level >= 675 && level < 700)
            {
                data.userData.fishClean_2_multiplier.Value = 93312;
                data.userData.fishClean_2_multiplierCoef.Value = 2f;//93312
                return;
            }
            if (level >= 700 && level < 725)
            {
                data.userData.fishClean_2_multiplier.Value = 186624;
                data.userData.fishClean_2_multiplierCoef.Value = 1f;//186624
                return;
            }
            if (level >= 725 && level < 750)
            {
                data.userData.fishClean_2_multiplier.Value = 186624;
                data.userData.fishClean_2_multiplierCoef.Value = 2f;//186624
                return;
            }
            if (level >= 750 && level < 775)
            {
                data.userData.fishClean_2_multiplier.Value = 373248;
                data.userData.fishClean_2_multiplierCoef.Value = 1f;//373248
                return;
            }
            if (level >= 775 && level < 800)
            {
                data.userData.fishClean_2_multiplier.Value = 373248;
                data.userData.fishClean_2_multiplierCoef.Value = 2f;//373248
                return;
            }
            if (level >= 800 && level < 825)
            {
                data.userData.fishClean_2_multiplier.Value = 746496;
                data.userData.fishClean_2_multiplierCoef.Value = 1f;//746496
                return;
            }
            if (level >= 825 && level < 850)
            {
                data.userData.fishClean_2_multiplier.Value = 746496;
                data.userData.fishClean_2_multiplierCoef.Value = 2f;//746496
                return;
            }
            if (level >= 850 && level < 875)
            {
                data.userData.fishClean_2_multiplier.Value = 1492992;
                data.userData.fishClean_2_multiplierCoef.Value = 1f;//1492992
                return;
            }
            if (level >= 875 && level < 900)
            {
                data.userData.fishClean_2_multiplier.Value = 1492992;
                data.userData.fishClean_2_multiplierCoef.Value = 2f;//1492992
                return;
            }
            if (level >= 900 && level < 925)
            {
                data.userData.fishClean_2_multiplier.Value = 2985984;
                data.userData.fishClean_2_multiplierCoef.Value = 1f;//2985984
                return;
            }
            if (level >= 925 && level < 950)
            {
                data.userData.fishClean_2_multiplier.Value = 2985984;
                data.userData.fishClean_2_multiplierCoef.Value = 2f;//2985984
                return;
            }
            if (level >= 950 && level < 975)
            {
                data.userData.fishClean_2_multiplier.Value = 5971968;
                data.userData.fishClean_2_multiplierCoef.Value = 1f;//5971968
                return;
            }
            if (level >= 975 && level < 1000)
            {
                data.userData.fishClean_2_multiplier.Value = 5971968;
                data.userData.fishClean_2_multiplierCoef.Value = 3f;//5971968
                return;
            }
            if (level >= 1000)
            {
                data.userData.fishClean_2_multiplier.Value = 17915904;
                data.userData.fishClean_2_multiplierCoef.Value = 1f;//17915904
                return;
            }
        }
    }
    private void SetMultiplierFishClean_3(int level)
    {
        if (level % nextMashineOpenLevel == 0 && level != 0 || init)
        {
            if (level < 25)
            {
                data.userData.fishClean_3_multiplier.Value = 1;
                data.userData.fishClean_3_multiplierCoef.Value = 2f;//1
                return;
            }
            if (level >= 25 && level < 50)
            {
                data.userData.fishClean_3_multiplier.Value = 2;
                data.userData.fishClean_3_multiplierCoef.Value = 1.5f;//2
                return;
            }
            if (level >= 50 && level < 75)
            {
                data.userData.fishClean_3_multiplier.Value = 3;
                data.userData.fishClean_3_multiplierCoef.Value = 1.5f;//3
                return;
            }
            if (level >= 75 && level < 100)
            {
                data.userData.fishClean_3_multiplier.Value = 4.5f;
                data.userData.fishClean_3_multiplierCoef.Value = 2f;//4.5
                return;
            }
            if (level >= 100 && level < 125)
            {
                data.userData.fishClean_3_multiplier.Value = 9;
                data.userData.fishClean_3_multiplierCoef.Value = 1f;//9
                return;
            }
            if (level >= 125 && level < 150)
            {
                data.userData.fishClean_3_multiplier.Value = 9;
                data.userData.fishClean_3_multiplierCoef.Value = 2f;//9
                return;
            }

            if (level >= 150 && level < 175)
            {
                data.userData.fishClean_3_multiplier.Value = 18;
                data.userData.fishClean_3_multiplierCoef.Value = 1f;//18
                return;
            }
            if (level >= 175 && level < 200)
            {
                data.userData.fishClean_3_multiplier.Value = 18;
                data.userData.fishClean_3_multiplierCoef.Value = 3f;//18
                return;
            }
            if (level >= 200 && level < 225)
            {
                data.userData.fishClean_3_multiplier.Value = 54;
                data.userData.fishClean_3_multiplierCoef.Value = 1f;//54
                return;
            }
            if (level >= 225 && level < 250)
            {
                data.userData.fishClean_3_multiplier.Value = 54;
                data.userData.fishClean_3_multiplierCoef.Value = 3f;//54
                return;
            }
            if (level >= 250 && level < 275)
            {
                data.userData.fishClean_3_multiplier.Value = 162;
                data.userData.fishClean_3_multiplierCoef.Value = 1f;//162
                return;
            }
            if (level >= 275 && level < 300)
            {
                data.userData.fishClean_3_multiplier.Value = 162;
                data.userData.fishClean_3_multiplierCoef.Value = 3f;//162
                return;
            }
            if (level >= 300 && level < 325)
            {
                data.userData.fishClean_3_multiplier.Value = 486;
                data.userData.fishClean_3_multiplierCoef.Value = 1f;//486
                return;
            }
            if (level >= 325 && level < 350)
            {
                data.userData.fishClean_3_multiplier.Value = 486;
                data.userData.fishClean_3_multiplierCoef.Value = 3f;//486
                return;
            }
            if (level >= 350 && level < 375)
            {
                data.userData.fishClean_3_multiplier.Value = 972;
                data.userData.fishClean_3_multiplierCoef.Value = 1f;//972
                return;
            }
            if (level >= 375 && level < 400)
            {
                data.userData.fishClean_3_multiplier.Value = 972;
                data.userData.fishClean_3_multiplierCoef.Value = 3f;//972
                return;
            }
            if (level >= 400 && level < 425)
            {
                data.userData.fishClean_3_multiplier.Value = 2916;
                data.userData.fishClean_3_multiplierCoef.Value = 1f;//2916
                return;
            }
            if (level >= 425 && level < 450)
            {
                data.userData.fishClean_3_multiplier.Value = 2916;
                data.userData.fishClean_3_multiplierCoef.Value = 2f;//2916
                return;
            }
            if (level >= 450 && level < 475)
            {
                data.userData.fishClean_3_multiplier.Value = 5832;
                data.userData.fishClean_3_multiplierCoef.Value = 1f;//5832
                return;
            }
            if (level >= 475 && level < 500)
            {
                data.userData.fishClean_3_multiplier.Value = 5832;
                data.userData.fishClean_3_multiplierCoef.Value = 2f;//5832
                return;
            }
            if (level >= 500 && level < 525)
            {
                data.userData.fishClean_3_multiplier.Value = 11664;
                data.userData.fishClean_3_multiplierCoef.Value = 1f;//11664
                return;
            }
            if (level >= 525 && level < 550)
            {
                data.userData.fishClean_3_multiplier.Value = 11664;
                data.userData.fishClean_3_multiplierCoef.Value = 2f;//11664
                return;
            }
            if (level >= 550 && level < 575)
            {
                data.userData.fishClean_3_multiplier.Value = 23328;
                data.userData.fishClean_3_multiplierCoef.Value = 1f;//23328
                return;
            }
            if (level >= 575 && level < 600)
            {
                data.userData.fishClean_3_multiplier.Value = 23328;
                data.userData.fishClean_3_multiplierCoef.Value = 2f;//23328
                return;
            }
            if (level >= 600 && level < 625)
            {
                data.userData.fishClean_3_multiplier.Value = 46656;
                data.userData.fishClean_3_multiplierCoef.Value = 1f;//46656
                return;
            }
            if (level >= 625 && level < 650)
            {
                data.userData.fishClean_3_multiplier.Value = 46656;
                data.userData.fishClean_3_multiplierCoef.Value = 2f;//46656
                return;
            }
            if (level >= 650 && level < 675)
            {
                data.userData.fishClean_3_multiplier.Value = 93312;
                data.userData.fishClean_3_multiplierCoef.Value = 1f;//93312
                return;
            }
            if (level >= 675 && level < 700)
            {
                data.userData.fishClean_3_multiplier.Value = 93312;
                data.userData.fishClean_3_multiplierCoef.Value = 2f;//93312
                return;
            }
            if (level >= 700 && level < 725)
            {
                data.userData.fishClean_3_multiplier.Value = 186624;
                data.userData.fishClean_3_multiplierCoef.Value = 1f;//186624
                return;
            }
            if (level >= 725 && level < 750)
            {
                data.userData.fishClean_3_multiplier.Value = 186624;
                data.userData.fishClean_3_multiplierCoef.Value = 2f;//186624
                return;
            }
            if (level >= 750 && level < 775)
            {
                data.userData.fishClean_3_multiplier.Value = 373248;
                data.userData.fishClean_3_multiplierCoef.Value = 1f;//373248
                return;
            }
            if (level >= 775 && level < 800)
            {
                data.userData.fishClean_3_multiplier.Value = 373248;
                data.userData.fishClean_3_multiplierCoef.Value = 2f;//373248
                return;
            }
            if (level >= 800 && level < 825)
            {
                data.userData.fishClean_3_multiplier.Value = 746496;
                data.userData.fishClean_3_multiplierCoef.Value = 1f;//746496
                return;
            }
            if (level >= 825 && level < 850)
            {
                data.userData.fishClean_3_multiplier.Value = 746496;
                data.userData.fishClean_3_multiplierCoef.Value = 2f;//746496
                return;
            }
            if (level >= 850 && level < 875)
            {
                data.userData.fishClean_3_multiplier.Value = 1492992;
                data.userData.fishClean_3_multiplierCoef.Value = 1f;//1492992
                return;
            }
            if (level >= 875 && level < 900)
            {
                data.userData.fishClean_3_multiplier.Value = 1492992;
                data.userData.fishClean_3_multiplierCoef.Value = 2f;//1492992
                return;
            }
            if (level >= 900 && level < 925)
            {
                data.userData.fishClean_3_multiplier.Value = 2985984;
                data.userData.fishClean_3_multiplierCoef.Value = 1f;//2985984
                return;
            }
            if (level >= 925 && level < 950)
            {
                data.userData.fishClean_3_multiplier.Value = 2985984;
                data.userData.fishClean_3_multiplierCoef.Value = 2f;//2985984
                return;
            }
            if (level >= 950 && level < 975)
            {
                data.userData.fishClean_3_multiplier.Value = 5971968;
                data.userData.fishClean_3_multiplierCoef.Value = 1f;//5971968
                return;
            }
            if (level >= 975 && level < 1000)
            {
                data.userData.fishClean_3_multiplier.Value = 5971968;
                data.userData.fishClean_3_multiplierCoef.Value = 3f;//5971968
                return;
            }
            if (level >= 1000)
            {
                data.userData.fishClean_3_multiplier.Value = 17915904;
                data.userData.fishClean_3_multiplierCoef.Value = 1f;//17915904
                return;
            }
        }
    }
    private void SetMultiplierSteakMashine(int level)
    {
        if (level % nextMashineOpenLevel == 0 && level != 0||init)
        {
            if (level < 25)
            {
                data.userData.steakMashine_multiplier.Value = 1;
                data.userData.steakMashine_multiplierCoef.Value = 2f;//1
                return;
            }
            if (level >= 25 && level < 50)
            {
                data.userData.steakMashine_multiplier.Value = 2;
                data.userData.steakMashine_multiplierCoef.Value = 1.5f;//2
                return;
            }
            if (level >= 50 && level < 75)
            {
                data.userData.steakMashine_multiplier.Value = 3;
                data.userData.steakMashine_multiplierCoef.Value = 1.5f;//3
                return;
            }
            if (level >= 75 && level < 100)
            {
                data.userData.steakMashine_multiplier.Value = 4.5f;
                data.userData.steakMashine_multiplierCoef.Value = 2f;//4.5
                return;
            }
            if (level >= 100 && level < 125)
            {
                data.userData.steakMashine_multiplier.Value = 9;
                data.userData.steakMashine_multiplierCoef.Value = 1f;//9
                return;
            }
            if (level >= 125 && level < 150)
            {
                data.userData.steakMashine_multiplier.Value = 9;
                data.userData.steakMashine_multiplierCoef.Value = 2f;//9
                return;
            }

            if (level >= 150 && level < 175)
            {
                data.userData.steakMashine_multiplier.Value = 18;
                data.userData.steakMashine_multiplierCoef.Value = 1f;//18
                return;
            }
            if (level >= 175 && level < 200)
            {
                data.userData.steakMashine_multiplier.Value = 18;
                data.userData.steakMashine_multiplierCoef.Value = 3f;//18
                return;
            }
            if (level >= 200 && level < 225)
            {
                data.userData.steakMashine_multiplier.Value = 54;
                data.userData.steakMashine_multiplierCoef.Value = 1f;//54
                return;
            }
            if (level >= 225 && level < 250)
            {
                data.userData.steakMashine_multiplier.Value = 54;
                data.userData.steakMashine_multiplierCoef.Value = 3f;//54
                return;
            }
            if (level >= 250 && level < 275)
            {
                data.userData.steakMashine_multiplier.Value = 162;
                data.userData.steakMashine_multiplierCoef.Value = 1f;//162
                return;
            }
            if (level >= 275 && level < 300)
            {
                data.userData.steakMashine_multiplier.Value = 162;
                data.userData.steakMashine_multiplierCoef.Value = 3f;//162
                return;
            }
            if (level >= 300 && level < 325)
            {
                data.userData.steakMashine_multiplier.Value = 486;
                data.userData.steakMashine_multiplierCoef.Value = 1f;//486
                return;
            }
            if (level >= 325 && level < 350)
            {
                data.userData.steakMashine_multiplier.Value = 486;
                data.userData.steakMashine_multiplierCoef.Value = 3f;//486
                return;
            }
            if (level >= 350 && level < 375)
            {
                data.userData.steakMashine_multiplier.Value = 972;
                data.userData.steakMashine_multiplierCoef.Value = 1f;//972
                return;
            }
            if (level >= 375 && level < 400)
            {
                data.userData.steakMashine_multiplier.Value = 972;
                data.userData.steakMashine_multiplierCoef.Value = 3f;//972
                return;
            }
            if (level >= 400 && level < 425)
            {
                data.userData.steakMashine_multiplier.Value = 2916;
                data.userData.steakMashine_multiplierCoef.Value = 1f;//2916
                return;
            }
            if (level >= 425 && level < 450)
            {
                data.userData.steakMashine_multiplier.Value = 2916;
                data.userData.steakMashine_multiplierCoef.Value = 2f;//2916
                return;
            }
            if (level >= 450 && level < 475)
            {
                data.userData.steakMashine_multiplier.Value = 5832;
                data.userData.steakMashine_multiplierCoef.Value = 1f;//5832
                return;
            }
            if (level >= 475 && level < 500)
            {
                data.userData.steakMashine_multiplier.Value = 5832;
                data.userData.steakMashine_multiplierCoef.Value = 2f;//5832
                return;
            }
            if (level >= 500 && level < 525)
            {
                data.userData.steakMashine_multiplier.Value = 11664;
                data.userData.steakMashine_multiplierCoef.Value = 1f;//11664
                return;
            }
            if (level >= 525 && level < 550)
            {
                data.userData.steakMashine_multiplier.Value = 11664;
                data.userData.steakMashine_multiplierCoef.Value = 2f;//11664
                return;
            }
            if (level >= 550 && level < 575)
            {
                data.userData.steakMashine_multiplier.Value = 23328;
                data.userData.steakMashine_multiplierCoef.Value = 1f;//23328
                return;
            }
            if (level >= 575 && level < 600)
            {
                data.userData.steakMashine_multiplier.Value = 23328;
                data.userData.steakMashine_multiplierCoef.Value = 2f;//23328
                return;
            }
            if (level >= 600 && level < 625)
            {
                data.userData.steakMashine_multiplier.Value = 46656;
                data.userData.steakMashine_multiplierCoef.Value = 1f;//46656
                return;
            }
            if (level >= 625 && level < 650)
            {
                data.userData.steakMashine_multiplier.Value = 46656;
                data.userData.steakMashine_multiplierCoef.Value = 2f;//46656
                return;
            }
            if (level >= 650 && level < 675)
            {
                data.userData.steakMashine_multiplier.Value = 93312;
                data.userData.steakMashine_multiplierCoef.Value = 1f;//93312
                return;
            }
            if (level >= 675 && level < 700)
            {
                data.userData.steakMashine_multiplier.Value = 93312;
                data.userData.steakMashine_multiplierCoef.Value = 2f;//93312
                return;
            }
            if (level >= 700 && level < 725)
            {
                data.userData.steakMashine_multiplier.Value = 186624;
                data.userData.steakMashine_multiplierCoef.Value = 1f;//186624
                return;
            }
            if (level >= 725 && level < 750)
            {
                data.userData.steakMashine_multiplier.Value = 186624;
                data.userData.steakMashine_multiplierCoef.Value = 2f;//186624
                return;
            }
            if (level >= 750 && level < 775)
            {
                data.userData.steakMashine_multiplier.Value = 373248;
                data.userData.steakMashine_multiplierCoef.Value = 1f;//373248
                return;
            }
            if (level >= 775 && level < 800)
            {
                data.userData.steakMashine_multiplier.Value = 373248;
                data.userData.steakMashine_multiplierCoef.Value = 2f;//373248
                return;
            }
            if (level >= 800 && level < 825)
            {
                data.userData.steakMashine_multiplier.Value = 746496;
                data.userData.steakMashine_multiplierCoef.Value = 1f;//746496
                return;
            }
            if (level >= 825 && level < 850)
            {
                data.userData.steakMashine_multiplier.Value = 746496;
                data.userData.steakMashine_multiplierCoef.Value = 2f;//746496
                return;
            }
            if (level >= 850 && level < 875)
            {
                data.userData.steakMashine_multiplier.Value = 1492992;
                data.userData.steakMashine_multiplierCoef.Value = 1f;//1492992
                return;
            }
            if (level >= 875 && level < 900)
            {
                data.userData.steakMashine_multiplier.Value = 1492992;
                data.userData.steakMashine_multiplierCoef.Value = 2f;//1492992
                return;
            }
            if (level >= 900 && level < 925)
            {
                data.userData.steakMashine_multiplier.Value = 2985984;
                data.userData.steakMashine_multiplierCoef.Value = 1f;//2985984
                return;
            }
            if (level >= 925 && level < 950)
            {
                data.userData.steakMashine_multiplier.Value = 2985984;
                data.userData.steakMashine_multiplierCoef.Value = 2f;//2985984
                return;
            }
            if (level >= 950 && level < 975)
            {
                data.userData.steakMashine_multiplier.Value = 5971968;
                data.userData.steakMashine_multiplierCoef.Value = 1f;//5971968
                return;
            }
            if (level >= 975 && level < 1000)
            {
                data.userData.steakMashine_multiplier.Value = 5971968;
                data.userData.steakMashine_multiplierCoef.Value = 3f;//5971968
                return;
            }
            if (level >= 1000)
            {
                data.userData.steakMashine_multiplier.Value = 17915904;
                data.userData.steakMashine_multiplierCoef.Value = 1f;//17915904
                return;
            }
        }
    }
    private void SetMultiplierFarshMashine(int level)
    {
        if (level % nextMashineOpenLevel == 0 && level != 0 || init)
        {
            if (level < 25)
            {
                data.userData.farshMashine_multiplier.Value = 1;
                data.userData.farshMashine_multiplierCoef.Value = 2f;//1
                return;
            }
            if (level >= 25 && level < 50)
            {
                data.userData.farshMashine_multiplier.Value = 2;
                data.userData.farshMashine_multiplierCoef.Value = 1.5f;//2
                return;
            }
            if (level >= 50 && level < 75)
            {
                data.userData.farshMashine_multiplier.Value = 3;
                data.userData.farshMashine_multiplierCoef.Value = 1.5f;//3
                return;
            }
            if (level >= 75 && level < 100)
            {
                data.userData.farshMashine_multiplier.Value = 4.5f;
                data.userData.farshMashine_multiplierCoef.Value = 2f;//4.5
                return;
            }
            if (level >= 100 && level < 125)
            {
                data.userData.farshMashine_multiplier.Value = 9;
                data.userData.farshMashine_multiplierCoef.Value = 1f;//9
                return;
            }
            if (level >= 125 && level < 150)
            {
                data.userData.farshMashine_multiplier.Value = 9;
                data.userData.farshMashine_multiplierCoef.Value = 2f;//9
                return;
            }

            if (level >= 150 && level < 175)
            {
                data.userData.farshMashine_multiplier.Value = 18;
                data.userData.farshMashine_multiplierCoef.Value = 1f;//18
                return;
            }
            if (level >= 175 && level < 200)
            {
                data.userData.farshMashine_multiplier.Value = 18;
                data.userData.farshMashine_multiplierCoef.Value = 3f;//18
                return;
            }
            if (level >= 200 && level < 225)
            {
                data.userData.farshMashine_multiplier.Value = 54;
                data.userData.farshMashine_multiplierCoef.Value = 1f;//54
                return;
            }
            if (level >= 225 && level < 250)
            {
                data.userData.farshMashine_multiplier.Value = 54;
                data.userData.farshMashine_multiplierCoef.Value = 3f;//54
                return;
            }
            if (level >= 250 && level < 275)
            {
                data.userData.farshMashine_multiplier.Value = 162;
                data.userData.farshMashine_multiplierCoef.Value = 1f;//162
                return;
            }
            if (level >= 275 && level < 300)
            {
                data.userData.farshMashine_multiplier.Value = 162;
                data.userData.farshMashine_multiplierCoef.Value = 3f;//162
                return;
            }
            if (level >= 300 && level < 325)
            {
                data.userData.farshMashine_multiplier.Value = 486;
                data.userData.farshMashine_multiplierCoef.Value = 1f;//486
                return;
            }
            if (level >= 325 && level < 350)
            {
                data.userData.farshMashine_multiplier.Value = 486;
                data.userData.farshMashine_multiplierCoef.Value = 3f;//486
                return;
            }
            if (level >= 350 && level < 375)
            {
                data.userData.farshMashine_multiplier.Value = 972;
                data.userData.farshMashine_multiplierCoef.Value = 1f;//972
                return;
            }
            if (level >= 375 && level < 400)
            {
                data.userData.farshMashine_multiplier.Value = 972;
                data.userData.farshMashine_multiplierCoef.Value = 3f;//972
                return;
            }
            if (level >= 400 && level < 425)
            {
                data.userData.farshMashine_multiplier.Value = 2916;
                data.userData.farshMashine_multiplierCoef.Value = 1f;//2916
                return;
            }
            if (level >= 425 && level < 450)
            {
                data.userData.farshMashine_multiplier.Value = 2916;
                data.userData.farshMashine_multiplierCoef.Value = 2f;//2916
                return;
            }
            if (level >= 450 && level < 475)
            {
                data.userData.farshMashine_multiplier.Value = 5832;
                data.userData.farshMashine_multiplierCoef.Value = 1f;//5832
                return;
            }
            if (level >= 475 && level < 500)
            {
                data.userData.farshMashine_multiplier.Value = 5832;
                data.userData.farshMashine_multiplierCoef.Value = 2f;//5832
                return;
            }
            if (level >= 500 && level < 525)
            {
                data.userData.farshMashine_multiplier.Value = 11664;
                data.userData.farshMashine_multiplierCoef.Value = 1f;//11664
                return;
            }
            if (level >= 525 && level < 550)
            {
                data.userData.farshMashine_multiplier.Value = 11664;
                data.userData.farshMashine_multiplierCoef.Value = 2f;//11664
                return;
            }
            if (level >= 550 && level < 575)
            {
                data.userData.farshMashine_multiplier.Value = 23328;
                data.userData.farshMashine_multiplierCoef.Value = 1f;//23328
                return;
            }
            if (level >= 575 && level < 600)
            {
                data.userData.farshMashine_multiplier.Value = 23328;
                data.userData.farshMashine_multiplierCoef.Value = 2f;//23328
                return;
            }
            if (level >= 600 && level < 625)
            {
                data.userData.farshMashine_multiplier.Value = 46656;
                data.userData.farshMashine_multiplierCoef.Value = 1f;//46656
                return;
            }
            if (level >= 625 && level < 650)
            {
                data.userData.farshMashine_multiplier.Value = 46656;
                data.userData.farshMashine_multiplierCoef.Value = 2f;//46656
                return;
            }
            if (level >= 650 && level < 675)
            {
                data.userData.farshMashine_multiplier.Value = 93312;
                data.userData.farshMashine_multiplierCoef.Value = 1f;//93312
                return;
            }
            if (level >= 675 && level < 700)
            {
                data.userData.farshMashine_multiplier.Value = 93312;
                data.userData.farshMashine_multiplierCoef.Value = 2f;//93312
                return;
            }
            if (level >= 700 && level < 725)
            {
                data.userData.farshMashine_multiplier.Value = 186624;
                data.userData.farshMashine_multiplierCoef.Value = 1f;//186624
                return;
            }
            if (level >= 725 && level < 750)
            {
                data.userData.farshMashine_multiplier.Value = 186624;
                data.userData.farshMashine_multiplierCoef.Value = 2f;//186624
                return;
            }
            if (level >= 750 && level < 775)
            {
                data.userData.farshMashine_multiplier.Value = 373248;
                data.userData.farshMashine_multiplierCoef.Value = 1f;//373248
                return;
            }
            if (level >= 775 && level < 800)
            {
                data.userData.farshMashine_multiplier.Value = 373248;
                data.userData.farshMashine_multiplierCoef.Value = 2f;//373248
                return;
            }
            if (level >= 800 && level < 825)
            {
                data.userData.farshMashine_multiplier.Value = 746496;
                data.userData.farshMashine_multiplierCoef.Value = 1f;//746496
                return;
            }
            if (level >= 825 && level < 850)
            {
                data.userData.farshMashine_multiplier.Value = 746496;
                data.userData.farshMashine_multiplierCoef.Value = 2f;//746496
                return;
            }
            if (level >= 850 && level < 875)
            {
                data.userData.farshMashine_multiplier.Value = 1492992;
                data.userData.farshMashine_multiplierCoef.Value = 1f;//1492992
                return;
            }
            if (level >= 875 && level < 900)
            {
                data.userData.farshMashine_multiplier.Value = 1492992;
                data.userData.farshMashine_multiplierCoef.Value = 2f;//1492992
                return;
            }
            if (level >= 900 && level < 925)
            {
                data.userData.farshMashine_multiplier.Value = 2985984;
                data.userData.farshMashine_multiplierCoef.Value = 1f;//2985984
                return;
            }
            if (level >= 925 && level < 950)
            {
                data.userData.farshMashine_multiplier.Value = 2985984;
                data.userData.farshMashine_multiplierCoef.Value = 2f;//2985984
                return;
            }
            if (level >= 950 && level < 975)
            {
                data.userData.farshMashine_multiplier.Value = 5971968;
                data.userData.farshMashine_multiplierCoef.Value = 1f;//5971968
                return;
            }
            if (level >= 975 && level < 1000)
            {
                data.userData.farshMashine_multiplier.Value = 5971968;
                data.userData.farshMashine_multiplierCoef.Value = 3f;//5971968
                return;
            }
            if (level >= 1000)
            {
                data.userData.farshMashine_multiplier.Value = 17915904;
                data.userData.farshMashine_multiplierCoef.Value = 1f;//17915904
                return;
            }
        }
    }
    private void SetMultiplierFileMashine(int level)
    {
        if (level % nextMashineOpenLevel == 0 && level != 0 || init)
        {
            if (level < 25)
            {
                data.userData.fileMashine_multiplier.Value = 1;
                data.userData.fileMashine_multiplierCoef.Value = 2f;//1
                return;
            }
            if (level >= 25 && level < 50)
            {
                data.userData.fileMashine_multiplier.Value = 2;
                data.userData.fileMashine_multiplierCoef.Value = 1.5f;//2
                return;
            }
            if (level >= 50 && level < 75)
            {
                data.userData.fileMashine_multiplier.Value = 3;
                data.userData.fileMashine_multiplierCoef.Value = 1.5f;//3
                return;
            }
            if (level >= 75 && level < 100)
            {
                data.userData.fileMashine_multiplier.Value = 4.5f;
                data.userData.fileMashine_multiplierCoef.Value = 2f;//4.5
                return;
            }
            if (level >= 100 && level < 125)
            {
                data.userData.fileMashine_multiplier.Value = 9;
                data.userData.fileMashine_multiplierCoef.Value = 1f;//9
                return;
            }
            if (level >= 125 && level < 150)
            {
                data.userData.fileMashine_multiplier.Value = 9;
                data.userData.fileMashine_multiplierCoef.Value = 2f;//9
                return;
            }

            if (level >= 150 && level < 175)
            {
                data.userData.fileMashine_multiplier.Value = 18;
                data.userData.fileMashine_multiplierCoef.Value = 1f;//18
                return;
            }
            if (level >= 175 && level < 200)
            {
                data.userData.fileMashine_multiplier.Value = 18;
                data.userData.fileMashine_multiplierCoef.Value = 3f;//18
                return;
            }
            if (level >= 200 && level < 225)
            {
                data.userData.fileMashine_multiplier.Value = 54;
                data.userData.fileMashine_multiplierCoef.Value = 1f;//54
                return;
            }
            if (level >= 225 && level < 250)
            {
                data.userData.fileMashine_multiplier.Value = 54;
                data.userData.fileMashine_multiplierCoef.Value = 3f;//54
                return;
            }
            if (level >= 250 && level < 275)
            {
                data.userData.fileMashine_multiplier.Value = 162;
                data.userData.fileMashine_multiplierCoef.Value = 1f;//162
                return;
            }
            if (level >= 275 && level < 300)
            {
                data.userData.fileMashine_multiplier.Value = 162;
                data.userData.fileMashine_multiplierCoef.Value = 3f;//162
                return;
            }
            if (level >= 300 && level < 325)
            {
                data.userData.fileMashine_multiplier.Value = 486;
                data.userData.fileMashine_multiplierCoef.Value = 1f;//486
                return;
            }
            if (level >= 325 && level < 350)
            {
                data.userData.fileMashine_multiplier.Value = 486;
                data.userData.fileMashine_multiplierCoef.Value = 3f;//486
                return;
            }
            if (level >= 350 && level < 375)
            {
                data.userData.fileMashine_multiplier.Value = 972;
                data.userData.fileMashine_multiplierCoef.Value = 1f;//972
                return;
            }
            if (level >= 375 && level < 400)
            {
                data.userData.fileMashine_multiplier.Value = 972;
                data.userData.fileMashine_multiplierCoef.Value = 3f;//972
                return;
            }
            if (level >= 400 && level < 425)
            {
                data.userData.fileMashine_multiplier.Value = 2916;
                data.userData.fileMashine_multiplierCoef.Value = 1f;//2916
                return;
            }
            if (level >= 425 && level < 450)
            {
                data.userData.fileMashine_multiplier.Value = 2916;
                data.userData.fileMashine_multiplierCoef.Value = 2f;//2916
                return;
            }
            if (level >= 450 && level < 475)
            {
                data.userData.fileMashine_multiplier.Value = 5832;
                data.userData.fileMashine_multiplierCoef.Value = 1f;//5832
                return;
            }
            if (level >= 475 && level < 500)
            {
                data.userData.fileMashine_multiplier.Value = 5832;
                data.userData.fileMashine_multiplierCoef.Value = 2f;//5832
                return;
            }
            if (level >= 500 && level < 525)
            {
                data.userData.fileMashine_multiplier.Value = 11664;
                data.userData.fileMashine_multiplierCoef.Value = 1f;//11664
                return;
            }
            if (level >= 525 && level < 550)
            {
                data.userData.fileMashine_multiplier.Value = 11664;
                data.userData.fileMashine_multiplierCoef.Value = 2f;//11664
                return;
            }
            if (level >= 550 && level < 575)
            {
                data.userData.fileMashine_multiplier.Value = 23328;
                data.userData.fileMashine_multiplierCoef.Value = 1f;//23328
                return;
            }
            if (level >= 575 && level < 600)
            {
                data.userData.fileMashine_multiplier.Value = 23328;
                data.userData.fileMashine_multiplierCoef.Value = 2f;//23328
                return;
            }
            if (level >= 600 && level < 625)
            {
                data.userData.fileMashine_multiplier.Value = 46656;
                data.userData.fileMashine_multiplierCoef.Value = 1f;//46656
                return;
            }
            if (level >= 625 && level < 650)
            {
                data.userData.fileMashine_multiplier.Value = 46656;
                data.userData.fileMashine_multiplierCoef.Value = 2f;//46656
                return;
            }
            if (level >= 650 && level < 675)
            {
                data.userData.fileMashine_multiplier.Value = 93312;
                data.userData.fileMashine_multiplierCoef.Value = 1f;//93312
                return;
            }
            if (level >= 675 && level < 700)
            {
                data.userData.fileMashine_multiplier.Value = 93312;
                data.userData.fileMashine_multiplierCoef.Value = 2f;//93312
                return;
            }
            if (level >= 700 && level < 725)
            {
                data.userData.fileMashine_multiplier.Value = 186624;
                data.userData.fileMashine_multiplierCoef.Value = 1f;//186624
                return;
            }
            if (level >= 725 && level < 750)
            {
                data.userData.fileMashine_multiplier.Value = 186624;
                data.userData.fileMashine_multiplierCoef.Value = 2f;//186624
                return;
            }
            if (level >= 750 && level < 775)
            {
                data.userData.fileMashine_multiplier.Value = 373248;
                data.userData.fileMashine_multiplierCoef.Value = 1f;//373248
                return;
            }
            if (level >= 775 && level < 800)
            {
                data.userData.fileMashine_multiplier.Value = 373248;
                data.userData.fileMashine_multiplierCoef.Value = 2f;//373248
                return;
            }
            if (level >= 800 && level < 825)
            {
                data.userData.fileMashine_multiplier.Value = 746496;
                data.userData.fileMashine_multiplierCoef.Value = 1f;//746496
                return;
            }
            if (level >= 825 && level < 850)
            {
                data.userData.fileMashine_multiplier.Value = 746496;
                data.userData.fileMashine_multiplierCoef.Value = 2f;//746496
                return;
            }
            if (level >= 850 && level < 875)
            {
                data.userData.fileMashine_multiplier.Value = 1492992;
                data.userData.fileMashine_multiplierCoef.Value = 1f;//1492992
                return;
            }
            if (level >= 875 && level < 900)
            {
                data.userData.fileMashine_multiplier.Value = 1492992;
                data.userData.fileMashine_multiplierCoef.Value = 2f;//1492992
                return;
            }
            if (level >= 900 && level < 925)
            {
                data.userData.fileMashine_multiplier.Value = 2985984;
                data.userData.fileMashine_multiplierCoef.Value = 1f;//2985984
                return;
            }
            if (level >= 925 && level < 950)
            {
                data.userData.fileMashine_multiplier.Value = 2985984;
                data.userData.fileMashine_multiplierCoef.Value = 2f;//2985984
                return;
            }
            if (level >= 950 && level < 975)
            {
                data.userData.fileMashine_multiplier.Value = 5971968;
                data.userData.fileMashine_multiplierCoef.Value = 1f;//5971968
                return;
            }
            if (level >= 975 && level < 1000)
            {
                data.userData.fileMashine_multiplier.Value = 5971968;
                data.userData.fileMashine_multiplierCoef.Value = 3f;//5971968
                return;
            }
            if (level >= 1000)
            {
                data.userData.fileMashine_multiplier.Value = 17915904;
                data.userData.fileMashine_multiplierCoef.Value = 1f;//17915904
                return;
            }
        }
    }
    private void SetMultiplierSteakPackingMashine(int level)
    {
        if (level % nextMashineOpenLevel == 0 && level != 0||init)
        {
            if (level < 25 )
            {
                data.userData.packingMashine_1_multiplier.Value = 1;
                data.userData.packingMashine_1_multiplierCoef.Value = 2f;//1
                return;
            }
            if (level >= 25 && level < 50)
            {
                data.userData.packingMashine_1_multiplier.Value = 2;
                data.userData.packingMashine_1_multiplierCoef.Value = 1.5f;//2
                return;
            }
            if (level >= 50 && level < 75)
            {
                data.userData.packingMashine_1_multiplier.Value = 3;
                data.userData.packingMashine_1_multiplierCoef.Value = 1.5f;//3
                return;
            }
            if (level >= 75 && level < 100)
            {
                data.userData.packingMashine_1_multiplier.Value = 4.5f;
                data.userData.packingMashine_1_multiplierCoef.Value = 2f;//4.5
                return;
            }
            if (level >= 100 && level < 125)
            {
                data.userData.packingMashine_1_multiplier.Value = 9;
                data.userData.packingMashine_1_multiplierCoef.Value = 1f;//9
                return;
            }
            if (level >= 125 && level < 150)
            {
                data.userData.packingMashine_1_multiplier.Value = 9;
                data.userData.packingMashine_1_multiplierCoef.Value = 2f;//9
                return;
            }

            if (level >= 150 && level < 175)
            {
                data.userData.packingMashine_1_multiplier.Value = 18;
                data.userData.packingMashine_1_multiplierCoef.Value = 1f;//18
                return;
            }
            if (level >= 175 && level < 200)
            {
                data.userData.packingMashine_1_multiplier.Value = 18;
                data.userData.packingMashine_1_multiplierCoef.Value = 3f;//18
                return;
            }
            if (level >= 200 && level < 225)
            {
                data.userData.packingMashine_1_multiplier.Value = 54;
                data.userData.packingMashine_1_multiplierCoef.Value = 1f;//54
                return;
            }
            if (level >= 225 && level < 250)
            {
                data.userData.packingMashine_1_multiplier.Value = 54;
                data.userData.packingMashine_1_multiplierCoef.Value = 3f;//54
                return;
            }
            if (level >= 250 && level < 275)
            {
                data.userData.packingMashine_1_multiplier.Value = 162;
                data.userData.packingMashine_1_multiplierCoef.Value = 1f;//162
                return;
            }
            if (level >= 275 && level < 300)
            {
                data.userData.packingMashine_1_multiplier.Value = 162;
                data.userData.packingMashine_1_multiplierCoef.Value = 3f;//162
                return;
            }
            if (level >= 300 && level < 325)
            {
                data.userData.packingMashine_1_multiplier.Value = 486;
                data.userData.packingMashine_1_multiplierCoef.Value = 1f;//486
                return;
            }
            if (level >= 325 && level < 350)
            {
                data.userData.packingMashine_1_multiplier.Value = 486;
                data.userData.packingMashine_1_multiplierCoef.Value = 3f;//486
                return;
            }
            if (level >= 350 && level < 375)
            {
                data.userData.packingMashine_1_multiplier.Value = 972;
                data.userData.packingMashine_1_multiplierCoef.Value = 1f;//972
                return;
            }
            if (level >= 375 && level < 400)
            {
                data.userData.packingMashine_1_multiplier.Value = 972;
                data.userData.packingMashine_1_multiplierCoef.Value = 3f;//972
                return;
            }
            if (level >= 400 && level < 425)
            {
                data.userData.packingMashine_1_multiplier.Value = 2916;
                data.userData.packingMashine_1_multiplierCoef.Value = 1f;//2916
                return;
            }
            if (level >= 425 && level < 450)
            {
                data.userData.packingMashine_1_multiplier.Value = 2916;
                data.userData.packingMashine_1_multiplierCoef.Value = 2f;//2916
                return;
            }
            if (level >= 450 && level < 475)
            {
                data.userData.packingMashine_1_multiplier.Value = 5832;
                data.userData.packingMashine_1_multiplierCoef.Value = 1f;//5832
                return;
            }
            if (level >= 475 && level < 500)
            {
                data.userData.packingMashine_1_multiplier.Value = 5832;
                data.userData.packingMashine_1_multiplierCoef.Value = 2f;//5832
                return;
            }
            if (level >= 500 && level < 525)
            {
                data.userData.packingMashine_1_multiplier.Value = 11664;
                data.userData.packingMashine_1_multiplierCoef.Value = 1f;//11664
                return;
            }
            if (level >= 525 && level < 550)
            {
                data.userData.packingMashine_1_multiplier.Value = 11664;
                data.userData.packingMashine_1_multiplierCoef.Value = 2f;//11664
                return;
            }
            if (level >= 550 && level < 575)
            {
                data.userData.packingMashine_1_multiplier.Value = 23328;
                data.userData.packingMashine_1_multiplierCoef.Value = 1f;//23328
                return;
            }
            if (level >= 575 && level < 600)
            {
                data.userData.packingMashine_1_multiplier.Value = 23328;
                data.userData.packingMashine_1_multiplierCoef.Value = 2f;//23328
                return;
            }
            if (level >= 600 && level < 625)
            {
                data.userData.packingMashine_1_multiplier.Value = 46656;
                data.userData.packingMashine_1_multiplierCoef.Value = 1f;//46656
                return;
            }
            if (level >= 625 && level < 650)
            {
                data.userData.packingMashine_1_multiplier.Value = 46656;
                data.userData.packingMashine_1_multiplierCoef.Value = 2f;//46656
                return;
            }
            if (level >= 650 && level < 675)
            {
                data.userData.packingMashine_1_multiplier.Value = 93312;
                data.userData.packingMashine_1_multiplierCoef.Value = 1f;//93312
                return;
            }
            if (level >= 675 && level < 700)
            {
                data.userData.packingMashine_1_multiplier.Value = 93312;
                data.userData.packingMashine_1_multiplierCoef.Value = 2f;//93312
                return;
            }
            if (level >= 700 && level < 725)
            {
                data.userData.packingMashine_1_multiplier.Value = 186624;
                data.userData.packingMashine_1_multiplierCoef.Value = 1f;//186624
                return;
            }
            if (level >= 725 && level < 750)
            {
                data.userData.packingMashine_1_multiplier.Value = 186624;
                data.userData.packingMashine_1_multiplierCoef.Value = 2f;//186624
                return;
            }
            if (level >= 750 && level < 775)
            {
                data.userData.packingMashine_1_multiplier.Value = 373248;
                data.userData.packingMashine_1_multiplierCoef.Value = 1f;//373248
                return;
            }
            if (level >= 775 && level < 800)
            {
                data.userData.packingMashine_1_multiplier.Value = 373248;
                data.userData.packingMashine_1_multiplierCoef.Value = 2f;//373248
                return;
            }
            if (level >= 800 && level < 825)
            {
                data.userData.packingMashine_1_multiplier.Value = 746496;
                data.userData.packingMashine_1_multiplierCoef.Value = 1f;//746496
                return;
            }
            if (level >= 825 && level < 850)
            {
                data.userData.packingMashine_1_multiplier.Value = 746496;
                data.userData.packingMashine_1_multiplierCoef.Value = 2f;//746496
                return;
            }
            if (level >= 850 && level < 875)
            {
                data.userData.packingMashine_1_multiplier.Value = 1492992;
                data.userData.packingMashine_1_multiplierCoef.Value = 1f;//1492992
                return;
            }
            if (level >= 875 && level < 900)
            {
                data.userData.packingMashine_1_multiplier.Value = 1492992;
                data.userData.packingMashine_1_multiplierCoef.Value = 2f;//1492992
                return;
            }
            if (level >= 900 && level < 925)
            {
                data.userData.packingMashine_1_multiplier.Value = 2985984;
                data.userData.packingMashine_1_multiplierCoef.Value = 1f;//2985984
                return;
            }
            if (level >= 925 && level < 950)
            {
                data.userData.packingMashine_1_multiplier.Value = 2985984;
                data.userData.packingMashine_1_multiplierCoef.Value = 2f;//2985984
                return;
            }
            if (level >= 950 && level < 975)
            {
                data.userData.packingMashine_1_multiplier.Value = 5971968;
                data.userData.packingMashine_1_multiplierCoef.Value = 1f;//5971968
                return;
            }
            if (level >= 975 && level < 1000)
            {
                data.userData.packingMashine_1_multiplier.Value = 5971968;
                data.userData.packingMashine_1_multiplierCoef.Value = 3f;//5971968
                return;
            }
            if (level >= 1000)
            {
                data.userData.packingMashine_1_multiplier.Value = 17915904;
                data.userData.packingMashine_1_multiplierCoef.Value = 1f;//17915904
                return;
            }
        }
    }
    private void SetMultiplierFarshPackingMashine(int level)
    {
        if (level % nextMashineOpenLevel == 0 && level != 0 || init)
        {
            if (level < 25)
            {
                data.userData.packingMashine_2_multiplier.Value = 1;
                data.userData.packingMashine_2_multiplierCoef.Value = 2f;//1
                return;
            }
            if (level >= 25 && level < 50)
            {
                data.userData.packingMashine_2_multiplier.Value = 2;
                data.userData.packingMashine_2_multiplierCoef.Value = 1.5f;//2
                return;
            }
            if (level >= 50 && level < 75)
            {
                data.userData.packingMashine_2_multiplier.Value = 3;
                data.userData.packingMashine_2_multiplierCoef.Value = 1.5f;//3
                return;
            }
            if (level >= 75 && level < 100)
            {
                data.userData.packingMashine_2_multiplier.Value = 4.5f;
                data.userData.packingMashine_2_multiplierCoef.Value = 2f;//4.5
                return;
            }
            if (level >= 100 && level < 125)
            {
                data.userData.packingMashine_2_multiplier.Value = 9;
                data.userData.packingMashine_2_multiplierCoef.Value = 1f;//9
                return;
            }
            if (level >= 125 && level < 150)
            {
                data.userData.packingMashine_2_multiplier.Value = 9;
                data.userData.packingMashine_2_multiplierCoef.Value = 2f;//9
                return;
            }

            if (level >= 150 && level < 175)
            {
                data.userData.packingMashine_2_multiplier.Value = 18;
                data.userData.packingMashine_2_multiplierCoef.Value = 1f;//18
                return;
            }
            if (level >= 175 && level < 200)
            {
                data.userData.packingMashine_2_multiplier.Value = 18;
                data.userData.packingMashine_2_multiplierCoef.Value = 3f;//18
                return;
            }
            if (level >= 200 && level < 225)
            {
                data.userData.packingMashine_2_multiplier.Value = 54;
                data.userData.packingMashine_2_multiplierCoef.Value = 1f;//54
                return;
            }
            if (level >= 225 && level < 250)
            {
                data.userData.packingMashine_2_multiplier.Value = 54;
                data.userData.packingMashine_2_multiplierCoef.Value = 3f;//54
                return;
            }
            if (level >= 250 && level < 275)
            {
                data.userData.packingMashine_2_multiplier.Value = 162;
                data.userData.packingMashine_2_multiplierCoef.Value = 1f;//162
                return;
            }
            if (level >= 275 && level < 300)
            {
                data.userData.packingMashine_2_multiplier.Value = 162;
                data.userData.packingMashine_2_multiplierCoef.Value = 3f;//162
                return;
            }
            if (level >= 300 && level < 325)
            {
                data.userData.packingMashine_2_multiplier.Value = 486;
                data.userData.packingMashine_2_multiplierCoef.Value = 1f;//486
                return;
            }
            if (level >= 325 && level < 350)
            {
                data.userData.packingMashine_2_multiplier.Value = 486;
                data.userData.packingMashine_2_multiplierCoef.Value = 3f;//486
                return;
            }
            if (level >= 350 && level < 375)
            {
                data.userData.packingMashine_2_multiplier.Value = 972;
                data.userData.packingMashine_2_multiplierCoef.Value = 1f;//972
                return;
            }
            if (level >= 375 && level < 400)
            {
                data.userData.packingMashine_2_multiplier.Value = 972;
                data.userData.packingMashine_2_multiplierCoef.Value = 3f;//972
                return;
            }
            if (level >= 400 && level < 425)
            {
                data.userData.packingMashine_2_multiplier.Value = 2916;
                data.userData.packingMashine_2_multiplierCoef.Value = 1f;//2916
                return;
            }
            if (level >= 425 && level < 450)
            {
                data.userData.packingMashine_2_multiplier.Value = 2916;
                data.userData.packingMashine_2_multiplierCoef.Value = 2f;//2916
                return;
            }
            if (level >= 450 && level < 475)
            {
                data.userData.packingMashine_2_multiplier.Value = 5832;
                data.userData.packingMashine_2_multiplierCoef.Value = 1f;//5832
                return;
            }
            if (level >= 475 && level < 500)
            {
                data.userData.packingMashine_2_multiplier.Value = 5832;
                data.userData.packingMashine_2_multiplierCoef.Value = 2f;//5832
                return;
            }
            if (level >= 500 && level < 525)
            {
                data.userData.packingMashine_2_multiplier.Value = 11664;
                data.userData.packingMashine_2_multiplierCoef.Value = 1f;//11664
                return;
            }
            if (level >= 525 && level < 550)
            {
                data.userData.packingMashine_2_multiplier.Value = 11664;
                data.userData.packingMashine_2_multiplierCoef.Value = 2f;//11664
                return;
            }
            if (level >= 550 && level < 575)
            {
                data.userData.packingMashine_2_multiplier.Value = 23328;
                data.userData.packingMashine_2_multiplierCoef.Value = 1f;//23328
                return;
            }
            if (level >= 575 && level < 600)
            {
                data.userData.packingMashine_2_multiplier.Value = 23328;
                data.userData.packingMashine_2_multiplierCoef.Value = 2f;//23328
                return;
            }
            if (level >= 600 && level < 625)
            {
                data.userData.packingMashine_2_multiplier.Value = 46656;
                data.userData.packingMashine_2_multiplierCoef.Value = 1f;//46656
                return;
            }
            if (level >= 625 && level < 650)
            {
                data.userData.packingMashine_2_multiplier.Value = 46656;
                data.userData.packingMashine_2_multiplierCoef.Value = 2f;//46656
                return;
            }
            if (level >= 650 && level < 675)
            {
                data.userData.packingMashine_2_multiplier.Value = 93312;
                data.userData.packingMashine_2_multiplierCoef.Value = 1f;//93312
                return;
            }
            if (level >= 675 && level < 700)
            {
                data.userData.packingMashine_2_multiplier.Value = 93312;
                data.userData.packingMashine_2_multiplierCoef.Value = 2f;//93312
                return;
            }
            if (level >= 700 && level < 725)
            {
                data.userData.packingMashine_2_multiplier.Value = 186624;
                data.userData.packingMashine_2_multiplierCoef.Value = 1f;//186624
                return;
            }
            if (level >= 725 && level < 750)
            {
                data.userData.packingMashine_2_multiplier.Value = 186624;
                data.userData.packingMashine_2_multiplierCoef.Value = 2f;//186624
                return;
            }
            if (level >= 750 && level < 775)
            {
                data.userData.packingMashine_2_multiplier.Value = 373248;
                data.userData.packingMashine_2_multiplierCoef.Value = 1f;//373248
                return;
            }
            if (level >= 775 && level < 800)
            {
                data.userData.packingMashine_2_multiplier.Value = 373248;
                data.userData.packingMashine_2_multiplierCoef.Value = 2f;//373248
                return;
            }
            if (level >= 800 && level < 825)
            {
                data.userData.packingMashine_2_multiplier.Value = 746496;
                data.userData.packingMashine_2_multiplierCoef.Value = 1f;//746496
                return;
            }
            if (level >= 825 && level < 850)
            {
                data.userData.packingMashine_2_multiplier.Value = 746496;
                data.userData.packingMashine_2_multiplierCoef.Value = 2f;//746496
                return;
            }
            if (level >= 850 && level < 875)
            {
                data.userData.packingMashine_2_multiplier.Value = 1492992;
                data.userData.packingMashine_2_multiplierCoef.Value = 1f;//1492992
                return;
            }
            if (level >= 875 && level < 900)
            {
                data.userData.packingMashine_2_multiplier.Value = 1492992;
                data.userData.packingMashine_2_multiplierCoef.Value = 2f;//1492992
                return;
            }
            if (level >= 900 && level < 925)
            {
                data.userData.packingMashine_2_multiplier.Value = 2985984;
                data.userData.packingMashine_2_multiplierCoef.Value = 1f;//2985984
                return;
            }
            if (level >= 925 && level < 950)
            {
                data.userData.packingMashine_2_multiplier.Value = 2985984;
                data.userData.packingMashine_2_multiplierCoef.Value = 2f;//2985984
                return;
            }
            if (level >= 950 && level < 975)
            {
                data.userData.packingMashine_2_multiplier.Value = 5971968;
                data.userData.packingMashine_2_multiplierCoef.Value = 1f;//5971968
                return;
            }
            if (level >= 975 && level < 1000)
            {
                data.userData.packingMashine_2_multiplier.Value = 5971968;
                data.userData.packingMashine_2_multiplierCoef.Value = 3f;//5971968
                return;
            }
            if (level >= 1000)
            {
                data.userData.packingMashine_2_multiplier.Value = 17915904;
                data.userData.packingMashine_2_multiplierCoef.Value = 1f;//17915904
                return;
            }
        }
    }
    private void SetMultiplierFilePackingMashine(int level)
    {
        if (level % nextMashineOpenLevel == 0 && level != 0 || init)
        {
            if (level < 25)
            {
                data.userData.packingMashine_3_multiplier.Value = 1;
                data.userData.packingMashine_3_multiplierCoef.Value = 2f;//1
                return;
            }
            if (level >= 25 && level < 50)
            {
                data.userData.packingMashine_3_multiplier.Value = 2;
                data.userData.packingMashine_3_multiplierCoef.Value = 1.5f;//2
                return;
            }
            if (level >= 50 && level < 75)
            {
                data.userData.packingMashine_3_multiplier.Value = 3;
                data.userData.packingMashine_3_multiplierCoef.Value = 1.5f;//3
                return;
            }
            if (level >= 75 && level < 100)
            {
                data.userData.packingMashine_3_multiplier.Value = 4.5f;
                data.userData.packingMashine_3_multiplierCoef.Value = 2f;//4.5
                return;
            }
            if (level >= 100 && level < 125)
            {
                data.userData.packingMashine_3_multiplier.Value = 9;
                data.userData.packingMashine_3_multiplierCoef.Value = 1f;//9
                return;
            }
            if (level >= 125 && level < 150)
            {
                data.userData.packingMashine_3_multiplier.Value = 9;
                data.userData.packingMashine_3_multiplierCoef.Value = 2f;//9
                return;
            }

            if (level >= 150 && level < 175)
            {
                data.userData.packingMashine_3_multiplier.Value = 18;
                data.userData.packingMashine_3_multiplierCoef.Value = 1f;//18
                return;
            }
            if (level >= 175 && level < 200)
            {
                data.userData.packingMashine_3_multiplier.Value = 18;
                data.userData.packingMashine_3_multiplierCoef.Value = 3f;//18
                return;
            }
            if (level >= 200 && level < 225)
            {
                data.userData.packingMashine_3_multiplier.Value = 54;
                data.userData.packingMashine_3_multiplierCoef.Value = 1f;//54
                return;
            }
            if (level >= 225 && level < 250)
            {
                data.userData.packingMashine_3_multiplier.Value = 54;
                data.userData.packingMashine_3_multiplierCoef.Value = 3f;//54
                return;
            }
            if (level >= 250 && level < 275)
            {
                data.userData.packingMashine_3_multiplier.Value = 162;
                data.userData.packingMashine_3_multiplierCoef.Value = 1f;//162
                return;
            }
            if (level >= 275 && level < 300)
            {
                data.userData.packingMashine_3_multiplier.Value = 162;
                data.userData.packingMashine_3_multiplierCoef.Value = 3f;//162
                return;
            }
            if (level >= 300 && level < 325)
            {
                data.userData.packingMashine_3_multiplier.Value = 486;
                data.userData.packingMashine_3_multiplierCoef.Value = 1f;//486
                return;
            }
            if (level >= 325 && level < 350)
            {
                data.userData.packingMashine_3_multiplier.Value = 486;
                data.userData.packingMashine_3_multiplierCoef.Value = 3f;//486
                return;
            }
            if (level >= 350 && level < 375)
            {
                data.userData.packingMashine_3_multiplier.Value = 972;
                data.userData.packingMashine_3_multiplierCoef.Value = 1f;//972
                return;
            }
            if (level >= 375 && level < 400)
            {
                data.userData.packingMashine_3_multiplier.Value = 972;
                data.userData.packingMashine_3_multiplierCoef.Value = 3f;//972
                return;
            }
            if (level >= 400 && level < 425)
            {
                data.userData.packingMashine_3_multiplier.Value = 2916;
                data.userData.packingMashine_3_multiplierCoef.Value = 1f;//2916
                return;
            }
            if (level >= 425 && level < 450)
            {
                data.userData.packingMashine_3_multiplier.Value = 2916;
                data.userData.packingMashine_3_multiplierCoef.Value = 2f;//2916
                return;
            }
            if (level >= 450 && level < 475)
            {
                data.userData.packingMashine_3_multiplier.Value = 5832;
                data.userData.packingMashine_3_multiplierCoef.Value = 1f;//5832
                return;
            }
            if (level >= 475 && level < 500)
            {
                data.userData.packingMashine_3_multiplier.Value = 5832;
                data.userData.packingMashine_3_multiplierCoef.Value = 2f;//5832
                return;
            }
            if (level >= 500 && level < 525)
            {
                data.userData.packingMashine_3_multiplier.Value = 11664;
                data.userData.packingMashine_3_multiplierCoef.Value = 1f;//11664
                return;
            }
            if (level >= 525 && level < 550)
            {
                data.userData.packingMashine_3_multiplier.Value = 11664;
                data.userData.packingMashine_3_multiplierCoef.Value = 2f;//11664
                return;
            }
            if (level >= 550 && level < 575)
            {
                data.userData.packingMashine_3_multiplier.Value = 23328;
                data.userData.packingMashine_3_multiplierCoef.Value = 1f;//23328
                return;
            }
            if (level >= 575 && level < 600)
            {
                data.userData.packingMashine_3_multiplier.Value = 23328;
                data.userData.packingMashine_3_multiplierCoef.Value = 2f;//23328
                return;
            }
            if (level >= 600 && level < 625)
            {
                data.userData.packingMashine_3_multiplier.Value = 46656;
                data.userData.packingMashine_3_multiplierCoef.Value = 1f;//46656
                return;
            }
            if (level >= 625 && level < 650)
            {
                data.userData.packingMashine_3_multiplier.Value = 46656;
                data.userData.packingMashine_3_multiplierCoef.Value = 2f;//46656
                return;
            }
            if (level >= 650 && level < 675)
            {
                data.userData.packingMashine_3_multiplier.Value = 93312;
                data.userData.packingMashine_3_multiplierCoef.Value = 1f;//93312
                return;
            }
            if (level >= 675 && level < 700)
            {
                data.userData.packingMashine_3_multiplier.Value = 93312;
                data.userData.packingMashine_3_multiplierCoef.Value = 2f;//93312
                return;
            }
            if (level >= 700 && level < 725)
            {
                data.userData.packingMashine_3_multiplier.Value = 186624;
                data.userData.packingMashine_3_multiplierCoef.Value = 1f;//186624
                return;
            }
            if (level >= 725 && level < 750)
            {
                data.userData.packingMashine_3_multiplier.Value = 186624;
                data.userData.packingMashine_3_multiplierCoef.Value = 2f;//186624
                return;
            }
            if (level >= 750 && level < 775)
            {
                data.userData.packingMashine_3_multiplier.Value = 373248;
                data.userData.packingMashine_3_multiplierCoef.Value = 1f;//373248
                return;
            }
            if (level >= 775 && level < 800)
            {
                data.userData.packingMashine_3_multiplier.Value = 373248;
                data.userData.packingMashine_3_multiplierCoef.Value = 2f;//373248
                return;
            }
            if (level >= 800 && level < 825)
            {
                data.userData.packingMashine_3_multiplier.Value = 746496;
                data.userData.packingMashine_3_multiplierCoef.Value = 1f;//746496
                return;
            }
            if (level >= 825 && level < 850)
            {
                data.userData.packingMashine_3_multiplier.Value = 746496;
                data.userData.packingMashine_3_multiplierCoef.Value = 2f;//746496
                return;
            }
            if (level >= 850 && level < 875)
            {
                data.userData.packingMashine_3_multiplier.Value = 1492992;
                data.userData.packingMashine_3_multiplierCoef.Value = 1f;//1492992
                return;
            }
            if (level >= 875 && level < 900)
            {
                data.userData.packingMashine_3_multiplier.Value = 1492992;
                data.userData.packingMashine_3_multiplierCoef.Value = 2f;//1492992
                return;
            }
            if (level >= 900 && level < 925)
            {
                data.userData.packingMashine_3_multiplier.Value = 2985984;
                data.userData.packingMashine_3_multiplierCoef.Value = 1f;//2985984
                return;
            }
            if (level >= 925 && level < 950)
            {
                data.userData.packingMashine_3_multiplier.Value = 2985984;
                data.userData.packingMashine_3_multiplierCoef.Value = 2f;//2985984
                return;
            }
            if (level >= 950 && level < 975)
            {
                data.userData.packingMashine_3_multiplier.Value = 5971968;
                data.userData.packingMashine_3_multiplierCoef.Value = 1f;//5971968
                return;
            }
            if (level >= 975 && level < 1000)
            {
                data.userData.packingMashine_3_multiplier.Value = 5971968;
                data.userData.packingMashine_3_multiplierCoef.Value = 3f;//5971968
                return;
            }
            if (level >= 1000)
            {
                data.userData.packingMashine_3_multiplier.Value = 17915904;
                data.userData.packingMashine_3_multiplierCoef.Value = 1f;//17915904
                return;
            }
        }
    }
    public void UpgradeHeadCutingMashineSpeed()
    {
        if (data.userData.headCutingMashineSpeedLevel >= Constant.maxSpeed)
        {
            data.userData.headCutingMashineSpeedLevel = Constant.maxSpeed;
            balanceSystem.SetHeadCut_1_Speed();
            return;
        }
           
        if (moneySystem.Buy(data.upgradesData.headCutMashine_1_UpgradeSpeedCost.Value))
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.headCutMashine_1_UpgradeSpeedCost.Value,
            //        "Upgrade", "UpgradeCarvingMachine1SpeedLevel");
            //var softPurchaseParameters = new Dictionary<string, object>();
            //softPurchaseParameters["mygame_purchased_item"] = "UpgradeCarvingMachine1SpeedLevel";
            //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.headCutMashine_1_UpgradeSpeedCost.Value, softPurchaseParameters);
            data.userData.headCutingMashineSpeedLevel++;
            _effectSystem.ShowHeadCut1SpeedParticle();
            balanceSystem.SetHeadCut_1_Speed();
        }
        else
        {
            noMoneyPopUp.SetActive(true);
        }
    }
    public void UpgradeHeadCutingMashine2Level()
    {
        if (data.userData.headCutingMashine2Level >= Constant.maxLevel) return;
        if (moneySystem.Buy(data.upgradesData.headCutMashine_2_UpgradeCost.Value))
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.headCutMashine_2_UpgradeCost.Value,
            //        "Upgrade", "UpgradeCarvingMachine2Level");
            //var softPurchaseParameters = new Dictionary<string, object>();
            //softPurchaseParameters["mygame_purchased_item"] = "UpgradeCarvingMachine2Level";
            //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.headCutMashine_2_UpgradeCost.Value, softPurchaseParameters);
            data.userData.headCutingMashine2Level++;
            _effectSystem.ShowHeadCut2Particle();
            SetMultiplierHeadCut_2(data.userData.headCutingMashine2Level);
            balanceSystem.SetHeadCut_2_Incomes();            
            if (data.userData.headCutingMashine2Level == nextMashineOpenLevel)
            {
                StartInit();
                foreach (var equipment in equipmentEnableds)
                {
                    if (equipment.headCut_2)
                    {
                        equipment.EnabledNextZone();
                    }
                }
            }
        }
        else
        {
            noMoneyPopUp.SetActive(true);
        }
    }
    public void UpgradeHeadCutingMashine2Speed()
    {
        if (data.userData.headCutingMashine2SpeedLevel >= Constant.maxSpeed)
        {
            data.userData.headCutingMashine2SpeedLevel = Constant.maxSpeed;
            balanceSystem.SetHeadCut_2_Speed();
            return;
        }            
        if (moneySystem.Buy(data.upgradesData.headCutMashine_2_UpgradeSpeedCost.Value))
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.headCutMashine_2_UpgradeSpeedCost.Value,
            //        "Upgrade", "UpgradeCarvingMachine2SpeedLevel");
            //var softPurchaseParameters = new Dictionary<string, object>();
            //softPurchaseParameters["mygame_purchased_item"] = "UpgradeCarvingMachine2SpeedLevel";
            //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.headCutMashine_2_UpgradeSpeedCost.Value, softPurchaseParameters);
            data.userData.headCutingMashine2SpeedLevel++;
            _effectSystem.ShowHeadCut2SpeedParticle();
            balanceSystem.SetHeadCut_2_Speed();
        }
        else
        {
            noMoneyPopUp.SetActive(true);
        }
    }
    public void UpgradeFishCleaningMashine()
    {
        if (data.userData.fishCleaningMashineLevel >= Constant.maxLevel) return;
        if (moneySystem.Buy(data.upgradesData.fishCleanMashine_1_UpgradeCost.Value))
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.fishCleanMashine_1_UpgradeCost.Value,
            //        "Upgrade", "UpgradeSlicingMachine1Level");
            //var softPurchaseParameters = new Dictionary<string, object>();
            //softPurchaseParameters["mygame_purchased_item"] = "UpgradeSlicingMachine1Level";
            //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.fishCleanMashine_1_UpgradeCost.Value, softPurchaseParameters);
            data.userData.fishCleaningMashineLevel++;
            _effectSystem.ShowFishClean1Particle();
            SetMultiplierFishClean_1(data.userData.fishCleaningMashineLevel);
            balanceSystem.SetFishClean_1_Incomes();            
            if (data.userData.fishCleaningMashineLevel == nextMashineOpenLevel)
            {
                StartInit();
                foreach (var equipment in equipmentEnableds)
                {
                    if (equipment.fishClean_1)
                    {
                        equipment.EnabledNextZone();
                    }
                }
            }
        }
        else
        {
            noMoneyPopUp.SetActive(true);
        }
    }
    public void UpgradeFishCleaningMashineSpeed()
    {
        if (data.userData.fishCleaningMashineSpeedLevel >= Constant.maxSpeed)
        {
            data.userData.fishCleaningMashineSpeedLevel = Constant.maxSpeed;
            balanceSystem.SetFishClean_1_Speed();
            return;
        }            
        if (moneySystem.Buy(data.upgradesData.fishCleanMashine_1_UpgradeSpeedCost.Value))
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.fishCleanMashine_1_UpgradeSpeedCost.Value,
            //        "Upgrade", "UpgradeSlicingMachine1SpeedLevel");
            //var softPurchaseParameters = new Dictionary<string, object>();
            //softPurchaseParameters["mygame_purchased_item"] = "UpgradeSlicingMachine1SpeedLevel";
            //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.fishCleanMashine_1_UpgradeSpeedCost.Value, softPurchaseParameters);
            data.userData.fishCleaningMashineSpeedLevel++;
            _effectSystem.ShowFishClean1SpeedParticle();
            balanceSystem.SetFishClean_1_Speed();
        }
        else
        {
            noMoneyPopUp.SetActive(true);
        }
    }
    public void UpgradeFishCleaningMashine2()
    {
        if (data.userData.fishCleaningMashine2Level >= Constant.maxLevel) return;
        if (moneySystem.Buy(data.upgradesData.fishCleanMashine_2_UpgradeCost.Value))
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.fishCleanMashine_2_UpgradeCost.Value,
            //        "Upgrade", "UpgradeSlicingMachine2Level");
            //var softPurchaseParameters = new Dictionary<string, object>();
            //softPurchaseParameters["mygame_purchased_item"] = "UpgradeSlicingMachine2Level";
            //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.fishCleanMashine_2_UpgradeCost.Value, softPurchaseParameters);
            data.userData.fishCleaningMashine2Level++;
            _effectSystem.ShowFishClean2Particle();
            SetMultiplierFishClean_2(data.userData.fishCleaningMashine2Level);
            balanceSystem.SetFishClean_2_Incomes();            
            if (data.userData.fishCleaningMashine2Level == nextMashineOpenLevel)
            {
                StartInit();
                foreach (var equipment in equipmentEnableds)
                {
                    if (equipment.fishClean_2)
                    {
                        equipment.EnabledNextZone();
                    }
                }
            }
        }
        else
        {
            noMoneyPopUp.SetActive(true);
        }
    }
    public void UpgradeFishCleaningMashine2Speed()
    {
        if (data.userData.fishCleaningMashine2SpeedLevel >= Constant.maxSpeed)
        {
            data.userData.fishCleaningMashine2SpeedLevel = Constant.maxSpeed;
            balanceSystem.SetFishClean_2_Speed();
            return;
        }           
        if (moneySystem.Buy(data.upgradesData.fishCleanMashine_2_UpgradeSpeedCost.Value))
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.fishCleanMashine_2_UpgradeSpeedCost.Value,
            //        "Upgrade", "UpgradeSlicingMachine2SpeedLevel");
            //var softPurchaseParameters = new Dictionary<string, object>();
            //softPurchaseParameters["mygame_purchased_item"] = "UpgradeSlicingMachine2SpeedLevel";
            //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.fishCleanMashine_2_UpgradeSpeedCost.Value, softPurchaseParameters);
            data.userData.fishCleaningMashine2SpeedLevel++;
            _effectSystem.ShowFishClean2SpeedParticle();
            balanceSystem.SetFishClean_2_Speed();
        }
        else
        {
            noMoneyPopUp.SetActive(true);
        }
    }
    public void UpgradeFishCleaningMashine3()
    {
        if (data.userData.fishCleaningMashine3Level >= Constant.maxLevel) return;
        if (moneySystem.Buy(data.upgradesData.fishCleanMashine_3_UpgradeCost.Value))
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.fishCleanMashine_3_UpgradeCost.Value,
             //       "Upgrade", "UpgradeSlicingMachine3Level");
            //var softPurchaseParameters = new Dictionary<string, object>();
            //softPurchaseParameters["mygame_purchased_item"] = "UpgradeSlicingMachine3Level";
            //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.fishCleanMashine_3_UpgradeCost.Value, softPurchaseParameters);
            data.userData.fishCleaningMashine3Level++;
            _effectSystem.ShowFishClean3Particle();
            SetMultiplierFishClean_3(data.userData.fishCleaningMashine3Level);
            balanceSystem.SetFishClean_3_Incomes();            
            if (data.userData.fishCleaningMashine3Level == nextMashineOpenLevel)
            {
                StartInit();
                foreach (var equipment in equipmentEnableds)
                {
                    if (equipment.fishClean_3)
                    {
                        equipment.EnabledNextZone();
                    }
                }
            }
        }
        else
        {
            noMoneyPopUp.SetActive(true);
        }
    }
    public void UpgradeFishCleaningMashine3Speed()
    {
        if (data.userData.fishCleaningMashine3SpeedLevel >= Constant.maxSpeed)
        {
            data.userData.fishCleaningMashine3SpeedLevel = Constant.maxSpeed;
            balanceSystem.SetFishClean_3_Speed();
            return;
        }            
        if (moneySystem.Buy(data.upgradesData.fishCleanMashine_3_UpgradeSpeedCost.Value))
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.fishCleanMashine_3_UpgradeSpeedCost.Value,
             //       "Upgrade", "UpgradeSlicingMachine3SpeedLevel");
            //var softPurchaseParameters = new Dictionary<string, object>();
            //softPurchaseParameters["mygame_purchased_item"] = "UpgradeSlicingMachine3SpeedLevel";
            //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.fishCleanMashine_3_UpgradeSpeedCost.Value, softPurchaseParameters);
            data.userData.fishCleaningMashine3SpeedLevel++;
            _effectSystem.ShowFishClean3SpeedParticle();
            balanceSystem.SetFishClean_3_Speed();
        }
        else
        {
            noMoneyPopUp.SetActive(true);
        }
    }
    public void UpgradeSteakMashine()
    {
        if (data.userData.steakMashineLevel >= Constant.maxLevel) return;
        if (moneySystem.Buy(data.upgradesData.steakMashine_UpgradeCost.Value))
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.steakMashine_UpgradeCost.Value,
            //        "Upgrade", "UpgradeSteakMachineLevel");
            //var softPurchaseParameters = new Dictionary<string, object>();
            //softPurchaseParameters["mygame_purchased_item"] = "UpgradeSteakMachineLevel";
           // FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.steakMashine_UpgradeCost.Value, softPurchaseParameters);
            data.userData.steakMashineLevel++;
            _effectSystem.ShowSteakMachinParticle();
            SetMultiplierSteakMashine(data.userData.steakMashineLevel);
            balanceSystem.SetSteakMashine_Incomes();            
            if (data.userData.steakMashineLevel == nextMashineOpenLevel)
            {
                StartInit();
                foreach (var equipment in equipmentEnableds)
                {
                    if (equipment.steakMashine)
                    {
                        equipment.EnabledNextZone();
                    }
                }
            }
        }
        else
        {
            noMoneyPopUp.SetActive(true);
        }
    }
    public void UpgradeSteakMashineSpeed()
    {
        if (data.userData.steakMashineSpeedLevel >= Constant.maxSpeed)
        {
            data.userData.steakMashineSpeedLevel = Constant.maxSpeed;
            balanceSystem.SetSteakMashine_Speed();
            return;
        }            
        if (moneySystem.Buy(data.upgradesData.steakMashine_UpgradeSpeedCost.Value))
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.steakMashine_UpgradeSpeedCost.Value,
            //        "Upgrade", "UpgradeSteakMachineSpeedLevel");
            //var softPurchaseParameters = new Dictionary<string, object>();
            //softPurchaseParameters["mygame_purchased_item"] = "UpgradeSteakMachineSpeedLevel";
            //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.steakMashine_UpgradeSpeedCost.Value, softPurchaseParameters);
            data.userData.steakMashineSpeedLevel++;
            _effectSystem.ShowSteakMachinSpeedParticle();
            balanceSystem.SetSteakMashine_Speed();
        }
        else
        {
            noMoneyPopUp.SetActive(true);
        }
    }
    public void UpgradeFarshMashine()
    {
        if (data.userData.farshMashineLevel >= Constant.maxLevel) return;
        if (moneySystem.Buy(data.upgradesData.farshMashine_UpgradeCost.Value))
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.farshMashine_UpgradeCost.Value,
            //        "Upgrade", "UpgradeFarshMachineLevel");
            //var softPurchaseParameters = new Dictionary<string, object>();
            //softPurchaseParameters["mygame_purchased_item"] = "UpgradeFarshMachineLevel";
            //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.farshMashine_UpgradeCost.Value, softPurchaseParameters);
            data.userData.farshMashineLevel++;
            _effectSystem.ShowFarshMachinParticle();
            SetMultiplierFarshMashine(data.userData.farshMashineLevel);
            balanceSystem.SetFarshMashine_Incomes();
            if (data.userData.farshMashineLevel == nextMashineOpenLevel)
            {
                StartInit();
                foreach (var equipment in equipmentEnableds)
                {
                    if (equipment.farshMashine)
                    {
                        equipment.EnabledNextZone();
                    }
                }
            }
        }
        else
        {
            noMoneyPopUp.SetActive(true);
        }
    }
    public void UpgradeFarshMashineSpeed()
    {
        if (data.userData.farshMashineSpeedLevel >= Constant.maxSpeed)
        {
            data.userData.farshMashineSpeedLevel = Constant.maxSpeed;
            balanceSystem.SetFarshMashine_Speed();
            return;
        }
        if (moneySystem.Buy(data.upgradesData.farshMashine_UpgradeSpeedCost.Value))
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.farshMashine_UpgradeSpeedCost.Value,
            //        "Upgrade", "UpgradeFarshMachineSpeedLevel");
            //var softPurchaseParameters = new Dictionary<string, object>();
            //softPurchaseParameters["mygame_purchased_item"] = "UpgradeFarshMachineSpeedLevel";
            //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.farshMashine_UpgradeSpeedCost.Value, softPurchaseParameters);
            data.userData.farshMashineSpeedLevel++;
            _effectSystem.ShowFarshMachinSpeedParticle();
            balanceSystem.SetFarshMashine_Speed();
        }
        else
        {
            noMoneyPopUp.SetActive(true);
        }
    }
    public void UpgradeFileMashine()
    {
        if (data.userData.fileMashineLevel >= Constant.maxLevel) return;
        if (moneySystem.Buy(data.upgradesData.fileMashine_UpgradeCost.Value))
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.fileMashine_UpgradeCost.Value,
            //        "Upgrade", "UpgradeFileMachineLevel");
            //var softPurchaseParameters = new Dictionary<string, object>();
            //softPurchaseParameters["mygame_purchased_item"] = "UpgradeFileMachineLevel";
            //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.fileMashine_UpgradeCost.Value, softPurchaseParameters);
            data.userData.fileMashineLevel++;
            _effectSystem.ShowFileMachinParticle();
            SetMultiplierFileMashine(data.userData.fileMashineLevel);
            balanceSystem.SetFileMashine_Incomes();
            if (data.userData.fileMashineLevel == nextMashineOpenLevel)
            {
                StartInit();
                foreach (var equipment in equipmentEnableds)
                {
                    if (equipment.fileMashine)
                    {
                        equipment.EnabledNextZone();
                    }
                }
            }
        }
        else
        {
            noMoneyPopUp.SetActive(true);
        }
    }
    public void UpgradeFileMashineSpeed()
    {
        if (data.userData.fileMashineSpeedLevel >= Constant.maxSpeed)
        {
            data.userData.fileMashineSpeedLevel = Constant.maxSpeed;
            balanceSystem.SetFileMashine_Speed();
            return;
        }
        if (moneySystem.Buy(data.upgradesData.fileMashine_UpgradeSpeedCost.Value))
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.fileMashine_UpgradeSpeedCost.Value,
            //        "Upgrade", "UpgradeFileMachineSpeedLevel");
            //var softPurchaseParameters = new Dictionary<string, object>();
            //softPurchaseParameters["mygame_purchased_item"] = "UpgradeFileMachineSpeedLevel";
            //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.fileMashine_UpgradeSpeedCost.Value, softPurchaseParameters);
            data.userData.fileMashineSpeedLevel++;
            _effectSystem.ShowFileMachinSpeedParticle();
            balanceSystem.SetFileMashine_Speed();
        }
        else
        {
            noMoneyPopUp.SetActive(true);
        }
    }
    public void UpgradePackingMashine()
    {
        if (data.userData.steakPackingMashineLevel >= Constant.maxLevel) return;
        if (moneySystem.Buy(data.upgradesData.packingMashine_1_UpgradeCost.Value))
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.packingMashine_1_UpgradeCost.Value,
            //        "Upgrade", "UpgradePackingMachineLevel");
            //var softPurchaseParameters = new Dictionary<string, object>();
            //softPurchaseParameters["mygame_purchased_item"] = "UpgradePackingMachineLevel";
            //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.packingMashine_1_UpgradeCost.Value, softPurchaseParameters);
            data.userData.steakPackingMashineLevel++;
            _effectSystem.ShowPackingMachinParticle();
            SetMultiplierSteakPackingMashine(data.userData.steakPackingMashineLevel);
            balanceSystem.SetPackingMashine_Incomes();
            if (data.userData.steakPackingMashineLevel == nextMashineOpenLevel)
            {
                StartInit();
                foreach (var equipment in equipmentEnableds)
                {
                    if (equipment.steakPacking)
                    {
                        equipment.EnabledNextZone();
                    }
                }
            }
        }
        else
        {
            noMoneyPopUp.SetActive(true);
        }
    }
    public void UpgradePackingMashineSpeed()
    {
        if (data.userData.steakPackingMashineSpeedLevel >= Constant.maxSpeed)
        {
            data.userData.steakPackingMashineSpeedLevel = Constant.maxSpeed;
            balanceSystem.SetPackingMashine_Speed();
            return;
        }            
        if (moneySystem.Buy(data.upgradesData.packingMashine_1_UpgradeSpeedCost.Value))
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.packingMashine_1_UpgradeSpeedCost.Value,
            //        "Upgrade", "UpgradePackingMachineSpeedLevel");
            //var softPurchaseParameters = new Dictionary<string, object>();
            //softPurchaseParameters["mygame_purchased_item"] = "UpgradePackingMachineSpeedLevel";
            //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.packingMashine_1_UpgradeSpeedCost.Value, softPurchaseParameters);
            data.userData.steakPackingMashineSpeedLevel++;
            _effectSystem.ShowPackingMachinSpeedParticle();
            balanceSystem.SetPackingMashine_Speed();
        }
        else
        {
            noMoneyPopUp.SetActive(true);
        }
    }
    public void UpgradeFarshPackingMashine()
    {
        if (data.userData.farshPackingMashineLevel >= Constant.maxLevel) return;
        if (moneySystem.Buy(data.upgradesData.packingMashine_2_UpgradeCost.Value))
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.packingMashine_2_UpgradeCost.Value,
            //        "Upgrade", "UpgradePackingMachine_2_Level");
            //var softPurchaseParameters = new Dictionary<string, object>();
            //softPurchaseParameters["mygame_purchased_item"] = "UpgradePackingMachine_2_Level";
            //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.packingMashine_2_UpgradeCost.Value, softPurchaseParameters);
            data.userData.farshPackingMashineLevel++;
            _effectSystem.ShowFarshPackingMachinParticle();
            SetMultiplierFarshPackingMashine(data.userData.farshPackingMashineLevel);
            balanceSystem.SetFarshPackingMashine_Incomes();
            if (data.userData.farshPackingMashineLevel == nextMashineOpenLevel)
            {
                StartInit();
                foreach (var equipment in equipmentEnableds)
                {
                    if (equipment.farshPacking)
                    {
                        equipment.EnabledNextZone();
                    }
                }
            }
        }
        else
        {
            noMoneyPopUp.SetActive(true);
        }
    }
    public void UpgradeFarshPackingMashineSpeed()
    {
        if (data.userData.farshPackingMashineSpeedLevel >= Constant.maxSpeed)
        {
            data.userData.farshPackingMashineSpeedLevel = Constant.maxSpeed;
            balanceSystem.SetFarshPackingMashine_Speed();
            return;
        }
        if (moneySystem.Buy(data.upgradesData.packingMashine_2_UpgradeSpeedCost.Value))
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.packingMashine_2_UpgradeSpeedCost.Value,
            //        "Upgrade", "UpgradeFarshPackingMachineSpeedLevel");
            //var softPurchaseParameters = new Dictionary<string, object>();
            //softPurchaseParameters["mygame_purchased_item"] = "UpgradeFarshPackingMachineSpeedLevel";
            //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.packingMashine_2_UpgradeSpeedCost.Value, softPurchaseParameters);
            data.userData.farshPackingMashineSpeedLevel++;
            _effectSystem.ShowFarshPackingMachinSpeedParticle();
            balanceSystem.SetFarshPackingMashine_Speed();
        }
        else
        {
            noMoneyPopUp.SetActive(true);
        }
    }
    public void UpgradeFilePackingMashine()
    {
        if (data.userData.filePackingMashineLevel >= Constant.maxLevel) return;
        if (moneySystem.Buy(data.upgradesData.packingMashine_3_UpgradeCost.Value))
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.packingMashine_3_UpgradeCost.Value,
            //        "Upgrade", "UpgradePackingMachine_3_Level");
            //var softPurchaseParameters = new Dictionary<string, object>();
            //softPurchaseParameters["mygame_purchased_item"] = "UpgradePackingMachine_3_Level";
            //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.packingMashine_3_UpgradeCost.Value, softPurchaseParameters);
            data.userData.filePackingMashineLevel++;
            _effectSystem.ShowFilePackingMachinParticle();
            SetMultiplierFilePackingMashine(data.userData.filePackingMashineLevel);
            balanceSystem.SetFilePackingMashine_Incomes();
        }
        else
        {
            noMoneyPopUp.SetActive(true);
        }
    }
    public void UpgradeFilePackingMashineSpeed()
    {
        if (data.userData.filePackingMashineSpeedLevel >= Constant.maxSpeed)
        {
            data.userData.filePackingMashineSpeedLevel = Constant.maxSpeed;
            balanceSystem.SetFilePackingMashine_Speed();
            return;
        }
        if (moneySystem.Buy(data.upgradesData.packingMashine_3_UpgradeSpeedCost.Value))
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.packingMashine_3_UpgradeSpeedCost.Value,
              //      "Upgrade", "UpgradeFilePackingMachineSpeedLevel");
            //var softPurchaseParameters = new Dictionary<string, object>();
            //softPurchaseParameters["mygame_purchased_item"] = "UpgradeFilePackingMachineSpeedLevel";
            //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.packingMashine_3_UpgradeSpeedCost.Value, softPurchaseParameters);
            data.userData.filePackingMashineSpeedLevel++;
            _effectSystem.ShowFilePackingMachinSpeedParticle();
            balanceSystem.SetFilePackingMashine_Speed();
        }
        else
        {
            noMoneyPopUp.SetActive(true);
        }
    }

}
