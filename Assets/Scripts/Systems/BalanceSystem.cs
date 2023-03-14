using UnityEngine;
using System;

public class BalanceSystem : MonoBehaviour
{
    [SerializeField] private HeadCutMashineWorcer headCut1MashineWorcer;
    [SerializeField] private HeadCutMashineWorcerOut headCut1MashineWorcerOut;
    [SerializeField] private HeadCutMashineWorcer headCut2MashineWorcer;
    [SerializeField] private HeadCutMashineWorcerOut headCut2MashineWorcerOut;
    [SerializeField] private CleanMashineWorcer clean_1_MashineWorcer;
    [SerializeField] private FishCleaningWorcer_2 fish_1_CleaningWorcer_2;
    [SerializeField] private CleanMashineWorcer clean_2_MashineWorcer;
    [SerializeField] private FishCleaningWorcer_2 fish_2_CleaningWorcer_2;
    [SerializeField] private CleanMashineWorcer clean_3_MashineWorcer;
    [SerializeField] private FishCleaningWorcer_2 fish_3_CleaningWorcer_2;
    [SerializeField] private StakeMashineWorcer stakeMashineWorcer;
    [SerializeField] private StakeMashineWorcer_2 stakeMashineWorcer_2;
    [SerializeField] private FarshMachineWorcer farshMashineWorcer;
    [SerializeField] private FarshMachineWorcer_2 farshMashineWorcer_2;
    [SerializeField] private FileZoneWorcer fileMashineWorcer;
    [SerializeField] private FileZoneWorcer2 fileMashineWorcer_2;
    [SerializeField] private AppData data;
    private float startSpeed = 20f;
    private float fishCleanStartSpeed = 45f;
    private float carStartSpeed = 10.3f;
    private void Start()
    {
        Invoke(nameof(InitSpeed), 3f);
    }

    private void InitSpeed()
    {
        SetHeadCut_1_Incomes();
        SetHeadCut_2_Incomes();
        SetFishClean_1_Incomes();
        SetFishClean_2_Incomes();
        SetFishClean_3_Incomes();
        SetSteakMashine_Incomes();
        SetFarshMashine_Incomes();
        SetFileMashine_Incomes();
        SetPackingMashine_Incomes();
        SetFarshPackingMashine_Incomes();
        SetFilePackingMashine_Incomes();
        SetHeadCut_1_Speed();
        SetHeadCut_2_Speed();
        SetFishClean_1_Speed();
        SetFishClean_2_Speed();
        SetFishClean_3_Speed();
        SetSteakMashine_Speed();
        SetFarshMashine_Speed();
        SetFileMashine_Speed();
        SetPackingMashine_Speed();
        SetFarshPackingMashine_Speed();
        SetFilePackingMashine_Speed();
        SetCarInPort_UpgradeCost();
        SetCarInWar_UpgradeCost();
        SetZone_1Car_UpgradeCost();
        SetZone_2Car_UpgradeCost();
        SetZone_3Car_UpgradeCost();
        Set_OutCar_UpgradeCost();
        Set_FinishZoneCar_UpgradeCost();
        Set_Ship_UpgradeCost();
    }
    
