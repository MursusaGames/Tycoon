using UnityEngine;

[CreateAssetMenu(menuName = "Data/AppData")]
public class AppData : ScriptableObject
{
    public UserData userData;
    public LocalizationData localizationData;
    public MatchData matchData;    
    public MoneyData moneyData;
    public UpgradesData upgradesData;    
    public RewardData rewardData;
}
