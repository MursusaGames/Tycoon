using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ZoneLine_1 : TransportMenuContent
{
    private void OnEnable()
    {
        CheckInfo();
    }

    public void CheckInfo()
    {
        count.text = data.userData.zone1Car.ToString();
        cost.text = data.upgradesData.zone1CarCost.ToString();        
    }
}
