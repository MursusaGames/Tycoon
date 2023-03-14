using UnityEngine;
using System.Collections;

public class FarmMenu : BaseMenu
{
    [SerializeField] private OrdersSystem ordersSystem;    

    private void OnEnable()
    {
        ordersSystem.GetInfo();        
    }

    
    public void OnFarmButton()
    {
        InterfaceManager.SetCurrentMenu(MenuName.Main);       
    }

    public void OnOrderBtn(int orderID, int id, string valute)
    {
        
    }
    public void ResetOrder()
    {
        ordersSystem.ResetCurrentOrder();        
    }       
}
