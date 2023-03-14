using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class ADPlaysmentSystem : MonoBehaviour
{
    [SerializeField] private AdmobRewardedSystem admobRewardedSystem;
    [SerializeField] private UpgradeSystem upgradeSystem;
    [SerializeField] private AppData data;
    [SerializeField] private GameObject x2SpeedBtn;
    [SerializeField] private GameObject x2SpeedBtnActiv;
    [SerializeField] private GameObject x2MoneyBtn;
    [SerializeField] private GameObject x2MoneyBtnActiv;
    [SerializeField] private GameObject VIPBtn;
    [SerializeField] private GameObject moreFishBtn;
    [SerializeField] private EggsDepositories fishDepo;
    [SerializeField] private GameObject x2SpeedWindow;
    [SerializeField] private Image filledX2SpeedBar;
    [SerializeField] private GameObject x2MoneyWindow;
    [SerializeField] private GameObject vipWindow;
    [SerializeField] private TextMeshProUGUI moneyVip;
    [SerializeField] private GameObject moreFishWindow;
    [SerializeField] private Image filledFishBar;
    private float x2pullBackTimer = 180f;
    private float vipBtnInScreen = 60f;
    private bool isX2SpeedBtn;
    private float x2SpeedFilledTime;
    private float moreFishBackTimer = 120f;
    private float moreFishBtnInScreen = 15f;
    private int fishPaletNumber = 3;
    private float vipMultCoef = 3.2f;
    private float x2Time = 120f;
    private double _allIncomeInMinut;
    private bool isMoreFishBtn;
    private float fishFilledTime;
    private bool addSpeed;
    private bool addMoney;


    private void Start()
    {
        data.userData.ADMultiplier = 1;
        data.userData.ADSpeedMultiplier = 1;        
        StartCoroutine(nameof(VIPWindow));
        Invoke(nameof(StartFishBtn), vipBtnInScreen);
        StartCoroutine(nameof(X2MoneyWindow));
        StartCoroutine(nameof(X2SpeedWindow));
    }


    #region X2Speed
    public void ShowX2SpeedWindow()
    {
        x2SpeedWindow.SetActive(true);
    }
    public void ShowSpeedAD() //ok btn inPopUp
    {
        admobRewardedSystem.GetPlacement(RewardedBtns.increaseSpeedBtn);        
    }
    public void AddSpeed()
    {
        Debug.Log("AddSpeed");
        HideX2SpeedWindow();
        data.userData.ADSpeedMultiplier = 2;
        addSpeed = true;
        //x2SpeedBtnActiv.SetActive(true);
        upgradeSystem.UpgradeAllSpeeds();        
        Invoke(nameof(ResetSpeedMultiplier), x2Time);
    }
    
    private void ResetSpeedMultiplier()
    {
        data.userData.ADSpeedMultiplier = 1;
        upgradeSystem.UpgradeAllSpeeds();
        x2SpeedBtnActiv.SetActive(false);        
    }

    public void HideX2SpeedWindow()   //cancel Btn inPopUp
    {
        x2SpeedWindow.SetActive(false);
        x2SpeedBtn.SetActive(false);
    }

    private void ShowSpeedBtn()
    {
        x2SpeedFilledTime = moreFishBtnInScreen;
        isX2SpeedBtn = true;
        x2SpeedBtn.SetActive(true);
    }
    private IEnumerator X2SpeedWindow()
    {
        while (true)
        {
            x2SpeedFilledTime = moreFishBtnInScreen;
            isX2SpeedBtn = true;
            x2SpeedBtn.SetActive(true);
            yield return new WaitForSeconds(moreFishBtnInScreen);
            x2SpeedBtn.SetActive(false);
            isX2SpeedBtn = false;
            yield return new WaitForSeconds(x2pullBackTimer-10);
        }
    }

    #endregion

    #region X2MoneyWindow

    public void ShowMoneyAD()
    {
        admobRewardedSystem.GetPlacement(RewardedBtns.increaseMoneyBtn);        
    }
    public void Getx2Money()
    {
        HideX2MoneyWindow();
        addMoney = true;
        //x2MoneyBtnActiv.SetActive(true);
        data.userData.ADMultiplier = 2;        
        Invoke(nameof(ResetMultiplier), x2Time);
    }
    private void ResetMultiplier()
    {
        data.userData.ADMultiplier = 1;
        x2MoneyBtnActiv.SetActive(false);
    }

    public void ShowX2MoneyWindow()
    {
        x2MoneyWindow.SetActive(true);
    }

    public void HideX2MoneyWindow()
    {
        x2MoneyWindow.SetActive(false);
        x2MoneyBtn.SetActive(false);
    }
    private IEnumerator X2MoneyWindow()
    {
        while (true)
        {
            x2MoneyBtn.SetActive(true);           
            yield return new WaitForSeconds(vipBtnInScreen);
            x2MoneyBtn.SetActive(false);            
            yield return new WaitForSeconds(x2pullBackTimer+20);
        }
    }
    #endregion

    #region FishAddWindow
    public void ShowMoreFishWindow()
    {
        moreFishWindow.SetActive(true);
    }

    public void ShowFishAD()
    {
        admobRewardedSystem.GetPlacement(RewardedBtns.addFishBtn);        
    }
    public void AddFish()
    {
        HideMoreFishWindow();
        moreFishBtn.SetActive(false);
        for (int i = 0; i < fishPaletNumber; i++)
        {
            fishDepo.AddBox();
        }
    }
    public void HideMoreFishWindow()
    {
        moreFishWindow.SetActive(false);
        moreFishBtn.SetActive(false);
    }
    private IEnumerator MoreFishWindow()
    {
        while (true)
        {
            moreFishBtn.SetActive(true);            
            fishFilledTime = moreFishBtnInScreen;
            isMoreFishBtn = true;
            yield return new WaitForSeconds(moreFishBtnInScreen);
            moreFishBtn.SetActive(false);
            isMoreFishBtn = false;
            yield return new WaitForSeconds(moreFishBackTimer-10);
        }
    }
    private void StartFishBtn()
    {
        StartCoroutine(nameof(MoreFishWindow));
    }
    #endregion

    #region VIPWindow
    private void GetIncome()
    {
        _allIncomeInMinut = GetCurrentAllIncome()*vipMultCoef;        
    }
    private double GetCurrentAllIncome()
    {
        var result = data.userData.headCutingMashinePromice;
        if (data.userData.fishCleaningMashine > 0) result += data.userData.fishCleaningMashinePromice;
        if (data.userData.fishCleaningMashine > 1) result += data.userData.fishCleaningMashine2Promice;
        if (data.userData.fishCleaningMashine > 2) result += data.userData.fishCleaningMashine3Promice;
        if (data.userData.headCutingMashine > 1) result += data.userData.headCutingMashine2Promice;
        if (data.userData.fileMashine > 0) result += data.userData.fileMashinePromice;
        if (data.userData.packingMashine > 0) result += data.userData.steakPackingMashinePromice;
        if (data.userData.packingMashine > 1) result += data.userData.farshPackingMashinePromice;
        if (data.userData.packingMashine > 2) result += data.userData.filePackingMashinePromice;
        return result;
    }
    public void ShowVipAD()
    {
        admobRewardedSystem.GetPlacement(RewardedBtns.vipBtn);        
    }
    public void ShowADInVIPWindow()
    {
        data.userData.coins.Value += _allIncomeInMinut;
        data.userData._coins.Value = Converter.instance.ConvertMoneyView(data.userData.coins.Value);
        HideVIPWindow();
        VIPBtn.SetActive(false);
    }

    public void ShowVIPWindow()
    {
        GetIncome();
        vipWindow.SetActive(true);
        moneyVip.text = Converter.instance.ConvertMoneyView(_allIncomeInMinut);
    }
    public void HideVIPWindow()
    {
        vipWindow.SetActive(false);
        VIPBtn.SetActive(false);
    }

    private IEnumerator VIPWindow()
    {
        while (true)
        {
            VIPBtn.SetActive(true);
            yield return new WaitForSeconds(vipBtnInScreen);
            VIPBtn.SetActive(false);
            yield return new WaitForSeconds(x2pullBackTimer-20);
        }
    }

    #endregion

    private void Update()
    {
        if (isMoreFishBtn)
        {
            fishFilledTime -= Time.deltaTime;
            filledFishBar.fillAmount = 1f - (fishFilledTime / moreFishBtnInScreen);            
        }
        if (isX2SpeedBtn)
        {
            x2SpeedFilledTime -= Time.deltaTime;
            filledX2SpeedBar.fillAmount = 1f - (x2SpeedFilledTime / moreFishBtnInScreen);
        }
        if (addSpeed)
        {
            x2SpeedBtnActiv.SetActive(true);
            addSpeed = false;
        }
        if (addMoney)
        {
            x2MoneyBtnActiv.SetActive(true);
            addMoney = false;
        }
    }
}
