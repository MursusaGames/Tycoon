using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorkMenu : BaseMenu
{
    [SerializeField] private GameObject warehouseScroll;
    [SerializeField] private GameObject transportScroll;
    [SerializeField] private GameObject mashinesScroll;
    [SerializeField] private Image warBtn;
    [SerializeField] private Image transportBtn;
    [SerializeField] private Image mashinesBtn;
    [SerializeField] private Sprite activFon;
    [SerializeField] private Sprite unactivFon;
    [SerializeField] private TextMeshProUGUI warehouseText;
    [SerializeField] private TextMeshProUGUI transportText;
    [SerializeField] private TextMeshProUGUI mashinesText;
    [SerializeField] private GameObject warehouseBGinActivMode;
    [SerializeField] private GameObject transportBGinActivMode;
    [SerializeField] private GameObject mashinesBGinActivMode;
    public void OnWorkButton()
    {
        if (data.matchData.isFue) return;
        InterfaceManager.SetCurrentMenu(MenuName.Main);        
    }
    public void ChangeMenuScroll(int id)
    {
        if (data.matchData.isFue) return;
        switch (id)
        {
            case 0: 
                warehouseScroll.SetActive(true);
                transportScroll.SetActive(false);
                mashinesScroll.SetActive(false);
                warBtn.sprite = activFon;
                transportBtn.sprite = unactivFon;
                mashinesBtn.sprite = unactivFon;
                warehouseText.color = Color.white;
                transportText.color = Color.cyan;
                mashinesText.color = Color.cyan;
                warehouseBGinActivMode.SetActive(true);
                transportBGinActivMode.SetActive(false);
                mashinesBGinActivMode.SetActive(false);
                break;
            case 1:
                warehouseScroll.SetActive(false);
                transportScroll.SetActive(true);
                mashinesScroll.SetActive(false);
                warBtn.sprite = unactivFon;
                transportBtn.sprite = activFon;
                mashinesBtn.sprite = unactivFon;
                warehouseText.color = Color.cyan;
                transportText.color = Color.white;
                mashinesText.color = Color.cyan;
                warehouseBGinActivMode.SetActive(false);
                transportBGinActivMode.SetActive(true);
                mashinesBGinActivMode.SetActive(false);
                break;
            case 2:
                warehouseScroll.SetActive(false);
                transportScroll.SetActive(false);
                mashinesScroll.SetActive(true);
                warBtn.sprite = unactivFon;
                transportBtn.sprite = unactivFon;
                mashinesBtn.sprite = activFon;
                warehouseText.color = Color.cyan;
                transportText.color = Color.cyan;
                mashinesText.color = Color.white;
                warehouseBGinActivMode.SetActive(false);
                transportBGinActivMode.SetActive(false);
                mashinesBGinActivMode.SetActive(true);
                break;
        }
    }
}
