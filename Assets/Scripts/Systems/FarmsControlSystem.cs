using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class FarmsControlSystem : BaseMonoSystem
{
    public override void Init(AppData data)
    {
        base.Init(data);
        SetObservables();
    }

    private void SetObservables()
    {
        data.matchData.state
            .Where(x => x == MatchData.State.WorcersTime)
            .Subscribe(_ => StartInit());
    }
    private void StartInit()
    {

    }

    public void BuyFarm()
    {

    }
    public void UpgradeFarm()
    {

    }
}
    
