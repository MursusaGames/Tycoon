using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListenerScript : MonoBehaviour
{
    [SerializeField] private Transform target;
    public AppData _data;
    void Update()
    {
        transform.position = target.position;
        var pos = transform.position;
        pos.y = 0;
        pos.z += 18f; //поправка на наклон камеры
        pos.x += 18f;
        transform.position = pos;
    }

}
