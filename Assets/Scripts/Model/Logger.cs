using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Logger : WorcerEntity
{
    [SerializeField] private float startEfficiency = 15f;
    private GameObject audioListener;
    private EggsDepositories[] depositories = new EggsDepositories[5];
    private ForestControlSystem forestControl;
    public Forest target;
    private EggsDepositories currentDepository;
    private Animator anim;
    [SerializeField] private Image image;
    private AudioSource audioSource;
    private NavMeshAgent agent;
    private float currentDystance;
    private float stopPoint = 2f;
    private float timer;
    [SerializeField] private float startSoundVolume = 0.04f;
    public bool isGo;
    public bool isGoal;
    private void Awake()
    {
        forestControl = FindObjectOfType<ForestControlSystem>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        audioListener = FindObjectOfType<AudioListenerScript>().gameObject;
        depositories = FindObjectsOfType<EggsDepositories>();
    }
    private void Start()
    {
        _efficiency = startEfficiency - _level;
        timer = _efficiency;
    }
    public override void Move()
    {
        agent.SetDestination(target.transform.position);
        anim.SetTrigger("Walk");                
    }
    public void FindTarget()
    {
        float dystance = float.MaxValue;        
        for (int i = 0; i < forestControl.woods.Count; i++)
        {
            if (forestControl.woods[i].isBusy) continue;
            currentDystance = Vector3.Distance(transform.position, forestControl.woods[i].gameObject.transform.position);
            if ( currentDystance < dystance)
            {
                dystance = currentDystance;
                target = forestControl.woods[i];
            }
        }
        if (target.isBusy)
        {
            anim.SetTrigger("Idle");
            return;
        }
        target.isBusy = true;
        isGo = true;
        Move();
    }
    private void FixedUpdate()
    {
        if (isGo)
        {
            currentDystance = Vector3.Distance(transform.position, target.transform.position);            
            if(currentDystance < stopPoint)
            {
                isGo = false;
                isGoal = true;
                anim.SetTrigger("Hack");
            }
        }
        if (isGoal)
        {
            currentDystance = Vector3.Distance(transform.position, audioListener.transform.position);            
            audioSource.volume = startSoundVolume - (currentDystance / 1000f);
            timer -=  0.02f;
            image.fillAmount = timer/_efficiency;
            if(timer<= 0)
            {
                isGoal = false;
                timer = _efficiency;
                AddCilindr();
                target.IsStump = true;
                FindTarget();
            }
        }
    }

    private void AddCilindr()
    {
        float dystance = float.MaxValue;
        for (int i = 0; i < depositories.Length; i++)
        {
            currentDystance = Vector3.Distance(transform.position, depositories[i].gameObject.transform.position);
            if (currentDystance < dystance)
            {
                dystance = currentDystance;
                currentDepository = depositories[i];
            }
        }
        currentDepository.AddBox();
    }
    public void PlaySound()
    {
        audioSource.Play();
    }
}
