using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class OfflineWindowSystem : MonoBehaviour
{
    [SerializeField] private AdmobRewardedSystem admobRewardedSystem;
    [SerializeField] private TextMeshProUGUI offlineIncome;
    [SerializeField] private TextMeshProUGUI timeToOffline;
    [SerializeField] private AppData data;
    [SerializeField] private GameObject offlineWindow;
    [SerializeField] private GameObject offerPopUp;
    [SerializeField] private TimerSystem timerSystem;
    [SerializeField] private Button ADBtn;
    [SerializeField] private Button crystalBtn;
    private double _allIncomeInMinut;
    private double _offlinelIncome;
    private float _timeToOffline;
    private float minutsIn1h = 60f;
    private float minutsOfflineForPay;
    private int crystallsToMult = 15;
    private bool offWindowUse;

    void Start()
    {
        Invoke(nameof(CheckShips), 5f);
    }

    private void CheckShips()
    {
        if (data.userData.ship > 0)
        {
            Invoke(nameof(ShowOfferPopUp), 2f);
        }
    }
    public void ShowOfferPopUp()
    {
        offerPopUp.SetActive(true);
    }

    public void HideOfferPopUp()
    {
        offerPopUp.SetActive(false);
        Invoke(nameof(ShowOfflineWindow), 2f);
    }
    private void ShowOfflineWindow()
    {
        if (offWindowUse) return;
        else offWindowUse = true;
        offlineWindow.SetActive(true);
        minutsOfflineForPay = minutsIn1h * data.userData.offlineLimit;
        if (data.userData.timerOff)
        {
            _timeToOffline = minutsOfflineForPay;
        }
        else
        {
            _timeToOffline = minutsOfflineForPay-timerSystem.GetTimeOffline();
        }        
        var minuts = _timeToOffline > minutsOfflineForPay ? minutsOfflineForPay : _timeToOffline;
        _allIncomeInMinut = GetCurrentAllIncome();
        _offlinelIncome = _allIncomeInMinut * minuts;
        offlineIncome.text = Converter.instance.ConvertMoneyView(_offlinelIncome);
        var timeOfline = data.userData.timerOff ? "more 2h" : ConvertMinutToHours((int)minuts);
        string firsttext = "You where away for .Your factory keeps operating where you're offline!";
        timeToOffline.text = firsttext.Insert(19, timeOfline);
        timerSystem.OnQuit();
    }

    private string ConvertMinutToHours(int minuts)
    {
        if (minuts < 60) return minuts.ToString()+"m";
        else return "1h " + (minuts - 60).ToString() + "m";
    }

    private double GetCurrentAllIncome()
    {
        var result = data.userData.headCutingMashinePromice;
        if(data.userData.fishCleaningMashine > 0) result += data.userData.fishCleaningMashinePromice;
        if (data.userData.fishCleaningMashine > 1) result += data.userData.fishCleaningMashine2Promice;
        if (data.userData.fishCleaningMashine > 2) result += data.userData.fishCleaningMashine3Promice;
        if (data.userData.headCutingMashine > 1) result += data.userData.headCutingMashine2Promice;
        if (data.userData.fileMashine > 0) result += data.userData.fileMashinePromice;
        if (data.userData.packingMashine > 0) result += data.userData.steakPackingMashinePromice;
        if (data.userData.packingMashine > 1) result += data.userData.farshPackingMashinePromice;
        if (data.userData.packingMashine > 2) result += data.userData.filePackingMashinePromice;
        return result;
    }
    public void HideOfflineWindow()
    {
        offlineWindow.SetActive(false);        
        data.userData.coins.Value += _offlinelIncome;
        SaveDataSystem.Instance.SaveMoney();
        data.userData._coins.Value = Converter.instance.ConvertMoneyView(data.userData.coins.Value);

    }

    public void ShowAD()
    {
        admobRewardedSystem.GetPlacement(RewardedBtns.offlainWindow);
        admobRewardedSystem.ShowRewardAd();
    }
    public void MultiplyOfflineIncomeForAD(int multCoef)
    {
        _offlinelIncome *= multCoef;
        offlineIncome.text = Converter.instance.ConvertMoneyView(_offlinelIncome);
        ADBtn.interactable = false;
    }

    public void MultiplyOfflineIncomeForCrystalls(int multCoef)
    {
        if(data.userData.cristalls.Value > crystallsToMult)
        {
            data.userData.cristalls.Value -= crystallsToMult;
            SaveDataSystem.Instance.SaveCrystal();
            _offlinelIncome *=multCoef;
            offlineIncome.text = Converter.instance.ConvertMoneyView(_offlinelIncome);
            crystalBtn.interactable = false;
        }
    }

    private void OnDisable()
    {
        if (!data.userData.closeGame)
        {
            data.userData.closeGame = true;
        }        
    }    

    public void OpenShop()
    {
        ShowOfferPopUp();
        HideOfflineWindow();        
    }
}
