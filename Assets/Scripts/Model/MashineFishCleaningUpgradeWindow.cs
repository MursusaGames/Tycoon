using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UniRx.Extensions;

public class MashineFishCleaningUpgradeWindow : MonoBehaviour
{
    [SerializeField] private UpgradeSystem upgradeSystem;     
    [SerializeField] private TextMeshProUGUI promiceValue;
    [SerializeField] private TMP_Text multiplierText;
    [SerializeField] private TMP_Text fishClean_1_upgradeCost;
    [SerializeField] private TMP_Text fishClean_2_upgradeCost;
    [SerializeField] private TMP_Text fishClean_3_upgradeCost;
    [SerializeField] private TMP_Text steakMashine_upgradeCost;
    [SerializeField] private TMP_Text farshMashine_upgradeCost;
    [SerializeField] private TMP_Text fileMashine_upgradeCost;
    [SerializeField] private TMP_Text packingMashine_upgradeCost;
    [SerializeField] private TMP_Text fishClean_1_upgradeSpeedCost;
    [SerializeField] private TMP_Text fishClean_2_upgradeSpeedCost;
    [SerializeField] private TMP_Text fishClean_3_upgradeSpeedCost;
    [SerializeField] private TMP_Text steakMashine_upgradeSpeedCost;
    [SerializeField] private TMP_Text farshMashine_upgradeSpeedCost;
    [SerializeField] private TMP_Text fileMashine_upgradeSpeedCost;
    [SerializeField] private TMP_Text packingMashine_upgradeSpeedCost;
    [SerializeField] private TextMeshProUGUI speedValue;
    [SerializeField] private TextMeshProUGUI speedValue2;
    [SerializeField] private TextMeshProUGUI capasityValue;
    [SerializeField] private TextMeshProUGUI currentLevelValue;
    [SerializeField] private TextMeshProUGUI lowLevel;
    [SerializeField] private TextMeshProUGUI hiLevel;
    [SerializeField] private AppData data;
    [SerializeField] private Image filledImg;
    public bool mashine1;
    public bool mashine2;
    public bool mashine0;
    public bool steak;
    public bool farsh;
    public bool file;
    public bool packing1;
    public bool packing2;
    public bool packing3;
    public bool carInPort;
    public bool carInWar;
    public bool carZone1;
    public bool carZone2;
    public bool carZone3;
    public bool carOut;
    public bool ship;
    public bool ship1;
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
        CheckInfo();
    }
    
    public void CheckInfo()
    {

        if (mashine1)
        {
            fishClean_2_upgradeCost.text = data.upgradesData._fishCleanMashine_2_UpgradeCost.ToString();
            fishClean_2_upgradeSpeedCost.text = data.upgradesData._fishCleanMashine_2_UpgradeSpeedCost.ToString();
            multiplierText.text = data.userData.fishClean_2_multiplierCoef.ToString();
            currentLevel = data.userData.fishCleaningMashine2Level;
            var promice = data.userData._fishCleaningMashine2Promice;
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
            currentLevel = data.userData.fishCleaningMashine2Level;
            hiLevelValue = lowLevelValue + levelsInScreen;
            hiLevel.text = hiLevelValue.ToString();
            speedValue.text = data.userData.fishCleaningMashine2SpeedLevel.ToString();
            capasityValue.text = data.userData.fishCleaningMashine2Speed.ToString("0.0");
            speedValue2.text = data.userData.fishCleaningMashine2Speed.ToString("0.0");
        }

        if (mashine2)
        {
            fishClean_3_upgradeSpeedCost.text = data.upgradesData._fishCleanMashine_3_UpgradeSpeedCost.ToString();
            fishClean_3_upgradeCost.text = data.upgradesData._fishCleanMashine_3_UpgradeCost.ToString();
            multiplierText.text = data.userData.fishClean_3_multiplierCoef.ToString();
            currentLevel = data.userData.fishCleaningMashine3Level;
            var promice = data.userData._fishCleaningMashine3Promice;
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
            currentLevel = data.userData.fishCleaningMashine3Level;
            hiLevelValue = lowLevelValue + levelsInScreen;
            hiLevel.text = hiLevelValue.ToString();
            speedValue.text = data.userData.fishCleaningMashine3SpeedLevel.ToString();
            capasityValue.text = data.userData.fishCleaningMashine3Speed.ToString("0.0");
            speedValue2.text = data.userData.fishCleaningMashine3Speed.ToString("0.0");
        }
        if (steak)
        {
            steakMashine_upgradeSpeedCost.text = data.upgradesData._steakMashine_UpgradeSpeedCost.ToString();
            steakMashine_upgradeCost.text = data.upgradesData._steakMashine_UpgradeCost.ToString();
            multiplierText.text = data.userData.steakMashine_multiplierCoef.ToString();
            currentLevel = data.userData.steakMashineLevel;
            var promice = data.userData._steakMashinePromice;
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
            currentLevel = data.userData.steakMashineLevel;
            hiLevelValue = lowLevelValue + levelsInScreen;
            hiLevel.text = hiLevelValue.ToString();
            speedValue.text = data.userData.steakMashineSpeedLevel.ToString();
            capasityValue.text = data.userData.steakMashineSpeed.ToString("0.0");
            speedValue2.text = data.userData.steakMashineSpeed.ToString("0.0");
        }
        if (farsh)
        {
            farshMashine_upgradeSpeedCost.text = data.upgradesData._farshMashine_UpgradeSpeedCost.ToString();
            farshMashine_upgradeCost.text = data.upgradesData._farshMashine_UpgradeCost.ToString();
            multiplierText.text = data.userData.farshMashine_multiplierCoef.ToString();
            currentLevel = data.userData.farshMashineLevel;
            var promice = data.userData._farshMashinePromice;
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
            currentLevel = data.userData.farshMashineLevel;
            hiLevelValue = lowLevelValue + levelsInScreen;
            hiLevel.text = hiLevelValue.ToString();
            speedValue.text = data.userData.farshMashineSpeedLevel.ToString();
            capasityValue.text = data.userData.farshMashineSpeed.ToString("0.0");
            speedValue2.text = data.userData.farshMashineSpeed.ToString("0.0");
        }
        if (file)
        {
            fileMashine_upgradeSpeedCost.text = data.upgradesData._fileMashine_UpgradeSpeedCost.ToString();
            fileMashine_upgradeCost.text = data.upgradesData._fileMashine_UpgradeCost.ToString();
            multiplierText.text = data.userData.fileMashine_multiplierCoef.ToString();
            currentLevel = data.userData.fileMashineLevel;
            var promice = data.userData._fileMashinePromice;
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
            currentLevel = data.userData.fileMashineLevel;
            hiLevelValue = lowLevelValue + levelsInScreen;
            hiLevel.text = hiLevelValue.ToString();
            speedValue.text = data.userData.fileMashineSpeedLevel.ToString();
            capasityValue.text = data.userData.fileMashineSpeed.ToString("0.0");
            speedValue2.text = data.userData.fileMashineSpeed.ToString("0.0");
        }
        if (packing1)
        {
            packingMashine_upgradeSpeedCost.text = data.upgradesData._packingMashine_1_UpgradeSpeedCost.ToString();
            packingMashine_upgradeCost.text = data.upgradesData._packingMashine_1_UpgradeCost.ToString();
            multiplierText.text = data.userData.packingMashine_1_multiplierCoef.ToString();
            currentLevel = data.userData.steakPackingMashineLevel;
            var promice = data.userData._steakPackingMashinePromice;
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
            currentLevel = data.userData.steakPackingMashineLevel;
            hiLevelValue = lowLevelValue + levelsInScreen;
            hiLevel.text = hiLevelValue.ToString();
            speedValue.text = data.userData.steakPackingMashineSpeedLevel.ToString();
            capasityValue.text = data.userData.steakPackingMashineSpeed.ToString("0.0");
            speedValue2.text = data.userData.steakPackingMashineSpeed.ToString("0.0");
        }
        if (packing2)
        {
            packingMashine_upgradeSpeedCost.text = data.upgradesData._packingMashine_2_UpgradeSpeedCost.ToString();
            packingMashine_upgradeCost.text = data.upgradesData._packingMashine_2_UpgradeCost.ToString();
            multiplierText.text = data.userData.packingMashine_2_multiplierCoef.ToString();
            currentLevel = data.userData.farshPackingMashineLevel;
            var promice = data.userData._farshPackingMashinePromice;
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
            currentLevel = data.userData.farshPackingMashineLevel;
            hiLevelValue = lowLevelValue + levelsInScreen;
            hiLevel.text = hiLevelValue.ToString();
            speedValue.text = data.userData.farshPackingMashineSpeedLevel.ToString();
            capasityValue.text = data.userData.farshPackingMashineSpeed.ToString("0.0");
            speedValue2.text = data.userData.farshPackingMashineSpeed.ToString("0.0");
        }
        if (packing3)
        {
            packingMashine_upgradeSpeedCost.text = data.upgradesData._packingMashine_3_UpgradeSpeedCost.ToString();
            packingMashine_upgradeCost.text = data.upgradesData._packingMashine_3_UpgradeCost.ToString();
            multiplierText.text = data.userData.packingMashine_3_multiplierCoef.ToString();
            currentLevel = data.userData.filePackingMashineLevel;
            var promice = data.userData._filePackingMashinePromice;
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
            currentLevel = data.userData.filePackingMashineLevel;
            hiLevelValue = lowLevelValue + levelsInScreen;
            hiLevel.text = hiLevelValue.ToString();
            speedValue.text = data.userData.filePackingMashineSpeedLevel.ToString();
            capasityValue.text = data.userData.filePackingMashineSpeed.ToString("0.0");
            speedValue2.text = data.userData.filePackingMashineSpeed.ToString("0.0");
        }
        if (carInPort)
        {
            currentLevel = data.userData.carPortLevel;            
            currentLevelValue.text = currentLevel.ToString();            
            speedValue.text = data.userData.carPortSpeed.ToString("0.00");
            capasityValue.text = data.userData.carPortCapasity.ToString();
        }
        if (carInWar)
        {
            currentLevel = data.userData.zoneInWarLevel;            
            currentLevelValue.text = currentLevel.ToString();            
            speedValue.text = data.userData.zoneInWarSpeed.ToString("0.00");
            capasityValue.text = data.userData.zoneInWarCapasity.ToString();
        }
        if (carZone1)
        {
            promiceValue.text = data.upgradesData._zone1CarUpgradeCost.ToString();
            currentLevel = data.userData.zone1CarLevel;            
            currentLevelValue.text = currentLevel.ToString();            
            speedValue.text = data.userData.zone1CarSpeed.ToString("0.00");
            capasityValue.text = data.userData.zone1CarCapasity.ToString();
        }
        if (carZone2)
        {
            promiceValue.text = data.upgradesData._zone2CarUpgradeCost.ToString();
            currentLevel = data.userData.zone2CarLevel;            
            currentLevelValue.text = currentLevel.ToString();            
            speedValue.text = data.userData.zone2CarSpeed.ToString("0.00");
            capasityValue.text = data.userData.zone2CarCapasity.ToString();
        }
        if (carZone3)
        {
            promiceValue.text = data.upgradesData._zone3CarUpgradeCost.ToString();
            currentLevel = data.userData.zone3CarLevel;            
            currentLevelValue.text = currentLevel.ToString();            
            speedValue.text = data.userData.zone3CarSpeed.ToString("0.00");
            capasityValue.text = data.userData.zone3CarCapacity.ToString();
        }
        if (carOut)
        {
            currentLevel = data.userData.zoneOutWarLevel;            
            currentLevelValue.text = currentLevel.ToString();            
            speedValue.text = data.userData.zoneOutWarSpeed.ToString("0.00");
            capasityValue.text = data.userData.zoneOutWarCapasity.ToString();
        }
        if (ship)
        {
            
            currentLevel = data.userData.shipLevel;            
            currentLevelValue.text = currentLevel.ToString();            
            speedValue.text = data.userData.shipSpeed.ToString("0.00");
            capasityValue.text = data.userData.shipCapasity.ToString();            
        }
        if (ship1)
        {
            promiceValue.text = data.upgradesData._finishZoneCarUpgradeCost.ToString();
            currentLevel = data.userData.finishZoneCarLevel;            
            currentLevelValue.text = currentLevel.ToString();           
            speedValue.text = data.userData.finishZoneCarSpeed.ToString("0.00");
            capasityValue.text = data.userData.finishZoneCarCapasity.ToString();
        }


        if (mashine0)
        {
            fishClean_1_upgradeSpeedCost.text = data.upgradesData._fishCleanMashine_1_UpgradeSpeedCost.ToString();
            fishClean_1_upgradeCost.text = data.upgradesData._fishCleanMashine_1_UpgradeCost.ToString();
            multiplierText.text = data.userData.fishClean_1_multiplierCoef.ToString();
            currentLevel = data.userData.fishCleaningMashineLevel;
            var promice = data.userData._fishCleaningMashinePromice;
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
            currentLevel = data.userData.fishCleaningMashineLevel;
            hiLevelValue = lowLevelValue + levelsInScreen;
            hiLevel.text = hiLevelValue.ToString();            
            speedValue.text = data.userData.fishCleaningMashineSpeedLevel.ToString();
            capasityValue.text = data.userData.fishCleaningMashineSpeed.ToString("0.0");
            speedValue2.text = data.userData.fishCleaningMashineSpeed.ToString("0.0");
        }
        if (!carInWar&& !carInPort&&!carZone1&&!carZone2&&!carZone3&&!ship&&!carOut&&!ship1)
        {
            float fillValue = 1f - (((float)hiLevelValue - currentLevel) / levelsInScreen);
            filledImg.fillAmount = fillValue;
        }
        

    }
    public void OnPointerDown()
    {
        if (data.matchData.isFue) return;
        upgradeBtnPressed = true;
        UpgradeMashine();
    }
    public void OnSpeedPointerDown()
    {
        upgradeSpeedBtnPressed = true;
        UpgradeMashineSpeed();
    }
    public void OnPointerUp()
    {
        if (data.matchData.isFue) return;
        upgradeBtnPressed = false;
        onPointerDelay = 0.5f;
        count = 0;
        if (mashine0)
            SaveDataSystem.Instance.SaveScalingMachine1Level();
        else if (mashine1)
            SaveDataSystem.Instance.SaveScalingMachine2Level();
        else if (mashine2)
            SaveDataSystem.Instance.SaveScalingMachine3Level();
        else if (steak)
            SaveDataSystem.Instance.SaveSteakingMachineLevel();
        else if (farsh)
            SaveDataSystem.Instance.SaveFarshMachineLevel();
        else if (file)
            SaveDataSystem.Instance.SaveFileMachineLevel();
        else if (packing1)
            SaveDataSystem.Instance.SavePackingMachineLevel();
        else if (packing2)
            SaveDataSystem.Instance.SavePackingMachine2Level();
        else if (packing3)
            SaveDataSystem.Instance.SavePackingMachine3Level();

    }
    public void OnSpeedPointerUp()
    {
        upgradeSpeedBtnPressed = false;
        onSpeedPointerDelay = 0.5f;
        speedCount = 0;
        if (mashine0)
            SaveDataSystem.Instance.SaveScalingMachine1SpeedLevel();
        else if (mashine1)
            SaveDataSystem.Instance.SaveScalingMachine2SpeedLevel();
        else if (mashine2)
            SaveDataSystem.Instance.SaveScalingMachine3SpeedLevel();
        else if (steak)
            SaveDataSystem.Instance.SaveSteakingMachineSpeedLevel();
        else if (farsh)
            SaveDataSystem.Instance.SaveFarshMachineSpeedLevel();
        else if (file)
            SaveDataSystem.Instance.SaveFileMachineSpeedLevel();
        else if (packing1)
            SaveDataSystem.Instance.SavePackingMachineSpeedLevel();
        else if (packing2)
            SaveDataSystem.Instance.SavePackingMachine2SpeedLevel();
        else if (packing3)
            SaveDataSystem.Instance.SavePackingMachine3SpeedLevel();
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
        onPointerDelay-=Time.deltaTime;
        if (onPointerDelay < 0)
        {
            count++;
            onPointerDelay = 0.5f-0.05f*count;
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
        if (carZone3) upgradeSystem.UpgradeZone3Car();
        if (carZone2) upgradeSystem.UpgradeZone2Car();
        if (carZone1) upgradeSystem.UpgradeZone1Car();
        if (carInPort) upgradeSystem.UpgradePortZoneCar();
        if (carOut) upgradeSystem.UpgradeZoneOutWar();
        if (ship) upgradeSystem.UpgradeShip();
        if (ship1) upgradeSystem.UpgradeFinishZoneCar();
        if (carInWar) upgradeSystem.UpgradeZoneInWar();
        if (mashine1) upgradeSystem.UpgradeFishCleaningMashine2();
        if (mashine2) upgradeSystem.UpgradeFishCleaningMashine3();
        if (steak) upgradeSystem.UpgradeSteakMashine();
        if (farsh) upgradeSystem.UpgradeFarshMashine();
        if (file) upgradeSystem.UpgradeFileMashine();
        if (packing1) upgradeSystem.UpgradePackingMashine();
        if (packing2) upgradeSystem.UpgradeFarshPackingMashine();
        if (packing3) upgradeSystem.UpgradeFilePackingMashine();
        if (mashine0) upgradeSystem.UpgradeFishCleaningMashine();
        CheckInfo();
    }
    private void UpgradeMashineSpeed()
    {
        if (mashine1) upgradeSystem.UpgradeFishCleaningMashine2Speed();
        if (mashine2) upgradeSystem.UpgradeFishCleaningMashine3Speed();
        if (steak) upgradeSystem.UpgradeSteakMashineSpeed();
        if (farsh) upgradeSystem.UpgradeFarshMashineSpeed();
        if (file) upgradeSystem.UpgradeFileMashineSpeed();
        if (packing1) upgradeSystem.UpgradePackingMashineSpeed();
        if (packing2) upgradeSystem.UpgradeFarshPackingMashineSpeed();
        if (packing3) upgradeSystem.UpgradeFilePackingMashineSpeed();
        if (mashine0) upgradeSystem.UpgradeFishCleaningMashineSpeed();
        CheckInfo();
    }
}
