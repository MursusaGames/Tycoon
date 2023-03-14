using UnityEngine;
using UniRx;

[CreateAssetMenu(menuName = "Data/UserData")]
public class UserData : ScriptableObject
{
    public int allPlayTimeInMinutes;
    public StringReactiveProperty userName;
    public DoubleReactiveProperty coins;
    public StringReactiveProperty _coins;
    public IntReactiveProperty cristalls;
    public StringReactiveProperty _crystalls;
    public IntReactiveProperty gold;
    public StringReactiveProperty _gold;
    public int offlineLimit;
    public float ADMultiplier = 1;
    public float ADSpeedMultiplier = 1;
    public FloatReactiveProperty headCut_1_multiplier;
    public FloatReactiveProperty headCut_1_multiplierCoef;
    public FloatReactiveProperty headCut_2_multiplier;
    public FloatReactiveProperty headCut_2_multiplierCoef;
    public FloatReactiveProperty fishClean_1_multiplier;
    public FloatReactiveProperty fishClean_1_multiplierCoef;
    public FloatReactiveProperty fishClean_2_multiplier;
    public FloatReactiveProperty fishClean_2_multiplierCoef;
    public FloatReactiveProperty fishClean_3_multiplier;
    public FloatReactiveProperty fishClean_3_multiplierCoef;
    public FloatReactiveProperty steakMashine_multiplier;
    public FloatReactiveProperty steakMashine_multiplierCoef;
    public FloatReactiveProperty farshMashine_multiplier;
    public FloatReactiveProperty farshMashine_multiplierCoef;
    public FloatReactiveProperty fileMashine_multiplier;
    public FloatReactiveProperty fileMashine_multiplierCoef;
    public FloatReactiveProperty packingMashine_1_multiplier;
    public FloatReactiveProperty packingMashine_1_multiplierCoef;
    public FloatReactiveProperty packingMashine_2_multiplier;
    public FloatReactiveProperty packingMashine_2_multiplierCoef;
    public FloatReactiveProperty packingMashine_3_multiplier;
    public FloatReactiveProperty packingMashine_3_multiplierCoef;
    public IntReactiveProperty warehousePromice;     
    public int zone1Car;
    public int zone1CarLevel;
    public int zone1CarCapasity;
    public float zone1CarSpeed;
    public int finishZoneCarLevel;
    public int finishZoneCarCapasity;
    public float finishZoneCarSpeed;
    public int ship;
    public int shipLevel;
    public float shipSpeed;
    public int shipCapasity;
    public int ship1Level;
    public float ship1Speed;
    public int carPort;
    public int carPortLevel;
    public int carPortCapasity;
    public float carPortSpeed;
    public float carPortSpeedLevel;
    //public int zoneInsideWar;
    //public int zoneInsideWarLevel;
    public int zoneOutWar;
    public int zoneOutWarLevel;
    public int zoneOutWarCapasity;
    public float zoneOutWarSpeed;
    public int zoneInWar;
    public int zoneInWarLevel;
    public int zoneInWarCapasity;
    public float zoneInWarSpeed;
    public float zoneInWarSpeedLevel;
    public int zone2Car;
    public int zone2CarLevel;
    public int zone2CarCapasity;
    public float zone2CarSpeed;
    public int zone3Car;
    public int zone3CarCapacity;
    public int zone3CarLevel;
    public float zone3CarSpeed;
    public int headCutingMashine;
    public double headCutingMashinePromice;
    public string _headCutingMashinePromice;
    public double headCutingMashine2Promice;
    public string _headCutingMashine2Promice;
    public int headCutingMashineLevel;
    public float headCutingMashineSpeed;
    public int headCutingMashineSpeedLevel;
    public int headCutingMashine2Level;
    public float headCutingMashine2Speed;
    public int headCutingMashine2SpeedLevel;
    public int fishCleaningMashine;
    public int fishCleaningMashineLevel;
    public float fishCleaningMashineSpeed;
    public int fishCleaningMashineSpeedLevel;
    public double fishCleaningMashinePromice;
    public string _fishCleaningMashinePromice;
    public int fishCleaningMashine2;
    public int fishCleaningMashine2Level;
    public float fishCleaningMashine2Speed;
    public int fishCleaningMashine2SpeedLevel;
    public double fishCleaningMashine2Promice;
    public string _fishCleaningMashine2Promice;
    public int fishCleaningMashine3;
    public int fishCleaningMashine3Level;
    public float fishCleaningMashine3Speed;
    public int fishCleaningMashine3SpeedLevel;
    public double fishCleaningMashine3Promice;
    public string _fishCleaningMashine3Promice;
    public int fishProcessingMashine;
    public int fishProcessingMashineLevel;
    public int fileMashine;
    public int fileMashineLevel;
    public float fileMashineSpeed;
    public int fileMashineSpeedLevel;
    public double fileMashinePromice;
    public string _fileMashinePromice;
    public int farshMashine;
    public int farshMashineLevel;
    public float farshMashineSpeed;
    public int farshMashineSpeedLevel;
    public double farshMashinePromice;
    public string _farshMashinePromice;
    public int steakMashine;
    public int steakMashineLevel;
    public float steakMashineSpeed;
    public int steakMashineSpeedLevel;
    public double steakMashinePromice;
    public string _steakMashinePromice;
    public int packingMashine;
    public double steakPackingMashinePromice;
    public string _steakPackingMashinePromice;
    public int steakPackingMashineLevel;
    public float steakPackingMashineSpeed;
    public int steakPackingMashineSpeedLevel;
    public int farshPackingMashine;
    public double farshPackingMashinePromice;
    public string _farshPackingMashinePromice;
    public int farshPackingMashineLevel;
    public float farshPackingMashineSpeed;
    public int farshPackingMashineSpeedLevel;
    public int filePackingMashine;
    public double filePackingMashinePromice;
    public string _filePackingMashinePromice;
    public int filePackingMashineLevel;
    public float filePackingMashineSpeed;
    public int filePackingMashineSpeedLevel;
    public bool isWatchADCrystal;
    public bool isWatchADGold;
    public int warehaus_updated;
    public bool getOrder;
    public bool inDeliver;
    public bool inWaiting;
    public bool closeGame;
    public bool timerOff;
    public int fishInWarehouse;
    public int unpSteakInWarehouse;
    public int steakInWarehouse;
    public int unpFarshInWarehouse;
    public int farshInWarehouse;
    public int unpFileInWarehouse;
    public int fileInWarehouse;
    public double fishPaletCost;
    public double unpSteakPaletCost;
    public double steakPaletCost;
    public double unpFarshPaletCost;
    public double farshPaletCost;
    public double unpFilePaletCost;
    public double filePaletCost;
    public int currentProductID;
    public int currentDeliverID;
    public double currentmoney;
    public double currentgold;
    public int currentProductCount;
    public int currentMaxProductCount;
    public bool getOffer;
    public bool firstPurshase;
    public string levelUser;
    public int dayInGame;
    public int startGame;
}