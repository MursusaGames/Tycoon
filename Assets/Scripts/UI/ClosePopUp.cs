using UnityEngine;

public class ClosePopUp : MonoBehaviour
{
    [SerializeField] private float timeInScren = 4f;
    void OnEnable()
    {
        Invoke(nameof(CloseWindow),timeInScren);
    }

    private void CloseWindow()
    {
        gameObject.SetActive(false);
    }

    
}
