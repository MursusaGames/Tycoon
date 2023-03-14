using UnityEngine;
using TMPro;
public class Warehouse_6_content : TransportMenuContent
{
    [SerializeField] private TextMeshProUGUI costWarehouse;
    [SerializeField] private GameObject btn;
    private void OnEnable()
    {
        CheckInfo();
    }

    public void CheckInfo()
    {
        count.text = data.userData.warehaus_updated == 0? "20": data.userData.warehaus_updated == 1 ? "24":
            data.userData.warehaus_updated == 2 ? "28": "32";
        costWarehouse.text = data.upgradesData.war_1_upgradeCost.ToString();
        if(data.userData.warehaus_updated == 3)
        {
            btn.SetActive(false);
        }
    }
}
