using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;

public enum MenuName
{
    None, Main, Worker, Work, Stats, Settings, GameOver, Transport, Shop, Farm
}
public class InterfaceManager : BaseMonoSystem
{
    private static InterfaceManager _instance;

    [SerializeField] private BaseMenu[] _menus;
    [SerializeField] private SaveDataSystem _saveData;
    public override void Init(AppData data)
    {
        base.Init(data);

        if (_instance != null) Destroy(_instance.gameObject);
        _instance = this;

        AddDataForAllBaseMenu();

        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Time.timeScale = 1;

        SetObservables();
    }

    private void SetObservables()
    {
        data.matchData.state
            .Where(x => x == MatchData.State.MainMenu)
            .Subscribe(_ => SetMainMenu());
    }

    private void SetMainMenu()
    {
        SetCurrentMenu(MenuName.Main);        
    }

    private void AddDataForAllBaseMenu()
    {
        foreach (var baseMenu in _menus)
        {
            baseMenu.SetData(data);
        }
    }
    private void SetMenu(MenuName name)
    {
        if (name == MenuName.Main)
        {
            SaveDataSystem.Instance.SetMenu(false);
        }
        else
        {
            SaveDataSystem.Instance.SetMenu(true);
        }
    }

    /// <summary>
    /// Makes the specified menu the only one enabled, all other menus are disabled
    /// </summary>
    /// <param name="name"></param>
    public static void SetCurrentMenu(MenuName name)
    {
        foreach (var baseMenu in _instance._menus)
        {
            var state = baseMenu.Name == name;
            baseMenu.gameObject.SetActive(state);
            baseMenu.SetState(state);
        }

        _instance.SetMenu(name);


    }

    /// <summary>
    /// Turns on/off the specified menu
    /// </summary>
    /// <param name="name"></param>
    /// <param name="state"></param>
    public static void SetActiveStatus(MenuName name, bool state)
    {
        var baseMenu = _instance._menus.SingleOrDefault(m => m.Name == name);
        if (baseMenu == null) return;
        baseMenu.gameObject.SetActive(state);
        baseMenu.SetState(state);
    }

    /// <summary>
    /// Get the activation status of the desired menu
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static bool GetMenuActive(MenuName name)
    {
        var baseMenu = _instance._menus.SingleOrDefault(m => m.Name == name);
        if (baseMenu == null) return false;
        return baseMenu.State;
    }
}
