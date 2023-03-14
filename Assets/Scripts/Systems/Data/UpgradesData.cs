using UnityEngine;
using UniRx;

[CreateAssetMenu(menuName = "Data/UpgradesData")]
public class UpgradesData : ScriptableObject
{
    [Header("Start values")]
    public int eggCollectorStartEfficiency;
    public int pourlyFarmerStartEfficiency;
    public int farmStartEfficiency;
    public int shipStartEfficiency;

    [Header("Cost farm , Worcers , Transport ")]
    public int eggCollectorCost;
    public int pourlyFarmerCost;
    public int farmCost;
    public int shipCost;
    public int carPortCost;
    public int carInWarCost;
    public int carInsideWarCost;
    public int zone1CarCost;
    public int zone2CarCost;
    public int zone3CarCost;
    public double headCutingMashineCost;
    public string _headCutingMashineCost;
    public double fishCleaning1MashineCost;
    public string _fishCleaning1MashineCost;
    public double fishCleaning2MashineCost;
    public string _fishCleaning2MashineCost;
    public double fishCleaning3MashineCost;
    public string _fishCleaning3MashineCost;
    public double steakPackingMashineCost;
    public string _steakPackingMashineCost;
    public double farshPackingMashineCost;
    public double filePackingMashineCost;
    public double steakMashineCost;
    public string _steakMashineCost;
    public double farshMashineCost;
    public string _farshMashineCost;
    public double fileMashineCost;
    public string _fileMashineCost;

    [Header("Cost off Upgrade")]
    public double carOutUpgradeCost;
    public string _carOutUpgradeCost;
    public double finishZoneCarUpgradeCost;
    public string _finishZoneCarUpgradeCost;
    public double war_1_upgradeCost;
    public string _war_1_upgradeCost;
    public double shipUpgradeCost;
    public string _shipUpgradeCost;
    public double carPortUpgradeCost;
    public string _carPortUpgradeCost;
    public double zoneInWarUpgradeCost;
    public string _zoneInWarUpgradeCost;
    public double zoneInsideWarUpgradeCost;
    public string _zoneInsideWarUpgradeCost;
    public double zone1CarUpgradeCost;
    public string _zone1CarUpgradeCost;
    public double zone2CarUpgradeCost;
    public string _zone2CarUpgradeCost;
    public double zone3CarUpgradeCost;
    public string _zone3CarUpgradeCost;
    public int stepOffMultyplyUpgrade;
    public DoubleReactiveProperty headCutMashine_1_UpgradeCost;
    public StringReactiveProperty _headCutMashine_1_UpgradeCost;
    public DoubleReactiveProperty headCutMashine_2_UpgradeCost;
    public StringReactiveProperty _headCutMashine_2_UpgradeCost;
    public DoubleReactiveProperty headCutMashine_1_UpgradeSpeedCost;
    public StringReactiveProperty _headCutMashine_1_UpgradeSpeedCost;
    public DoubleReactiveProperty headCutMashine_2_UpgradeSpeedCost;
    public StringReactiveProperty _headCutMashine_2_UpgradeSpeedCost;
    public DoubleReactiveProperty fishCleanMashine_1_UpgradeCost;
    public StringReactiveProperty _fishCleanMashine_1_UpgradeCost;
    public DoubleReactiveProperty fishCleanMashine_1_UpgradeSpeedCost;
    public StringReactiveProperty _fishCleanMashine_1_UpgradeSpeedCost;
    public DoubleReactiveProperty fishCleanMashine_2_UpgradeCost;
    public StringReactiveProperty _fishCleanMashine_2_UpgradeCost;
    public DoubleReactiveProperty fishCleanMashine_2_UpgradeSpeedCost;
    public StringReactiveProperty _fishCleanMashine_2_UpgradeSpeedCost;
    public DoubleReactiveProperty fishCleanMashine_3_UpgradeCost;
    public StringReactiveProperty _fishCleanMashine_3_UpgradeCost;
    public DoubleReactiveProperty fishCleanMashine_3_UpgradeSpeedCost;
    public StringReactiveProperty _fishCleanMashine_3_UpgradeSpeedCost;
    public DoubleReactiveProperty steakMashine_UpgradeCost;
    public StringReactiveProperty _steakMashine_UpgradeCost;
    public DoubleReactiveProperty steakMashine_UpgradeSpeedCost;
    public StringReactiveProperty _steakMashine_UpgradeSpeedCost;
    public DoubleReactiveProperty farshMashine_UpgradeCost;
    public StringReactiveProperty _farshMashine_UpgradeCost;
    public DoubleReactiveProperty farshMashine_UpgradeSpeedCost;
    public StringReactiveProperty _farshMashine_UpgradeSpeedCost;
    public DoubleReactiveProperty fileMashine_UpgradeCost;
    public StringReactiveProperty _fileMashine_UpgradeCost;
    public DoubleReactiveProperty fileMashine_UpgradeSpeedCost;
    public StringReactiveProperty _fileMashine_UpgradeSpeedCost;
    public DoubleReactiveProperty packingMashine_1_UpgradeCost;
    public StringReactiveProperty _packingMashine_1_UpgradeCost;
    public DoubleReactiveProperty packingMashine_1_UpgradeSpeedCost;
    public StringReactiveProperty _packingMashine_1_UpgradeSpeedCost;
    public DoubleReactiveProperty packingMashine_2_UpgradeCost;
    public StringReactiveProperty _packingMashine_2_UpgradeCost;
    public DoubleReactiveProperty packingMashine_2_UpgradeSpeedCost;
    public StringReactiveProperty _packingMashine_2_UpgradeSpeedCost;
    public DoubleReactiveProperty packingMashine_3_UpgradeCost;
    public StringReactiveProperty _packingMashine_3_UpgradeCost;
    public DoubleReactiveProperty packingMashine_3_UpgradeSpeedCost;
    public StringReactiveProperty _packingMashine_3_UpgradeSpeedCost;

}
