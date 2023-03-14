using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarFinalZone : MonoBehaviour
{
    [SerializeField] private Transform outDepoTransform;    
    [SerializeField] private Transform depo_Steak;
    [SerializeField] private Transform depo_unpSteak;
    [SerializeField] private Transform depo_Fish1;
    [SerializeField] private Transform depo_Fish2;
    [SerializeField] private Transform depo_Fish3;
    [SerializeField] private Transform depo_Farsh;
    [SerializeField] private Transform depo_unpFarsh;
    [SerializeField] private Transform depo_File;
    [SerializeField] private Transform depo_unpFile;
    [SerializeField] private Transform stayPoint;
    [SerializeField] private Transform stayPoint1;
    [SerializeField] private List<Stock> mainDepos;    
    [SerializeField] private OutDepository outDepo_1;
    [SerializeField] private OutDepository outDepo_2;
    [SerializeField] private OutDepository startOutDepo;
    [SerializeField] private OutDepository outDepo_3;
    private OutDepository currentOutDepo;
    private NavMeshAgent agent;
    private float currentDystance;
    [SerializeField] private AppData data;
    [SerializeField] public GameObject boxFish;
    [SerializeField] public GameObject boxUnpackedStake;
    [SerializeField] public GameObject boxStake;
    [SerializeField] public GameObject boxUnpackedFarsh;
    [SerializeField] public GameObject boxFarsh;
    [SerializeField] public GameObject boxUnpackedFile;
    [SerializeField] public GameObject boxFile;
    private GameObject curBox;
    public bool inStay;
    public bool isGoToStay1;
    private Stock currentMainDepo;
    [SerializeField] private float maxTime = 10f;
    public float currentTime;
    public bool isWait;
    public bool isLoading;
    public bool isShipment;
    public bool isGoToShipment;
    public bool isGoToLoading; 
    public bool isGoToStay;
    private float stopPoint = 1f;
    private bool noFullDepo;
    private int indexFreePalet;
    private Transform currentTransform;
    private int currentID;
    private float timeToFlip = 4f;
    public bool tv;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        Invoke(nameof(ChangeSpeed),5f);  
        inStay = true;        
        isLoading = true;
        FindDepopsitory();        
    }
    private void FindDepopsitory()
    {
        if (data.userData.steakMashine == 0)
        {
            for (int i = mainDepos.Count - 1; i >= 0; i--)
            {
                if (mainDepos[i].gameObject.activeInHierarchy && mainDepos[i].loading &&
                    mainDepos[i].currentBox >= 2 && !mainDepos[i].isBuzy)
                {
                    currentMainDepo = mainDepos[i];
                    currentMainDepo.isBuzy = true;
                    currentID = i == 0 ? 2 : i == 1 ? 1 : 0;
                    currentTransform = i == 0 ? depo_Steak : i == 1 ? depo_unpSteak : i == 2 ? depo_Fish1 : i == 3 ? depo_Fish2 : depo_Fish3;
                    curBox = i == 0 ? boxStake : i == 1 ? boxUnpackedStake : boxFish;
                    noFullDepo = false;
                    MoveToLoading();
                    return;
                }
            }
        }
        else
        {
            for (int i = 0; i < mainDepos.Count; i++)
            {
                if (mainDepos[i].gameObject.activeInHierarchy && mainDepos[i].loading &&
                    mainDepos[i].currentBox >= 2 && !mainDepos[i].isBuzy)
                {
                    currentMainDepo = mainDepos[i];
                    currentMainDepo.isBuzy = true;
                    currentID = i == 0 ? 2 : i == 1 ? 1 : i == 2 ? 3 : i == 3 ? 4 : i == 4 ? 5 : i == 5 ? 6 : 0;
                    currentTransform = i == 0 ? depo_Steak : i == 1 ? depo_unpSteak : i == 2 ? depo_Farsh : i == 3 ? depo_unpFarsh :
                        i == 4 ? depo_File : i == 5 ? depo_unpFile : i == 6 ? depo_Fish1 : i == 7 ? depo_Fish2 : depo_Fish3;
                    curBox = i == 0 ? boxStake : i == 1 ? boxUnpackedStake : i == 2 ? boxUnpackedFarsh : i == 3 ? boxFarsh
                        : i == 4 ? boxUnpackedFile : i == 5 ? boxFile : boxFish;
                    noFullDepo = false;
                    MoveToLoading();
                    return;
                }
            }
        }
        
        
        noFullDepo = true;
        if(!inStay) MoveToStay();
        Wait();
    }
    
    public void ChangeSpeed()
    {
        agent.speed = Constant.baseSpeed  + (data.userData.zoneOutWarLevel * Constant.speedDecreeseKoeff*data.userData.ADSpeedMultiplier);
    }
    private void TakeBox()
    {
        if (currentMainDepo.currentBox>=2)
        {
            currentMainDepo.RemovBox(3);
            currentMainDepo.isBuzy = false;
            inStay = false;
            isGoToStay1 = false;
            curBox.SetActive(true);
            MoveToShipment();
            return;
        }
        Wait();
    }
    private void GiveBox()
    {
        if (data.userData.warehaus_updated == 0)
        {
            currentOutDepo = startOutDepo;
        }
        else if (data.userData.warehaus_updated == 1)
        {
            currentOutDepo = outDepo_1;
        }
        else if (data.userData.warehaus_updated == 2)
        {
            currentOutDepo = outDepo_2;
        }
        else if (data.userData.warehaus_updated == 3)
        {
            currentOutDepo = outDepo_3;
        }
        else
        {
            currentOutDepo = startOutDepo;
        }

        if (currentOutDepo.AddBox(currentID))
        {
            curBox.SetActive(false);
            isLoading = true;            
            FindDepopsitory();
            return;
        }
        Wait();
    }
    private void MoveToStay()
    {
        agent.SetDestination(stayPoint.position);
        isGoToStay = true;
    }
    private void MoveToLoading()
    {
        isLoading = true;
        isShipment = false;        
        
        agent.SetDestination(currentTransform.position);
        isGoToLoading = true;
    }
   
    private void MoveToShipment()
    {
        agent.SetDestination(outDepoTransform.position);
        isLoading = false;
        isShipment = true;
        isGoToShipment = true;
    }

    private void Wait()
    {
        if (!inStay)
        {
            agent.SetDestination(stayPoint.position);
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
        if (!isGoToStay1)
        {
            agent.SetDestination(stayPoint1.position);            
            Invoke(nameof(GoToStay), timeToFlip);

        }
    }
    private void GoToStay()
    {
        if (!inStay)
        {
            agent.updateRotation = false;
            agent.SetDestination(stayPoint.position);
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
                currentDystance = Vector3.Distance(transform.position, stayPoint.position);
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
                if (noFullDepo)
                {
                    FindDepopsitory();
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
        if (isGoToStay)
        {
            currentDystance = Vector3.Distance(transform.position, stayPoint.position);
            if (currentDystance < stopPoint)
            {
                isGoToStay = false;
                Wait();
            }
        }
        if (isGoToShipment)
        {
            currentDystance = Vector3.Distance(transform.position, outDepoTransform.position);
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
}
