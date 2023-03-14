using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarHeadCutZone : MonoBehaviour
{
    [SerializeField] private Transform repo_1;
    [SerializeField] private Transform repo_2;
    [SerializeField] private Transform stay;
    [SerializeField] private Transform stay1;
    [SerializeField] private Transform startPoint;
    [SerializeField] private EggsDepositories mainDepository;
    [SerializeField] private List<Stock> workDepos;
    private NavMeshAgent agent;
    private float currentDystance;   
    [SerializeField] private AppData data;    
    [SerializeField] public GameObject box;
    private Stock currentDepo;    
    private float maxTime = 10f;
    public float currentTime;
    public bool isWait;
    public bool isLoading;
    public bool isShipment;
    public bool isGoToShipment;
    public bool isGoToLoading;
    private float stopPoint = 2.1f;
    private bool noFreeDepo;
    private bool finding;
    private int indexFullPalet;    
    private Transform currentDepoTransform;
    public bool inGame;    
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
        finding = true;
        WaitDepo();        
    }
    private void WaitDepo()
    {
        if (mainDepository.isEmpty || !inGame)
        {
            CheckInGameEvent();
            Wait();
            return;
        }
        FindDepopsitory();
        if (noFreeDepo) return;
        finding = false;
        MoveToLoading();
    }
    private void CheckInGameEvent()
    {
        if (data.userData.headCutingMashine > 0) inGame = true;
    }
    private void FindDepopsitory()
    {
        int temp = 100;
        var deposCount = data.userData.headCutingMashine == 0 ? 0 : data.userData.headCutingMashine == 1 ? 1 : workDepos.Count;
        for (int i = 0; i < deposCount; i++)
        {
            if (workDepos[i].currentBox<6 && workDepos[i].currentBox < temp && workDepos[i].loading)
            {
                currentDepo = workDepos[i];
                currentDepoTransform = i == 0 ? repo_1 : repo_2;
                noFreeDepo = false;                
                return;
            }
        }
        noFreeDepo = true;
        Wait();
    }
    private void TakeBox()
    {
        if (mainDepository.RemoveBox())
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
        if (currentDepo.currentBox<6)
        {
            currentDepo.AddBox(3);
            box.SetActive(false);
            isShipment = false;
            finding = true;            
            WaitDepo();            
            return;
        }
        Wait();
    }
    private void MoveToLoading()
    {
        isLoading = true;
        isShipment = false;
        indexFullPalet = mainDepository.GetFullGO();
        if(indexFullPalet == int.MaxValue)
        {
            finding = true;
            WaitDepo();
            return;
        }
        agent.SetDestination(mainDepository.boxes[indexFullPalet].gameObject.transform.position);
        isGoToLoading = true;
    }
    
    private void MoveToShipment()
    {
        agent.SetDestination(currentDepoTransform.position);
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
                //dream.SetActive(false);
                if (finding)
                {
                    WaitDepo();
                    return;
                }
                if (noFreeDepo)
                {
                    FindDepopsitory();
                    return;
                }
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
            currentDystance = Vector3.Distance(transform.position, currentDepoTransform.position);
            if (currentDystance < stopPoint)
            {
                isGoToShipment = false;
                GiveBox();
            }
        }
        if (isGoToLoading)
        {
            currentDystance = Vector3.Distance(transform.position, mainDepository.boxes[indexFullPalet].gameObject.transform.position);
            if (currentDystance < stopPoint)
            {
                isGoToLoading = false;
                TakeBox();
            }
        }
    }
    public void ChangeSpeed()
    {
        agent.speed = 4 + (data.userData.zone1CarLevel * Constant.speedDecreeseKoeff*data.userData.ADSpeedMultiplier);//TODO change
    }
}
