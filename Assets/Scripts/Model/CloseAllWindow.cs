using UnityEngine;

public class CloseAllWindow : MonoBehaviour
{
    [SerializeField] private UpgradesMenu upgradesMenu;
    void OnEnable()
    {
        Debug.Log("FourtenTextOpen");
        upgradesMenu.CloseAllWindow();
    }

    
}
