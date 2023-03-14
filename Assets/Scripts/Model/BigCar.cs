using UnityEngine;

public class BigCar : MonoBehaviour
{
    private Vector3 startPos;
    [SerializeField] private float finishPoint = -80f;
    [SerializeField] private float speed = 3f;
    private bool goToFinish;
    void Start()
    {
        startPos = transform.position;
    }
    public void Go()
    {
        goToFinish = true;
    }
    void Update()
    {
        if (goToFinish)
        {
            var pos = transform.position;
            pos.x -= speed*Time.deltaTime;
            transform.position = pos; 
            if(pos.x <= finishPoint)
            {
                goToFinish = false;
                transform.position = startPos;
            }
        }
    }
}
