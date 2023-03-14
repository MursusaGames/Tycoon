using UnityEngine;
using UniRx;


[CreateAssetMenu(menuName = "Data/RewardData")]
public class RewardData : ScriptableObject
{
    public enum RewardTypes { None = 0, Skin, Gems }
    public GameObject gemsChestPrefab;
    public int gemsAmout = 100;

    [Space]
    public RewardTypes rewardType;
    public GameObject currentRewardView;
    public IntReactiveProperty rewardProgress;
    
    [ContextMenu("Clear Current Reward")]
    public void ClearCurrentReward()
    {
        rewardType = RewardTypes.None;
        currentRewardView = null;
        rewardProgress.Value = 0;

        PlayerPrefs.SetInt("rewardProgress", rewardProgress.Value);
        PlayerPrefs.SetInt("rewardType", (int)rewardType);
    }
}