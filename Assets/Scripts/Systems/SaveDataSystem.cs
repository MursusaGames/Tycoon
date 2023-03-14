using UnityEngine;
using System;

public class SaveDataSystem : BaseMonoSystem
{
    [SerializeField] private UpgradeSystem upgradeSystem;
    [SerializeField] private CarShipment carShipment;
    [SerializeField] private ChangeWarSizeSystem changeWarSizeSystem;
    [SerializeField] private TransportControlSystem transportControlSystem;
    [SerializeField] private MachineControlSystem machineControlSystem;
    [SerializeField] private FueSystem fueSystem;
    [SerializeField] private SwipeControl swipeControl;    
    public static SaveDataSystem Instance;
    [Header("Start values")]
    [SerializeField] private string _userName;
    [SerializeField] private double _coins;
    [SerializeField] private int _crystalls;
    [SerializeField] private int _trees;
    private string stockCapasity = "st4oock";
    private string offlainLimit = "off4loineLimit";
    private string coins="Coio4n0";
    private string crystal = "Cr4ysotal0";
    private string gold = "Gol4od0";
    private string inWar = "In4Waro00";
    private string inWarLevel = "In4WoarLevel";
    private string ship = "Shi4op00";
    private string shipLevel = "Shipo4Level";
    private string carPort = "Ca4ro2Por0t0";
    private string carPortLevel = "Car4oPortLevel";
    private string carOut = "Caro4Out";
    private string carOutLevel = "Caro4OutLevel";
    private string car3 = "Caro400";
    private string car3Level = "Caro4Level";
    private string car1Level = "Caro4Level";
    private string car2Level = "Caro4Level";
    private string finishCarLevel = "Finish4oCarLevel";
    private string fishOut = "fi4shoOut0";
    private string unpSteakOut = "U4np2SteakoOut0";
    private string steakOut = "steako4Out0";
    private string unpFarshOut = "U4np2FarshoOut0";
    private string farshOut = "farsho4Out0";
    private string unpFileOut = "U4npFileoOut0";
    private string fileOut = "fileo4Out0";
    public string firstOrder = "Firs6toOrder0";
    private string allTime = "AllTimo4e0";
    private string userLevel = "UserLe4veol0";
    private string firstPurshase = "Fir4stoPurshase0";
    private string getOffer = "GetOf4ofer0";
    private string carvingMachinesCount = "Car4oving0";
    private string carvingMachines_1_Level = "Caroving4Level0";
    private string carvingMachines_1_SpeedLevel = "Carvoing4SpeedLevel0";
    private string carvingMachines_2_Level = "Carvingo4evel0";
    private string carvingMachines_2_SpeedLevel = "Carvoing4SpeedLevel0";
    private string scalingMachinesCount = "Scaliong4";
    private string scalingMachines_1_Level = "Scaling1oevel4";
    private string scalingMachines_1_SpeedLevel = "Scaloing1SpeedLevel4";
    private string scalingMachines_2_Level = "Scalin2goLevel4";
    private string scalingMachines_2_SpeedLevel = "Scaloing2SpeedLevel4";
    private string scalingMachines_3_Level = "Scaling3Loevel4";
    private string scalingMachines_3_SpeedLevel = "Scaloing3SpeedLevel4";
    private string steakingMachinesCount = "Steakoing4";
    private string steakingMachines_1_Level = "SteakiongLoevel4";
    private string steakingMachines_1_SpeedLevel = "SteaookingSpeedLevel4";
    private string farshMachinesCount = "FarshCount";
    private string farshMachinesLevel = "FarshLevel";
    private string farshMachinesSpeedLevel = "FarshSpeedLevel";
    private string fileMachinesCount = "FileCount";
    private string fileMachinesLevel = "FileLevel";
    private string fileMachinesSpeedLevel = "FileSpeedLevel";
    private string packingMachinesCount = "Packioong4";
    private string packingMachines_1_Level = "PackionogLevel4";
    private string packingMachines_1_SpeedLevel = "PackiongSpeedLevel4";
    private string packingMachines_2_Level = "PackingLevel2";
    private string packingMachines_2_SpeedLevel = "PackingSpeedLevel2";
    private string packingMachines_3_Level = "PackingLevel3";
    private string packingMachines_3_SpeedLevel = "PackingSpeedLevel3";
    private string startGame = "StartGame";
    public override void Init(AppData data)
    {
        base.Init(data);        
    }
    public void SetMenu(bool state)
    {
        swipeControl.inMenu = state;
    }  
    
