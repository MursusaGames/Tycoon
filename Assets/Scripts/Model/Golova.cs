using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golova : MonoBehaviour
{
    public bool isRotate;
    public float speed;
    public bool knife;
    void Start()
    {
        isRotate = true;
    }
    
    void Update()
    {
        if (isRotate)
        {
            if(knife) gameObject.transform.Rotate(Vector3.left, -speed*Time.deltaTime );
            else gameObject.transform.Rotate(Vector3.left, speed*Time.deltaTime);
        }
    }
}
