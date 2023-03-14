using UnityEngine;

public class Up : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 startPos;
    private float timeToHide = 3f;
    private void OnEnable()
    {
        startPos = transform.localPosition;
        Invoke(nameof(Hide), timeToHide);
    }
    void Update()
    {
        transform.localPosition += Vector3.up * speed * Time.deltaTime;
    }
    private void Hide()
    {
        startPos.y = 0;
        transform.localPosition = startPos;
        gameObject.SetActive(false);        
    }
     
}