    private void Start()
    {
        Instance = this;
        LoadPlayerData();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
            SavePlayerData();
    }

    private void OnApplicationQuit() => SavePlayerData();

    public void SaveData() => SavePlayerData();
    private void OnDestroy()
    {
        SavePlayerData();
    }
    public void SaveStartsGame()
    {
        PlayerPrefs.SetInt(startGame, data.userData.startGame);
    }
    public void SavePlayerTime()
    {
        PlayerPrefs.SetInt(allTime, data.userData.allPlayTimeInMinutes);
    }
    public void SaveStockCapasity()
    {
        PlayerPrefs.SetInt(stockCapasity, data.userData.warehaus_updated);
    }
    public void SaveCarvingMachineCount()
    {
        PlayerPrefs.SetInt(carvingMachinesCount, data.userData.headCutingMashine);
    }
    public void SaveCarvingMachine1Level()
    {
        PlayerPrefs.SetInt(carvingMachines_1_Level, data.userData.headCutingMashineLevel);
    }
    public void SaveCarvingMachine1SpeedLevel()
    {
        PlayerPrefs.SetInt(carvingMachines_1_SpeedLevel, data.userData.headCutingMashineSpeedLevel);
    }
    public void SaveCarvingMachine2Level()
    {
        PlayerPrefs.SetInt(carvingMachines_2_Level, data.userData.headCutingMashine2Level);
    }
    public void SaveCarvingMachine2SpeedLevel()
    {
        PlayerPrefs.SetInt(carvingMachines_2_SpeedLevel, data.userData.headCutingMashine2SpeedLevel);
    }
    public void SaveScalingMachineCount()
    {
        PlayerPrefs.SetInt(scalingMachinesCount, data.userData.fishCleaningMashine);
    }
    public void SaveScalingMachine1Level()
    {
        PlayerPrefs.SetInt(scalingMachines_1_Level, data.userData.fishCleaningMashineLevel);
    }
    public void SaveScalingMachine1SpeedLevel()
    {
        PlayerPrefs.SetInt(scalingMachines_1_SpeedLevel, data.userData.fishCleaningMashineSpeedLevel);
    }
    public void SaveScalingMachine2Level()
    {
        PlayerPrefs.SetInt(scalingMachines_2_Level, data.userData.fishCleaningMashine2Level);
    }
    public void SaveScalingMachine2SpeedLevel()
    {
        PlayerPrefs.SetInt(scalingMachines_2_SpeedLevel, data.userData.fishCleaningMashine2SpeedLevel);
    }
    public void SaveScalingMachine3Level()
    {
        PlayerPrefs.SetInt(scalingMachines_3_Level, data.userData.fishCleaningMashine3Level);
    }
    public void SaveScalingMachine3SpeedLevel()
    {
        PlayerPrefs.SetInt(scalingMachines_3_SpeedLevel, data.userData.fishCleaningMashine3SpeedLevel);
    }
    public void SaveSteakingMachineCount()
    {
        PlayerPrefs.SetInt(steakingMachinesCount, data.userData.steakMashine);
    }
    public void SaveSteakingMachineLevel()
    {
        PlayerPrefs.SetInt(steakingMachines_1_Level, data.userData.steakMashineLevel);
    }
    public void SaveSteakingMachineSpeedLevel()
    {
        PlayerPrefs.SetInt(steakingMachines_1_SpeedLevel, data.userData.steakMashineSpeedLevel);
    }
    public void SaveFarshMachineCount()
    {
        PlayerPrefs.SetInt(farshMachinesCount, data.userData.farshMashine);
    }
    public void SaveFarshMachineLevel()
    {
        PlayerPrefs.SetInt(farshMachinesLevel, data.userData.farshMashineLevel);
    }
    public void SaveFarshMachineSpeedLevel()
    {
        PlayerPrefs.SetInt(farshMachinesSpeedLevel, data.userData.farshMashineSpeedLevel);
    }
    public void SaveFileMachineCount()
    {
        PlayerPrefs.SetInt(fileMachinesCount, data.userData.fileMashine);
    }
    public void SaveFileMachineLevel()
    {
        PlayerPrefs.SetInt(fileMachinesLevel, data.userData.fileMashineLevel);
    }
    public void SaveFileMachineSpeedLevel()
    {
        PlayerPrefs.SetInt(fileMachinesSpeedLevel, data.userData.fileMashineSpeedLevel);
    }
    public void SavePackingMachineCount()
    {
        PlayerPrefs.SetInt(packingMachinesCount, data.userData.packingMashine);
    }
    public void SavePackingMachineLevel()
    {
        PlayerPrefs.SetInt(packingMachines_1_Level, data.userData.steakPackingMashineLevel);
    }
    public void SavePackingMachineSpeedLevel()
    {
        PlayerPrefs.SetInt(packingMachines_1_SpeedLevel, data.userData.steakPackingMashineSpeedLevel);
    }
    public void SavePackingMachine2Level()
    {
        PlayerPrefs.SetInt(packingMachines_2_Level, data.userData.farshPackingMashineLevel);
    }
    public void SavePackingMachine2SpeedLevel()
    {
        PlayerPrefs.SetInt(packingMachines_2_SpeedLevel, data.userData.farshPackingMashineSpeedLevel);
    }
    public void SavePackingMachine3Level()
    {
        PlayerPrefs.SetInt(packingMachines_3_Level, data.userData.filePackingMashineLevel);
    }
    public void SavePackingMachine3SpeedLevel()
    {
        PlayerPrefs.SetInt(packingMachines_3_SpeedLevel, data.userData.filePackingMashineSpeedLevel);
    }
    public void SaveShipCount()
    {
        PlayerPrefs.SetInt(ship, data.userData.ship);
    }
    public void SaveShipLevel()
    {
        PlayerPrefs.SetInt(shipLevel, data.userData.shipLevel);
    }
    public void SaveInWarCount()
    {
        PlayerPrefs.SetInt(inWar, data.userData.zoneInWar);
    }
    public void SaveInWarLevel()
    {
        PlayerPrefs.SetInt(inWarLevel, data.userData.zoneInWarLevel);
    }
    public void SaveCarPort()
    {
        PlayerPrefs.SetInt(carPort, data.userData.carPort);
    }
    public void SaveCarPortLevel()
    {
        PlayerPrefs.SetInt(carPortLevel, data.userData.carPortLevel);
    }
    public void SaveCar3()
    {
        PlayerPrefs.SetInt(car3, data.userData.zone3Car);
    }
    public void SaveCar3Level()
    {
        PlayerPrefs.SetInt(car3Level, data.userData.zone3CarLevel);
    }
    public void SaveCarOut()
    {
        PlayerPrefs.SetInt(carOut, data.userData.zoneOutWar);
    }
    public void SaveOutWarLevel()
    {
        PlayerPrefs.SetInt(carOutLevel, data.userData.zoneOutWarLevel);
    }
    public void SaveCar2Level()
    {
        PlayerPrefs.SetInt(car2Level, data.userData.zone2CarLevel);
    }
    public void SaveCar1Level()
    {
        PlayerPrefs.SetInt(car1Level, data.userData.zone1CarLevel);
    }
    public void SaveCarFinishZoneLevel()
    {
        PlayerPrefs.SetInt(finishCarLevel, data.userData.finishZoneCarLevel);
    }
    public void SaveUserLevel()
    {
        PlayerPrefs.SetString(userLevel, data.userData.levelUser);
    }
    public void SaveFirstOrder()
    {
        PlayerPrefs.SetInt(firstOrder, data.matchData.firstOrder);
    }
    public void SaveFirstPurshase()
    {
        if (data.userData.firstPurshase)
        {
            PlayerPrefs.SetInt(firstPurshase, 1);
        }
        else
        {
            PlayerPrefs.SetInt(firstPurshase, 0);
        }
    }
    public void SaveGetOffer()
    {
        if (data.userData.getOffer)
        {
            PlayerPrefs.SetInt(getOffer, 1);
        }
        else
        {
            PlayerPrefs.SetInt(getOffer, 0);
        }
    }
    public void SaveMoney()
    {
        PlayerPrefs.SetString(coins, data.userData.coins.Value.ToString());
    }
    public void SaveGold()
    {
        PlayerPrefs.SetInt(gold, data.userData.gold.Value);
    }
    public void SaveCrystal()
    {
        PlayerPrefs.SetInt(crystal, data.userData.cristalls.Value);
    }
    public void SaveOfflineLimit()
    {
        PlayerPrefs.SetInt(offlainLimit, data.userData.offlineLimit);
    }

