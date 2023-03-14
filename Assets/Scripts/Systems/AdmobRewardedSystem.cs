using UnityEngine;
using GoogleMobileAds.Api;
using System;

public enum RewardedBtns
{
    offlainWindow,
    increaseSpeedBtn,
    increaseMoneyBtn,
    addFishBtn,
    vipBtn,
    dontWaitNewOrders,
    orderIncomex2,
    none
}

public class AdmobRewardedSystem : MonoBehaviour
{
    public Action<RewardedBtns> rewardEvent;
    private RewardedAd rewardedAd;
    private RewardedBtns currentBtn;
    [SerializeField] private AppData data;
    [SerializeField] private AdjustEventsSystem adjustEvents;
    [SerializeField] private OfflineWindowSystem offlineWindowSystem;
    [SerializeField] private ADPlaysmentSystem aDPlaysmentSystem;
    [SerializeField] private OrdersSystem ordersSystem;
    [SerializeField] private GameObject noInetPopUp;
    private string placement;
    

    private void Start()
    {
        MobileAds.Initialize(initStatus => { });
        Invoke(nameof(InitAD),3f);
        currentBtn = RewardedBtns.none;
        rewardEvent += GetPriz;
    }
    private void OnDisable()
    {
        rewardEvent -= GetPriz;
    }
    void InitAD()
    {
        CreateAndLoadRewardedAd();        
    }
    
    private void LoadReward()
    {
        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);        
    }        

    private void HandleUserEarnedReward(object sender, Reward e)
    {
        rewardEvent?.Invoke(currentBtn);
        //GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.RewardedVideo, "admob - UserEarnedReward", placement);
    }
    
    public void GetPriz(RewardedBtns btn)
    {
        adjustEvents.AdustEventForReward(placement);
        switch (btn)
        {
            case RewardedBtns.offlainWindow:
                offlineWindowSystem.MultiplyOfflineIncomeForAD(2);
                //placement = "Multiply_Offline_Income";
                break;
            case RewardedBtns.increaseSpeedBtn:
                aDPlaysmentSystem.AddSpeed();
                //placement = "Multiply_Speed_x2";
                break;
            case RewardedBtns.vipBtn:
                aDPlaysmentSystem.ShowADInVIPWindow();
                //placement = "VIP_Investor_Money";
                break;
            case RewardedBtns.increaseMoneyBtn:
                aDPlaysmentSystem.Getx2Money();
                //placement = "Multiply_Money_x2";
                break;
            case RewardedBtns.addFishBtn:
                aDPlaysmentSystem.AddFish();
                //placement = "Get_3_Boxes_Off_Fish";
                break;
            case RewardedBtns.dontWaitNewOrders:
                ordersSystem.ResetTimerForAD();
                //placement = "Dont_Wait_120_second_in_Orders";
                break;
            case RewardedBtns.orderIncomex2:
                ordersSystem.AcceptForADOrder();
                //placement = "Multiply_Order_Income_x2";
                break;
        }
    }

    public void GetPlacement(RewardedBtns btn)
    {
        currentBtn = btn;        
        switch (currentBtn)
        {
            case RewardedBtns.offlainWindow:
                Debug.Log("offlineIncomex2");
                placement = "Multiply_Offline_Income";
                break;
            case RewardedBtns.increaseSpeedBtn:
                Debug.Log("increaseSpeed");
                placement = "Multiply_Speed_x2";
                ShowRewardAd();
                break;
            case RewardedBtns.vipBtn:
                Debug.Log("VIP");
                placement = "VIP_Investor_Money";
                ShowRewardAd();
                break;
            case RewardedBtns.increaseMoneyBtn:
                Debug.Log("increaseMoney");
                placement = "Multiply_Money_x2";
                ShowRewardAd();
                break;
            case RewardedBtns.addFishBtn:
                Debug.Log("AddFish");
                placement = "Get_3_Boxes_Off_Fish";
                ShowRewardAd();
                break;
            case RewardedBtns.dontWaitNewOrders:
                Debug.Log("DontWait");
                placement = "Dont_Wait_120_second_in_Orders";
                break;
            case RewardedBtns.orderIncomex2:
                Debug.Log("orderIncomex2");
                placement = "Multiply_Order_Income_x2";
                break;
        }
    }
    public void ShowRewardAd()
    {
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
            //GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.RewardedVideo, "admob - startAD", placement);
        }
        else
        {
            noInetPopUp.SetActive(true);
            //GameAnalytics.NewAdEvent(GAAdAction.FailedShow, GAAdType.RewardedVideo, "admob - failed", placement);
        }        
    }
    public void CreateAndLoadRewardedAd()
    {
        string adUnitId = "ca-app-pub-1508151491850635/8986572494";// "ca-app-pub-1508151491850635/8986572494";//test"ca-app-pub-3940256099942544/5224354917";
        rewardedAd = new RewardedAd(adUnitId);        
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        rewardedAd.LoadAd(request);
    }

    public void HandleRewardedAdClosed(object sender, System.EventArgs args)
    {
        CreateAndLoadRewardedAd();
    }
    
}


