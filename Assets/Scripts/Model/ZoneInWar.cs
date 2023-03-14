using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ZoneInWar : TransportMenuContent
{
    [SerializeField] private GameObject buyBtn;
    [SerializeField] private TMP_Text upgradeCost;
    [SerializeField] private TMP_Text hireCost;
    public bool inWar;
    public bool outWar;

    private void OnEnable()
    {
        CheckInfo();
    }

    public void CheckInfo()
    {
        if (inWar)
        {
            if (data.userData.zoneInWar < 2)
            {
                buyBtn.SetActive(true);
            }
            upgradeCost.text = Converter.instance.ConvertMoneyView(data.upgradesData.zoneInWarUpgradeCost);
            count.text = data.userData.zoneInWar.ToString();
            if (buyBtn.activeInHierarchy)
            {
                hireCost.text = Converter.instance.ConvertMoneyView(data.upgradesData.carInWarCost);
                if (data.userData.zoneInWar == 2)
                {
                    buyBtn.SetActive(false);
                }
            }
                
        }
        if (outWar)
        {
            if (data.userData.zone3Car == 1)
            {
                buyBtn.SetActive(true);
            }
            upgradeCost.text = Converter.instance.ConvertMoneyView(data.upgradesData.carOutUpgradeCost);
            count.text = data.userData.zone3Car.ToString();
            if (buyBtn.activeInHierarchy)
            {
                hireCost.text = Converter.instance.ConvertMoneyView(data.upgradesData.zone3CarCost);
                if (data.userData.zone3Car == 2)
                {
                    buyBtn.SetActive(false);
                }
            }
                
        }

    }    
}
