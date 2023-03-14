using System;
using UnityEngine;
using System.Collections;

public class TimeCountSystem : MonoBehaviour
{
    private DateTime expiryTime;
    private DateTime expiryTimeC;
    private DateTime expiryTimeG;
    [SerializeField] private TimerSystem timerSystem;
    [SerializeField] private AppData data;
    [SerializeField] private AdjustEventsSystem adjustEvents;
    private string keyT = "_Timer_";
    private string keyC = "_Crystal_";
    private string keyG = "_TimeinGame_";
    private int secondInMinute = 60;

    private void OnEnable()
    {
        ReadTimestamp(keyT);
        ReadTimestampC(keyC);
        StartCoroutine(nameof(TimeOffPlayerInGame));
    }
    private void OnDisable()
    {
        
        StopCoroutine(nameof(TimeOffPlayerInGame));
    }

    private IEnumerator TimeOffPlayerInGame()
    {
        while (true)
        {
            yield return new WaitForSeconds(secondInMinute);
            data.userData.allPlayTimeInMinutes++;
            SaveDataSystem.Instance.SavePlayerTime();
            adjustEvents.AdustEventForTime();
            timerSystem.OnQuit();
        }
    }
    private void Start()
    {
        if (!PlayerPrefs.HasKey(keyT)) data.matchData.isTime = true;
        if (!PlayerPrefs.HasKey(keyC)) data.matchData.isCrystal = true;
        if (!PlayerPrefs.HasKey(keyG)) ScheduleTimerG();
    }
    public void StartTimer()
    {
        data.matchData.isTime = false;
        this.ScheduleTimer();
    }
    public void StartTimerC()
    {
        data.matchData.isCrystal = false;
        this.ScheduleTimerC();
    }
    void Update()
    {
        if (data.matchData.isTime != true)
        {
            if (DateTime.Now > expiryTime)
            {
                data.matchData.isTime = true;
                this.ScheduleTimer();
            }
        }
        if (data.matchData.isCrystal != true)
        {
            if (DateTime.Now > expiryTimeC)
            {
                data.matchData.isCrystal = true;
                this.ScheduleTimerC();
            }
        }

    }
    void ScheduleTimerG()
    {
        expiryTimeG = DateTime.Now.AddHours(0.0);
        this.WriteTimestamp(keyT);
    }
    void ScheduleTimer()
    {
        expiryTime = DateTime.Now.AddHours(24.0);
        this.WriteTimestamp(keyT);
    }
    void ScheduleTimerC()
    {
        expiryTimeC = DateTime.Now.AddHours(5.0);
        this.WriteTimestampC(keyC);
    }
    private bool ReadTimestamp(string key)
    {
        long tmp = Convert.ToInt64(PlayerPrefs.GetString(key, "0"));
        if (tmp == 0)
        {
            return false;
        }
        expiryTime = DateTime.FromBinary(tmp);
        return true;
    }
    private bool ReadTimestampC(string key)
    {
        long tmp = Convert.ToInt64(PlayerPrefs.GetString(key, "0"));
        if (tmp == 0)
        {
            return false;
        }
        expiryTimeC = DateTime.FromBinary(tmp);
        return true;
    }
    private bool ReadTimestampG(string key)
    {
        long tmp = Convert.ToInt64(PlayerPrefs.GetString(key, "0"));
        if (tmp == 0)
        {
            return false;
        }
        expiryTimeG = DateTime.FromBinary(tmp);
        return true;
    }

    private void WriteTimestamp(string key)
    {
        PlayerPrefs.SetString(key, expiryTime.ToBinary().ToString());
    }
    private void WriteTimestampC(string key)
    {
        PlayerPrefs.SetString(key, expiryTimeC.ToBinary().ToString());
    }
    private void WriteTimestampG(string key)
    {
        PlayerPrefs.SetString(key, expiryTimeG.ToBinary().ToString());
    }
    public string GetTime()
    {
        TimeSpan countdown = expiryTime - DateTime.Now;
        return countdown.Hours.ToString("00") + ":" + countdown.Minutes.ToString("00")
                            + ":" + countdown.Seconds.ToString("00");
    }
    public string GetTimeC()
    {
        TimeSpan countdown = expiryTimeC - DateTime.Now;
        return countdown.Hours.ToString("00") + ":" + countdown.Minutes.ToString("00")
                            + ":" + countdown.Seconds.ToString("00");
    }
    public int GetTimeG()
    {
        ReadTimestampG(keyG);
        TimeSpan countdown = DateTime.Now - expiryTimeG;
        return countdown.Days;
    }
}
