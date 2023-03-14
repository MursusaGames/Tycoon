using UnityEngine;

public class FarshPodlojka : MonoBehaviour
{
    [SerializeField] private BoxInStakeMashine boxInStakeMashine;
    [SerializeField] private FarshMachineWorcer_2 worcer;
    private Vector3 startPos;
    private bool goFwd;
    private float _speed;

    void OnEnable()
    {
        startPos = transform.position;
    }

    public void Go(float speed = 1f)
    {
        _speed = speed;
        goFwd = true;
    }
    void Update()
    {
        if (goFwd)
        {
            var pos = transform.position;
            pos += Vector3.forward * _speed * Time.deltaTime;
            transform.position = pos;            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            goFwd = false;
            transform.position = startPos;
            boxInStakeMashine.AddFirstStake();
            worcer.GetBox();
            Debug.Log("GetBoxinPodlogka");
        }
    }
}
