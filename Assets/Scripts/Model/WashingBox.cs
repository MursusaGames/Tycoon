using UnityEngine;

public class WashingBox : MonoBehaviour
{
    public EggsDepositories[] depositories = new EggsDepositories[5];
    [SerializeField] private float speed = 1f;
    private EggsDepositories currentDepository;
    public BoxDirectionPoint target;
    private bool isGo;    
    public float efficciency;

    private void Awake()
    {
        depositories = FindObjectsOfType<EggsDepositories>();
    }
    void Start()
    {
        isGo = true;
        Invoke(nameof(FindDepopsitory), efficciency);       
    }

    
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
