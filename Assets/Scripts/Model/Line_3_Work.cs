
using TMPro;
using UnityEngine;

public class Line_3_Work : TransportMenuContent
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
            if (data.userData.packingMashine > 0) buyButtom.SetActive(false);
        }
        if (mashine_2)
        {
            if (data.userData.packingMashine > 1) buyButtom.SetActive(false);
        }
        if (!mashine_1 && mashine_2)
        {
            if (data.userData.packingMashine > 2) buyButtom.SetActive(false);
        }
        CheckInfo();
    }

    public void CheckInfo()
    {

        if (mashine_1)
        {
            promice.text = data.userData.steakPackingMashineLevel.ToString();
            speed.text = data.userData.steakPackingMashineSpeed.ToString();
            count.text = data.userData.packingMashine > 0 ? "1" : "0";
            buyCost.text = Converter.instance.ConvertMoneyView(data.upgradesData.steakPackingMashineCost);
            if (data.userData.packingMashine > 0) buyButtom.SetActive(false);
        }
        else if (mashine_2)
        {
            promice.text = data.userData.farshPackingMashineLevel.ToString();
            speed.text = data.userData.farshPackingMashineSpeed.ToString();
            count.text = data.userData.packingMashine > 1 ? "1" : "0";
            buyCost.text = Converter.instance.ConvertMoneyView(data.upgradesData.farshPackingMashineCost);
            if (data.userData.packingMashine > 1) buyButtom.SetActive(false);
        }
        else
        {
            promice.text = data.userData.filePackingMashineLevel.ToString();
            speed.text = data.userData.filePackingMashineSpeed.ToString();
            count.text = data.userData.packingMashine > 2 ? "1" : "0";
            buyCost.text = Converter.instance.ConvertMoneyView(data.upgradesData.filePackingMashineCost);
            if (data.userData.packingMashine > 2) buyButtom.SetActive(false);
        }        

    }
    public void OnImageBtn()
    {
        if (mashine_1)
            upgradesMenu.Packing_1_ZonePopUpShow();
        else if (mashine_2)
            upgradesMenu.Packing_2_ZonePopUpShow();
        else
            upgradesMenu.Packing_3_ZonePopUpShow();
        InterfaceManager.SetCurrentMenu(MenuName.Main);
    }
    
}
