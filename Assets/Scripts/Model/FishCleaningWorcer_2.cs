using UnityEngine;
using UnityEngine.AI;

public class FishCleaningWorcer_2 : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    private AppData data;
    private AudioListenerScript audioListener;
    [SerializeField] private CleanMashineWorcer worcer_1;
    [SerializeField] private Transform paletPoint;
    [SerializeField] private GameObject box;    
    [SerializeField] private BoxInHeadCutMashine boxInMashine;
    [SerializeField] private Stock stock;
    private float currentDystance;
    public Stock currentStock;    
    public bool isGoToLoading;
    public bool isGoToShipment;
    public float stopPoint = 1.0f;
    public bool isWait;
    private float maxTime = 10f;
    public float currentTime;
    public bool isLoading;
    [SerializeField] private float animDelay = 0.5f;
    public bool mashine_1;
    public bool mashine_2;
    public bool isFinding;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        audioListener = FindObjectOfType<AudioListenerScript>();
    }
    public void CheckSpeed()
    {
        var additivSpeed = mashine_1 ? data.userData.fishCleaningMashineSpeedLevel : mashine_2 ? data.userData.fishCleaningMashine2SpeedLevel :
            data.userData.fishCleaningMashine3SpeedLevel;
        agent.speed = Constant.baseSpeed + (additivSpeed * Constant.speedDecreeseKoeff);

    }
    public void GetBox()
    {
        if (!FindPalette())
        {
            worcer_1.StopPlay();
            return;
        }
        agent.SetDestination(boxInMashine.gameObject.transform.position);
        anim.SetBool("idle", false);
        anim.SetBool("walk", true);
        isGoToLoading = true;
    }
    void Start()
    {
        data = audioListener._data;
        anim.SetBool("walk", false);
        anim.SetBool("idle", true);
        FindPalette();
        CheckSpeed();
    }

    private bool FindPalette()
    {
        isFinding = true;
        if (stock.currentBox<8)
        {
            currentStock = stock;            
            isFinding = false;
            worcer_1.PlayAgain();
            return true;
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
                if (isFinding) FindPalette();
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
                if (currentStock.currentBox<8)
                {
                    currentStock.AddBox();
                    box.SetActive(false);                    
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
        if (currentStock.currentBox>7)
        {
            if (FindPalette())
            {
                isLoading = false;
                worcer_1.PlayAgain();
            }
            else
            {
                worcer_1.StopPlay();
                StopCarryAnim();
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
