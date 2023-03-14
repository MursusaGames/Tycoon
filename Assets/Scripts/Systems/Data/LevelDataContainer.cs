using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/LevelDataContainer")]
public class LevelDataContainer : ScriptableObject
{
    public List<LevelData> levels;
}

[System.Serializable]
public struct LevelData
{
    public GameObject prefab;
    public bool bonusLevel;
}
