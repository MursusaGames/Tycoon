using UnityEngine;
using System.Collections.Generic;

public class TransportControlSystem : BaseMonoSystem
{
    [SerializeField] private GameObject hireInWarBtn;
    [SerializeField] private GameObject upgradeInWarBtn;
    [SerializeField] private GameObject hireOutWarBtn;
    [SerializeField] private GameObject noMoneyPopUp;
    [SerializeField] private GameObject ship1;
    [SerializeField] private GameObject ship2;
    [SerializeField] private GameObject car_Inside_War;
    [SerializeField] private GameObject carPort;
    [SerializeField] private GameObject carPort2;
    [SerializeField] private GameObject car_In_War;
    [SerializeField] private GameObject car_In_War2;
    [SerializeField] private GameObject zone1Car;
    [SerializeField] private GameObject zone2Car;
    [SerializeField] private GameObject zone2_1Car;
    [SerializeField] private GameObject zone3Car;
    [SerializeField] private GameObject zone3Car_2;
    [SerializeField] private MoneySystem moneySystem;
    [SerializeField] private Transform cameraPort;
    [SerializeField] private Transform cameraMain;
    [SerializeField] private FueSystem fueSystem;
    
    public override void Init(AppData data)
    {
        base.Init(data);        
    }

    
    public void StartInit()
    {
        car_In_War.SetActive(data.userData.zoneInWar >= 1);
        car_In_War2.SetActive(data.userData.zoneInWar == 2);
        zone1Car.SetActive(true);
        zone2Car.SetActive(true);
        if (data.userData.zone3Car == 0) data.userData.zone3Car = 1;
        zone3Car.SetActive(data.userData.zone3Car > 0);
        zone3Car_2.SetActive(data.userData.zone3Car > 1);               
        carPort.SetActive(data.userData.carPort > 0);
        carPort2.SetActive(data.userData.carPort > 1);
        car_Inside_War.SetActive(data.userData.zone2Car > 0);        
        if (data.userData.carPort == 0) data.userData.carPort = 1;
        hireInWarBtn.SetActive(data.userData.zoneInWar < 1);
        upgradeInWarBtn.SetActive(data.userData.zoneInWar > 0);
        hireOutWarBtn.SetActive(data.userData.zone3Car < 2);
        if (data.userData.ship > 1)
        {
            ship2.SetActive(true);
            ship1.SetActive(true);
        }
        else if (data.userData.ship > 0)
        {
            ship1.SetActive(true);
        }        
    }    

    public void HireShip()
    {
        if (data.userData.ship == 0)
        {
            if (moneySystem.Buy(data.upgradesData.shipCost))
            {
                //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.shipCost,
                //    "Buy", "BuyShip_1");
                //var softPurchaseParameters = new Dictionary<string, object>();
                //softPurchaseParameters["mygame_purchased_item"] = "BuyShip_1";
                //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.shipCost, softPurchaseParameters);
                ship1.SetActive(true);
                data.userData.ship = 1;
                SaveDataSystem.Instance.SaveShipCount();
                if (data.matchData.isFue)
                {
                    Invoke(nameof(ShipActivEvent),1f);
                }
                
            }
            else
            {
                noMoneyPopUp.SetActive(true);
            }
        }
        else if (data.userData.ship == 1 && !data.matchData.isFue)
        {
            if (moneySystem.Buy(data.upgradesData.shipCost))
            {
                //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.shipCost,
                //    "Buy", "BuyShip_2");
                //var softPurchaseParameters = new Dictionary<string, object>();
                //softPurchaseParameters["mygame_purchased_item"] = "BuyShip_2";
                //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.shipCost, softPurchaseParameters);
                ship2.SetActive(true);
                data.userData.ship = 2;
                SaveDataSystem.Instance.SaveShipCount();
            }
            else
            {
                noMoneyPopUp.SetActive(true);
            }
        }       
    }
    private void ShipActivEvent()
    {
        fueSystem.SetBtnStep5();
        InterfaceManager.SetCurrentMenu(MenuName.Main);
    }

