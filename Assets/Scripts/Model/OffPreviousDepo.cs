using UnityEngine;

public class OffPreviousDepo : MonoBehaviour
{
    [SerializeField] private Stock previousDepo;
    private void OnEnable()
    {
        Invoke(nameof(SetLoad), 3f);
    }

    private void SetLoad()
    {
        previousDepo.loading = false;
    }
}
