using UnityEngine.AI;
using UnityEngine;

public class CarShipment : MonoBehaviour
{
    private OutDepository removedDepository;
    [SerializeField] private OutDepository outDepo_1;
    [SerializeField] private OutDepository outDepo_2;
    [SerializeField] private OutDepository startOutDepo;
    [SerializeField] private OutDepository outDepo_3;
    private NavMeshAgent agent;
    private float currentDystance;
    [SerializeField] private GameObject startPoint;
    [SerializeField] private GameObject endPoint;
    [SerializeField] private GameObject stayPoint;
    [SerializeField] private GameObject stayPointEnd;
    [SerializeField] private AppData data;
    [SerializeField] public GameObject Fishbox;
    [SerializeField] public GameObject UnpStakebox;
    [SerializeField] public GameObject Stakebox;
    [SerializeField] public GameObject UnpFarshbox;
    [SerializeField] public GameObject Farshbox;
    [SerializeField] public GameObject UnpFilebox;
    [SerializeField] public GameObject Filebox;
    [SerializeField] private OrdersSystem ordersSystem;
    public int currentBox;
    private float maxTime = 10f;
    public float currentTime;
    public bool isWait;
    public bool isLoading;
    public bool isShipment;
    public bool isFinding;
    public bool isGoToShipment;
    public bool isGoToLoading;
    [SerializeField] private float stopPoint = 1f;    
    public bool inStay;
    public bool isGoToStay;
    private float timeToiFlip = 3f;
    private int currentOrderProductID;
    private int countProducts = 7;
    private FishProductType productType;
    private int maxProductCount;
    private int currentProductCount;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void StartInit()
    {
        CheckDepo();
        Invoke(nameof(ChangeSpeed), 5f);
        inStay = true;        
        isLoading = true;
        FindOrder();        
        currentBox = 0;
    }
    public void CheckDepo()
    {
        if (data.userData.warehaus_updated == 0)
        {
            removedDepository = startOutDepo;
        }
        else if (data.userData.warehaus_updated == 1)
        {
            removedDepository = outDepo_1;
        }
        else if (data.userData.warehaus_updated == 2)
        {
            removedDepository = outDepo_2;
        }
        else if (data.userData.warehaus_updated == 3)
        {
            removedDepository = outDepo_3;
        }
        else
        {
            removedDepository = startOutDepo;
        }
    }
    private void FindOrder()
    {
        isFinding = true;        
        if (data.userData.inDeliver)
        {
            maxProductCount = data.userData.currentMaxProductCount;
            currentProductCount = data.userData.currentProductCount;
            currentOrderProductID = data.userData.currentProductID;
            if(maxProductCount > currentProductCount)
            {
                isFinding = false;
                productType = currentOrderProductID == 0 ? FishProductType.Fish : currentOrderProductID == 1 ? FishProductType.UnpackStake
                    : currentOrderProductID == 2 ? FishProductType.Stake : currentOrderProductID == 3 ? FishProductType.Farsh
                    : currentOrderProductID == 4 ? FishProductType.UnpackFarsh : currentOrderProductID == 5 ? FishProductType.File
                    :  FishProductType.UnpackFile;
                MoveToLoading();
            }
            else
            {
                Wait();
            }
            
        }
        else
        {
            Wait();
        }
    }
    
    private void TakeBox()
    {
        if (removedDepository.FindActivPaleteForProduct(productType) < int.MaxValue)
        {
            removedDepository.RemoveBox(productType, 1, currentOrderProductID);
            inStay = false;
            isGoToStay = false;
            if(productType == FishProductType.Fish) Fishbox.SetActive(true);
            else if (productType == FishProductType.UnpackStake) UnpStakebox.SetActive(true);
            else if (productType == FishProductType.Stake) Stakebox.SetActive(true);
            else if (productType == FishProductType.Farsh) Farshbox.SetActive(true);
            else if (productType == FishProductType.UnpackFarsh) UnpFarshbox.SetActive(true);
            else if (productType == FishProductType.File) Filebox.SetActive(true);
            else if (productType == FishProductType.UnpackFile) UnpFilebox.SetActive(true);
            MoveToShipment();
            return;
        }
        Wait();
    }
    private void GiveBox()
    {
        if(currentOrderProductID == 0)
        {
            ordersSystem.FishAdd();
        }            
        else if (currentOrderProductID == 1)
        {
            ordersSystem.UnpackedStakeAdd();
        }            
        else if (currentOrderProductID == 2)
        {
            ordersSystem.StakeAdd();
        }
        else if (currentOrderProductID == 3)
        {
            ordersSystem.FarshAdd();
        }
        else if (currentOrderProductID == 4)
        {
            ordersSystem.UnpackedFarshAdd();
        }
        else if (currentOrderProductID == 5)
        {
            ordersSystem.FileAdd();
        }
        else if (currentOrderProductID == 6)
        {
            ordersSystem.UnpackedFileAdd();
        }

        isShipment = false;
        isLoading = true;        
        Fishbox.SetActive(false);
        Stakebox.SetActive(false);
        UnpStakebox.SetActive(false);
        Farshbox.SetActive(false);
        UnpFarshbox.SetActive(false);
        Filebox.SetActive(false);
        UnpFilebox.SetActive(false);
        FindOrder(); 
    }
    private void MoveToLoading()
    {
        if (removedDepository.FindActivPaleteForProduct(productType) < int.MaxValue && currentOrderProductID < countProducts)
        {
            isShipment = false;
            agent.SetDestination(startPoint.transform.position);
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
            
            if (isGoToStay)
            {
                currentDystance = Vector3.Distance(transform.position, stayPoint.transform.position);
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
                if (isFinding)
                {
                    FindOrder();
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
                GiveBox();
            }
        }
        if (isGoToLoading)
        {
            currentDystance = Vector3.Distance(transform.position, startPoint.transform.position);
            if (currentDystance < stopPoint)
            {
                isGoToLoading = false;
                TakeBox();
            }
        }
    }
    public void ChangeSpeed()
    {
        agent.speed = Constant.baseSpeed + (data.userData.finishZoneCarLevel * Constant.speedDecreeseKoeff*data.userData.ADSpeedMultiplier);
    }
}
