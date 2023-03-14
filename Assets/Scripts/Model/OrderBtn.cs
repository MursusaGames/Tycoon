using UnityEngine;

public class OrderBtn : MonoBehaviour
{
    [SerializeField] private AppData data;
    [SerializeField] private SwipeControl swipeControl;
    [SerializeField] private FueSystem fueSystem;


    private void OnEnable()
    {
        if (data.matchData.firstOrder == 0)
        {
            Debug.Log("BtnActiv");
            fueSystem.OrdersIn();
            data.matchData.firstOrder = 1;
            SaveDataSystem.Instance.SaveFirstOrder();            
        }
    }
    
}
