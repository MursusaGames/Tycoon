using UnityEngine;
using TMPro;

public class OfferPopUp : MonoBehaviour
{
    [SerializeField] private MatchData data;
    [SerializeField] private TimeCountSystem timeCountSystem;
    [SerializeField] private TextMeshProUGUI timeText;

    private void OnEnable()
    {
        if (data.isTime)
        {
            timeCountSystem.StartTimer();
        }
    }

    private void Update()
    {
        timeText.text = timeCountSystem.GetTime();
    }
}
