using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PackingWorcer_2 : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    private AppData data;
    private AudioListenerScript audioListener;
    [SerializeField] private PackingMashineWorcer_1 worcer_1;
    [SerializeField] private Transform paletPoint;
    [SerializeField] private GameObject box;
    [SerializeField] private EggsDepositories currentDepository;
    [SerializeField] private BoxInHeadCutMashine boxInMashine;
    [SerializeField] private List<Palette> palettes;
    private float currentDystance;
    private Palette currentPalette;
    public int currentBoxInPalet;
    private bool isGoToLoading;
    private bool isGoToShipment;
    public float stopPoint = 1.0f;
    private bool isWait;
    private float maxTime = 10f;
    public float currentTime;
    public bool isLoading;
    [SerializeField] private float animDelay = 0.5f;
    public bool machine1;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        audioListener = FindObjectOfType<AudioListenerScript>();
        data = audioListener._data;
    }
    public void SetSpeed()
    {
        agent.speed = machine1 ? 3.5f + data.userData.headCutingMashineSpeedLevel / 20f
            : 3.5f + data.userData.headCutingMashine2SpeedLevel / 20f; //изменяем скорость работника от 3.5 до 6
    }
    public void GetBox()
    {
        if (!worcer_1.stop)
        {

            agent.SetDestination(boxInMashine.gameObject.transform.position);
            anim.SetBool("idle", false);
            anim.SetBool("walk", true);
            isGoToLoading = true;
        }
        else
        {
            anim.SetBool("walk", false);
            anim.SetBool("idle", true);
        }

    }
    void Start()
    {
        anim.SetBool("walk", false);
        anim.SetBool("idle", true);
        FindPalette();
    }

    private bool FindPalette()
    {
        foreach (var palet in palettes)
        {
            if (!palet.gameObject.activeInHierarchy)
            {
                currentPalette = palet;
                palet.gameObject.SetActive(true);
                palet.Empty();
                return true;
            }
        }
        Wait();
        return false;
    }

    void Update()
    {
        if (isWait)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                isWait = false;
                if (isLoading) TakeBox();
            }
            return;
        }
        if (isGoToLoading)
        {
            currentDystance = Vector3.Distance(transform.position, boxInMashine.gameObject.transform.position);
            if (currentDystance < stopPoint)
            {
                anim.SetBool("walk", false);
                anim.SetBool("idle", true);
                isGoToLoading = false;
                isLoading = true;
                TakeBox();
            }
        }
        if (isGoToShipment)
        {
            currentDystance = Vector3.Distance(transform.position, paletPoint.position);
            if (currentDystance < stopPoint)
            {
                Invoke(nameof(StopCarryAnim), animDelay);
                isGoToShipment = false;
                if (currentPalette.gameObject.activeInHierarchy && currentPalette.GiveBox())
                {
                    box.SetActive(false);
                    currentBoxInPalet++;
                    if (currentBoxInPalet == currentPalette.boxes.Length)
                    {
                        currentBoxInPalet = 0;
                        FindPalette();
                    }
                    return;
                }
                FindPalette();
                Wait();
            }
        }
    }

    private void StopCarryAnim()
    {
        anim.SetBool("carry", false);
        anim.SetBool("idle", true);
    }
    private void Wait()
    {
        isWait = true;
        currentTime = maxTime;
    }
    private void TakeBox()
    {
        if (currentPalette.isFull)
        {
            if (FindPalette())
            {
                isLoading = false;
                worcer_1.stop = false;
            }
            else
            {
                worcer_1.stop = true;
                Wait();
                return;
            }
        }
        boxInMashine.HideFish();
        box.SetActive(true);
        agent.SetDestination(paletPoint.position);
        anim.SetBool("idle", false);
        anim.SetBool("carry", true);
        isGoToShipment = true;
    }
}
