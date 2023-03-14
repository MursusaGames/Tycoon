using UnityEngine.AI;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class HeadCutMashineWorcer : MonoBehaviour
{
    [SerializeField] private Transform startPosFishWiyhOutHead;
    [SerializeField] private Transform parent;
    [SerializeField] private Transform fishParent;
    [SerializeField] private Transform fishPosition;
    [SerializeField] private GameObject fish;
    [SerializeField] private Stock previousStock;    
    [SerializeField] private Stock stock;
    private NavMeshAgent agent;    
    [SerializeField] private GameObject boxPrefab;    
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform startPoint;
    private AppData data;
    [SerializeField] public GameObject box;    
    private Animator anim;
    private float maxTime = 8f;
    public float currentTime;
    public bool isWait;
    public bool isLoading;
    private bool isFinding;
    public bool isShipment;
    public bool isGoToShipment;
    public bool isGoToLoading;
    private float stopPoint = 1f;
    private AudioListenerScript audioListener;    
    private float currentDystance;    
    private int fishInBox;
    public bool stop;    
    public bool headCut1;
    private void Awake()
    {
        anim = GetComponent<Animator>();        
        agent = GetComponent<NavMeshAgent>();
        audioListener = FindObjectOfType<AudioListenerScript>();
        data = audioListener._data;
    }

    void Start()
    {
        fishInBox = data.matchData.fisInBox;
        isFinding = true;
        FindPalet();
    }    
    private void FindPalet()
    {
        isFinding = true;
        if (CheckFreePalet()&& !stop)
        {
            isFinding = false;
            MoveToLoading();
        }
        else Wait();
    }
    private void TakeBox()
    {
        stock.RemovBox();
        box.SetActive(true);             
        MoveToShipment();
    }

    private bool CheckFreePalet()
    {
        if(stock.currentBox>=0)
        {
            return true;
        }
        return false;
    }
    private void GiveBox()
    {
        box.SetActive(false);
        TakeAFish();          
    }

    public void SetSpeed()
    {
        agent.speed = headCut1 ? 3.5f + data.userData.headCutingMashineSpeedLevel / 20f
            : 3.5f + data.userData.headCutingMashine2SpeedLevel / 20f; //изменяем скорость работника от 3.5 до 7
    }
    private void MoveToLoading()
    {
        isLoading = true;
        isShipment = false;        
        agent.SetDestination(startPoint.position);
        anim.SetBool("idle", false);
        anim.SetBool("walk", true);
        isGoToLoading = true;
    }
    private void MoveToShipment()
    {
        agent.SetDestination(endPoint.position);
        anim.SetBool("idle", false);
        anim.SetBool("carry", true);
        isLoading = false;
        isShipment = true;
        isGoToShipment = true;
    }

    private void Wait()
    {
        isWait = true;
        currentTime = headCut1? maxTime - (data.userData.headCutingMashineSpeedLevel * 0.1f):
            (data.userData.headCutingMashine2SpeedLevel * 0.1f); //изменяем время ожидания от 9 до 1.5 сек
    }

    private void TakeAFish()
    {
        StartCoroutine(nameof(GetFishes));
    }
    private IEnumerator GetFishes()
    {
        float timeToCicle = headCut1 ? 1.8f - (data.userData.headCutingMashineSpeedLevel*data.userData.ADSpeedMultiplier / 42)
            : 1.8f - (data.userData.headCutingMashine2SpeedLevel * data.userData.ADSpeedMultiplier / 42);        
        if (timeToCicle <= 0) timeToCicle = 0.1f;
        while (fishInBox > 0)
        {
            var currentFish = Instantiate(fish, fishPosition.position, Quaternion.identity, fishParent).GetComponent<BlueFish>();
            currentFish.startPosFishWithOutHead = startPosFishWiyhOutHead;
            currentFish.StartCount();
            currentFish.data = data;
            currentFish.masine1 = headCut1 ? true : false;
            fishInBox--;
            yield return new WaitForSeconds(timeToCicle);
        }
        fishInBox = data.matchData.fisInBox;
        if (stock.currentBox >= 0)
        {
            MoveToLoading();
        }
        else
        {
            isFinding = true;
            Wait();
        }
        yield break;

    }

    public void StopWork()
    {
        stop = true;
        anim.SetBool("carry", false);
        anim.SetBool("walk", false);
        anim.SetBool("idle", true);
    }
    public void WorkAgain()
    {
        stop = false;
        MoveToLoading();
    }

    void FixedUpdate()
    {
        if (stop) return;
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
                if (isShipment) GiveBox();
            }
        }
        if (isGoToShipment)
        {
            currentDystance = Vector3.Distance(transform.position, endPoint.position);
            if (currentDystance < stopPoint)
            {
                anim.SetBool("carry", false);
                anim.SetBool("idle", true);
                isGoToShipment = false;
                GiveBox();
            }
        }
        if (isGoToLoading)
        {
            currentDystance = Vector3.Distance(transform.position, startPoint.position);
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
