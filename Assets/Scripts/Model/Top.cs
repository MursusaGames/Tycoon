using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private GameObject greenLamp;
    [SerializeField] private GameObject redLamp;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OpenTop()
    {
        greenLamp.SetActive(true);
        redLamp.SetActive(false);
        anim.SetBool("close", false);
        anim.SetBool("open", true);        
    }
    public void CloseTop()
    {
        anim.SetBool("open", false);
        anim.SetBool("close", true);
        greenLamp.SetActive(false);
        redLamp.SetActive(true);
        Invoke(nameof(ResetLamp), 4f);
    }
    private void ResetLamp()
    {
        redLamp.SetActive(false);
    }
}
