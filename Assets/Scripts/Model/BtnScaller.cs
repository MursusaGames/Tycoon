using UnityEngine;

public class BtnScaller : MonoBehaviour
{
    private RectTransform trans;
    void Start()
    {
        trans = GetComponent<RectTransform>();  
    }

    public void DecreaseScale()
    {
        trans.localScale = new Vector3(0.9f, 0.9f, 0.9f);
    }
    public void IncreaseScale()
    {
        trans.localScale = new Vector3(1f, 1f, 1f);
    }
}
