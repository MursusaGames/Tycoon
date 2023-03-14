using UnityEngine;

public class Barier : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private float timeOpen = 3f;
    private float currentTime = 0f;
    private bool open;
    private bool close;
    
    public void Open()
    {
        currentTime = timeOpen;
        open = true;
        Invoke(nameof(Close), 9f);
    }
    public void Close()
    {
        currentTime = timeOpen;
        close = true;
    }

    private void Update()
    {
        if (open)
        {
            currentTime -= Time.deltaTime;
            gameObject.transform.Rotate(Vector3.right, speed * Time.deltaTime);            
            if(currentTime <= 0)
            {
                open=false;                
            }
        }
        if (close)
        {
            currentTime -= Time.deltaTime;
            gameObject.transform.Rotate(Vector3.right, -speed * Time.deltaTime);            
            if (currentTime <= 0)
            {
                close = false;                
            }
        }

    }
}
