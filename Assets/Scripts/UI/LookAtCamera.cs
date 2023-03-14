using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    void FixedUpdate()
    {
        gameObject.transform.LookAt(Camera.main.transform.forward);
    }
}
