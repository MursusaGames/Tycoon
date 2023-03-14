using UnityEngine;

public class ShipmentRoofBtn : MonoBehaviour
{
    [SerializeField] private GameObject orderBtn;
    [SerializeField] private MainMenu mainMenu;
    
    public void OnBtnPressed()
    {
        if (orderBtn.activeInHierarchy)
        {
            mainMenu.OnFarmButton();
        }
    }
}