    public void SetCarInPort_UpgradeCost()
    {
        data.upgradesData.carPortUpgradeCost = GetCarUpgradeSpeedCost(data.userData.carPortLevel);
        data.upgradesData._carPortUpgradeCost = Converter.instance.ConvertMoneyView(data.upgradesData.carPortUpgradeCost);
        data.userData.carPortSpeed = (0.248f * (data.userData.carPortLevel-1)) + carStartSpeed;
    }
    public void SetCarInWar_UpgradeCost()
    {
        data.upgradesData.zoneInWarUpgradeCost = GetCarUpgradeSpeedCost(data.userData.zoneInWarLevel);
        data.upgradesData._zoneInWarUpgradeCost = Converter.instance.ConvertMoneyView(data.upgradesData.zoneInWarUpgradeCost);
        data.userData.zoneInWarSpeed = (0.248f * (data.userData.zoneInWarLevel-1)) + carStartSpeed;
    }
    public void SetZone_1Car_UpgradeCost()
    {
        data.upgradesData.zone1CarUpgradeCost = GetCarUpgradeSpeedCost(data.userData.zone1CarLevel);
        data.upgradesData._zone1CarUpgradeCost = Converter.instance.ConvertMoneyView(data.upgradesData.zone1CarUpgradeCost);
        data.userData.zone1CarSpeed = (0.248f * (data.userData.zone1CarLevel-1)) + carStartSpeed;
    }
    public void SetZone_2Car_UpgradeCost()
    {
        data.upgradesData.zone2CarUpgradeCost = GetCarUpgradeSpeedCost(data.userData.zone2CarLevel);
        data.upgradesData._zone2CarUpgradeCost = Converter.instance.ConvertMoneyView(data.upgradesData.zone2CarUpgradeCost);
        data.userData.zone2CarSpeed = (0.248f * (data.userData.zone2CarLevel-1)) + carStartSpeed;
    }
    public void SetZone_3Car_UpgradeCost()
    {
        data.upgradesData.zone3CarUpgradeCost = GetCarUpgradeSpeedCost(data.userData.zone3CarLevel);
        data.upgradesData._zone3CarUpgradeCost = Converter.instance.ConvertMoneyView(data.upgradesData.zone3CarUpgradeCost);
        data.userData.zone3CarSpeed = (0.248f * (data.userData.zone3CarLevel-1)) + carStartSpeed;
    }
    public void Set_OutCar_UpgradeCost()
    {
        data.upgradesData.carOutUpgradeCost = GetCarUpgradeSpeedCost(data.userData.zoneOutWarLevel);
        data.upgradesData._carOutUpgradeCost = Converter.instance.ConvertMoneyView(data.upgradesData.carOutUpgradeCost);
        data.userData.zoneOutWarSpeed = (0.248f * (data.userData.zoneOutWarLevel - 1)) + carStartSpeed;
    }
    public void Set_FinishZoneCar_UpgradeCost()
    {
        data.upgradesData.finishZoneCarUpgradeCost = GetCarUpgradeSpeedCost(data.userData.finishZoneCarLevel);
        data.upgradesData._finishZoneCarUpgradeCost = Converter.instance.ConvertMoneyView(data.upgradesData.finishZoneCarUpgradeCost);
        data.userData.finishZoneCarSpeed = (0.248f * (data.userData.finishZoneCarLevel - 1)) + carStartSpeed;
    }
    public void Set_Ship_UpgradeCost()
    {
        data.upgradesData.shipUpgradeCost = GetCarUpgradeSpeedCost(data.userData.shipLevel);
        data.upgradesData._shipUpgradeCost = Converter.instance.ConvertMoneyView(data.upgradesData.shipUpgradeCost);
        data.userData.shipSpeed = (0.248f * (data.userData.shipLevel-1)) + carStartSpeed;
    }
    public void SetHeadCut_1_Incomes()
    {
        data.userData.headCutingMashinePromice = GetHeadCut_1_MashineIncome(data.userData.headCutingMashineLevel, data.userData.headCut_1_multiplier.Value);        
        data.userData._headCutingMashinePromice = Converter.instance.ConvertMoneyView(data.userData.headCutingMashinePromice);
        data.upgradesData.headCutMashine_1_UpgradeCost.Value = GetHeadCut_1_MashineUpgradeCost(data.userData.headCutingMashineLevel);
        data.upgradesData._headCutMashine_1_UpgradeCost.Value = Converter.instance.ConvertMoneyView(data.upgradesData.headCutMashine_1_UpgradeCost.Value);
    }
    public void SetHeadCut_1_Speed()
    {
        data.userData.headCutingMashineSpeed = startSpeed - (0.15f * (data.userData.headCutingMashineSpeedLevel-1));
        data.upgradesData.headCutMashine_1_UpgradeSpeedCost.Value = GetHeadCut_1_MashineUpgradeSpeedCost(data.userData.headCutingMashineSpeedLevel);
        data.upgradesData._headCutMashine_1_UpgradeSpeedCost.Value = Converter.instance.ConvertMoneyView(data.upgradesData.headCutMashine_1_UpgradeSpeedCost.Value);
        headCut1MashineWorcer.SetSpeed();
        headCut1MashineWorcerOut.SetSpeed();
    }
    public void SetHeadCut_2_Incomes()
    {
        data.userData.headCutingMashine2Promice = GetHeadCut_2_MashineIncome(data.userData.headCutingMashine2Level, data.userData.headCut_2_multiplier.Value);
        data.userData._headCutingMashine2Promice = Converter.instance.ConvertMoneyView(data.userData.headCutingMashine2Promice);
        data.upgradesData.headCutMashine_2_UpgradeCost.Value = GetHeadCut_2_MashineUpgradeCost(data.userData.headCutingMashine2Level);
        data.upgradesData._headCutMashine_2_UpgradeCost.Value = Converter.instance.ConvertMoneyView(data.upgradesData.headCutMashine_2_UpgradeCost.Value);
    }
    public void SetFishClean_1_Incomes()
    {
        data.userData.fishCleaningMashinePromice = GetFishClean_1_MashineIncome(data.userData.fishCleaningMashineLevel, data.userData.fishClean_1_multiplier.Value);
        data.userData._fishCleaningMashinePromice = Converter.instance.ConvertMoneyView(data.userData.fishCleaningMashinePromice);
        data.upgradesData.fishCleanMashine_1_UpgradeCost.Value = GetFishClean_1_MashineUpgradeCost(data.userData.fishCleaningMashineLevel);
        data.upgradesData._fishCleanMashine_1_UpgradeCost.Value = Converter.instance.ConvertMoneyView(data.upgradesData.fishCleanMashine_1_UpgradeCost.Value);
    }
    public void SetFishClean_2_Incomes()
    {
        data.userData.fishCleaningMashine2Promice = GetFishClean_2_MashineIncome(data.userData.fishCleaningMashine2Level, data.userData.fishClean_2_multiplier.Value);
        data.userData._fishCleaningMashine2Promice = Converter.instance.ConvertMoneyView(data.userData.fishCleaningMashine2Promice);
        data.upgradesData.fishCleanMashine_2_UpgradeCost.Value = GetFishClean_2_MashineUpgradeCost(data.userData.fishCleaningMashine2Level);
        data.upgradesData._fishCleanMashine_2_UpgradeCost.Value = Converter.instance.ConvertMoneyView(data.upgradesData.fishCleanMashine_2_UpgradeCost.Value);
    }
    public void SetFishClean_3_Incomes()
    {
        data.userData.fishCleaningMashine3Promice = GetFishClean_3_MashineIncome(data.userData.fishCleaningMashine3Level, data.userData.fishClean_3_multiplier.Value);
        data.userData._fishCleaningMashine3Promice = Converter.instance.ConvertMoneyView(data.userData.fishCleaningMashine3Promice);
        data.upgradesData.fishCleanMashine_3_UpgradeCost.Value = GetFishClean_3_MashineUpgradeCost(data.userData.fishCleaningMashine3Level);
        data.upgradesData._fishCleanMashine_3_UpgradeCost.Value = Converter.instance.ConvertMoneyView(data.upgradesData.fishCleanMashine_3_UpgradeCost.Value);
    }
    public void SetSteakMashine_Incomes()
    {
        data.userData.steakMashinePromice = GetSteak_MashineIncome(data.userData.steakMashineLevel, data.userData.steakMashine_multiplier.Value );
        data.userData._steakMashinePromice = Converter.instance.ConvertMoneyView(data.userData.steakMashinePromice);
        data.upgradesData.steakMashine_UpgradeCost.Value = GetSteak_MashineUpgradeCost(data.userData.steakMashineLevel);
        data.upgradesData._steakMashine_UpgradeCost.Value = Converter.instance.ConvertMoneyView(data.upgradesData.steakMashine_UpgradeCost.Value);
    }
    public void SetFarshMashine_Incomes()
    {
        data.userData.farshMashinePromice = GetFarsh_MashineIncome(data.userData.farshMashineLevel, data.userData.farshMashine_multiplier.Value);
        data.userData._farshMashinePromice = Converter.instance.ConvertMoneyView(data.userData.farshMashinePromice);
        data.upgradesData.farshMashine_UpgradeCost.Value = GetFarsh_MashineUpgradeCost(data.userData.farshMashineLevel);
        data.upgradesData._farshMashine_UpgradeCost.Value = Converter.instance.ConvertMoneyView(data.upgradesData.farshMashine_UpgradeCost.Value);
    }
    public void SetFileMashine_Incomes()
    {
        data.userData.fileMashinePromice = GetFile_MashineIncome(data.userData.fileMashineLevel, data.userData.fileMashine_multiplier.Value);
        data.userData._fileMashinePromice = Converter.instance.ConvertMoneyView(data.userData.fileMashinePromice);
        data.upgradesData.fileMashine_UpgradeCost.Value = GetFile_MashineUpgradeCost(data.userData.fileMashineLevel);
        data.upgradesData._fileMashine_UpgradeCost.Value = Converter.instance.ConvertMoneyView(data.upgradesData.fileMashine_UpgradeCost.Value);
    }
    public void SetPackingMashine_Incomes()
    {
        data.userData.steakPackingMashinePromice = GetPacking_MashineIncome(data.userData.steakPackingMashineLevel, data.userData.packingMashine_1_multiplier.Value);
        data.userData._steakPackingMashinePromice = Converter.instance.ConvertMoneyView(data.userData.steakPackingMashinePromice);
        data.upgradesData.packingMashine_1_UpgradeCost.Value = GetPacking_MashineUpgradeCost(data.userData.steakPackingMashineLevel);
        data.upgradesData._packingMashine_1_UpgradeCost.Value = Converter.instance.ConvertMoneyView(data.upgradesData.packingMashine_1_UpgradeCost.Value);
    }
    public void SetFarshPackingMashine_Incomes()
    {
        data.userData.farshPackingMashinePromice = GetFarshPacking_MashineIncome(data.userData.farshPackingMashineLevel, data.userData.packingMashine_2_multiplier.Value);
        data.userData._farshPackingMashinePromice = Converter.instance.ConvertMoneyView(data.userData.farshPackingMashinePromice);
        data.upgradesData.packingMashine_2_UpgradeCost.Value = GetFarshPacking_MashineUpgradeCost(data.userData.farshPackingMashineLevel);
        data.upgradesData._packingMashine_2_UpgradeCost.Value = Converter.instance.ConvertMoneyView(data.upgradesData.packingMashine_2_UpgradeCost.Value);
    }
    public void SetFilePackingMashine_Incomes()
    {
        data.userData.filePackingMashinePromice = GetFilePacking_MashineIncome(data.userData.filePackingMashineLevel, data.userData.packingMashine_3_multiplier.Value);
        data.userData._filePackingMashinePromice = Converter.instance.ConvertMoneyView(data.userData.filePackingMashinePromice);
        data.upgradesData.packingMashine_3_UpgradeCost.Value = GetFilePacking_MashineUpgradeCost(data.userData.filePackingMashineLevel);
        data.upgradesData._packingMashine_3_UpgradeCost.Value = Converter.instance.ConvertMoneyView(data.upgradesData.packingMashine_3_UpgradeCost.Value);
    }
    public void SetHeadCut_2_Speed()
    {
        data.userData.headCutingMashine2Speed = startSpeed - 0.15f* (data.userData.headCutingMashine2SpeedLevel-1);
        data.upgradesData.headCutMashine_2_UpgradeSpeedCost.Value = GetHeadCut_2_MashineUpgradeSpeedCost(data.userData.headCutingMashine2SpeedLevel);
        data.upgradesData._headCutMashine_2_UpgradeSpeedCost.Value = Converter.instance.ConvertMoneyView(data.upgradesData.headCutMashine_2_UpgradeSpeedCost.Value);
        if (headCut2MashineWorcer.gameObject.activeInHierarchy)
        {
            headCut2MashineWorcer.SetSpeed();
            headCut2MashineWorcerOut.SetSpeed();
        }
        
    }
    public void SetFishClean_1_Speed()
    {
        data.userData.fishCleaningMashineSpeed = fishCleanStartSpeed - (0.45f * (data.userData.fishCleaningMashineSpeedLevel-1));
        data.upgradesData.fishCleanMashine_1_UpgradeSpeedCost.Value = GetFishClean_1_MashineUpgradeSpeedCost(data.userData.fishCleaningMashineSpeedLevel);
        data.upgradesData._fishCleanMashine_1_UpgradeSpeedCost.Value = Converter.instance.ConvertMoneyView(data.upgradesData.fishCleanMashine_1_UpgradeSpeedCost.Value);
        if (clean_1_MashineWorcer.gameObject.activeInHierarchy)
        {
            clean_1_MashineWorcer.CheckSpeed();
            fish_1_CleaningWorcer_2.CheckSpeed();
        }        
    }
    public void SetFishClean_2_Speed()
    {
        data.userData.fishCleaningMashine2Speed = fishCleanStartSpeed - (0.45f * (data.userData.fishCleaningMashine2SpeedLevel-1));
        data.upgradesData.fishCleanMashine_2_UpgradeSpeedCost.Value = GetFishClean_2_MashineUpgradeSpeedCost(data.userData.fishCleaningMashine2SpeedLevel);
        data.upgradesData._fishCleanMashine_2_UpgradeSpeedCost.Value = Converter.instance.ConvertMoneyView(data.upgradesData.fishCleanMashine_2_UpgradeSpeedCost.Value);
        
        if (clean_2_MashineWorcer.gameObject.activeInHierarchy)
        {
            clean_2_MashineWorcer.CheckSpeed();
            fish_2_CleaningWorcer_2.CheckSpeed();
        }
        
    }
    public void SetFishClean_3_Speed()
    {
        data.userData.fishCleaningMashine3Speed = fishCleanStartSpeed - (0.45f * (data.userData.fishCleaningMashine3SpeedLevel-1));
        data.upgradesData.fishCleanMashine_3_UpgradeSpeedCost.Value = GetFishClean_3_MashineUpgradeSpeedCost(data.userData.fishCleaningMashine3SpeedLevel);
        data.upgradesData._fishCleanMashine_3_UpgradeSpeedCost.Value = Converter.instance.ConvertMoneyView(data.upgradesData.fishCleanMashine_3_UpgradeSpeedCost.Value);
        if (clean_3_MashineWorcer.gameObject.activeInHierarchy)
        {
            clean_3_MashineWorcer.CheckSpeed();
            fish_3_CleaningWorcer_2.CheckSpeed();
        }        
    }
    public void SetSteakMashine_Speed()
    {
        data.userData.steakMashineSpeed = fishCleanStartSpeed - (0.45f * (data.userData.steakMashineSpeedLevel-1));
        data.upgradesData.steakMashine_UpgradeSpeedCost.Value = GetSteak_MashineUpgradeSpeedCost(data.userData.steakMashineSpeedLevel);
        data.upgradesData._steakMashine_UpgradeSpeedCost.Value = Converter.instance.ConvertMoneyView(data.upgradesData.steakMashine_UpgradeSpeedCost.Value);
        if (stakeMashineWorcer.gameObject.activeInHierarchy)
        {
            stakeMashineWorcer.SetSpeed();
            stakeMashineWorcer_2.SetSpeed();
        }
    }
    public void SetFarshMashine_Speed()
    {
        data.userData.farshMashineSpeed = fishCleanStartSpeed - (0.45f * (data.userData.farshMashineSpeedLevel - 1));
        data.upgradesData.farshMashine_UpgradeSpeedCost.Value = GetFarsh_MashineUpgradeSpeedCost(data.userData.farshMashineSpeedLevel);
        data.upgradesData._farshMashine_UpgradeSpeedCost.Value = Converter.instance.ConvertMoneyView(data.upgradesData.farshMashine_UpgradeSpeedCost.Value);
        if (farshMashineWorcer.gameObject.activeInHierarchy)
        {
            farshMashineWorcer.SetSpeed();
            farshMashineWorcer_2.SetSpeed();
        }
    }
    public void SetFileMashine_Speed()
    {
        data.userData.fileMashineSpeed = fishCleanStartSpeed - (0.45f * (data.userData.fileMashineSpeedLevel - 1));
        data.upgradesData.fileMashine_UpgradeSpeedCost.Value = GetFile_MashineUpgradeSpeedCost(data.userData.fileMashineSpeedLevel);
        data.upgradesData._fileMashine_UpgradeSpeedCost.Value = Converter.instance.ConvertMoneyView(data.upgradesData.fileMashine_UpgradeSpeedCost.Value);
        if (fileMashineWorcer.gameObject.activeInHierarchy)
        {
            fileMashineWorcer.SetSpeed();
            fileMashineWorcer_2.SetSpeed();
        }
    }
    public void SetPackingMashine_Speed()
    {
        data.userData.steakPackingMashineSpeed = fishCleanStartSpeed - (0.45f * (data.userData.steakPackingMashineSpeedLevel-1));
        data.upgradesData.packingMashine_1_UpgradeSpeedCost.Value = GetPacking_MashineUpgradeSpeedCost(data.userData.steakPackingMashineSpeedLevel);
        data.upgradesData._packingMashine_1_UpgradeSpeedCost.Value = Converter.instance.ConvertMoneyView(data.upgradesData.packingMashine_1_UpgradeSpeedCost.Value);
    }
    public void SetFarshPackingMashine_Speed()
    {
        data.userData.farshPackingMashineSpeed = fishCleanStartSpeed - (0.45f * (data.userData.farshPackingMashineSpeedLevel - 1));
        data.upgradesData.packingMashine_2_UpgradeSpeedCost.Value = GetFarshPacking_MashineUpgradeSpeedCost(data.userData.farshPackingMashineSpeedLevel);
        data.upgradesData._packingMashine_2_UpgradeSpeedCost.Value = Converter.instance.ConvertMoneyView(data.upgradesData.packingMashine_2_UpgradeSpeedCost.Value);
    }
    public void SetFilePackingMashine_Speed()
    {
        data.userData.filePackingMashineSpeed = fishCleanStartSpeed - (0.45f * (data.userData.filePackingMashineSpeedLevel - 1));
        data.upgradesData.packingMashine_3_UpgradeSpeedCost.Value = GetFilePacking_MashineUpgradeSpeedCost(data.userData.filePackingMashineSpeedLevel);
        data.upgradesData._packingMashine_3_UpgradeSpeedCost.Value = Converter.instance.ConvertMoneyView(data.upgradesData.packingMashine_3_UpgradeSpeedCost.Value);
    }
    public double GetCarUpgradeSpeedCost(int level)
    {
        double result = 824d * Mathf.Exp(0.501f * level);
        double _result = (double)Math.Round((double)result, 1);
        return _result;
    }
    public double GetHeadCut_1_MashineIncome(int level, float multiplier)
    {
        double result = (8.39f*(level - 1)*multiplier) + (80.4f*multiplier);
        double _result = (double)Math.Round((double)result, 1);
        return _result;
    }
    public double GetHeadCut_1_MashineUpgradeCost(int level)
    {
        float _level = (float)level;
        double result = 3.0042f * Mathf.Exp(0.044f * _level);
        double _result = (double)Math.Round((double)result,1);
        return _result;
    }
    public double GetHeadCut_1_MashineUpgradeSpeedCost(int level)
    {
        double result = 578d * Mathf.Exp(0.139f * (level - 1));
        double _result = (double)Math.Round((double)result, 1);
        return _result;
    }
    public double GetHeadCut_2_MashineIncome(int level, float multiplier)
    {
        double result = (39855d * (level - 1) * multiplier) + (1093333.33d * multiplier);
        double _result = (double)Math.Round((double)result, 1);
        return _result;
    }
    public double GetHeadCut_2_MashineUpgradeCost(int level)
    {
        float _level = (float)level;
        double result = 1686666.66d * Mathf.Exp(0.0537f * (_level-1));
        double _result = (double)Math.Round((double)result, 1);
        return _result;
    }
    public double GetHeadCut_2_MashineUpgradeSpeedCost(int level)
    {
        double result = 7666666.66d * Mathf.Exp(0.14f * (level - 1));
        double _result = (double)Math.Round((double)result, 1);
        return _result;
    }
    public double GetFishClean_1_MashineIncome(int level, float multiplier)
    {
        double result = (29.3f * (level - 1) * multiplier) + (296d * multiplier);
        double _result = (double)Math.Round((double)result, 1);
        return _result;
    }
    public double GetFishClean_1_MashineUpgradeCost(int level)
    {
        float _level = (float)level;
        double result = 105d * Mathf.Exp(0.044f * (_level - 1));
        double _result = (double)Math.Round((double)result, 1);
        return _result;
    }
    public double GetFishClean_1_MashineUpgradeSpeedCost(int level)
    {
        double result = Math.Round(9.9928d * Mathf.Exp(0.1399f * (level - 1)),0)*115;
        double _result = (double)Math.Round((double)result, 1);
        return _result;// Œ –”√À¬¬≈–’(9, 9928 * EXP(0, 1399 * A3); 0)*100
    }
    public double GetFishClean_2_MashineIncome(int level, float multiplier)
    {
        double result = (354f * (level - 1) * multiplier) + (5967f * multiplier);
        double _result = (double)Math.Round((double)result, 1);
        return _result;
    }
    public double GetFishClean_2_MashineUpgradeCost(int level)
    {
        float _level = (float)level;
        double result = 1568d * Mathf.Exp(0.0469f * (_level - 1));
        double _result = (double)Math.Round((double)result, 1);
        return _result;
    }
    public double GetFishClean_2_MashineUpgradeSpeedCost(int level)
    {
        double result = Math.Round(9.9928d * Mathf.Exp(0.1399f * (level - 1)), 0) * 115*25;
        double _result = (double)Math.Round((double)result, 1);
        return _result;
    }
    public double GetFishClean_3_MashineIncome(int level, float multiplier)
    {
        double result = (6800d * (level - 1) * multiplier) + (204800d * multiplier);
        double _result = (double)Math.Round((double)result, 1);
        return _result;
    }
    public double GetFishClean_3_MashineUpgradeCost(int level)
    {
        float _level = (float)level;
        double result = 630016d * Mathf.Exp(0.0488f * (_level - 1));
        double _result = (double)Math.Round((double)result, 1);
        return _result;
    }
    public double GetFishClean_3_MashineUpgradeSpeedCost(int level)
    {
        double result = Math.Round(9.9928d * Mathf.Exp(0.1399f * (level - 1)), 0) * 115 * 25*100;
        double _result = (double)Math.Round((double)result, 1);
        return _result;
    }
    public double GetSteak_MashineIncome(int level, float multiplier)
    {
        double result = (21166.66d * (level - 1) * multiplier) + (570000d * multiplier);
        double _result = (double)Math.Round((double)result, 1);
        return _result;
    }
    public double GetSteak_MashineUpgradeCost(int level)
    {
        float _level = (float)level;
        double result = 23600000d * Mathf.Exp(0.0584f * (_level - 2));
        double _result = (double)Math.Round((double)result, 1);
        return _result; //23600000 * EXP(0, 0584 * (A3 - 2))
    }
    public double GetSteak_MashineUpgradeSpeedCost(int level)
    {
        double result = 683000000d * Mathf.Exp(0.14f * (level - 1));
        double _result = (double)Math.Round((double)result, 1);
        return _result;
    }
    public double GetFarsh_MashineIncome(int level, float multiplier)
    {
        double result = (21166.66d * (level - 1) * multiplier) + (35700000d * multiplier);
        double _result = (double)Math.Round((double)result, 1);
        return _result;
    }
    public double GetFarsh_MashineUpgradeCost(int level)
    {
        float _level = (float)level;
        double result = 1023600000d * Mathf.Exp(0.0584f * (_level - 2));
        double _result = (double)Math.Round((double)result, 1);
        return _result; //23600000 * EXP(0, 0584 * (A3 - 2))
    }
    public double GetFarsh_MashineUpgradeSpeedCost(int level)
    {
        double result = 189500000000d * Mathf.Exp(0.14f * (level - 1));
        double _result = (double)Math.Round((double)result, 1);
        return _result;
    }
    public double GetFile_MashineIncome(int level, float multiplier)
    {
        double result = (21166.66d * (level - 1) * multiplier) + (3200570000d * multiplier);
        double _result = (double)Math.Round((double)result, 1);
        return _result;
    }
    public double GetFile_MashineUpgradeCost(int level)
    {
        float _level = (float)level;
        double result = 37023600000d * Mathf.Exp(0.0584f * (_level - 2));
        double _result = (double)Math.Round((double)result, 1);
        return _result; //23600000 * EXP(0, 0584 * (A3 - 2))
    }
    public double GetFile_MashineUpgradeSpeedCost(int level)
    {
        double result = 9600000000000d * Mathf.Exp(0.14f * (level - 1));
        double _result = (double)Math.Round((double)result, 1);
        return _result;
    }
    public double GetPacking_MashineIncome(int level, float multiplier)
    {
        double result = (814814814.81d * (level - 1) * multiplier) + (15046296296.26d * multiplier);
        double _result = (double)Math.Round((double)result, 1);
        return _result;
    }
    public double GetPacking_MashineUpgradeCost(int level)
    {
        float _level = (float)level;
        double result = 677000000000d * Mathf.Exp(0.0722f * (_level - 1));
        double _result = (double)Math.Round((double)result, 1);
        return _result;
    }
    public double GetPacking_MashineUpgradeSpeedCost(int level)
    {
        double result = 209000000000000d * Mathf.Exp(0.148f * (level - 1));
        double _result = (double)Math.Round((double)result, 1);
        return _result;
    }
    public double GetFarshPacking_MashineIncome(int level, float multiplier)
    {
        double result = (814814814.81d * (level - 1) * multiplier) + (850046296296.00d * multiplier);
        double _result = (double)Math.Round((double)result, 1);
        return _result;
    }
    public double GetFarshPacking_MashineUpgradeCost(int level)
    {
        float _level = (float)level;
        double result = 21677000000000d * Mathf.Exp(0.0722f * (_level - 1));
        double _result = (double)Math.Round((double)result, 1);
        return _result;
    }
    public double GetFarshPacking_MashineUpgradeSpeedCost(int level)
    {
        double result = 9209000000000000d * Mathf.Exp(0.148f * (level - 1));
        double _result = (double)Math.Round((double)result, 1);
        return _result;
    }
    public double GetFilePacking_MashineIncome(int level, float multiplier)
    {
        double result = (814814814.81d * (level - 1) * multiplier) + (750015046296296.26d * multiplier);
        double _result = (double)Math.Round((double)result, 1);
        return _result;
    }
    public double GetFilePacking_MashineUpgradeCost(int level)
    {
        float _level = (float)level;
        double result = 9000677000000000d * Mathf.Exp(0.0722f * (_level - 1));
        double _result = (double)Math.Round((double)result, 1);
        return _result;
    }
    public double GetFilePacking_MashineUpgradeSpeedCost(int level)
    {
        double result = 705209000000000000d * Mathf.Exp(0.148f * (level - 1));
        double _result = (double)Math.Round((double)result, 1);
        return _result;
    }
}
