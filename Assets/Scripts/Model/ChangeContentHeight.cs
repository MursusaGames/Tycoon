using UnityEngine;

public class ChangeContentHeight : MonoBehaviour
{
    private RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void OnEnable()
    {
        ChangeHeight();
    }

    public void ChangeHeight()
    {
        var rt = rectTransform.sizeDelta;
        rt.y = 400 * transform.childCount;
        rectTransform.sizeDelta = rt;
    }
}
