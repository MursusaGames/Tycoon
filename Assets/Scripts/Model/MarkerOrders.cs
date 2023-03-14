using UnityEngine;
using UnityEngine.UI;

public class MarkerOrders : MonoBehaviour
{
    [SerializeField] private Image arrowImg;
    [SerializeField] private float _timeToBlinck = 1f;   
    private float timeToBlinck;
    private bool meshEnabled;

    private void OnEnable()
    {
        timeToBlinck = _timeToBlinck;        
    }
    
    void Update()
    {
        timeToBlinck-=Time.deltaTime; 
        if(timeToBlinck <= 0)
        {
            meshEnabled = !meshEnabled;
            arrowImg.enabled = meshEnabled;
            timeToBlinck = _timeToBlinck;
        }
    }
}
