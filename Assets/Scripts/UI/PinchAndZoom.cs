using UnityEngine;
public class PinchAndZoom : MonoBehaviour
{
    [SerializeField] private MovementSystem movementSystem;
    [SerializeField] private AppData data;
    private float MouseZoomSpeed = 7.5f;
    private float TouchZoomSpeed = 0.05f;
    private float ZoomMinBound = 10f;
    private float ZoomMaxBound = 50f;
    private float ortoSizeRatio = 2f;
    private bool noZoom;
    public bool noTouch;


    Camera cam;
    
    void Start()
    {
        cam = Camera.main;
        cam.orthographicSize = 35f;
        movementSystem.smoothTime = (cam.orthographicSize / ortoSizeRatio) / 5;
    }

    private void GetTouch()
    {
        if (noZoom)
        {
            noZoom = false;
            noTouch = false;
        }
    }
    void Update()
    {
        if (data.matchData.isFue) return;
        if (Input.touchSupported)
        {
            // Pinch to zoom
            if (Input.touchCount == 2)
            {
                // get current touch positions
                Touch tZero = Input.GetTouch(0);
                Touch tOne = Input.GetTouch(1);                
                // get touch position from the previous frame
                Vector2 tZeroPrevious = tZero.position - tZero.deltaPosition;
                Vector2 tOnePrevious = tOne.position - tOne.deltaPosition;
                float oldTouchDistance = Vector2.Distance(tZeroPrevious, tOnePrevious);
                float currentTouchDistance = Vector2.Distance(tZero.position, tOne.position);
                // get offset value
                float deltaDistance = oldTouchDistance - currentTouchDistance;
                Zoom(deltaDistance, TouchZoomSpeed);
                noTouch = true;
                noZoom = false;
            }
            else
            {
                if (noTouch)
                {
                    noZoom = true;
                    Invoke(nameof(GetTouch), 0.2f);
                }
            }

        }
        else
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            Zoom(scroll, MouseZoomSpeed);            
        }
        if (cam.orthographicSize < ZoomMinBound)
        {
            cam.orthographicSize = ZoomMinBound;
        }
        else
        if (cam.orthographicSize > ZoomMaxBound)
        {
            cam.orthographicSize = ZoomMaxBound;
        }
    }
    void Zoom(float deltaMagnitudeDiff, float speed)
    {
        cam.orthographicSize += deltaMagnitudeDiff * speed;
        // set min and max value of Clamp function upon your requirement
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, ZoomMinBound, ZoomMaxBound);
        movementSystem.smoothTime = (cam.orthographicSize / ortoSizeRatio)/5;        
    }
}