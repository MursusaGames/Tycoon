using UnityEngine;

public class ActivationEqipment : MonoBehaviour
{
    [SerializeField] private GameObject equipment;
    [SerializeField] private Stock stock;
    void OnEnable()
    {
        equipment.SetActive(true);
        stock.loading = true;
    }
    
}
