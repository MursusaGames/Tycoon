using UnityEngine;
using UnityEngine.AI;

public class CarZoneShipment : MonoBehaviour
{
    [SerializeField] private GameObject money;
    private EggsDepositories currentDepository;
    private NavMeshAgent agent;
    private float currentDystance;
    [SerializeField] private GameObject startPoint;
    [SerializeField] private GameObject endPoint;
    [SerializeField] private AppData data;
    //TODO исправить перенести в SO
    [SerializeField] private int maxBox = 70;
    //[SerializeField] private int costBox = 10;
    [SerializeField] private MoneySystem moneySystem;
   
    public int currentBox;
    private float maxTime = 10f;
    public float currentTime;
    public bool isWait;
    public bool isLoading;
    public bool isShipment;
    public bool isGoToShipment;
    public bool isGoToLoading;
    //private float stopPoint = 1f;

    public EggsDepositories[] depositories = new EggsDepositories[5];
    private void Awake()
    {
        depositories = FindObjectsOfType<EggsDepositories>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        isLoading = true;
        FindDepopsitory();
        currentBox = 0;
    }
    private void FindDepopsitory()
    {
        float dystance = float.MaxValue;
        for (int i = 0; i < depositories.Length; i++)
        {
            currentDystance = Vector3.Distance(transform.position, depositories[i].gameObject.transform.position);
            if (currentDystance < dystance)
            {
                dystance = currentDystance;
                currentDepository = depositories[i];
            }
        }
        if (isLoading)
        {
            TakeBox();
            return;
        }        
    }
    private void TakeBox()
    {
        if (currentDepository.RemoveBox())
        {
            currentBox++;
            if (currentBox >= maxBox)
            {
                MoveToShipment();
                return;
            }
        }
        Wait();
    }    
    
    private void MoveToShipment()
    {
        isLoading = false;
        isShipment = true;
        money.SetActive(true);
    }

    private void Wait()
    {
        isWait = true;
        currentTime = maxTime - (data.upgradesData.stepOffMultyplyUpgrade * data.userData.zone1CarLevel);
    }

    void FixedUpdate()
    {
        if (isWait)
        {
            currentTime -= 0.02f;
            if (currentTime <= 0)
            {
                isWait = false;
                if (isLoading)
                {
                    TakeBox();
                    return;
                }                
            }
        }        
    }
}