    #region SAVE
    private void SavePlayerData()
    {
        //SaveProductInOutWarehouse();                     
    }
    

    private void SaveProductInOutWarehouse()
    {
        PlayerPrefs.SetInt(fishOut, data.userData.fishInWarehouse);
        PlayerPrefs.SetInt(unpSteakOut, data.userData.unpSteakInWarehouse);
        PlayerPrefs.SetInt(steakOut, data.userData.steakInWarehouse);
        
    }
    
    #endregion

    #region LOADS
    private void LoadPlayerData()
    {
        data.userData.ship = PlayerPrefs.GetInt(ship, 0);       
        data.userData.levelUser = PlayerPrefs.GetString(userLevel, "Carving mashine 1");
        data.userData.fishInWarehouse = PlayerPrefs.GetInt(fishOut,0);
        data.userData.unpSteakInWarehouse = PlayerPrefs.GetInt(unpSteakOut,0);
        data.userData.farshInWarehouse = PlayerPrefs.GetInt(farshOut,0);
        data.userData.unpFarshInWarehouse = PlayerPrefs.GetInt(unpFarshOut, 0);
        data.userData.fileInWarehouse = PlayerPrefs.GetInt(fileOut, 0);
        data.userData.unpFileInWarehouse = PlayerPrefs.GetInt(unpFileOut, 0);
        data.userData.steakInWarehouse = PlayerPrefs.GetInt(steakOut, 0);
        data.userData.offlineLimit = PlayerPrefs.GetInt(offlainLimit, 2);
        data.userData.zone3Car = PlayerPrefs.GetInt(car3,1);
        data.userData.zone3CarLevel = PlayerPrefs.GetInt(car3Level, 1);
        data.userData.zone2CarLevel = PlayerPrefs.GetInt(car2Level, 1);
        data.userData.zone1CarLevel = PlayerPrefs.GetInt(car1Level, 1);
        data.userData.finishZoneCarLevel = PlayerPrefs.GetInt(finishCarLevel, 1);
        string temp = PlayerPrefs.GetString(coins,"1150000");
        double _result = Double.Parse(temp);
        data.userData.coins.Value = (double)Math.Round((double)_result, 1);
        data.userData.allPlayTimeInMinutes = PlayerPrefs.GetInt(allTime, 0);
        data.userData.cristalls.Value = PlayerPrefs.GetInt(crystal, _crystalls);
        data.userData.gold.Value = PlayerPrefs.GetInt(gold, _trees);
        data.userData.carPort = PlayerPrefs.GetInt(carPort,1);
        data.userData.carPortLevel = PlayerPrefs.GetInt(carPortLevel, 1);        
        data.userData.warehaus_updated = PlayerPrefs.GetInt(stockCapasity);
        data.userData.shipLevel = PlayerPrefs.GetInt(shipLevel,1);
        data.userData.zoneInWar = PlayerPrefs.GetInt(inWar,0);
        data.userData.zoneInWarLevel = PlayerPrefs.GetInt(inWarLevel, 1);
        data.userData.zoneOutWar = PlayerPrefs.GetInt(carOut, 1);
        data.userData.zoneOutWarLevel = PlayerPrefs.GetInt(carOutLevel, 1);
        data.matchData.firstOrder = PlayerPrefs.GetInt(firstOrder, 0);      
        data.userData.startGame = PlayerPrefs.GetInt(startGame, 1);
        if (PlayerPrefs.GetInt(getOffer)==1)
        {
            data.userData.getOffer = true;            
        }
        else
        {
            data.userData.getOffer = false;
        }
        if (PlayerPrefs.GetInt(firstPurshase) == 1)
        {
            data.userData.firstPurshase = true;

        }
        else
        {
            data.userData.firstPurshase = false;
        }
        data.userData.headCutingMashine = PlayerPrefs.GetInt(carvingMachinesCount, 1);
        data.userData.headCutingMashineLevel = PlayerPrefs.GetInt(carvingMachines_1_Level, 1);
        data.userData.headCutingMashineSpeedLevel = PlayerPrefs.GetInt(carvingMachines_1_SpeedLevel, 1);
        data.userData.headCutingMashine2Level = PlayerPrefs.GetInt(carvingMachines_2_Level, 1);
        data.userData.headCutingMashine2SpeedLevel = PlayerPrefs.GetInt(carvingMachines_2_SpeedLevel, 1);
        data.userData.fishCleaningMashine = PlayerPrefs.GetInt(scalingMachinesCount, 0);
        data.userData.fishCleaningMashineLevel = PlayerPrefs.GetInt(scalingMachines_1_Level, 1);
        data.userData.fishCleaningMashineSpeedLevel = PlayerPrefs.GetInt(scalingMachines_1_SpeedLevel, 1);
        data.userData.fishCleaningMashine2Level = PlayerPrefs.GetInt(scalingMachines_2_Level, 1);
        data.userData.fishCleaningMashine2SpeedLevel = PlayerPrefs.GetInt(scalingMachines_2_SpeedLevel, 1);
        data.userData.fishCleaningMashine3Level = PlayerPrefs.GetInt(scalingMachines_3_Level, 1);
        data.userData.fishCleaningMashine3SpeedLevel = PlayerPrefs.GetInt(scalingMachines_3_SpeedLevel, 1);
        data.userData.steakMashine = PlayerPrefs.GetInt(steakingMachinesCount, 0);
        data.userData.steakMashineLevel = PlayerPrefs.GetInt(steakingMachines_1_Level, 1);
        data.userData.steakMashineSpeedLevel = PlayerPrefs.GetInt(steakingMachines_1_SpeedLevel, 1);
        data.userData.farshMashine = PlayerPrefs.GetInt(farshMachinesCount, 0);
        data.userData.farshMashineLevel = PlayerPrefs.GetInt(farshMachinesLevel, 1);
        data.userData.farshMashineSpeedLevel = PlayerPrefs.GetInt(farshMachinesSpeedLevel, 1);
        data.userData.fileMashine = PlayerPrefs.GetInt(fileMachinesCount, 0);
        data.userData.fileMashineLevel = PlayerPrefs.GetInt(fileMachinesLevel, 1);
        data.userData.fileMashineSpeedLevel = PlayerPrefs.GetInt(fileMachinesSpeedLevel, 1);
        data.userData.packingMashine = PlayerPrefs.GetInt(packingMachinesCount, 0);
        data.userData.steakPackingMashineLevel = PlayerPrefs.GetInt(packingMachines_1_Level, 1);
        data.userData.steakPackingMashineSpeedLevel = PlayerPrefs.GetInt(packingMachines_1_SpeedLevel, 1);
        data.userData.farshPackingMashineLevel = PlayerPrefs.GetInt(packingMachines_2_Level, 1);
        data.userData.farshPackingMashineSpeedLevel = PlayerPrefs.GetInt(packingMachines_2_SpeedLevel, 1);
        data.userData.filePackingMashineLevel = PlayerPrefs.GetInt(packingMachines_3_Level, 1);
        data.userData.filePackingMashineSpeedLevel = PlayerPrefs.GetInt(packingMachines_3_SpeedLevel, 1);
        if (data.userData.ship == 0)
        {
            data.matchData.isFue = true;
            fueSystem.StartFue();
        }
        else
        {
            data.matchData.isFue = false;
        }
        upgradeSystem.InitMultiplier();
        changeWarSizeSystem.InitStocks();
        machineControlSystem.StartInit();
        transportControlSystem.StartInit();
        carShipment.StartInit();
        Invoke(nameof(ConvertMoney), 2f);
    }
    private void ConvertMoney()
    {
        data.userData._coins.Value = Converter.instance.ConvertMoneyView(data.userData.coins.Value);
    }

   
    #endregion
}
