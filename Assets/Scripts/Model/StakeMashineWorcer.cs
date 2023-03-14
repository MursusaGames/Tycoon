using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class StakeMashineWorcer : MonoBehaviour
{
    public NarezkaMashineAnimation narezka;
    [SerializeField] private StakeBox stakeBox;
    [SerializeField] private GameObject fishOwn;
    [SerializeField] private Transform fishPosition;
    [SerializeField] private Transform stakePosition;
    [SerializeField] private GameObject fish;    
    [SerializeField] private Stock stock;    
    private NavMeshAgent agent;
    [SerializeField] private Transform depoPoint;
    [SerializeField] private GameObject endPoint;
    private AppData data;
    [SerializeField] public GameObject box;
    private Animator anim;
    private float maxTime = 10f;
    public float currentTime;
    public bool isWait;
    public bool isLoading;
    private bool isFinding;
    public bool isShipment;
    public bool isGoToShipment;
    public bool isGoToLoading;
    public float stopPoint = 2f;
    private AudioListenerScript audioListener;
    private float currentDystance;
    private Stock currentPalette;
    public int currentBoxInPalet;
    private int fishInBox;
    public float cleaningDelay = 1f;
    public float fishOutDelay = 2f;
    private float _fishOutDelay;
    public bool machine1;
    public bool machine2;
    public bool machine3;
    private float fishSpeed = 1f;
    public bool stop;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        audioListener = FindObjectOfType<AudioListenerScript>();
    }

    void Start()
    {
        data = audioListener._data;
        fishInBox = data.matchData.fisInBox;
        isFinding = true;
        FindPalet();
    }

    public void SetSpeed()
    {
        agent.speed = 3.5f + data.userData.fileMashineSpeedLevel / 20f;   //изменяем скорость работника от 3.5 до 7
        fishSpeed = 1.0f + (data.userData.fileMashineSpeedLevel * Constant.speedDecreeseKoeff * data.userData.ADSpeedMultiplier);
        _fishOutDelay = fishOutDelay - data.userData.fileMashineSpeedLevel / 25;
    }
    private void FindPalet()
    {
        
        if (CheckFreePalet())
        {
            MoveToLoading();
        }
        else Wait();
    }
    private void MoveToLoading()
    {
        isLoading = true;
        isShipment = false;
        agent.SetDestination(depoPoint.position);
        anim.SetBool("idle", false);
        anim.SetBool("walk", true);
        isGoToLoading = true;
    }
    private void TakeBox()
    {
        if (CheckFreePalet())
        {
            currentPalette.RemovBox();
            box.SetActive(true);            
            MoveToShipment();
        }
        else
        {
            isFinding = true;
            Wait();
        }
    }

    private bool CheckFreePalet()
    {
        if (stock.currentBox>=0)
        {
            currentPalette = stock;
            return true;
        }
        return false;
    }
    
    private void MoveToShipment()
    {
        agent.SetDestination(endPoint.transform.position);
        anim.SetBool("idle", false);
        anim.SetBool("carry", true);
        isLoading = false;
        isShipment = true;
        isGoToShipment = true;
    }

    private void Wait()
    {
        isWait = true;
        currentTime = maxTime - (data.upgradesData.stepOffMultyplyUpgrade * data.userData.zone1CarLevel);
    }
    

    private void TakeAFish()
    {
        StartCoroutine(nameof(GetFishes));
    }
    private IEnumerator GetFishes()
    {
        fishOwn.SetActive(true);
        anim.SetBool("idle", false);
        anim.SetBool("putFish", true);        
        stakeBox.HideFish();
        yield return new WaitForSeconds(cleaningDelay);
        var currentFish = Instantiate(fish, fishPosition.position, Quaternion.identity, fishPosition).GetComponent<BlueFish>();
        currentFish.inStake = true;
        currentFish.startPosFishWithOutHead = stakePosition;
        currentFish.speed = fishSpeed;
        currentFish.narezka = narezka;
        fishOwn.SetActive(false);
        anim.SetBool("idle", true);
        anim.SetBool("putFish", false);
        yield return new WaitForSeconds(fishOutDelay);
        box.SetActive(false);        
        MoveToLoading();
        yield break;
    }
    private void StopAnim()
    {
        anim.SetBool("carry", false);
        anim.SetBool("walk", false);
        anim.SetBool("idle", true);
    }

    void FixedUpdate()
    {
        if (stop)
        {
            StopAnim();
            return;
        }
        if (isWait)
        {
            currentTime -= Time.fixedDeltaTime;
            if (currentTime <= 0)
            {
                isWait = false;
                if (isFinding)
                {
                    FindPalet();
                    return;
                }
            }
        }
        if (isGoToShipment)
        {
            currentDystance = Vector3.Distance(transform.position, endPoint.transform.position);
            if (currentDystance < stopPoint)
            {
                anim.SetBool("carry", false);
                anim.SetBool("idle", true);
                isGoToShipment = false;
                TakeAFish();
            }
        }
        if (isGoToLoading)
        {
            currentDystance = Vector3.Distance(transform.position, depoPoint.position);
            if (currentDystance < stopPoint)
            {
                anim.SetBool("walk", false);
                anim.SetBool("idle", true);
                isGoToLoading = false;
                TakeBox();
            }
        }
    }
}
