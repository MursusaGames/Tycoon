using UnityEngine;
using UniRx;

[CreateAssetMenu(menuName = "Data/MatchData")]
public class MatchData : ScriptableObject
{
    public enum State
    {
        None,
        AppStart,
        InitializeLevel,
        MainMenu,
        InitializeTrees,
        WorcersTime,
        Game,
        Finish,
        GameOver,
        Reward,
        Bonus        
    }

    public ReactiveProperty<State> state = new ReactiveProperty<State>(State.None);    
    public OrderDataContainer orderContainer;
    public IntReactiveProperty currentLevelNum;
    public IntReactiveProperty sessionCoins;
    public IntReactiveProperty currentPlayerPlace; // place in the race
    public bool isFirstPlay;
    public int repositoriesNumber;
    public int fisInBox;
    public int stakeInBox;
    public bool isTime;
    public bool isCrystal;
    public bool isFue;
    public int firstOrder;
}
