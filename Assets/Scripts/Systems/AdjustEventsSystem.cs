using System.Collections.Generic;
using UnityEngine;



public class AdjustEventsSystem : MonoBehaviour
{
    [SerializeField] private AppData data;
    [SerializeField] private AdmobRewardedSystem admobRewarded;
    [SerializeField] private TimeCountSystem timeCount;
    private string placement;
    private string machine;
    private int stepIndex;
    private string stepName;
    //private string _currency = "USD";
    private int _price;
    private string _productId;
    private float _doublePrice;
    private string level;
    private string _transactionId = "No update";
    private string _itemCount;
    private string _receipt;
    public struct Receipt
    {
        public string Store;
        public string TransactionID;
        public string Payload;
    }

    // Additional information about the IAP for Android.
    [System.Serializable]
    public struct PayloadAndroid
    {
        public string Json;
        public string Signature;
    }
    private void Start()
    {
        Invoke(nameof(GetLevelInfo), 3f);
    }

    private void GetLevelInfo()
    {
        machine = data.userData.levelUser;
        //GameAnalytics.NewDesignEvent("StartGame. Number of starts = " +data.userData.startGame+ " Day since reg ="+ timeCount.GetTimeG());        
        //AppMetrica.Instance.ResumeSession();
        //AppMetrica.Instance.ReportEvent("Start_Game - "+data.userData.startGame+ " Day since reg - "+ timeCount.GetTimeG());
        data.userData.startGame++;
        SaveDataSystem.Instance.SaveStartsGame();
        //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, machine, timeCount.GetTimeG());
       // AppMetrica.Instance.ReportEvent("Start_Level - " + machine + " Day since reg - " + timeCount.GetTimeG());
        //AppMetrica.Instance.SendEventsBuffer();
        //var tutParams = new Dictionary<string, object>();
        //tutParams[AppEventParameterName.ContentID] = "Start_Game_in_Level - " + machine;
        //FB.LogAppEvent(AppEventName.AchievedLevel, parameters: tutParams);
    }
    public void OnPurchaseComplete(UnityEngine.Purchasing.Product _product)
    {
        _receipt = _product.receipt;
        string substring = "GPA";
        int indexOfSubstring = _receipt.IndexOf(substring);
        if (indexOfSubstring < 0) return;
        var endIndex = 24;
        _transactionId = _receipt.Substring(indexOfSubstring, endIndex);
        AdjustCallBackPurshase();
        /*var product = _product;
        if (null!=product)
        {
            string currency = product.metadata.isoCurrencyCode;
            decimal price = product.metadata.localizedPrice;

            // Creating the instance of the YandexAppMetricaRevenue class.
            //YandexAppMetricaRevenue revenue = new YandexAppMetricaRevenue(price, currency);
            if (product.receipt != null)
            {
                // Creating the instance of the YandexAppMetricaReceipt class.
                //YandexAppMetricaReceipt yaReceipt = new YandexAppMetricaReceipt();
                Receipt receipt = JsonUtility.FromJson<Receipt>(product.receipt);
#if UNITY_ANDROID
                PayloadAndroid payloadAndroid = JsonUtility.FromJson<PayloadAndroid>(receipt.Payload);
                yaReceipt.Signature = payloadAndroid.Signature;
                yaReceipt.Data = payloadAndroid.Json;
#elif UNITY_IPHONE
            yaReceipt.TransactionID = receipt.TransactionID;
            yaReceipt.Data = receipt.Payload;
#endif
                revenue.Receipt = yaReceipt;
            }
            // Sending data to the AppMetrica server.
            AppMetrica.Instance.ReportRevenue(revenue);
        }*/
    }

      
    
    #region RewardEvents
    public void AdustEventForReward(string _placement)
    {
        placement = _placement;        
        //admobRewarded.GetPriz();
        Invoke(nameof(SetEvent), 2f);
    }
    private void SetEvent()
    {
        AdjustCallBackReward();
    }
    
    private void AdjustCallBackReward()
    {
        Debug.Log("REWARDED WATCHED EVENT");
        //var tutParams = new Dictionary<string, object>();
        //tutParams[AppEventParameterName.ContentID] = placement;               
        //FB.LogAppEvent("REWARDED WATCHED EVENT", parameters: tutParams);
        //AppMetrica.Instance.ReportEvent("REWARDED WATCHED EVENT. Ad_type - Rewarded. Placement - " + placement + " Day since reg - " + timeCount.GetTimeG());
        /*AdjustEvent curEvent = new AdjustEvent("jfre1r");
        curEvent.addCallbackParameter("placement", placement);
        curEvent.addCallbackParameter("time", data.userData.allPlayTimeInMinutes.ToString());
        curEvent.addCallbackParameter("event_name", placement + " event");
        Adjust.trackEvent(curEvent);*/
    }

    #endregion

