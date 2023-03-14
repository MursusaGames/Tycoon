using UnityEngine;

public class BoxInStakeMashine : MonoBehaviour
{
    [SerializeField] private GameObject firstStake;    
    [SerializeField] private GameObject moneyBoom;
    public void AddFirstStake()
    {
        firstStake.SetActive(true);
        moneyBoom.SetActive(true);
    }
    
    public void ClearBox()
    {
        firstStake.SetActive(false);        
    }
}
