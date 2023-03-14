using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    [SerializeField] private PinchAndZoom pinchAndZoom;
    [SerializeField] private GameObject movObject;
    [SerializeField] private float minXBoard = 0f;
    [SerializeField] private float maxXBoard = -85f;
    [SerializeField] private float minZBoard = 0f;
    [SerializeField] private float maxZBoard = 130f;

    
    public float smoothTime = 1f;    
    [SerializeField] private Vector3 _distanceFromObject; 
    public void MoveTo(Vector2 pos)
    {
        Vector3 newPos = new Vector3(pos.x+pos.y, 0, pos.y-pos.x);
        Vector3 positionToGo = movObject.transform.position - newPos; 
        Vector3 smoothPosition = Vector3.Lerp(a: movObject.transform.position, b: positionToGo, t: smoothTime*Time.deltaTime);       
        movObject.transform.position = smoothPosition;
        var objPos = movObject.transform.position;
        objPos.x = objPos.x < minXBoard ? minXBoard : objPos.x > maxXBoard ? maxXBoard : objPos.x;
        objPos.z = objPos.z < minZBoard ? minZBoard : objPos.z > maxZBoard ? maxZBoard : objPos.z;
        movObject.transform.position = objPos;
        
    }
    
}
