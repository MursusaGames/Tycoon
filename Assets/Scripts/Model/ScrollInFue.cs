using UnityEngine;
using UnityEngine.UI;

public class ScrollInFue : MonoBehaviour
{
    private ScrollRect scroll;
    [SerializeField] private AppData data;    

    private void Awake()
    {
        scroll = GetComponent<ScrollRect>();
    }
    void OnEnable()
    {
        Invoke(nameof(OffScroll), 1f);    
    }

    private void OffScroll()
    {
        scroll.enabled = !data.matchData.isFue;
    }
    
}