    #region ProgresEvent
    public void AdustEventForProgres(string _machine)
    {
        //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, machine, timeCount.GetTimeG());
        //AppMetrica.Instance.ReportEvent("Finish_Level - " + machine + " Day since reg - " + timeCount.GetTimeG());
        //AppMetrica.Instance.SendEventsBuffer();        
        machine = _machine;
        AdjustCallBackProgres();
    }
    private void AdjustCallBackProgres()
    {
        Debug.Log("PROGRESS EVENT!");        
        //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, machine, timeCount.GetTimeG());
        //AppMetrica.Instance.ReportEvent("Start_Level - " + machine + " Day since reg - " + timeCount.GetTimeG());
        //AppMetrica.Instance.SendEventsBuffer();
        //var tutParams = new Dictionary<string, object>();
        //tutParams[AppEventParameterName.ContentID] = "Start_Level - " + machine;            
        //FB.LogAppEvent(AppEventName.AchievedLevel,parameters: tutParams);
        
        /*AdjustEvent curEvent = new AdjustEvent("rfig3p");
        curEvent.addCallbackParameter("machine_name", machine);
        curEvent.addCallbackParameter("status", "open");
        curEvent.addCallbackParameter("time", data.userData.allPlayTimeInMinutes.ToString());        
        Adjust.trackEvent(curEvent);*/
    }

    #endregion

    #region TimerEvent
    public void AdustEventForTime()
    {
        AdjustCallBackTime();
    }
    private void AdjustCallBackTime()
    {
        //GameAnalytics.NewDesignEvent("Total_PlayTime", data.userData.allPlayTimeInMinutes);
        //AppMetrica.Instance.ReportEvent("Total_PlayTime - " + data.userData.allPlayTimeInMinutes);
        //var tutParams = new Dictionary<string, object>();
        //tutParams[AppEventParameterName.NumItems] = data.userData.allPlayTimeInMinutes.ToString();
        //FB.LogAppEvent("Total_PlayTime", parameters: tutParams);
        /* AdjustEvent curEvent = new AdjustEvent("uwra8m");        
         curEvent.addCallbackParameter("time", data.userData.allPlayTimeInMinutes.ToString());
         Adjust.trackEvent(curEvent);*/
    }
    #endregion

    #region FirstPurshase
    public void AdustEventForFirstPurshase()
    {
        AdjustCallBackFirstPurshase();
    }
    private void AdjustCallBackFirstPurshase()
    {
        //Debug.Log("FIRST PURCHASE!!!");
        /*AdjustEvent curEvent = new AdjustEvent("bj5b6q");
        curEvent.addCallbackParameter("time", data.userData.allPlayTimeInMinutes.ToString());
        Adjust.trackEvent(curEvent);*/
    }
    #endregion

    #region TutorialEvent
    public void AdustEventForTutorial(int _index, string _name)
    {
        stepIndex = _index;
        stepName = _name;
        AdjustCallBackTutorial();
    }
    private void AdjustCallBackTutorial()
    {
        /*AdjustEvent curEvent = new AdjustEvent("90gef5");
        curEvent.addCallbackParameter("step", stepIndex.ToString());
        curEvent.addCallbackParameter("step_name", stepName);
        curEvent.addCallbackParameter("time", data.userData.allPlayTimeInMinutes.ToString());
        Adjust.trackEvent(curEvent);*/
    }
    #endregion

    #region PurshaseEvents

    
    public void AdustEventForPurshase(float price, string productID, string itemCount)
    {
        _price = (int)(price*100);
        _productId = productID;
        _doublePrice = price;
        _itemCount = itemCount;
        level = data.userData.levelUser;
        //var iapParameters = new Dictionary<string, object>();
        //iapParameters["mygame_packagename"] = _productId;
       // FB.LogPurchase(price,_currency,iapParameters);
    }
    private void AdjustCallBackPurshase()
    {
        Debug.Log("PURCHASE EVENT");
        //Debug.Log("CURRENCY = " + _currency + " PRICE = " + _price + " ID = " + _productId  +" TRANSACTION ID = " + _transactionId);
        //GameAnalytics.NewBusinessEventGooglePlay(_currency, _price, _productId,
            //_itemCount, "VISA", _receipt, _transactionId);
        
        /*AdjustEvent curEvent = new AdjustEvent("n4vaw5");
        curEvent.setRevenue(_doublePrice, _currency);
        curEvent.setTransactionId(_transactionId);
        curEvent.addCallbackParameter("revenue", _price);        
        curEvent.addCallbackParameter("product_id", _productId);
        curEvent.addCallbackParameter("transaction_id", _transactionId);
        curEvent.addCallbackParameter("time", data.userData.allPlayTimeInMinutes.ToString());
        curEvent.addCallbackParameter("level", level);
        Adjust.trackEvent(curEvent);*/
    }
    #endregion




}
