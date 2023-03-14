using System;
using UnityEngine;

public class TimerSystem : MonoBehaviour
{
    DateTime expiryTimeOffline;    
    [SerializeField] AppData data;
    private string _offline = "offline";    

    private void OnEnable()
    {
        ReadTimestamp(_offline);        
    }
    private void Start()
    {
        if(!PlayerPrefs.HasKey(_offline)) data.userData.timerOff= false;
        Invoke(nameof(CheckTime), 3f);
    }
    private void CheckTime()
    {
        if (DateTime.Now > expiryTimeOffline)
        {
            data.userData.timerOff = true;
        }
    }
    public void OnQuit()
    {
        data.userData.timerOff = false;
        expiryTimeOffline = DateTime.Now.AddHours(data.userData.offlineLimit);
        PlayerPrefs.SetString(_offline, expiryTimeOffline.ToBinary().ToString());        
    }
    
    private bool ReadTimestamp(string key)
    {
        long tmp = Convert.ToInt64(PlayerPrefs.GetString(key, "0"));
        if (tmp == 0)
        {
            return false;
        }
        if(key == _offline)
        {
            expiryTimeOffline = DateTime.FromBinary(tmp);            
        } 
            
        
        return true;
    }    
    public int GetTimeOffline()
    {
        TimeSpan countdown = expiryTimeOffline - DateTime.Now;
        return (countdown.Hours*60 + countdown.Minutes);
    } 
    
    public bool GetTimerFreeDiamonds()
    {
        return true;
    }
}
