using UnityEngine.AI;
using UnityEngine;

public class CarPort : MonoBehaviour
{
    [SerializeField] private EggsDepositories removedDepository;
    [SerializeField] private EggsDepositories addedDepository;
    private NavMeshAgent agent;
    private float currentDystance;
    [SerializeField] private GameObject startPoint;
    [SerializeField] private GameObject point_1;
    [SerializeField] private GameObject point_2;
    [SerializeField] private GameObject point_3;
    [SerializeField] private GameObject point_4;
    [SerializeField] private AppData data;   
    [SerializeField] public GameObject box;
    [SerializeField] private float timeToAnimation = 3f;
    [SerializeField] private float timeBoxAppier = 1.5f;
    private Animator animator;
    public int currentBox;
    private float maxTime = 3f;
    public float currentTime;
    public bool isWait;
    public bool isLoading;
    public bool isShipment;
    public bool isGoToShipment;
    public bool isGoToLoading;
    public bool isGoToPoint_1;
    public bool isGoToPoint_3;
    public bool isGoToPoint_4;
    [SerializeField] private float stopPoint = 1.5f;
    public bool port;

    
    private void Awake()
    {
        animator = GetComponent<Animator>();    
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        Invoke(nameof(ChangeSpeed), 5f);
        isLoading = true;
        MoveToLoading();                
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
    public void TakeBox()
    {
        if (removedDepository.RemoveBox())
        {
            animator.SetBool("down", false);
            animator.SetBool("up", true);
            Invoke(nameof(BoxOnBoard), timeBoxAppier);
        }
        else
        {
            Wait();
        }
        
    }
    private void BoxOnBoard()
    {
        box.SetActive(true);
        Invoke(nameof(GoToPoint_1),timeToAnimation);
    }
    private void GoToPoint_1()
    {
        isGoToPoint_1 = true;
        if(addedDepository.GetFreeGO() < int.MaxValue)
        {
            agent.SetDestination(point_1.transform.position);
        }
        else
        {
            Wait();
        }
    }
    private void GoToPoint_3()
    {
        isGoToPoint_3 = true;
        agent.SetDestination(point_3.transform.position);
    }
    private void GoToPoint_4()
    {
        isGoToPoint_4 = true;
        agent.SetDestination(point_4.transform.position);
    }
    private void GiveBox()
    {
        animator.SetBool("up", false);
        animator.SetBool("down", true);
        Invoke(nameof(BoxOutBoard), timeBoxAppier);
    }
    private void BoxOutBoard()
    {
        if (addedDepository.AddBox())
        {
            box.SetActive(false);
            Invoke(nameof(GoToPoint_3),timeToAnimation);
            return;
        }
        Wait();
    }
    private void MoveToLoading()
    {
        isShipment = false;
        agent.SetDestination(startPoint.transform.position);
        isGoToLoading = true;
    }
    private void MoveToShipment()
    {
        agent.SetDestination(point_2.transform.position);
        isLoading = false;
        isShipment = true;
        isGoToShipment = true;
    }

    private void Wait()
    {
        isWait = true;
        currentTime = maxTime;
    }

    void FixedUpdate()
    {
        if (isWait)
        {
            currentTime -= Time.fixedDeltaTime;
            if (currentTime <= 0)
            {
                if (isGoToPoint_1)
                {
                    isWait = false;
                    GoToPoint_1();                    
                    return; 
                }
                isWait = false;
                FindDepopsitory();
            }
        }
        if (isGoToPoint_1)
        {
            currentDystance = Vector3.Distance(transform.position, point_1.transform.position);
            if (currentDystance < stopPoint)
            {
                isGoToPoint_1 = false;
                MoveToShipment();
                return;
            }
        }
        if (isGoToPoint_3)
        {
            currentDystance = Vector3.Distance(transform.position, point_3.transform.position);
            if (currentDystance < stopPoint)
            {
                isGoToPoint_3 = false;
                GoToPoint_4();
                return;
            }
        }
        if (isGoToPoint_4)
        {
            currentDystance = Vector3.Distance(transform.position, point_4.transform.position);
            if (currentDystance < stopPoint)
            {
                isGoToPoint_4 = false;
                MoveToLoading();
                return;
            }
        }
        if (isGoToShipment)
        {
            currentDystance = Vector3.Distance(transform.position, point_2.transform.position);
            if (currentDystance < stopPoint)
            {
                isGoToShipment = false;
                GiveBox();
            }
        }
        if (isGoToLoading)
        {
            currentDystance = Vector3.Distance(transform.position, startPoint.transform.position);
            if (currentDystance < stopPoint)
            {
                isGoToLoading = false;
                isLoading = true;
                TakeBox();
            }
        }
    }
    public void ChangeSpeed()
    {
        agent.speed = Constant.baseSpeed+(data.userData.zoneInWarLevel * Constant.speedDecreeseKoeff * data.userData.ADSpeedMultiplier);
        
    }
}
