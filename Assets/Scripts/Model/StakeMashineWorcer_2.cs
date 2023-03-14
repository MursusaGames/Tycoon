using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class StakeMashineWorcer_2 : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    private AppData data;
    private AudioListenerScript audioListener;
    [SerializeField] private StakeMashineWorcer worcer_1;
    [SerializeField] private GameObject ownBox;
    [SerializeField] private BoxInStakeMashine boxInStake;    
    [SerializeField] private GameObject boxInMashine;
    [SerializeField] private Stock stock;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private GameObject moneyBoom;
    private float currentDystance;
    private Stock currentPalette;
    public int currentStakeInBox =0;
    public int currentBoxInPalet = 0;
    private bool isGoToLoading;
    private bool isGoToShipment;
    public float stopPoint = 1.5f;
    private bool isWait;
    private float maxTime = 10f;
    private float currentTime;
    private bool finding;
    private int stakeInBoxCount;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        audioListener = FindObjectOfType<AudioListenerScript>();
    }

    public void GetBox()
    {
        agent.SetDestination(startPoint.position);
        anim.SetBool("idle", false);
        anim.SetBool("walk", true);
        isGoToLoading = true;
    }
    
    void Start()
    {
        data = audioListener._data;
        anim.SetBool("walk", false);
        anim.SetBool("idle", true);
        stakeInBoxCount = data.matchData.stakeInBox;
        FindPalette();
    }
    public void SetSpeed()
    {
        agent.speed = 3.5f + data.userData.fileMashineSpeedLevel / 20f;   //изменяем скорость работника от 3.5 до 7        
    }
    private bool FindPalette()
    {
        finding = true;
        if (stock.currentBox<8)
        {
            currentPalette = stock;            
            finding = false;
            worcer_1.stop = false;
            return true;
        }
        Wait();
        worcer_1.stop = true;
        return false;
    }

    private void TakePalet()
    {
        agent.SetDestination(endPoint.position);
        anim.SetBool("idle", false);
        anim.SetBool("carry", true);
        isGoToShipment = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWait)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                isWait = false;
                if (finding) FindPalette();
            }
            return;
        }
        if (isGoToLoading)
        {
            currentDystance = Vector3.Distance(transform.position, startPoint.position);
            if (currentDystance < stopPoint)
            {
                anim.SetBool("walk", false);
                anim.SetBool("idle", true);
                isGoToLoading = false;
                
            }
        }
        if (isGoToShipment)
        {
            currentDystance = Vector3.Distance(transform.position, endPoint.position);
            if (currentDystance < stopPoint)
            {
                anim.SetBool("carry", false);
                anim.SetBool("idle", true);
                if (currentPalette.currentBox < 8)
                {
                    currentPalette.AddBox();
                    isGoToShipment = false;
                    ownBox.SetActive(false);
                    MoveToLoading();
                    return;
                }
                else
                {
                    finding = true;
                    Wait();
                }
            }
        }

    }
    private void MoveToLoading()
    {
        agent.SetDestination(startPoint.transform.position);
        anim.SetBool("idle", false);
        anim.SetBool("walk", true);
        isGoToLoading = true;
    }
    private void Wait()
    {
        anim.SetBool("walk", false);
        anim.SetBool("idle", true);
        isWait = true;
        currentTime = maxTime;
    }
    public void TakeStake() //вызов от стейка точка входа
    {
        GiveStake();
    }
    private void GiveStake()
    {
        boxInMashine.SetActive(false);
        currentStakeInBox++;
        boxInStake.AddFirstStake();
        
        if (currentStakeInBox >= stakeInBoxCount)
        {
            if (FindPalette())
            {
                boxInStake.ClearBox();
                currentStakeInBox = 0;
                ownBox.SetActive(true);
                moneyBoom.SetActive(true);
                TakePalet();
            }           
            
        }
    }
}
