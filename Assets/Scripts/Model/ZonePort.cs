using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ZonePort : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI carPortEfficiency;
    [SerializeField] private Text carPortCost;
    [SerializeField] private Text carPortUpgradeCost;
    [SerializeField] private TextMeshProUGUI carPortCount;
    [SerializeField] private AppData data;

    private void OnEnable()
    {
        CheckInfo();
    }

    public void CheckInfo()
    {
        carPortCount.text = data.userData.carPort.ToString();
        carPortCost.text = data.upgradesData.carPortCost.ToString();
        carPortUpgradeCost.text = data.upgradesData.carPortUpgradeCost.ToString();
        carPortEfficiency.text = (data.upgradesData.shipStartEfficiency - data.userData.carPortLevel * 2).ToString();
    }
}