    public void HirePortCar()
    {
        if (data.matchData.isFue) return;
        if (data.userData.carPort == 1)
        {
            if (moneySystem.Buy(data.upgradesData.carPortCost))
            {
                //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.carPortCost,
                //    "Buy", "BuyCarPort_2");
                //var softPurchaseParameters = new Dictionary<string, object>();
                //softPurchaseParameters["mygame_purchased_item"] = "BuyCarPort_2";
                //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.carPortCost, softPurchaseParameters);
                carPort2.SetActive(true);
                data.userData.carPort = 2;
                SaveDataSystem.Instance.SaveCarPort();
            }
            else
            {
                noMoneyPopUp.SetActive(true);
            }
        }
    }
    public void HireInWarZoneCar()
    {
        if(data.matchData.isFue && data.userData.zoneInWar==1) return;
        if (data.userData.zoneInWar == 0)
        {
            if (moneySystem.Buy(data.upgradesData.carInWarCost))
            {
                //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.carInWarCost,
                //    "Buy", "BuyCarInWar_1");
                //var softPurchaseParameters = new Dictionary<string, object>();
                //softPurchaseParameters["mygame_purchased_item"] = "BuyCarInWar_1";
                //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.carInWarCost, softPurchaseParameters);
                car_In_War.SetActive(true);
                data.userData.zoneInWar = 1;
                SaveDataSystem.Instance.SaveInWarCount();
                upgradeInWarBtn.SetActive(true);
                InWarZoneCarActivEvent();                
            }
            else
            {
                noMoneyPopUp.SetActive(true);
            }
            return;
        }
        if (data.userData.zoneInWar == 1)
        {
            if (moneySystem.Buy(data.upgradesData.carInWarCost))
            {
                //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.carInWarCost,
                //    "Buy", "BuyCarInWar_2");
                //var softPurchaseParameters = new Dictionary<string, object>();
                //softPurchaseParameters["mygame_purchased_item"] = "BuyCarInWar_2";
                //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.carInWarCost, softPurchaseParameters);
                car_In_War2.SetActive(true);
                data.userData.zoneInWar = 2;
                SaveDataSystem.Instance.SaveInWarCount();
                hireInWarBtn.SetActive(false);                
            }
            else
            {
                noMoneyPopUp.SetActive(true);
            }
        }
    }
    private void InWarZoneCarActivEvent()
    {
        fueSystem.SetBtnStep8();
    }
    public void HireInsideWarZoneCar()
    {
        if (data.userData.zone2Car == 0)
        {
            if (moneySystem.Buy(data.upgradesData.carInsideWarCost))
            {

                car_Inside_War.SetActive(true);
                data.userData.zone2Car = 1;                
            }
            else
            {
                noMoneyPopUp.SetActive(true);
            }
        }
    }
    public void HireOutWarZoneCar()
    {
        if (data.matchData.isFue) return;
        if (data.userData.zone3Car == 1)
        {
             if (moneySystem.Buy(data.upgradesData.zone3CarCost))
             {
                //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Money", (float)data.upgradesData.zone3CarCost,
                //    "Buy", "BuyCarZone3");
                //var softPurchaseParameters = new Dictionary<string, object>();
                //softPurchaseParameters["mygame_purchased_item"] = "BuyOutCar_2";
                //FB.LogAppEvent(AppEventName.SpentCredits, (float)data.upgradesData.zone3CarCost, softPurchaseParameters);
                zone3Car_2.SetActive(true);
                data.userData.zone3Car = 2;
                SaveDataSystem.Instance.SaveCar3();
                hireInWarBtn.SetActive(false);
            }
            else
            {
                noMoneyPopUp.SetActive(true);
            }
        }
    }    
}
