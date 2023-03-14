using UnityEngine;

public class Gates : MonoBehaviour
{
    [SerializeField] private float speed = 30f;    
    public bool isOpen;
    public bool isClose;
    public bool reverce;

    private void Start()
    {
        isClose = true;        
    }
    

    private void OpenGate()
    {
        float degrees = reverce ? -110 : 110;
        Vector3 to = new Vector3(-90, degrees, 0);
        transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, to, speed);
    }

    private void CloseGate()
    {
        isClose = true;
        isOpen = false;
        float degrees = 180;
        Vector3 to = new Vector3(-90, degrees, 0);
        transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, to, speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EggCollector") && isClose)
        {
            isClose = false;
            isOpen = true;
            OpenGate();
            Invoke(nameof(CloseGate), 3f);
        }            
    }    
}
