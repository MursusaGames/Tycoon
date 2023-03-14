using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject _object; 
    [SerializeField] private float speed;
   
    private void FixedUpdate() 
    {
        Vector3 target = new Vector3(_object.transform.position.x, 16, _object.transform.position.z);
        Vector3 pos = Vector3.Lerp(a: transform.position, b: target, t: speed*Time.fixedDeltaTime);
        transform.position = pos;
    }
}

