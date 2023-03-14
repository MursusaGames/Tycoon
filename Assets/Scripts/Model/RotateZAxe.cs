using UnityEngine;

public class RotateZAxe : MonoBehaviour
{
    public float sign;
    public float speed = 5f;
    private bool rotate;
    void OnEnable()
    {
        rotate = true;
    }

    void Update()
    {
        if (rotate)
        {
            transform.Rotate(Vector3.forward, speed*sign);
        }
    }
    
}
