using UnityEngine;


public class ShopSystem : MonoBehaviour
{
    [SerializeField] private AdjustEventsSystem adjustEvents;
    [SerializeField] private GameObject noPurshaseWindow;
    [SerializeField] private MatchData matchData;
    [SerializeField] private TimeCountSystem _timeCountSystem;
    [SerializeField] private GameObject pushBtnPopUp;
    [SerializeField] private GameObject noCrystallsPopUp;
    [SerializeField] private TimeTravelScreen timeTravelScreen;
    [SerializeField] private TimerSystem timerSystem;
    [SerializeField] private float moneyForOffer = 5.99f;
    [SerializeField] private float moneyFor50Crystalls = 1.99f;
    [SerializeField] private float moneyFor150Crystalls = 4.99f;
    [SerializeField] private float moneyFor400Crystalls = 9.99f;
    [SerializeField] private float moneyFor900Crystalls = 19.99f;
    [SerializeField] private float moneyFor2600Crystalls = 49.99f;
    [SerializeField] private int goldFor5Crystalls = 7;
    [SerializeField] private int goldFor10Crystalls = 10;
    [SerializeField] private int goldFor50Crystalls = 70;
    [SerializeField] private AppData data;
    [SerializeField] private GameObject diamondsScreen;
    [SerializeField] private SwipeControl swipeControl;
    private int offerCrystals = 250;
    private int offerGold = 100;
    private int offerOfflineHour = 10;
    
    
    public void OnShopButton()
    {
        InterfaceManager.SetCurrentMenu(MenuName.Main);
        
    }
    
    public void ShowDiamondsScreen()
    {
        if (data.matchData.isFue) return;
        diamondsScreen.SetActive(true);
        swipeControl.inMenu = true;
    }
    public void HideDiamondsScreen()
    {
        diamondsScreen.SetActive(false);
        swipeControl.inMenu = false;
    }
    
