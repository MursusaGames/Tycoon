using TMPro;
using UniRx.Extensions;
using UnityEngine;

public class MainMenu : BaseMenu
{
    [SerializeField] private TMP_Text _levelNumText;
    [SerializeField] private TMP_Text _coins;
    [SerializeField] private TMP_Text _crystalls;
    [SerializeField] private TMP_Text _gold;
    [SerializeField] private GameObject chitPanel;
    [SerializeField] private GameObject settingsPanel;
    public override void SetData(AppData data)
    {
        base.SetData(data);
        data.matchData.currentLevelNum.SubscribeToText(_levelNumText);
        data.userData._coins.SubscribeToText(_coins);
        data.userData.cristalls.SubscribeToText(_crystalls);
        data.userData.gold.SubscribeToText(_gold);
    }

    
    public void OnTapToCore()
    {
        data.matchData.state.Value = MatchData.State.InitializeLevel;
        InterfaceManager.SetActiveStatus(MenuName.Main, false);        
    }

    public void OnSettingsButton()
    {
        if (data.matchData.isFue) return;
        if(settingsPanel.activeInHierarchy) InterfaceManager.SetCurrentMenu(MenuName.Main);
        else InterfaceManager.SetCurrentMenu(MenuName.Settings);
    }
    public void OnWorkersButton()
    {
        InterfaceManager.SetCurrentMenu(MenuName.Worker);
    }
    public void OnFarmButton()
    {
        InterfaceManager.SetCurrentMenu(MenuName.Farm);
    }
    public void OnWorkButton()
    {
        if (data.matchData.isFue) return;
        InterfaceManager.SetCurrentMenu(MenuName.Work);
    }
    public void OnStatsButton()
    {
        if(data.matchData.isFue) return;
        InterfaceManager.SetCurrentMenu(MenuName.Stats);
    }
    public void OnShopButton()
    {
        if (data.matchData.isFue) return;
        InterfaceManager.SetCurrentMenu(MenuName.Shop);
    }
    public void OnTransportButton()
    {
        if (data.matchData.isFue) return;
        InterfaceManager.SetCurrentMenu(MenuName.Transport);
    }    
    public void ChitPanelShow()
    {
        chitPanel.SetActive(true);
    }
    public void ChitPanelHide()
    {
        chitPanel.SetActive(false);
    }
}
