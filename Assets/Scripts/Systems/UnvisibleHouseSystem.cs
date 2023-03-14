using UnityEngine;
using System.Collections.Generic;

public class UnvisibleHouseSystem : MonoBehaviour
{
    [SerializeField] private AudioListenerScript audioListener;
    [SerializeField] private float startSoundVolume = 0.04f;
    [SerializeField] private List<MeshRenderer> houseMaterials;
    [SerializeField] private Material houseTransparentMaterial;
    [SerializeField] private Material houseNormalMaterial;
    private float volumeTreshold = 0.01f;
    [SerializeField] private float unvisibleBoard = 15f;   
    private float distanceToCamera;
    private AudioSource audioSource;
    public List<bool> isTransparents;
    public bool isPlay;
    private void Awake()
    {
       
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            isTransparents.Add(false);
        }        
    }

    private void FixedUpdate()
    {
        for(int i = 0; i < houseMaterials.Count; i++)
        {
            CheckDistance(i);
        }
    }
    private void CheckDistance(int index)
    {
        distanceToCamera = Vector3.Distance(houseMaterials[index].gameObject.transform.position, audioListener.gameObject.transform.position);        
        audioSource.volume = startSoundVolume - (distanceToCamera / 1000f);

        if (audioSource.volume < volumeTreshold && isPlay)
        {
            audioSource.Stop();
            isPlay = false;
        }

        if (audioSource.volume >= volumeTreshold && !isPlay)
        {
            audioSource.Play();
            isPlay = true;
        }

        if (distanceToCamera < unvisibleBoard && !isTransparents[index])
        {
            houseMaterials[index].material = houseTransparentMaterial;
            isTransparents[index] = true;
        }
        if (distanceToCamera > unvisibleBoard && isTransparents[index])
        {
            houseMaterials[index].material = houseNormalMaterial;
            isTransparents[index] = false;
        }
    }
}
    
