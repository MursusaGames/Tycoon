using UnityEngine;
using TMPro;
using System;

public class ShopMenu : BaseMenu
{
    [SerializeField] private MatchData matchData;
    [SerializeField] private TimeCountSystem _timeCountSystem;    
    [SerializeField] private TextMeshProUGUI timeText;    
    
    private void OnEnable()
    {
        if (matchData.isTime)
        {
            _timeCountSystem.StartTimer();
        }
    }
    public void OnShopButton()
    {
        InterfaceManager.SetCurrentMenu(MenuName.Main);        
    }    
    
    private void Update()
    {
        timeText.text = _timeCountSystem.GetTime();        
    }
}