    private void EventFor50Crystal()
    {
        adjustEvents.AdustEventForPurshase(moneyFor50Crystalls, "CrystalPack_1", "50 crystals");
    }
    private void EventFor150Crystal()
    {
        adjustEvents.AdustEventForPurshase(moneyFor150Crystalls, "CrystalPack_2", "150 crystals");
    }
    private void EventFor400Crystal()
    {
        adjustEvents.AdustEventForPurshase(moneyFor400Crystalls, "CrystalPack_3", "400 crystals");
    }
    private void EventFor900Crystal()
    {
        adjustEvents.AdustEventForPurshase(moneyFor900Crystalls, "CrystalPack_4", "900 crystals");
    }
    private void EventFor2600Crystal()
    {
        adjustEvents.AdustEventForPurshase(moneyFor2600Crystalls, "CrystalPack_5", "2600 crystals");
    }

    
    public void AddCrystals(int money)
    {
        if (money == 5)
        {
            if (matchData.isCrystal)
            {
                _timeCountSystem.StartTimerC();
                //GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Crystal", 5f,"Shop", "FreeCrystals");
                data.userData.cristalls.Value += money;
                SaveDataSystem.Instance.SaveCrystal();
            }
            else
            {
                pushBtnPopUp.SetActive(true);
            }
            return;
        }
        else if (money == 50)
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Crystal", 50f, "IAP", "Purshase 1.99USD");
            EventFor50Crystal();
            data.userData.cristalls.Value += money;            
            SaveDataSystem.Instance.SaveCrystal();
        }
        else if (money == 150)
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Crystal", 150f, "IAP", "Purshase 4.99USD");
            EventFor150Crystal();
            data.userData.cristalls.Value += money;
            SaveDataSystem.Instance.SaveCrystal();
        }
        else if (money == 400)
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Crystal", 400f, "IAP", "Purshase 9.99USD");
            EventFor400Crystal();
            data.userData.cristalls.Value += money;
            SaveDataSystem.Instance.SaveCrystal();
        }
        else if (money == 900)
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Crystal", 900f, "IAP", "Purshase 19.99USD");
            EventFor900Crystal();
            data.userData.cristalls.Value += money;
            SaveDataSystem.Instance.SaveCrystal();
        }
        else if (money == 2600)
        {
            //GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Crystal", 2600f, "IAP", "Purshase 49.99USD");
            EventFor2600Crystal();
            data.userData.cristalls.Value += money;
            SaveDataSystem.Instance.SaveCrystal();
        }
        if (!data.userData.firstPurshase)
        {
            adjustEvents.AdustEventForFirstPurshase();
            data.userData.firstPurshase = true;
            SaveDataSystem.Instance.SaveFirstPurshase();
        }
        SaveDataSystem.Instance.SaveCrystal();
    }
    public void GetOffer()
    {
        data.userData.cristalls.Value += offerCrystals;
        SaveDataSystem.Instance.SaveCrystal();
        data.userData.gold.Value += offerGold;
        SaveDataSystem.Instance.SaveGold();
        data.userData.offlineLimit = offerOfflineHour;
        SaveDataSystem.Instance.SaveOfflineLimit();
        data.userData.getOffer = true;
        SaveDataSystem.Instance.SaveGetOffer();
        Invoke(nameof(EventForOffer), 2f);
        if (!data.userData.firstPurshase)
        {
            adjustEvents.AdustEventForFirstPurshase();
            data.userData.firstPurshase = true;
            SaveDataSystem.Instance.SaveFirstPurshase();
        }
    } 
    
    private void EventForOffer()
    {
        //GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Offer", 250,
        //            "Shop", "250Crystal, 100Gold, 10hOffline");
        adjustEvents.AdustEventForPurshase(moneyForOffer, "Offer", "250 crystals, 100 gold, 10h offline");
    }
    public void NoPurshase()
    {
        noPurshaseWindow.SetActive(true);
    }

    private bool GetBuy(float money)
    {
        return true;
    }
    public void AddMoneyForCrystalls(int value)
    {
        if (value == 10)
        {
            if (data.userData.cristalls.Value >= value)
            {
                //GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Money", (float)timeTravelScreen.moneyFor10min,
                //    "Shop", "10Crystal");
                //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Crystal", value,"Shop", "BuyTimeTravelfor10min");
                data.userData.coins.Value += timeTravelScreen.moneyFor10min;
                SaveDataSystem.Instance.SaveMoney();
                data.userData._coins.Value = Converter.instance.ConvertMoneyView(data.userData.coins.Value);
                data.userData.cristalls.Value -= value;
                SaveDataSystem.Instance.SaveCrystal();
            }
            else
            {
                noCrystallsPopUp.SetActive(true);
            }
        }
        else if (value == 25)
        {
            if (data.userData.cristalls.Value >= value)
            {
                //GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Money", (float)timeTravelScreen.moneyFor30min,
                //    "Shop", "25Crystal");
                //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Crystal", value, "Shop", "BuyTimeTravelfor25min");
                data.userData.coins.Value += timeTravelScreen.moneyFor30min;
                SaveDataSystem.Instance.SaveMoney();
                data.userData._coins.Value = Converter.instance.ConvertMoneyView(data.userData.coins.Value);
                data.userData.cristalls.Value -= value;
                SaveDataSystem.Instance.SaveCrystal();
            }
            else
            {
                noCrystallsPopUp.SetActive(true);
            }
        }
        else
        {
            if (data.userData.cristalls.Value >= value)
            {
                //GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Money", (float)timeTravelScreen.moneyFor1hour,
                //    "Shop", "40Crystal");
                //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Crystal", value, "Shop", "BuyTimeTravelfor1hour");
                data.userData.coins.Value += timeTravelScreen.moneyFor1hour;
                SaveDataSystem.Instance.SaveMoney();
                data.userData._coins.Value = Converter.instance.ConvertMoneyView(data.userData.coins.Value);
                data.userData.cristalls.Value -= value;
                SaveDataSystem.Instance.SaveCrystal();
            }
            else
            {
                noCrystallsPopUp.SetActive(true);
            }
        }

    }
    public void AddGoldForCrystalls(int value)
    {
        if (value == 5)
        {
            if (data.userData.cristalls.Value >= value)
            {
                //GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Gold", (float)goldFor5Crystalls,
                //    "Shop", "5_Crystal");
                //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Crystal", value, "Shop", "Buy_7_Gold");
                data.userData.gold.Value += goldFor5Crystalls;
                SaveDataSystem.Instance.SaveGold();
                data.userData._gold.Value = Converter.instance.ConvertMoneyView(data.userData.gold.Value);
                data.userData.cristalls.Value -= value;
                SaveDataSystem.Instance.SaveCrystal();
                data.userData._crystalls.Value = Converter.instance.ConvertMoneyView(data.userData.cristalls.Value);
            }
            else
            {
                noCrystallsPopUp.SetActive(true);
            }
        }
        else if (value == 10)
        {
            if (data.userData.cristalls.Value >= value)
            {
                //GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Gold", (float)goldFor10Crystalls,
                //    "Shop", "10_Crystal");
                //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Crystal", value, "Shop", "Buy_`10_Gold");
                data.userData.gold.Value += goldFor10Crystalls;
                SaveDataSystem.Instance.SaveGold();
                data.userData._gold.Value = Converter.instance.ConvertMoneyView(data.userData.gold.Value);
                data.userData.cristalls.Value -= value;
                SaveDataSystem.Instance.SaveCrystal();
                data.userData._crystalls.Value = Converter.instance.ConvertMoneyView(data.userData.cristalls.Value);
            }
            else
            {
                noCrystallsPopUp.SetActive(true);
            }
        }
        else
        {
            if (data.userData.cristalls.Value >= value)
            {
                //GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Gold", (float)goldFor50Crystalls,
                //    "Shop", "50_Crystal");
                //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Crystal", value, "Shop", "Buy_70_Gold");
                data.userData.gold.Value += goldFor50Crystalls;
                SaveDataSystem.Instance.SaveGold();
                data.userData._gold.Value = Converter.instance.ConvertMoneyView(data.userData.gold.Value);
                data.userData.cristalls.Value -= value;
                SaveDataSystem.Instance.SaveCrystal();
                data.userData._crystalls.Value = Converter.instance.ConvertMoneyView(data.userData.cristalls.Value);
            }
            else
            {
                noCrystallsPopUp.SetActive(true);
            }
        }

    }     
}
