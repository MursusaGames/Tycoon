using UnityEngine;

public class MoneyPack : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    private Rigidbody rg;
    Vector3 startPos;
    [SerializeField] private float force = 200;
    [SerializeField] private float timeBoom = 3f;
    public bool first;
    
    private void Awake()
    {
        rg = GetComponent<Rigidbody>();               
    }
    void OnEnable()
    {
        startPos = transform.position;
        Vector3 pos = transform.position;
        pos -= new Vector3(0, 3,0);
        rg.AddForceAtPosition(new Vector3(Random.Range(-1f, 1f),0, Random.Range(-1f, 1f)) * force, pos);
        Invoke(nameof(Hide), timeBoom);
    }
    

    void Hide()
    {
        gameObject.transform.position = startPos;
        if(first)  parent.SetActive(false);
    }
}
