using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class PackingMashineWorcer_2 : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    private AppData data;
    private AudioListenerScript audioListener;
    [SerializeField] private Transform point;
    [SerializeField] private Transform steakPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private GameObject box;
    [SerializeField] private GameObject stake;
    [SerializeField] private GameObject ownStake;    
    [SerializeField] private BoxStake boxInMashine;
    [SerializeField] private Stock stock;
    [SerializeField] private PackingMashineWorcer_1 worcer_1;
    private float currentDystance;
    private Stock currentPalette;
    public int currentBoxInPalet;
    private bool isGoToLoading;
    private bool isGoToShipment;
    private bool isGoToBox;
    public float stopPoint = 1.0f;
    private bool isWait;
    private float maxTime = 2f;
    public float currentTime;
    public bool finding;
    public bool getBox;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        audioListener = FindObjectOfType<AudioListenerScript>();
    }

    public void GetBox()
    {
        if (FindPalette())
        {
            agent.SetDestination(steakPoint.position);
            anim.SetBool("idle", false);
            anim.SetBool("walk", true);
            isGoToLoading = true;
        }
        else
        {
            getBox = true;
        }
    }
    void Start()
    {
        data = audioListener._data;
        anim.SetBool("walk", false);
        anim.SetBool("idle", true);
        FindPalette();
    }

    private bool FindPalette()
    {
        finding = true;
        if (stock.currentBox<8)
        {
            currentPalette = stock;            
            worcer_1.PlayGame();
            finding = false;
            if (getBox)
            {
                agent.SetDestination(steakPoint.position);
                anim.SetBool("idle", false);
                anim.SetBool("walk", true);
                isGoToLoading = true;
            }
            return true;
        }
        worcer_1.StopGame();
        Wait();
        return false;

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
        if (isGoToBox)
        {
            currentDystance = Vector3.Distance(transform.position, point.position);
            if (currentDystance < stopPoint)
            {
                anim.SetBool("carry", false);
                anim.SetBool("idle", true);
                isGoToBox = false;
                GiveStake();
            }
        }
        if (isGoToLoading)
        {
            currentDystance = Vector3.Distance(transform.position, steakPoint.position);            
            if (currentDystance < stopPoint)
            {
                anim.SetBool("walk", false);
                anim.SetBool("idle", true);
                isGoToLoading = false;
                TakeStake();
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
                if (FindPalette())
                {
                    currentPalette.AddBox();
                    box.SetActive(false);                   
                }                
            }
        }
    }
    private void TakeStake()
    {
        ownStake.SetActive(true);
        stake.SetActive(false);
        getBox = false;
        agent.SetDestination(point.position);
        anim.SetBool("idle", false);
        anim.SetBool("carry", true);
        isGoToBox = true;
    }
    private void GiveStake()
    {
        boxInMashine.TakeStake();
        ownStake.SetActive(false);
    }
    private void Wait()
    {
        isWait = true;
        currentTime = maxTime;
    }
    public void TakeBox()
    {
        box.SetActive(true);
        agent.SetDestination(endPoint.position);
        anim.SetBool("idle", false);
        anim.SetBool("carry", true);
        isGoToShipment = true;
    }
}
