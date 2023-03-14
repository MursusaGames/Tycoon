
using UnityEngine;

public class TV : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform target;
    
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime*speed);
        transform.LookAt(target);
    }
}
