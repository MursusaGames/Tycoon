using UnityEngine;

public class SystemInitializer : MonoBehaviour
{
    [SerializeField] private BaseMonoSystem[] _systems;

    public void InitializeSystems(AppData data)
    {
        foreach (var system in _systems)
        {
            system.Init(data);
        }

        data.matchData.state.Value = MatchData.State.InitializeLevel;
    }
}
