using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PackingMashineWorcer_1 : MonoBehaviour
{
    [SerializeField] private Transform war13Point;
    [SerializeField] private GameObject stakeInTop;
    [SerializeField] private Top top;
    [SerializeField] private GameObject stakeOwn;
    [SerializeField] private GameObject ownBox;    
    [SerializeField] private GameObject stake;    
    [SerializeField] private Stock stock;    
    private NavMeshAgent agent;    
    [SerializeField] private GameObject endPoint;
    private AppData data;    
    private Animator anim;
    private float maxTime = 10f;
    public float currentTime;
    public bool isWait;
    public bool isLoading;
    public bool isFinding;
    public bool isShipment;
    public bool isGoToShipment;
    public bool isGoToLoading;
    public float stopPoint = 2f;
    private AudioListenerScript audioListener;
    private float currentDystance;
    public Stock currentPalette;
    public int currentBoxInPalet;
    public int fishInBox;
    public float cleaningDelay = 1f;
    public float fishOutDelay = 3.2f;
    private float _fishOutDelay;
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
        fishInBox = data.matchData.stakeInBox;
        isFinding = true;
        FindPalet();
    }
    public void SetSpeed()
    {
        agent.speed = 3.5f + data.userData.steakPackingMashineSpeedLevel / 20f;   //изменяем скорость работника от 3.5 до 7        
        _fishOutDelay = fishOutDelay - data.userData.steakPackingMashineSpeedLevel / 25;
    }
    private void FindPalet()
    {
        isFinding = true;
        if (CheckFreePalet())
        {
            isFinding=false;
            MoveToLoading();
        }
        else
        {
            Wait();
        }
            
    }
    private void TakeBox()
    {
        if (currentPalette.currentBox>=0)
        {
            ownBox.SetActive(true);
            currentPalette.RemovBox();            
            MoveToShipment();
        }
        else
        {
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
    private void MoveToLoading()
    {
        isLoading = true;
        isFinding = false;
        isShipment = false;
        agent.SetDestination(war13Point.position);
        anim.SetBool("idle", false);
        anim.SetBool("walk", true);
        isGoToLoading = true;
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
        currentTime = maxTime;
    }

    private void TakeAStake()
    {
        StartCoroutine(nameof(GetStakes));
    }
    private IEnumerator GetStakes()
    {
        while (fishInBox > 0)
        {
            currentBoxInPalet++;            
            fishInBox--;
            top.OpenTop();
            yield return new WaitForSeconds(cleaningDelay);           
            stakeInTop.SetActive(true);
            ownBox.SetActive(false);
            yield return new WaitForSeconds(cleaningDelay);
            top.CloseTop();
            yield return new WaitForSeconds(_fishOutDelay);
            stakeInTop.SetActive(false);
            stake.SetActive(true);
            yield return new WaitForSeconds(cleaningDelay);
        }
        
        fishInBox = data.matchData.stakeInBox;
        FindPalet();
        yield break;        
    }
    private void StopAnim()
    {
        anim.SetBool("carry", false);
        anim.SetBool("walk", false);
        anim.SetBool("idle", true);
    }
    public void StopGame()
    {
        stop = true;
        StopAnim();
    }

    public void PlayGame()
    {
        stop = false;
        isFinding = true;
        FindPalet();
    }
    void FixedUpdate()
    {
        if (stop)
        {
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
                if (isLoading) TakeBox();
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
                TakeAStake();
            }
        }
        if (isGoToLoading)
        {
            currentDystance = Vector3.Distance(transform.position, war13Point.position);
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

