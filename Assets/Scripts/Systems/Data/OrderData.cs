using UnityEngine;
using UniRx;

[CreateAssetMenu(menuName = "Data/OrderData")]

public class OrderData : ScriptableObject
{
    public IntReactiveProperty orderId;
    public IntReactiveProperty productId;
    public IntReactiveProperty orderCost;    
    public IntReactiveProperty productCount;
    public Sprite productSprite;
    public string productName;
    public string orderValute;
}
