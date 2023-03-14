using UnityEngine;

public class FishInFarsh : MonoBehaviour
{
    private bool go;
    [SerializeField] private float speed;
    [SerializeField] private Farsh farsh;
    void OnEnable()
    {
        var scale = transform.localScale;
        scale.z = 1;
        transform.localScale = scale;
        go = true;
    }

    void Update()
    {
        if (go)
        {
            var scale = transform.localScale;
            scale.z -= Time.deltaTime*speed;
            transform.localScale = scale;
            if(scale.z <= 0)
            {
                go = false;
                farsh.Go();
                gameObject.SetActive(false);
            }
        }
    }
}
