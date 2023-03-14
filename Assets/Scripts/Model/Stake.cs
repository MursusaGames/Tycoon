using UnityEngine;

public class Stake : MonoBehaviour
{
    public bool move;
    public float speed = 1f;
    public NarezkaMashineAnimation narezka;
    private void OnEnable()
    {
        //narezka = FindObjectOfType<NarezkaMashineAnimation>();
    }
    void Start()
    {
        move = true;
    }

    private void Update()
    {
        if (move)
        {
            var pos = gameObject.transform.localPosition;
            pos.z += speed * Time.deltaTime;
            gameObject.transform.localPosition = pos;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stol"))
        {
            move = false;
            narezka.StartAnimation(this.gameObject);
        }
    }
}
