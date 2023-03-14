using UnityEngine;
using UnityEngine.AI;

public class CarWorkZone : MonoBehaviour
{
    [SerializeField] private EggsDepositories removedDepository;
    [SerializeField] private EggsDepositories addedDepository;
    private NavMeshAgent agent;
    private float currentDystance;
    [SerializeField] private GameObject startPoint;
    [SerializeField] private GameObject endPoint;
    [SerializeField] private GameObject stayPoint;
    [SerializeField] private GameObject stayPointEnd;
    [SerializeField] private AppData data;    
    [SerializeField] public GameObject box;
    public int currentBox;
    public float maxTime = 10f;
    public float currentTime;
    public bool isWait;
    public bool isLoading;
    public bool isShipment;
    public bool isGoToShipment;
    public bool isGoToLoading;
    [SerializeField] private float stopPoint = 1f;
    private bool isEmpty;
    public bool inStay;
    public bool isGoToStay;    
    private float timeToiFlip = 3f;
    public bool car_1;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        if (car_1) maxTime = 13;
        Invoke(nameof(ChangeSpeed), 5f);
        inStay = true;
        //isGoToStay = true;
        isLoading = true;
        isEmpty = true;
        MoveToLoading();
        currentBox = 0;
    }
    private void FindDepopsitory()
    {
        if (isLoading)
        {
            TakeBox();
            return;
        }
        if (isShipment) GiveBox();
    }
    private void TakeBox()
    {
        if (removedDepository.RemoveBox())
        {
            inStay = false;
            isGoToStay = false; 
            box.SetActive(true);
            MoveToShipment();
            return;
        }
        Wait();
    }
    private void GiveBox()
    {
        if (addedDepository.AddBox())
        {
            isShipment = false;
            isLoading = true;
            isEmpty = true;
            box.SetActive(false);
            MoveToLoading();
            return;            
        }
        Wait();
    }
    private void MoveToLoading()
    {
        if (removedDepository.GetFullGO() < int.MaxValue&& !addedDepository.isFull)
        {
            isEmpty = false;
            isShipment = false;
            agent.SetDestination(startPoint.transform.position);
            inStay=false;
            isGoToLoading = true;
        }
        else Wait();
        
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
        if (!inStay)
        {
            agent.SetDestination(stayPoint.transform.position);
            isGoToStay = true;            
        } 
            
        currentTime = maxTime;
        isWait = true;
        
    }

    private void AgentRotationEnabled()
    {
        agent.updateRotation = true;
    }
    private void GoToStay1()
    {
        if (!isGoToStay)
        {
            agent.SetDestination(stayPointEnd.transform.position);            
            Invoke(nameof(GoToStay), timeToiFlip);

        }
    }
    private void GoToStay()
    {
        if (!inStay)
        {
            agent.updateRotation = false;
            agent.SetDestination(stayPoint.transform.position);
            inStay = true;
            Invoke(nameof(AgentRotationEnabled), timeToiFlip);

        }
    }


    void FixedUpdate()
    {
        if (isWait)
        {
            currentTime -= Time.fixedDeltaTime;
            if (isGoToStay)
            {
                currentDystance = Vector3.Distance(transform.position, stayPoint.transform.position);
                if(currentDystance <= stopPoint)
                {
                    isGoToStay=false;
                    GoToStay1();
                }
                return;
            }
            if (currentTime <= 0)
            {
                isWait = false;
                if (isEmpty)
                {
                    MoveToLoading();
                    return;
                }
                if (isLoading)
                {
                    MoveToLoading();
                    return;
                }
                if (isShipment) MoveToShipment();
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
    public void ChangeSpeed()
    {
        agent.speed = Constant.baseSpeed + (data.userData.carPortLevel * Constant.speedDecreeseKoeff * data.userData.ADSpeedMultiplier);
    }
}
