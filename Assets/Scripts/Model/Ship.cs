using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private Transform ship_0;
    [SerializeField] private Transform ship_1;
    [SerializeField] private float timeToUp = 1f;
    [SerializeField] private float timeToDown = 3f;
    [SerializeField] private Transform paletParent;
    [SerializeField] private Transform oldPaletParent;
    [SerializeField] private CarPort carPort;
    private Vector3 startPos;
    private Quaternion startRot;
    private bool isMoveToPort;    
    public bool isShipment;    
    private float portX = 11f;
    private float portOutX = 130f;
    [SerializeField] private float speed = 3f;    
    [SerializeField] private EggsDepositories currentDepository;
    private float currentDystance;    
    [SerializeField] public GameObject[] boxes;
    [SerializeField] private AppData data;
    [SerializeField] private float startSpeed = 7;
    
    public int currentBox = 0;
    private float maxTime = 1f;
    public float currentTime;
    public bool isWait;
    public bool isGoToLoading;
    
    private int capasity;
    private Animator anim;
    
    private Vector3 shipStartPos;
    public bool ship1;
    private float criticalDystance = 20f;
    private bool pause;
    private float timeToPause = 5f;
    private void Awake()
    {
        anim = GetComponent<Animator>();        
    }
    void OnEnable()
    {
        shipStartPos = transform.position;
        isMoveToPort = true;        
    }
    private void Start()
    {
        TakeBox();
        Invoke(nameof(ChangeSpeed), 5f);
    }

    private void GiveBox()
    {
        if (currentDepository.AddBox())
        {
            anim.SetBool("ship", true);
            Invoke(nameof(BoxUp), timeToUp);
        }
        else
        {
            Wait();
        }
        
    }
    private void BoxUp()
    {
        startPos = boxes[currentBox].transform.localPosition;
        startRot = boxes[currentBox].transform.localRotation;
        boxes[currentBox].transform.SetParent(paletParent);
        boxes[currentBox].transform.localPosition = new Vector3(-2.15f,1.87f,0f);        
        Invoke(nameof(BoxDown), timeToDown);
    }

    private void BoxDown()
    {
        boxes[currentBox].transform.SetParent(oldPaletParent);
        boxes[currentBox].SetActive(false);
        currentDepository.BoxShow();
        boxes[currentBox].transform.localPosition = startPos;
        boxes[currentBox].transform.localRotation = startRot;
        anim.SetBool("ship", false);
        
        GivenBoxProcess();
    }

    private void GivenBoxProcess()
    {
        currentBox++;
        if (currentBox == capasity)
        {
            MoveToLoading();
            return;
        }
        Wait();
    }
    private void Wait()
    {
        isWait = true;
        currentTime = maxTime;                
    }
    private void MoveToLoading()
    {
        isShipment = false;        
        isGoToLoading = true;
    }
    void FixedUpdate()
    {
        if (isMoveToPort&&!isWait)
        {
            MoveToPort();
        }
        if (isWait)
        {
            currentTime -= Time.fixedDeltaTime;
            if (currentTime <= 0)
            {
                isWait = false;                
                if (isShipment) GiveBox();
            }
        }
        if (isGoToLoading)
        {
            MoveToSea();
        }
        if (ship1&&!pause)
        {
            if (Vector3.Distance(transform.localPosition, ship_0.localPosition) < criticalDystance&&
                transform.localPosition.x < ship_0.localPosition.x)
            {
                pause = true;
                Invoke(nameof(ResetPause), timeToPause);
            }
        }
        if (!ship1 && !pause&& ship_1.gameObject.activeInHierarchy)
        {
            if (Vector3.Distance(transform.localPosition, ship_1.localPosition) < criticalDystance&&
                transform.localPosition.x < ship_1.localPosition.x)
            {
                pause = true;
                Invoke(nameof(ResetPause), timeToPause);
            }
        }
    }
    private void ResetPause()
    {
        pause = false;
    }

    private void TakeBox()
    {
        capasity = data.userData.shipCapasity;
        if(capasity > boxes.Length)
        {
            capasity = boxes.Length;
        }
        for (int i = 0; i < capasity; i++)
        {
            boxes[i].SetActive(true);
        }              
    }

    private void MoveToPort()
    {
        if (!pause)
        {
            if (transform.localPosition.x < portX)
            {
                var pos = transform.position;
                pos.x += speed * Time.fixedDeltaTime;
                transform.position = pos;
            }
            else
            {
                isMoveToPort = false;
                isShipment = true;
                GiveBox();
            }
        }        
        
    }
    private void MoveToSea()
    {
        if (!pause)
        {
            if (transform.localPosition.x < portOutX)
            {
                var pos = transform.position;
                pos.x += speed * Time.fixedDeltaTime;
                transform.position = pos;
            }
            else
            {
                isGoToLoading = false;
                transform.position = shipStartPos;
                Wait();
                isMoveToPort = true;
                TakeBox();
                currentBox = 0;
            }
        }        
        
    }
    public void ChangeSpeed()
    {
        speed = startSpeed + (data.userData.shipLevel * Constant.speedIncreeseKoeff);
    }
}
