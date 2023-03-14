using System.Collections.Generic;
using UnityEngine;
using UniRx;


[CreateAssetMenu(menuName = "Data/OrderDataContainer")]

public class OrderDataContainer : ScriptableObject
{
    public List<OrderData>orders;    
}
