using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneInsideWar : TransportMenuContent
{
    private void OnEnable()
    {
        CheckInfo();
    }

    public void CheckInfo()
    {
        count.text = data.userData.zone2Car.ToString();
        cost.text = data.upgradesData.carInsideWarCost.ToString();        
    }
}
