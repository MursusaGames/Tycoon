using UnityEngine;
using TMPro;

public class PopUp : MonoBehaviour
{
    [SerializeField] private TimeCountSystem timeCountSystem;
    [SerializeField] private TMP_Text timeText;
    public bool isCrystal;
    void OnEnable()
    {
        Invoke(nameof(HideWindow), 2F);
    }

    private void HideWindow()
    {
        this.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (isCrystal)
        {
            timeText.text = timeCountSystem.GetTimeC();
        }
        
    }

}
