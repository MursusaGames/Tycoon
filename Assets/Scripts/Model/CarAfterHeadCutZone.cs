using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine;

public class CarAfterHeadCutZone : MonoBehaviour
{
    [SerializeField] private Transform depo_3;
    [SerializeField] private Transform workDepo1;
    [SerializeField] private Transform workDepo2;
    [SerializeField] private Transform workDepo3;    
    [SerializeField] private Transform stay_2;
    [SerializeField] private Transform stay_3;
    [SerializeField] private List<EggsDepositories> mainDepositories;
    [SerializeField] private List<Stock> workDepos;
    private NavMeshAgent agent;
    private float currentDystance;
    [SerializeField] private AppData data;
    [SerializeField] public GameObject box;
    private Stock currentDepo;
    private EggsDepositories currentMainDepo;
    private float maxTime = 10f;
    public float currentTime;
    public bool isWait;
    public bool isLoading;
    public bool isShipment;
    public bool isGoToShipment;
    public bool isGoToLoading;
    [SerializeField] private float stopPoint = 1f;
    public bool noFreeDepo;    
    private Transform currentTransform;
    private Transform currentWorkTransform;    
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
        isLoading = true;
        FindDepopsitory();        
    }
    private void FindDepopsitory()
    {
        var deposCount = data.userData.fishCleaningMashine == 0 ? 0 : data.userData.fishCleaningMashine == 1 
            ? 1 : workDepos.Count;
        for (int i = 0; i < deposCount; i++)
        {
            if (workDepos[i].currentBox<6 && workDepos[i].loading)
            {
                currentWorkTransform = i == 0 ? workDepo1 : i == 1 ? workDepo2 : workDepo3;
                currentDepo = workDepos[i];
                noFreeDepo = false;
                MoveToLoading();
                return;
            }
        }
        noFreeDepo = true;
        Wait();
    }
    private void TakeBox()
    {
        if (currentMainDepo.RemoveBox())
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
            isLoading = true;
            FindDepopsitory();
            return;
        }
        Wait();
    }
    private void MoveToLoading()
    {
        isLoading = true;
        isShipment = false;
        if (!FindFreeDepo())
        {
            Wait();
            return;
        }        
        agent.SetDestination(currentTransform.position);
        isGoToLoading = true;
    }
    private bool FindFreeDepo()
    {
        for(int i = 0; i < mainDepositories.Count; i++)
        {
            if (mainDepositories[i].GetFullGO() < int.MaxValue)
            {
                currentMainDepo = mainDepositories[i];
                currentTransform = depo_3;
                return true;
            }
        }
        return false;        
    }
    private void MoveToShipment()
    {
        agent.SetDestination(currentWorkTransform.position);
        isLoading = false;
        isShipment = true;
        isGoToShipment = true;
    }

    private void Wait()
    {
        if (!inStay)
        {
            agent.SetDestination(stay_2.position);
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
            agent.SetDestination(stay_3.position);            
            Invoke(nameof(GoToStay), timeToFlip);

        }
    }
    private void GoToStay()
    {
        if (!inStay)
        {
            agent.updateRotation = false;
            agent.SetDestination(stay_2.position);
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
                currentDystance = Vector3.Distance(transform.position, stay_2.position);
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
                if (noFreeDepo)
                {
                    FindDepopsitory();
                    return;
                }
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
            currentDystance = Vector3.Distance(transform.position, currentWorkTransform.position);
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
        agent.speed = Constant.baseSpeed + (data.userData.zone3CarLevel * Constant.speedDecreeseKoeff*data.userData.ADSpeedMultiplier);
    }
}
