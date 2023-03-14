using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public enum PromiseType
{
    warehouse,
    none,
    inWar,
    car2,
    warehouse_1,
    warehouse_2,
    warehouse_3,
    headCutingMashine,
    headCutingMashine1,
    fishCleaningMashine,
    fishCleaningMashine2,
    fishCleaningMashine3,
    steakMashine,
    farshMashine,
    fileMashine,
    steakPackingMashine,
    farshPackingMashine,
    filePackingMashine
}
public class MoneyBoom : MonoBehaviour
{
    [SerializeField] private ParticleSystem moneyEffect;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private AppData data;
    [SerializeField] private Slider slider;
    public PromiseType promiseType;
    private double promice;
    private float div;
    private float secInMin = 60f;
    private AudioSource audioSource;
    private AudioListener audioListener;
    private float startSoundVolume = 0.04f;
    public bool play;
    private void Awake()
    {
        audioListener = FindObjectOfType<AudioListener>();
        audioSource = GetComponent<AudioSource>();
    }
    void OnEnable()
    {
        play = false;
        moneyEffect.Play();
        switch (promiseType)
        {
            case PromiseType.headCutingMashine:
                promice = data.userData.headCutingMashinePromice*data.userData.ADMultiplier;
                div = data.userData.headCutingMashineSpeed;
                break;
            case PromiseType.headCutingMashine1:
                promice = data.userData.headCutingMashine2Promice * data.userData.ADMultiplier;
                div = data.userData.headCutingMashine2Speed;
                break;
            case PromiseType.fishCleaningMashine:
                promice = data.userData.fishCleaningMashinePromice * data.userData.ADMultiplier;
                div = data.userData.fishCleaningMashineSpeed;
                break;
            case PromiseType.fishCleaningMashine2:
                promice = data.userData.fishCleaningMashine2Promice * data.userData.ADMultiplier;
                div = data.userData.fishCleaningMashine2Speed;
                break;
            case PromiseType.fishCleaningMashine3:
                promice = data.userData.fishCleaningMashine3Promice * data.userData.ADMultiplier;
                div = data.userData.fishCleaningMashine3Speed;
                break;
            case PromiseType.steakMashine:
                promice = data.userData.steakMashinePromice * data.userData.ADMultiplier;
                div = data.userData.steakMashineSpeed;
                break;
            case PromiseType.farshMashine:
                promice = data.userData.farshMashinePromice * data.userData.ADMultiplier;
                div = data.userData.farshMashineSpeed;
                break;
            case PromiseType.fileMashine:
                promice = data.userData.fileMashinePromice * data.userData.ADMultiplier;
                div = data.userData.fileMashineSpeed;
                break;
            case PromiseType.steakPackingMashine:
                promice = data.userData.steakPackingMashinePromice * data.userData.ADMultiplier;
                div = data.userData.steakPackingMashineSpeed;
                break;
            case PromiseType.farshPackingMashine:
                promice = data.userData.farshPackingMashinePromice * data.userData.ADMultiplier;
                div = data.userData.farshPackingMashineSpeed;
                break;
            case PromiseType.filePackingMashine:
                promice = data.userData.filePackingMashinePromice * data.userData.ADMultiplier;
                div = data.userData.filePackingMashineSpeed;
                break;
        }
        var _perMinut = secInMin / div;
        double result = (double)Math.Round(promice/_perMinut, 1);
        moneyText.text = Converter.instance.ConvertMoneyView(result);
        data.userData.coins.Value += result;
        //GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Money", (float)result, "Income", "GamePlay_Income");
        SaveDataSystem.Instance.SaveMoney();
        ConvertMoneyView();
        Invoke(nameof(ResetGo), 3f);
    }

    private void ResetGo()
    {
        gameObject.SetActive(false);
    }   

    private void ConvertMoneyView()
    {
        var score = data.userData.coins.Value;
        if (score >= 1000000000000000000000d) data.userData._coins.Value = (score / 1000000000000000000000d).ToString("F1") + "B";
        else if (score >= 1000000000000000000d) data.userData._coins.Value = (score / 1000000000000000000d).ToString("F1") + "M";
        else if (score >= 1000000000000000d) data.userData._coins.Value = (score / 1000000000000000d).ToString("F1") + "K";
        else if (score >= 1000000000000d) data.userData._coins.Value = (score / 1000000000000d).ToString("F1") + "t";
        else if (score > 1000000000d) data.userData._coins.Value = (score / 1000000000d).ToString("F1") + "b";
        else if (score > 1000000d) data.userData._coins.Value = (score / 1000000d).ToString("F1") + "m";
        else if (score > 1000d) data.userData._coins.Value = (score / 1000d).ToString("F1") + "k";
        else data.userData._coins.Value = score.ToString("F1");
    }
    void Update()
    {
        var currentDystance = Vector3.Distance(transform.position, audioListener.transform.position);
        //Debug.Log("CurrentDystance = "+currentDystance);
        if (currentDystance < 25 && !play)
        {
            audioSource.volume = (startSoundVolume - (currentDystance / 1000f))*slider.value;
            if(PlayerPrefs.GetInt("Sound", 0) == 1) audioSource.Play();
            play = true;
        }
    }
}
