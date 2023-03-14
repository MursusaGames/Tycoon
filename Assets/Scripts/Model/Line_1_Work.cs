using UnityEngine;
using TMPro;
public class Line_1_Work : TransportMenuContent
{
    [SerializeField] private GameObject buyButtom;
    [SerializeField] private UpgradesMenu upgradesMenu;    
    [SerializeField] private TextMeshProUGUI promice;
    [SerializeField] private TextMeshProUGUI speed;
    [SerializeField] private TextMeshProUGUI buyCost;

    public bool mashine_1;
    public bool mashine_2;
    private void OnEnable()
    {
        if (mashine_1)
        {
            if(data.userData.fishCleaningMashine>0) buyButtom.SetActive(false);   
        }
        if (mashine_2)
        {
            if (data.userData.fishCleaningMashine > 1) buyButtom.SetActive(false);
        }
        if (!mashine_1 && mashine_2)
        {
            if (data.userData.fishCleaningMashine > 2) buyButtom.SetActive(false);
        }
        CheckInfo();

    }

    public void CheckInfo()
    {
        
        if (mashine_1)
        {
            promice.text = data.userData.fishCleaningMashineLevel.ToString();
            speed.text = data.userData.fishCleaningMashineSpeed.ToString();
            count.text = data.userData.fishCleaningMashine>0?"1":"0";
            buyCost.text = Converter.instance.ConvertMoneyView(data.upgradesData.fishCleaning1MashineCost);
            if (data.userData.fishCleaningMashine > 0) buyButtom.SetActive(false);
        }
        else if(mashine_2)
        {
            promice.text = data.userData.fishCleaningMashine2Level.ToString();
            speed.text = data.userData.fishCleaningMashine2Speed.ToString();
            count.text = data.userData.fishCleaningMashine > 1 ? "1" : "0";
            buyCost.text = Converter.instance.ConvertMoneyView(data.upgradesData.fishCleaning2MashineCost);
            if (data.userData.fishCleaningMashine > 1) buyButtom.SetActive(false);
        }
        else
        {
            promice.text = data.userData.fishCleaningMashine3Level.ToString();
            speed.text = data.userData.fishCleaningMashine3Speed.ToString();
            count.text = data.userData.fishCleaningMashine > 2 ? "1" : "0";
            buyCost.text = Converter.instance.ConvertMoneyView(data.upgradesData.fishCleaning3MashineCost);
            if (data.userData.fishCleaningMashine > 2) buyButtom.SetActive(false);
        }

        

    }
    public void OnImageBtn()
    {
        if (mashine_1)
            upgradesMenu.FishCleaning_1_PopUpShow();
        else if (mashine_2)
            upgradesMenu.FishCleaning_2_PopUpShow();
        else
            upgradesMenu.FishCleaning_3_PopUpShow();
        InterfaceManager.SetCurrentMenu(MenuName.Main);
    }    
}
