using UnityEngine;
using UniRx;

[CreateAssetMenu(menuName = "Data/MoneyData")]
public class MoneyData : ScriptableObject
{
    public enum State
    {
        None,
        Waiting,
        Accept,
        Failed
    }

    public ReactiveProperty<State> state = new ReactiveProperty<State>();

    public class Order
    {
        public int price;
        public Order(int price)
        {
            this.price = price;
        }
    }

    public Order curOrder;

    public void CreateOrder(int price)
    {
        curOrder = new Order(price);
        state.Value = State.Waiting;
    }

    public void Clear()
    {
        curOrder = null;
        state.Value = State.None;
    }
}
