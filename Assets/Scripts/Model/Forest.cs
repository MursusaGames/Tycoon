using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private GameObject listener;
    private AudioSource audioSource;
    private float startSoundVolume = 0.04f;
    public bool isBusy;
    private bool isStump;
    public bool isTree;
    public int id;
    public bool IsStump
    {
        get => isStump;
        set
        {
            isStump = value;
            if (isStump) meshRenderer.enabled = false;
            else meshRenderer.enabled = true;
            audioSource.Play();
        }
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        listener = FindObjectOfType<AudioListenerScript>().gameObject;
    }
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    
    void FixedUpdate()
    {
        var currentDystance = Vector3.Distance(transform.position, listener.transform.position);
        if(currentDystance < 10)
        {
            audioSource.volume = startSoundVolume - (currentDystance / 1000f);
            audioSource.Play();
        }
        
    }
}
