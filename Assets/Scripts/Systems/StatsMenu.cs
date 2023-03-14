using UniRx.Extensions;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;

public class StatsMenu : BaseMenu
{
    [SerializeField] private TMP_Text _goldText;
    [SerializeField] private TMP_Text _gemText;

    [SerializeField] private GameObject _keysGroup;
    [SerializeField] private Image[] _keys;
    [SerializeField] private Color _activeKeysColor = Color.yellow;
    [SerializeField] private Color _deactiveKeysColor = Color.gray;

    public override void SetData(AppData data)
    {
        base.SetData(data);
        SetObservables();
    }

    private void SetObservables()
    {
        data.matchData.state
            .Where(x => x == MatchData.State.MainMenu)
            .Subscribe(_ => ToggleKeyGroup(false));
        data.matchData.state
            .Where(x => x == MatchData.State.Game)
            .Subscribe(_ => ToggleKeyGroup(true));

        //data.userData.coins.SubscribeToText(_goldText);
        data.userData.cristalls.SubscribeToText(_gemText);

        //data.userData.keys.Subscribe(_ => UpdateKeysCount());
    }

   /* private void UpdateKeysCount()
    {
        for(int i = 0; i < data.userData.keys.Value; i++)
            _keys[i].color = _activeKeysColor;

        for(int i = data.userData.keys.Value; i < _keys.Length; i++)
            _keys[i].color = _deactiveKeysColor;
    }*/

    private void ToggleKeyGroup(bool value)
    {
        _keysGroup.SetActive(value);
    }
}
