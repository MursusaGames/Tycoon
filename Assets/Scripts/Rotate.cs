using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up, speed * Time.fixedDeltaTime);
    }
}
