using UnityEngine;

public class Podlogka : MonoBehaviour
{
    [SerializeField] private StakeMashineWorcer_2 worcer;
    [SerializeField] private PackingMashineWorcer_2 _worcer;
    public bool packing;
    void OnEnable()
    {
        Invoke(nameof(CallWorcer), 1f);
    }

    private void CallWorcer()
    {
        if (packing) 
            _worcer.GetBox();
        else 
            worcer.TakeStake();
    }

    
}
