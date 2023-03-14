using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ForestControlSystem : BaseMonoSystem
{
    public List<Forest> woods = new List<Forest>();

    public override void Init(AppData data)
    {
        base.Init(data);
        SetObservables();
    }

    private void SetObservables()
    {
        data.matchData.state
            .Where(x => x == MatchData.State.InitializeTrees)
            .Subscribe(_ => FindAllTrees());
    }
    private void Start()
    {
        //FindAllTrees();
    }

    private void FindAllTrees()
    {
        Debug.Log("State InitializeTree");
        int i = 0;
        var trees = FindObjectsOfType<Forest>();
        foreach(var tree in trees)
        {
            woods.Add(tree);
            tree.id = i;
            i++;
        }
        data.matchData.state.Value = MatchData.State.InitializeLevel;
    }
}
