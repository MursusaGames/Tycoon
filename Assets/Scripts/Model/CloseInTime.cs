using UnityEngine;

public class CloseInTime : MonoBehaviour
{
    [SerializeField] private float timeToClose = 2f;
    private bool startTimer;
    void OnEnable()
    {
        startTimer = true;
    }
    
    void Update()
    {
        if (startTimer)
        {
            timeToClose -=Time.deltaTime;
            if (timeToClose <= 0)
            {
                startTimer=false;
                timeToClose = 2;
                this.gameObject.SetActive(false);   
            }
        }
    }
}
