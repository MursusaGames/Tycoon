using UnityEngine;

public class HeadCutingMashine : MonoBehaviour
{
    [SerializeField] private GameObject equipment;

    private void OnEnable()
    {
        equipment.SetActive(true);
    }
}
