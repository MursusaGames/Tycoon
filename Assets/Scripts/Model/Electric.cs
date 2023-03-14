using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Electric : MonoBehaviour
{
    [SerializeField] private List<Transform> points;
    [SerializeField] private List<string> animations;
    [SerializeField] private Animator mashineAnim;
    [SerializeField] private GameObject fog;
    [SerializeField] private GameObject fire;

    private string walk = "walk";
    private NavMeshAgent agent;
    private Animator anim;
    public bool inGame;
    private int rand;
    private int minTimeWait = 5;
    private int maxTimeWait = 10;
    private bool point1;
    private bool inPoint;
    private bool goInPoint;
    private int randomPoint;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>(); 
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        mashineAnim.enabled = false;
        inGame = true;
        StartCoroutine(nameof(RemontZone));
    }
    private int GetRandomPoint()
    {
        rand = Random.Range(0,points.Count);
        return rand;
    }
    private int GetRandomTime()
    {
        rand = Random.Range(minTimeWait, maxTimeWait);
        return rand;
    }
    private int GetRandomAnimation()
    {
        rand = Random.Range(0, animations.Count);
        return rand;
    }

    private IEnumerator RemontZone()
    {
        while (inGame)
        {
            randomPoint = GetRandomPoint();
            if (randomPoint == 0) point1 = true;            
            foreach(var animation in animations)
            {
                anim.SetBool(animation, false);
            }
            anim.SetBool(walk, true);
            agent.SetDestination(points[randomPoint].position);
            goInPoint = true;   
            yield return new WaitUntil(predicate: () => inPoint);
            if (point1)
            {
                anim.SetBool(walk, false);
                SetFire();
                anim.SetBool(animations[4], true);                
                point1 = false;
            }
            else
            {
                anim.SetBool(walk, false);
                anim.SetBool(animations[GetRandomAnimation()], true);
            }
            inPoint = false;    
            yield return new WaitForSeconds(GetRandomTime());

        }
        

    }

    private void Update()
    {
        if (goInPoint)
        {
            var distance = Vector3.Distance(transform.position, points[randomPoint].position);
            if(distance < 0.5f)
            {
                goInPoint=false;
                inPoint = true;
            }
        }
    }
    private void SetFire()
    {
        mashineAnim.enabled = true;
        fog.SetActive(true);
        fire.SetActive(true);
        Invoke(nameof(ResetFire), 7f);
    }
    private void ResetFire()
    {
        mashineAnim.enabled = false;
        fog.SetActive(false);
        fire.SetActive(false);

    }
}
