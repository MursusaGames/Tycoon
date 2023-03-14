using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EggCollector : WorcerEntity
{
    private AudioListenerScript audioListener;
    public EggsDepositories[] depositories = new EggsDepositories[5];
    public ChickenHoum[] chickenHoums = new ChickenHoum[5];
    public Nest[] nests = new Nest[14];
    private EggsDepositories currentDepository;
    private ChickenHoum currentChickenHoum;
    private Animator anim;
    [SerializeField] private Image filAmountImage;   
    [SerializeField] private GameObject bag;
    [SerializeField] private AppData data;    
    private NavMeshAgent agent;
    private float currentDystanceToDepo;
    private float currentDystanceToChickenHoum;    
    [SerializeField] private float stopPoint = 1.5f;
    private float timer;
    private int index;
    public bool isGoToChickenHoum;
    public bool isGoToDepo;
    public bool isGoToEgg;
    public bool isGoal;
    public bool isSleep;
    public bool isFull;
    public bool isTake;
    private void Awake()
    {
        nests = FindObjectsOfType<Nest>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();        
        audioListener = FindObjectOfType<AudioListenerScript>();
        depositories = FindObjectsOfType<EggsDepositories>();
        chickenHoums = FindObjectsOfType<ChickenHoum>();
        FindDepopsitory();
        FindChickenHoum();
    }
    private void Start()
    {
        data = audioListener._data;
        CheckEfficiency();         
    }
    private void CheckEfficiency()
    {
        //_efficiency = data.userData.eggCollectorStartEfficiency.Value - (data.userData.eggCollectorsLevel.Value*data.upgradesData.stepOffMultyplyUpgrade);
        //timer = _efficiency;
    }
    private void FindDepopsitory()
    {
        float dystance = float.MaxValue;
        for (int i = 0; i < depositories.Length; i++)
        {
            currentDystanceToDepo = Vector3.Distance(transform.position, depositories[i].gameObject.transform.position);
            if (currentDystanceToDepo < dystance)
            {
                dystance = currentDystanceToDepo;
                currentDepository = depositories[i];
            }
        }
    }
    private void FindChickenHoum()
    {
        float dystance = float.MaxValue;
        for (int i = 0; i < chickenHoums.Length; i++)
        {
            currentDystanceToChickenHoum = Vector3.Distance(transform.position, chickenHoums[i].gameObject.transform.position);
            if (currentDystanceToChickenHoum < dystance)
            {
                dystance = currentDystanceToChickenHoum;
                currentChickenHoum = chickenHoums[i];
            }
        }
    }
    public override void Move()
    {
        agent.SetDestination(currentChickenHoum.gameObject.transform.position);
        anim.SetBool("idle", false);
        anim.SetBool("walk",true);
    }
    public void FindTarget()
    {
        isGoToChickenHoum = true;
        Move();
    }

    private void TakeEgg()
    {
        nests[index].egg.SetActive(false);
        nests[index].isFull = false;
        isTake = false;
        anim.SetBool("walk", false);
        anim.SetBool("idle", true);
    }
    private void FixedUpdate()
    {
        if (isGoToChickenHoum)
        {
            currentDystanceToChickenHoum = Vector3.Distance(transform.position, currentChickenHoum.transform.position);            
            if (currentDystanceToChickenHoum < stopPoint)
            {
                CheckEfficiency();
                isGoToChickenHoum = false;
                isGoal = true;
                anim.SetBool("walk", false);
                anim.SetBool("idle",true);
                bag.SetActive(true);
            }
        }
        if (isGoal)
        {
            timer -= 0.02f;
            filAmountImage.fillAmount = timer / _efficiency;
            if(timer > 1&&!isTake)
            {
                var eggPos = GetFreeNestPosition();
                if (eggPos != Vector3.zero)
                {
                    isFull = true;
                    isTake = true;
                    agent.SetDestination(eggPos);
                    anim.SetBool("idle", false);
                    anim.SetBool("walk", true);
                    Invoke(nameof(TakeEgg), 1f);
                }
            }         
            if(timer<=0)
            {
                if (isFull)
                {
                    isFull = false;
                    isGoal = false;
                    timer = _efficiency;
                    MoveToDepository();
                    return;
                }
                else
                {
                    timer = _efficiency;
                    Sleep();
                }                              
            }
        }
        if (isGoToDepo)
        {
            currentDystanceToDepo = Vector3.Distance(transform.position, currentDepository.transform.position);
            
            if (currentDystanceToDepo < stopPoint)
            {
                isGoToDepo = false;                
                bag.SetActive(false);
                if (currentDepository.AddBox())
                {
                    isGoToChickenHoum = true;
                    Move();
                }
                else Sleep();                
            }
        }
    }
    private bool CheckNest()
    {
        for (int i = 0; i < nests.Length; i++)
        {
            if (nests[i].isFull)
            {
                return true;
            }
        }
        return false;
    }

    private Vector3 GetFreeNestPosition()
    {
        for (int i = 0; i < nests.Length; i++)
        {
            if (nests[i].isFull)
            {
                index = i;
                return nests[i].gameObject.transform.position;
            }
        }
        return Vector3.zero;
    }
    private void Sleep()
    {
        isSleep = true;         
        Invoke(nameof(ResetSleep), 5f);
    }
    
    private void ResetSleep()
    {
        isSleep = false;        
    }
    private void MoveToDepository()
    {
        agent.SetDestination(currentDepository.gameObject.transform.position);
        anim.SetBool("idle", false);
        anim.SetBool("walk", true);
        isGoToDepo = true;
    }    
}
