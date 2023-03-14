using UnityEngine;

public class PackingBox : MonoBehaviour
{
    public EggsDepositories[] depositories = new EggsDepositories[5];
    [SerializeField] private float speed = 1f;
    private EggsDepositories currentDepository;
    private bool isGo;
    public BoxDirectionPoint target;
    [SerializeField] private Material white;
    public float efficcienty = 15;

    private void Awake()
    {
        depositories = FindObjectsOfType<EggsDepositories>();
    }
    void Start()
    {
        isGo = true;
        Invoke(nameof(ChangeMesh), 3f);
        Invoke(nameof(FindDepopsitory), efficcienty);
    }

    private void ChangeMesh()
    {
        GetComponent<MeshRenderer>().material = white;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGo)
        {
            transform.Translate((target.transform.position-transform.position).normalized * speed * Time.deltaTime);
        }


    }
    private void FindDepopsitory()
    {
        float dystance = float.MaxValue;
        for (int i = 0; i < depositories.Length; i++)
        {
            var currentDystance = Vector3.Distance(transform.position, depositories[i].gameObject.transform.position);
            if (currentDystance < dystance)
            {
                dystance = currentDystance;
                currentDepository = depositories[i];
            }
        }
        isGo = false;
        currentDepository.AddBox();
        Destroy(gameObject);
    }
}