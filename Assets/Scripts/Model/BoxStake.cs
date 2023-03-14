using UnityEngine;

public class BoxStake : MonoBehaviour
{
    [SerializeField] private GameObject stake_1;
    [SerializeField] private GameObject stake_2;
    [SerializeField] private GameObject stake_3;    
    [SerializeField] private PackingMashineWorcer_2 worcer;
    [SerializeField] private GameObject moneyBoom;
    private int stakeCount=0;

    
    public void TakeStake()
    {
        stakeCount++;
        stake_1.SetActive(stakeCount >= 2);
        stake_2.SetActive(stakeCount >= 3);
        stake_3.SetActive(stakeCount >= 4);
        if(stakeCount == 1)
        {
            moneyBoom.SetActive(true); 
            worcer.TakeBox();
            stake_1.SetActive(false);
            stake_2.SetActive(false);
            stake_3.SetActive(false);
            stakeCount = 0;
        }
    }
}
