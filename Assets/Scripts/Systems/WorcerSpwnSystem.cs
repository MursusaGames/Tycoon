using System.Collections.Generic;
using UnityEngine;
using UniRx;

public enum Workers
{
    Logger, Planter
}
public class WorcerSpwnSystem : BaseMonoSystem
{
    [SerializeField] private GameObject eggsCollectorPrefab;
    [SerializeField] private GameObject poultryFarmerPrefab;
    [SerializeField] private MoneySystem moneySystem;
    [SerializeField] private Vector3 startPoint;
    [SerializeField] private Transform parent;
    private List<EggCollector> eggsCollectors = new List<EggCollector>();   

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
        /*for (int i =0; i < data.userData.eggCollectors.Value; i++)
        {
            var eggCollector = Instantiate(eggsCollectorPrefab, startPoint+(Vector3.right*i), Quaternion.identity, parent).GetComponent<EggCollector>();
            eggCollector._level = data.userData.eggCollectorsLevel.Value;
            eggCollector.FindTarget();            
        }
        for (int i = 0; i < data.userData.poultryFarmers.Value; i++)
        {
            var pourlyFarmer = Instantiate(poultryFarmerPrefab, startPoint, Quaternion.identity, parent).GetComponent<PourlyFarmer>();
            pourlyFarmer._level = data.userData.poultryFarmersLevel.Value;
            pourlyFarmer.FindTarget();            
        }*/
    }

    public void BuyEggCollector()
    {
       /* if (data.matchData.isFirstPlay)
        {
            data.matchData.state.Value = MatchData.State.FueState3;
        }
        if (moneySystem.Buy(data.upgradesData.eggCollectorCost))
        {
            SpawnEggCollector();
        }*/
    }
    public void SpawnEggCollector()
    {
       /* var eggCollector = Instantiate(eggsCollectorPrefab, startPoint, Quaternion.identity,parent).GetComponent<EggCollector>();
        eggsCollectors.Add (eggCollector);
        data.userData.eggCollectors.Value ++;
        eggCollector.FindTarget();
        if (!data.matchData.isFirstPlay) data.matchData.state.Value = MatchData.State.Game;*/
    }
    public void BuyPourlyFarmer()
    {
       /* if (data.matchData.isFirstPlay)
        {
            data.matchData.state.Value = MatchData.State.FueState2;
        }
        if (moneySystem.Buy(data.upgradesData.pourlyFarmerCost))
        {
            SpawnPourlyFarmer();
        }*/
    }
    public void SpawnPourlyFarmer()
    {
        /*var pourlyFarmer = Instantiate(poultryFarmerPrefab, startPoint, Quaternion.identity, parent).GetComponent<PourlyFarmer>();       
        data.userData.poultryFarmers.Value++;
        pourlyFarmer.FindTarget();
        if(!data.matchData.isFirstPlay) data.matchData.state.Value = MatchData.State.Game;*/
    }
}
