using UnityEngine;

public class MenuScaller : MonoBehaviour
{
    [SerializeField] private float speed = 0.8f;
    [SerializeField] private float hiLimit = 1.1f;
    private RectTransform _trans;
    private bool isUp;
    private bool isDown;
    private void Awake()
    {
        _trans = GetComponent<RectTransform>();
    }
    void OnEnable()
    {
        _trans.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        isUp = true;
    }

    private void Update()
    {
        if (isUp)
        {
            var scale = _trans.localScale;
            scale += Vector3.one*speed*Time.deltaTime;
            _trans.localScale = scale;
            if(scale.x > hiLimit)
            {
                isUp = false;
                isDown = true;
            }
        }
        if (isDown)
        {
            var scale = _trans.localScale;
            scale -= Vector3.one * speed * Time.deltaTime;
            _trans.localScale = scale;
            if (scale.x <= 1)
            {
                isUp = false;
                isDown = false;
                _trans.localScale = Vector3.one;
            }
        }
    }

}
