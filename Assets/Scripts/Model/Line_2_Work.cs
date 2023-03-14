
using TMPro;
using UnityEngine;

public class Line_2_Work : TransportMenuContent
{
    [SerializeField] private GameObject buyButtom;
    [SerializeField] private UpgradesMenu upgradesMenu;    
    [SerializeField] private TextMeshProUGUI promice;
    [SerializeField] private TextMeshProUGUI speed;
    [SerializeField] private TextMeshProUGUI upgradeCost;
    public bool mashine_1;
    public bool mashine_2;
    private void OnEnable()
    {
        if (mashine_1)
        {
            if (data.userData.steakMashine > 0) buyButtom.SetActive(false);
        }
        if (mashine_2)
        {
            if (data.userData.farshMashine > 0) buyButtom.SetActive(false);
        }
        if (!mashine_1 && mashine_2)
        {
            if (data.userData.fileMashine > 0) buyButtom.SetActive(false);
        }
        CheckInfo();
    }

    public void CheckInfo()
    {

        if (mashine_1)
        {
            promice.text = data.userData.steakMashineLevel.ToString();   
            speed.text = data.userData.steakMashineSpeed.ToString();
            count.text = data.userData.steakMashine > 0 ? "1" : "0";
            upgradeCost.text = Converter.instance.ConvertMoneyView(data.upgradesData.steakMashineCost);
            if (data.userData.steakMashine > 0) buyButtom.SetActive(false);
        }
        else if (mashine_2)
        {
            promice.text = data.userData.farshMashineLevel.ToString();
            speed.text = data.userData.farshMashineSpeed.ToString();
            count.text = data.userData.farshMashine > 0 ? "1" : "0";
            upgradeCost.text = Converter.instance.ConvertMoneyView(data.upgradesData.farshMashineCost);
            if (data.userData.farshMashine > 0) buyButtom.SetActive(false);
        }
        else
        {
            promice.text = data.userData.fileMashineLevel.ToString();
            speed.text = data.userData.fileMashineSpeed.ToString();
            count.text = data.userData.fileMashine > 0 ? "1" : "0";
            upgradeCost.text = Converter.instance.ConvertMoneyView(data.upgradesData.fileMashineCost);
            if (data.userData.fileMashine > 0) buyButtom.SetActive(false);
        }
        

    }
    public void OnImageBtn()
    {
        if (mashine_1)
            upgradesMenu.SteakZonePopUpShow();
        else if (mashine_2)
            upgradesMenu.FarshZonePopUpShow();
        else
            upgradesMenu.FileZonePopUpShow();
        InterfaceManager.SetCurrentMenu(MenuName.Main);
    }
    
}
