using UnityEngine;

public class EquipmentEnabled : MonoBehaviour
{
    [SerializeField] private AppData data;
    [SerializeField] private GameObject equipment;
    [SerializeField] private int nextZoneOnCount = 25;
    
    public bool headCut_1;
    public bool headCut_2;
    public bool fishClean_1;
    public bool fishClean_2;
    public bool fishClean_3;
    public bool steakMashine;
    public bool fileMashine;
    public bool farshMashine;
    public bool steakPacking;
    public bool farshPacking;
    public bool filePacking;

    private void OnEnable()
    {
        
        if (headCut_1)
        {
            if (data.userData.headCutingMashineLevel >= nextZoneOnCount)
            {
                EnabledNextZone();
            }
        }
        if (fishClean_1)
        {
            if (data.userData.fishCleaningMashineLevel >= nextZoneOnCount)
            {
                EnabledNextZone();
            }
        }
        if (fishClean_2)
        {
            if (data.userData.fishCleaningMashine2Level >= nextZoneOnCount)
            {
                EnabledNextZone();
            }
        }
        if (fishClean_3)
        {
            if (data.userData.fishCleaningMashine3Level >= nextZoneOnCount)
            {
                EnabledNextZone();
            }
        }
        if (headCut_2)
        {
            if (data.userData.headCutingMashine2Level >= nextZoneOnCount)
            {
                EnabledNextZone();
            }
        }
        if (fileMashine)
        {
            if (data.userData.fileMashineLevel >= nextZoneOnCount)
            {
                EnabledNextZone();                
            }
        }
        if (steakMashine)
        {
            if (data.userData.steakMashineLevel >= nextZoneOnCount)
            {
                EnabledNextZone();
            }
        }
        if (farshMashine)
        {
            if (data.userData.farshMashineLevel >= nextZoneOnCount)
            {
                EnabledNextZone();
            }
        }
        if (steakPacking)
        {
            if (data.userData.steakPackingMashineLevel >= nextZoneOnCount)
            {
                EnabledNextZone();
            }
        }
        if (farshPacking)
        {
            if (data.userData.farshPackingMashineLevel >= nextZoneOnCount)
            {
                EnabledNextZone();
            }
        }
        if (filePacking)
        {
            if (data.userData.filePackingMashineLevel >= nextZoneOnCount)
            {
                EnabledNextZone();
            }
        }

    }
    public void EnabledNextZone()
    {
        equipment.SetActive(true);
    }
}
