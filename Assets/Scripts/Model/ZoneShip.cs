using TMPro;
using UnityEngine;

public class ZoneShip : MonoBehaviour
{
    
    [SerializeField] private TMP_Text shipCost;
    [SerializeField] private TMP_Text upgradeCost;
    [SerializeField] private TextMeshProUGUI shipCount;    
    [SerializeField] private AppData data;
    [SerializeField] private GameObject buyBtn;
    public bool ship;

    private void Start()
    {
        CheckInfo();
    }

    public void CheckInfo()
    {
        if (ship)
        {
            if (data.userData.ship == 1)
            {
                buyBtn.SetActive(true);
            }
            shipCount.text = data.userData.ship.ToString();
            //shipCost.text = data.upgradesData._shipCost.ToString();
            upgradeCost.text = data.upgradesData._shipUpgradeCost;
            if (data.userData.ship == 2)
            {
                buyBtn.SetActive(false);
            }
        }
        else
        {
            if (data.userData.carPort == 1)
            {
                buyBtn.SetActive(true);
            }
            shipCount.text = data.userData.carPort.ToString();
            //shipCost.text = data.upgradesData.carPortCost.ToString();
            upgradeCost.text = data.upgradesData._carPortUpgradeCost;
            if (data.userData.carPort == 2)
            {
                buyBtn.SetActive(false);
            }
        }             
    }
}
