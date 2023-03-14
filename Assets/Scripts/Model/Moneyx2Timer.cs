using TMPro;
using UnityEngine;

public class Moneyx2Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI minuts;
    [SerializeField] private TextMeshProUGUI secunds;
    private float startValue = 119f;
    private float currentValue;
    private bool startTimer;
    void OnEnable()
    {
        currentValue = startValue;
        startTimer = true;        
    }
    
    void Update()
    {
        if (startTimer)
        {
            currentValue -= Time.deltaTime;
            if(currentValue >= 59.5f)
            {
                minuts.text = "01";
                secunds.text = (currentValue - 60).ToString("00");
            }
            else
            {
                minuts.text = "00";
                secunds.text = currentValue.ToString("00");
            }
            if (currentValue <= 0)
            {
                currentValue = startValue;
                gameObject.SetActive(false);
            }
                
        }
    }
}
