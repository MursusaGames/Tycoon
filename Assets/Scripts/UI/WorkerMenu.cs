using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WorkerMenu : BaseMenu
{
    [SerializeField] private TextMeshProUGUI eggCollectorEfficiencyText;
    [SerializeField] private TextMeshProUGUI pourlyFarmerEfficiencyText;
    [SerializeField] private Text eggCollectorCostText;
    [SerializeField] private Text eggCollectorUpgradeCostText;
    [SerializeField] private Text pourlyFarmerCostText;
    [SerializeField] private Text pourlyFarmerUpgradeCostText;
    [SerializeField] private TextMeshProUGUI eggsCollectorsNumberText;
    [SerializeField] private TextMeshProUGUI pourlyFarmersNumberText;
    

    private void OnEnable()
    {
        /*eggCollectorEfficiencyText.text = (data.upgradesData.eggCollectorStartEfficiency - (data.userData.eggCollectorsLevel.Value * data.upgradesData.stepOffMultyplyUpgrade)).ToString();
        pourlyFarmerEfficiencyText.text = (data.upgradesData.pourlyFarmerStartEfficiency - (data.userData.poultryFarmersLevel.Value * data.upgradesData.stepOffMultyplyUpgrade)).ToString();
        eggCollectorCostText.text = data.upgradesData.eggCollectorCost.ToString();
        pourlyFarmerCostText.text = data.upgradesData.pourlyFarmerCost.ToString();
        eggCollectorUpgradeCostText.text = data.upgradesData.eggCollectorUpgradeCost.ToString();
        pourlyFarmerUpgradeCostText.text = data.upgradesData.pourlyFarmerUpgradeCost.ToString();
        eggsCollectorsNumberText.text = data.userData.eggCollectors.ToString();
        pourlyFarmersNumberText.text = data.userData.poultryFarmers.ToString();*/       
    }
    public void OnWorcerButton()
    {
        InterfaceManager.SetCurrentMenu(MenuName.Main);        
    }
}
