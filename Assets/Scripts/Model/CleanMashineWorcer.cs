using UnityEngine.AI;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CleanMashineWorcer : MonoBehaviour
{
    
    [SerializeField] private BoxInCleanMashine boxInMashine;
    [SerializeField] private GameObject fishOwn;
    [SerializeField] private Transform fishPosition;
    [SerializeField] private GameObject fish;
    [SerializeField] private Stock currentDepository;
    [SerializeField] private Stock stock;
    [SerializeField] private Transform loadingPoint;
    private NavMeshAgent agent;
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private GameObject endPoint;
    private AppData data;
    [SerializeField] public GameObject box;
    private Animator anim;
    private float maxTime = 10f;
    public float currentTime;
    public bool isWait;
    public bool isLoading;
    public bool isFinding;
    public bool isShipment;
    public bool isGoToShipment;
    public bool isGoToLoading;
    public float stopPoint = 1f;
    private AudioListenerScript audioListener;
    private float currentDystance;    
    private int fishInBox;
    public float cleaningDelay = 3f;
    private float startCleaningDelay = 5f;
    public float fishOutDelay = 1f;
    public bool stop;
    public bool stopAnimation;
    public bool mashine_1;
    public bool mashine_2;
    private float speedFish = 1f;
    private int fishInBoxData = 1;//TODO исправить
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        audioListener = FindObjectOfType<AudioListenerScript>();
    }

    void Start()
    {
        data = audioListener._data;
        fishInBox = fishInBoxData;
        isFinding = true;        
        FindPalet();
        CheckSpeed();
    }

    public void CheckSpeed()
    {
        var additivSpeed = mashine_1 ? data.userData.fishCleaningMashineSpeedLevel : mashine_2 ? data.userData.fishCleaningMashine2SpeedLevel :
            data.userData.fishCleaningMashine3SpeedLevel;
        agent.speed = Constant.baseSpeed + (additivSpeed * Constant.speedDecreeseKoeff);

    }
    private void FindPalet()
    {
        isFinding = true;
        if (CheckFreePalet()&&!stop)
        {
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
            isFinding = false;
            return true;
        }
        return false;
    }    
    private void MoveToLoading()
    {
        if (stop) return;        
        isLoading = true;
        isShipment = false;
        agent.SetDestination(loadingPoint.position);
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

    private void TakeAFish()
    {
        boxInMashine.ShowMesh();
        box.SetActive(false);
        StartCoroutine(nameof(GetFishes));
    }
    private IEnumerator GetFishes()
    {
        fishOutDelay = mashine_1 ? startCleaningDelay - (data.userData.fishCleaningMashineSpeedLevel / 20)
            : mashine_2? startCleaningDelay - (data.userData.fishCleaningMashine2SpeedLevel / 20) 
            : startCleaningDelay - (data.userData.fishCleaningMashine3SpeedLevel / 20);
        fishOwn.SetActive(true);
        anim.SetBool("fish", true);
        boxInMashine.HideFish();
        fishInBox--;
        yield return new WaitForSeconds(cleaningDelay);
        var currentFish = Instantiate(fish, fishPosition.position, Quaternion.identity, fishPosition);
        currentFish.GetComponent<BlueFish>().withOutHead = true;
        if (mashine_1)
        {
            if (data.userData.fishCleaningMashineSpeedLevel > 65)
            {
                speedFish = 5f * data.userData.ADSpeedMultiplier;
            }
            else if (data.userData.fishCleaningMashineSpeedLevel > 55)
            {
                speedFish = 4f * data.userData.ADSpeedMultiplier;
            }
            else if (data.userData.fishCleaningMashineSpeedLevel > 45)
            {
                speedFish = 3f * data.userData.ADSpeedMultiplier;
            }
            else if (data.userData.fishCleaningMashineSpeedLevel > 35)
            {
                speedFish = 2f * data.userData.ADSpeedMultiplier;
            }
            else if (data.userData.fishCleaningMashineSpeedLevel > 20)
            {
                speedFish = 1.5f * data.userData.ADSpeedMultiplier;
            }
            else if (data.userData.fishCleaningMashineSpeedLevel > 10)
            {
                speedFish = 1.0f * data.userData.ADSpeedMultiplier;
            }
            else speedFish = 0.4f * data.userData.ADSpeedMultiplier;
        }
        else if (mashine_2)
        {
            if (data.userData.fishCleaningMashine2SpeedLevel > 65)
            {
                speedFish = 5f * data.userData.ADSpeedMultiplier;
            }
            else if (data.userData.fishCleaningMashine2SpeedLevel > 55)
            {
                speedFish = 4f * data.userData.ADSpeedMultiplier;
            }
            else if (data.userData.fishCleaningMashine2SpeedLevel > 45)
            {
                speedFish = 3f * data.userData.ADSpeedMultiplier;
            }
            else if (data.userData.fishCleaningMashine2SpeedLevel > 35)
            {
                speedFish = 2f * data.userData.ADSpeedMultiplier;
            }
            else if (data.userData.fishCleaningMashine2SpeedLevel > 20)
            {
                speedFish = 1.5f * data.userData.ADSpeedMultiplier;
            }
            else if (data.userData.fishCleaningMashine2SpeedLevel > 10)
            {
                speedFish = 1.0f * data.userData.ADSpeedMultiplier;
            }
            else speedFish = 0.4f * data.userData.ADSpeedMultiplier;
        }
        else
        {
            if (data.userData.fishCleaningMashine3SpeedLevel > 65)
            {
                speedFish = 5f * data.userData.ADSpeedMultiplier;
            }
            else if (data.userData.fishCleaningMashine3SpeedLevel > 55)
            {
                speedFish = 4f * data.userData.ADSpeedMultiplier;
            }
            else if (data.userData.fishCleaningMashine3SpeedLevel > 45)
            {
                speedFish = 3.0f * data.userData.ADSpeedMultiplier;
            }
            else if (data.userData.fishCleaningMashine3SpeedLevel > 35)
            {
                speedFish = 2f * data.userData.ADSpeedMultiplier;
            }
            else if (data.userData.fishCleaningMashine3SpeedLevel > 20)
            {
                speedFish = 1.5f * data.userData.ADSpeedMultiplier;
            }
            else if (data.userData.fishCleaningMashine3SpeedLevel > 10)
            {
                speedFish = 1f * data.userData.ADSpeedMultiplier;
            }
            else speedFish = 0.4f * data.userData.ADSpeedMultiplier;
        }
        currentFish.GetComponent<BlueFish>().speed = speedFish;
        fishOwn.SetActive(false);
        anim.SetBool("fish", false);
        yield return new WaitForSeconds(fishOutDelay);
        FindPalet();
        yield break;
    }

    public void StopPlay()
    {
        stop = true;       
    }

    public void PlayAgain()
    {
        if (!stop) return;
        stop = false;
        if (stopAnimation) stopAnimation = false;            
        FindPalet();
    }
    private void ControlStop()
    {
        isGoToLoading = false;
        anim.SetBool("walk", false);
        anim.SetBool("carry", false);
        anim.SetBool("idle", true);
    }

    void FixedUpdate()
    {
        if (stop)
        {
            if (!stopAnimation)
            {
                isGoToLoading = false;
                anim.SetBool("walk", false);
                anim.SetBool("carry", false);
                anim.SetBool("idle", true);
                stopAnimation = true;
                //Invoke(nameof(ControlStop), 1f);
            }            
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
            currentDystance = Vector3.Distance(transform.position, loadingPoint.position);
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
