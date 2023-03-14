using UnityEngine;
using UnityEngine.AI;

public class Chicken : MonoBehaviour
{
    public Trough[] troughs = new Trough[5];
    private Trough currentTrough;
    public ChickenHoum[] chickenHoums = new ChickenHoum[5];    
    private ChickenHoum currentChickenHoum;
    public Nest[] nests = new Nest[14];
    private Animator anim;
    private float zMin = -5f;
    private float zMax = 10f;
    private float xMin = 0f;
    private float xMax = 14f;
    [SerializeField] private GameObject transformEmpty;
    [SerializeField] private Transform parent;
    private NavMeshAgent agent;
    public bool isGo;
    public bool isGoToTrough;
    public bool isGoToChickenHoum;
    public bool isGoToNest;
    public bool isStay;
    public bool isFull;
    private float timer;
    public GameObject target;
    private float distanceToTrough;
    private float distanceToChickenHoum;
    private Nest currentNest;

    private void Awake()
    {
        nests = FindObjectsOfType<Nest>();
        chickenHoums = FindObjectsOfType<ChickenHoum>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        troughs = FindObjectsOfType<Trough>();
    }
    void Start()
    {
        FindTrough();
        FindChickenHoum();
        target = Instantiate(transformEmpty, parent);
        Go();
    }
    private void FindChickenHoum()
    {
        float dystance = float.MaxValue;
        for (int i = 0; i < chickenHoums.Length; i++)
        {
            distanceToChickenHoum = Vector3.Distance(transform.position, chickenHoums[i].gameObject.transform.position);
            if (distanceToChickenHoum < dystance)
            {
                dystance = distanceToChickenHoum;
                currentChickenHoum = chickenHoums[i];
            }
        }
    }
    private void FindTrough()
    {
        float dystance = float.MaxValue;
        for (int i = 0; i < troughs.Length; i++)
        {
            distanceToTrough = Vector3.Distance(transform.position, troughs[i].gameObject.transform.position);
            if (distanceToTrough < dystance)
            {
                dystance = distanceToTrough;
                currentTrough = troughs[i];
            }
        }
    }
    private void Go()
    {
        if (CheckTrough()) return;
        target.transform.localPosition = new Vector3(Random.Range(xMin, xMax), -0.7f, Random.Range(zMin, zMax));        
        transform.LookAt(target.transform);
        agent.SetDestination(target.transform.position);
        timer = Random.Range(3, 7);        
        anim.SetBool("Idle", false);
        anim.SetBool("Walk", true);
        isGo = true;
    }

    private bool CheckTrough()
    {
        if (currentTrough.isFood)
        {
            agent.SetDestination(currentTrough.gameObject.transform.position);
            anim.SetBool("Idle", false);
            anim.SetBool("Walk", true);
            isGoToTrough = true;
            timer = Random.Range(3, 7);
            return true;
        }
        return false;
    }
    private void Stay()
    {
        timer = Random.Range(3, 5);
        anim.SetBool("Walk", false);
        anim.SetBool("Idle", true);        
        isStay = true;
    }

    private void GoToChickenHoum()
    {
        agent.SetDestination(currentChickenHoum.gameObject.transform.position);
        anim.SetBool("Idle", false);
        anim.SetBool("Walk", true);
        isGoToChickenHoum = true;
        timer = Random.Range(3, 7);
    }

    private int GetFreeRandomNestIndex()
    {
        for(int i =0; i < nests.Length; i++)
        {
            if (!nests[i].isFull)
            {
                return i;
            }
         
        }
        return 15;
    }
    private void GiveEgg()
    {
        int index = GetFreeRandomNestIndex();
        if (index == 15) Go();
        else
        {
            currentNest = nests[index];
            currentNest.isFull = true;
            agent.SetDestination(nests[index].transform.position);
            isGoToNest = true;
        }
    }

    private void MakeEgg()
    {
        currentNest.egg.SetActive(true);
    }
    void FixedUpdate()
    {
        if (isGoToNest)
        {
            var currentDystance = Vector3.Distance(transform.position, currentNest.gameObject.transform.position);
            timer -= 0.02f;
            if (currentDystance < 1.5f)
            {
                isFull = false;
                isGoToNest = false;
                Invoke(nameof(MakeEgg), 3f);
                Stay();
            }
            if(timer <= 0)
            {
                isFull = false;
                isGoToNest = false;
                Stay();
            }
        }
        if (isGoToTrough)
        {
            var currentDystance = Vector3.Distance(transform.position, currentTrough.gameObject.transform.position);            
            if (currentDystance < 2.5)
            {
                isFull = true;
                isGoToTrough = false;
                Stay();
            }
        }
        if (isGoToChickenHoum)
        {
            var currentDystance = Vector3.Distance(transform.position, currentChickenHoum.gameObject.transform.position);            
            if (currentDystance < 2)
            {
                isFull =false;
                isGoToChickenHoum = false;
                GiveEgg();
            }
        }
        if (isGo)
        {
            var currentDystance = Vector3.Distance(transform.localPosition, target.transform.localPosition);            
            timer -= Time.deltaTime;
            if (currentDystance < 2 || timer<=0)
            {
                isGo = false;
                Stay();
            }
        }
        if (isStay)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                isStay = false;
                if (isFull) GoToChickenHoum();
                else Go();
            }
        }        
    }
}
