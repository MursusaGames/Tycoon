using UnityEngine;
using UnityEngine.AI;

public class CarZoneEggCollectors : MonoBehaviour
{
    private EggsDepositories currentDepository;
    private NavMeshAgent agent;
    private float currentDystance;
    [SerializeField] private GameObject startPoint;
    [SerializeField] private GameObject endPoint;
    [SerializeField] private AppData data;
    //TODO исправить перенести в SO
    [SerializeField] private int maxBox = 5;
    [SerializeField] public GameObject[] boxes;
    public int currentBox;
    private float maxTime = 10f;
    public float currentTime;
    public bool isWait;
    public bool isLoading;
    public bool isShipment;
    public bool isGoToShipment;
    public bool isGoToLoading;
    private float stopPoint = 3f;
    public bool port;

    public EggsDepositories[] depositories;
    private void Awake()
    {
        depositories = FindObjectsOfType<EggsDepositories>();
        agent = GetComponent<NavMeshAgent>();
    }
    
    void Start()
    {
        isLoading = true;
        Invoke(nameof(FindDepopsitory), 3f);
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
        if (isShipment) GiveBox();
    }
    private void TakeBox()
    {
        if (currentDepository.RemoveBox())
        {
            boxes[currentBox].SetActive(true);
            currentBox++;
            if(currentBox >= maxBox)
            {
                MoveToShipment();
                return;
            }
        }
        Wait();
    }
    private void GiveBox()
    {
        if (currentDepository.AddBox())
        {
            boxes[currentBox-1].SetActive(false);
            currentBox--;
            if (currentBox == 0)
            {
                MoveToLoading();
                return;
            }
        }
        Wait();
    }
    private void MoveToLoading()
    {
        isLoading = true;
        isShipment = false;
        agent.SetDestination(startPoint.transform.position);
        isGoToLoading = true;
    }
    private void MoveToShipment()
    {
        agent.SetDestination(endPoint.transform.position);
        isLoading = false;
        isShipment = true;
        isGoToShipment = true;
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
                if (isShipment) GiveBox();
            }
        }
        if (isGoToShipment)
        {
            currentDystance = Vector3.Distance(transform.position, endPoint.transform.position);            
            if (currentDystance < stopPoint)
            {
                isGoToShipment = false;
                FindDepopsitory();                             
            }
        }
        if (isGoToLoading)
        {
            currentDystance = Vector3.Distance(transform.position, startPoint.transform.position);
            if (currentDystance < stopPoint)
            {
                isGoToLoading = false;
                FindDepopsitory();                
            }
        }
    }
}
