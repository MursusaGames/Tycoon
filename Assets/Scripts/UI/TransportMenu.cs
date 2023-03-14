using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TransportMenu : BaseMenu
{
    [SerializeField] private GameObject transportScroll;
    [SerializeField] private GameObject pirsScroll;
    [SerializeField] private Image pirsBtn;
    [SerializeField] private Image transportBtn;
    [SerializeField] private Sprite activFon;
    [SerializeField] private Sprite unactivFon;
    [SerializeField] private TextMeshProUGUI pirsText;
    [SerializeField] private TextMeshProUGUI transportText;    
    [SerializeField] private GameObject pirsBGinActivMode;
    [SerializeField] private GameObject transportBGinActivMode;
    

    public void OnTransportButton()
    {
        if (data.matchData.isFue) return;
        InterfaceManager.SetCurrentMenu(MenuName.Main);        
    }
    public void ChangeMenuScroll(int id)
    {
        if(data.matchData.isFue) return;    
        switch (id)
        {
            case 0:
                transportScroll.SetActive(true);
                pirsScroll.SetActive(false);
                transportBtn.sprite = activFon;
                pirsBtn.sprite = unactivFon;                
                transportText.color = Color.white;
                pirsText.color = Color.cyan;                
                pirsBGinActivMode.SetActive(false);
                transportBGinActivMode.SetActive(true);                
                break;
            case 1:
                transportScroll.SetActive(false);
                pirsScroll.SetActive(true);
                transportBtn.sprite = unactivFon;
                pirsBtn.sprite = activFon;
                transportText.color = Color.cyan;
                pirsText.color = Color.white;
                pirsBGinActivMode.SetActive(true);
                transportBGinActivMode.SetActive(false);
                break;            
        }
    }
}
