using UnityEngine;

public class BoxInHeadCutMashine : MonoBehaviour
{
    [SerializeField] private AppData data;
    [SerializeField] private GameObject fish_1;
    [SerializeField] private GameObject fish_2;
    [SerializeField] private HeadCutMashineWorcerOut worcer;
    [SerializeField] private FishCleaningWorcer_2 worcerClean;
    [SerializeField] private GameObject moneyBoom;    
    public int fishCount = 0;
    public bool cleanMashine;
    

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fish"))
        {
            fishCount++;
            Destroy(other.gameObject);
            if (fishCount == 3) fish_1.SetActive(true);
            if(fishCount == data.matchData.fisInBox)
            {
                fish_2.SetActive(true);
                if(cleanMashine) worcerClean.GetBox();
                else worcer.GetBox();
                GetIncome();
                fishCount = 0;                
            }
        }        
    }
    private void GetIncome()
    {
        moneyBoom.SetActive(true);
    }
    public void HideFish()
    {
        fish_1.SetActive(false);
        fish_2.SetActive(false);
    }
}
