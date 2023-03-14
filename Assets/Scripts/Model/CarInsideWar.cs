using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarInsideWar : MonoBehaviour
{
    [SerializeField] private Transform depo_1;
    [SerializeField] private Transform depo_2;    
    [SerializeField] private Transform stay1;
    [SerializeField] private Transform stay;
    [SerializeField] private Transform uploadPoint;
    [SerializeField] private List<Stock> stocks;
    [SerializeField] private EggsDepositories workDepo;
    private NavMeshAgent agent;
    private float currentDystance;
    [SerializeField] private AppData data;
    [SerializeField] public GameObject box;    
    private Stock currentMainDepo;
    private float maxTime = 10f;
    public float currentTime;
    public bool isWait;
    public bool isLoading;
    public bool isShipment;
    public bool isGoToShipment;
    public bool isGoToLoading;
    [SerializeField] private float stopPoint = 1f;    
    //private int indexFreePalet;
    private Transform currentTransform;      
    public bool inStay;
    public bool isGoToStay;
    private float timeToFlip = 3f;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        Invoke(nameof(ChangeSpeed), 5f);
        inStay = true;            
        MoveToLoading();
    }
    
    private void TakeBox()
    {
        if (currentMainDepo.currentBox >2)
        {
            currentMainDepo.RemovBox(3);
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
        if (workDepo.AddBox())
        {
            box.SetActive(false);            
            MoveToLoading();            
            return;
        }
        Wait();
    }
    private void MoveToLoading()
    {
        isLoading = true;
        isShipment = false;
        if (!FindFreeDepo() || workDepo.GetFreeGO()== int.MaxValue)
        {
            Wait();
            return;
        }
        //indexFreePalet = currentMainDepo.GetFullGO();
        agent.SetDestination(currentTransform.position);
        isGoToLoading = true;
    }
    private bool FindFreeDepo()
    {
        var count = data.userData.headCutingMashine;
        for (int i = 0; i < count; i++)
        {
            if (stocks[i].currentBox > 2)
            {
                currentMainDepo = stocks[i];
                currentTransform = i == 0 ? depo_1 : depo_2;
                return true;
            }
        }
        return false;
    }
    private void MoveToShipment()
    {
        agent.SetDestination(uploadPoint.position);
        isLoading = false;
        isShipment = true;
        isGoToShipment = true;
    }

    private void Wait()
    {
        if (!inStay)
        {
            agent.SetDestination(stay.position);
            isGoToStay = true;  
        }
        isWait = true;
        currentTime = maxTime;
    }

    private void AgentRotationEnabled()
    {
        agent.updateRotation = true;
    }
    private void GoToStay1()
    {
        if (!isGoToStay)
        {
            agent.SetDestination(stay1.position);            
            Invoke(nameof(GoToStay), timeToFlip);
        }
    }
    private void GoToStay()
    {
        if (!inStay)
        {
            agent.updateRotation = false;
            agent.SetDestination(stay.position);
            inStay = true;
            Invoke(nameof(AgentRotationEnabled), timeToFlip);

        }
    }
    void FixedUpdate()
    {
        if (isWait)
        {
            if (isGoToStay)
            {
                currentDystance = Vector3.Distance(transform.position, stay.position);
                if (currentDystance <= stopPoint)
                {
                    isGoToStay = false;
                    GoToStay1();                    
                }
                return;
            }
            currentTime -= Time.fixedDeltaTime;
            if (currentTime <= 0)
            {
                isWait = false;                
                if (isLoading)
                {
                    MoveToLoading();
                    return;
                }
                if (isShipment) GiveBox();
            }
        }
        if (isGoToShipment)
        {
            currentDystance = Vector3.Distance(transform.position, uploadPoint.position);
            if (currentDystance < stopPoint)
            {
                isGoToShipment = false;
                GiveBox();
            }
        }
        if (isGoToLoading)
        {
            currentDystance = Vector3.Distance(transform.position, currentTransform.position);
            if (currentDystance < stopPoint)
            {
                isGoToLoading = false;
                TakeBox();
            }
        }
    }
    public void ChangeSpeed()
    {
        agent.speed = Constant.baseSpeed  + (data.userData.zone2CarLevel * Constant.speedDecreeseKoeff*data.userData.ADSpeedMultiplier);
    }
}
