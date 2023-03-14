using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UniRx.Extensions;

public class HeadCutMashUpgradePopUp : MonoBehaviour
{
    [SerializeField] private UpgradeSystem upgradeSystem;    
    [SerializeField] private TextMeshProUGUI promiceValue;
    //[SerializeField] private TextMeshProUGUI lowLevel;
    [SerializeField] private TextMeshProUGUI hiLevel;
    [SerializeField] private TextMeshProUGUI speedValue;
    [SerializeField] private TextMeshProUGUI speedValue2;
    [SerializeField] private TextMeshProUGUI speedUpValue;
    [SerializeField] private TextMeshProUGUI currentLevelValue;
    [SerializeField] private TMP_Text headCut_1_UpgradeCost;
    [SerializeField] private TMP_Text headCut_2_UpgradeCost;
    [SerializeField] private TMP_Text headCut_1_UpgradeSpeedCost;
    [SerializeField] private TMP_Text headCut_2_UpgradeSpeedCost;
    [SerializeField] private TMP_Text multiplierText;
    [SerializeField] private AppData data;
    [SerializeField] private Image filledImg;
    public bool mashine1;    
    private int currentLevel;    
    private float onPointerDelay = 0.5f;
    private float onSpeedPointerDelay = 0.5f;
    private bool upgradeBtnPressed;
    private bool upgradeSpeedBtnPressed;
    private float count;
    private float speedCount;
    private float lowLevelValue;
    private float hiLevelValue;
    private float levelsInScreen = 25f;
    private void OnEnable()
    {
        upgradeBtnPressed = false;
        CheckInfo();
    }
    private void Start()
    {
        if (mashine1)
        {
            data.upgradesData._headCutMashine_1_UpgradeCost.SubscribeToText(headCut_1_UpgradeCost);
            data.upgradesData._headCutMashine_1_UpgradeSpeedCost.SubscribeToText(headCut_1_UpgradeSpeedCost);
        }
        else
        {
            data.upgradesData._headCutMashine_2_UpgradeCost.SubscribeToText(headCut_2_UpgradeCost);
            data.upgradesData._headCutMashine_2_UpgradeSpeedCost.SubscribeToText(headCut_2_UpgradeSpeedCost);
        }       
              
    }
    public void CheckInfo()
    {
        
        if (mashine1)
        {
            multiplierText.text = data.userData.headCut_1_multiplierCoef.ToString();
            currentLevel = data.userData.headCutingMashineLevel;
            var promice = data.userData._headCutingMashinePromice;
            currentLevelValue.text = currentLevel.ToString();
            promiceValue.text = promice;            
            for(int i = 0; i < levelsInScreen; i++)
            {
                if(currentLevel%levelsInScreen == 0)
                {
                    //lowLevel.text = currentLevel.ToString();
                    lowLevelValue = currentLevel;
                    continue;
                }
                currentLevel--;
            }
            currentLevel = data.userData.headCutingMashineLevel;
            hiLevelValue = lowLevelValue+levelsInScreen;
            hiLevel.text = hiLevelValue.ToString();
            speedValue.text = data.userData.headCutingMashineSpeed.ToString("0.00");
            speedValue2.text = data.userData.headCutingMashineSpeed.ToString("0.00");
            speedUpValue.text = data.userData.headCutingMashineSpeedLevel.ToString();            
        }
        else
        {
            multiplierText.text = data.userData.headCut_2_multiplierCoef.ToString();
            currentLevel = data.userData.headCutingMashine2Level;
            var promice = data.userData._headCutingMashine2Promice;
            currentLevelValue.text = currentLevel.ToString();
            promiceValue.text = promice;            
            for (int i = 0; i < levelsInScreen; i++)
            {
                if (currentLevel % levelsInScreen == 0)
                {
                    //lowLevel.text = currentLevel.ToString();
                    lowLevelValue = currentLevel;
                    continue;
                }
                currentLevel--;
            }
            currentLevel = data.userData.headCutingMashine2Level;
            hiLevelValue = lowLevelValue + levelsInScreen;
            hiLevel.text = hiLevelValue.ToString();
            speedValue.text = data.userData.headCutingMashine2Speed.ToString("0.00");
            speedValue2.text = data.userData.headCutingMashine2Speed.ToString("0.00");
            speedUpValue.text = data.userData.headCutingMashine2SpeedLevel.ToString();            
        }
        float fillValue = 1f - (((float)hiLevelValue - currentLevel) / levelsInScreen);           
        filledImg.fillAmount = fillValue; 

    }
    public void OnPointerDown()
    {
        upgradeBtnPressed = true;
        UpgradeMashine();
    }
    public void OnSpeedPointerDown()
    {
        if (data.matchData.isFue) return;
        upgradeSpeedBtnPressed = true;
        UpgradeMashineSpeed();
    }
    public void OnPointerUp()
    {
        upgradeBtnPressed = false;
        onPointerDelay = 0.5f;
        count = 0;
        if(mashine1)
            SaveDataSystem.Instance.SaveCarvingMachine1Level();
        else
            SaveDataSystem.Instance.SaveCarvingMachine2Level();
    }
    public void OnSpeedPointerUp()
    {
        if (data.matchData.isFue) return;
        upgradeSpeedBtnPressed = false;
        onSpeedPointerDelay = 0.5f;
        speedCount = 0;
        if (mashine1)
            SaveDataSystem.Instance.SaveCarvingMachine1SpeedLevel();
        else
            SaveDataSystem.Instance.SaveCarvingMachine2SpeedLevel();
    }
    private void Update()
    {
        if (upgradeBtnPressed)
        {
            OnUpgradeBtnEnter();
        }
        if (upgradeSpeedBtnPressed)
        {
            OnSpeedUpgradeBtnEnter();
        }
    }
    public void OnUpgradeBtnEnter()
    {
        onPointerDelay -= Time.deltaTime;
        if (onPointerDelay < 0)
        {
            count++;
            onPointerDelay = 0.5f - 0.05f * count;
            if (onPointerDelay < 0.1f) onPointerDelay = 0.1f;
            UpgradeMashine();
        }
    }
    public void OnSpeedUpgradeBtnEnter()
    {
        onSpeedPointerDelay -= Time.deltaTime;
        if (onSpeedPointerDelay < 0)
        {
            speedCount++;
            onSpeedPointerDelay = 0.5f - 0.05f * speedCount;
            if (onSpeedPointerDelay < 0.1f) onSpeedPointerDelay = 0.1f;
            UpgradeMashineSpeed();
        }
    }
    private void UpgradeMashine()
    {
        if (mashine1) upgradeSystem.UpgradeHeadCutingMashineLevel();
        if (!mashine1) upgradeSystem.UpgradeHeadCutingMashine2Level();
        CheckInfo();
    }
    private void UpgradeMashineSpeed()
    {
        if (mashine1) upgradeSystem.UpgradeHeadCutingMashineSpeed();
        if (!mashine1) upgradeSystem.UpgradeHeadCutingMashine2Speed();
        CheckInfo();
    }
}
