using UnityEngine;
using TMPro;
public class ZoneInWork : TransportMenuContent
{
    [SerializeField] private GameObject buyButtom;
    [SerializeField] private UpgradesMenu upgradesMenu;    
    [SerializeField] private TextMeshProUGUI promice;
    [SerializeField] private TextMeshProUGUI speed;
    [SerializeField] private TextMeshProUGUI buyCost;
    public bool mashine_1;
    private void OnEnable()
    {
        if (mashine_1)
        {
            if (data.userData.headCutingMashine > 0) buyButtom.SetActive(false);
        }
        if (!mashine_1)
        {
            if (data.userData.headCutingMashine > 1) buyButtom.SetActive(false);
        }
        CheckInfo();
    }

    public void CheckInfo()
    {
        
        if (mashine_1)
        {
            promice.text = data.userData.headCutingMashineLevel.ToString();
            speed.text = data.userData.headCutingMashineSpeed.ToString();
            count.text = "1";
            if (data.userData.headCutingMashine > 0) buyButtom.SetActive(false);
        }
        else
        {
            promice.text = data.userData.headCutingMashine2Level.ToString();
            speed.text = data.userData.headCutingMashine2Speed.ToString();            
            count.text = data.userData.headCutingMashine>1? "1":"0";
            buyCost.text = Converter.instance.ConvertMoneyView(data.upgradesData.headCutingMashineCost);
            if (data.userData.headCutingMashine > 1) buyButtom.SetActive(false);
        }       

    }
    public void OnImageBtn()
    {
        if(mashine_1) 
            upgradesMenu.HeadCut_1_PopUpShow();
        else 
            upgradesMenu.HeadCut_2_PopUpShow();
        InterfaceManager.SetCurrentMenu(MenuName.Main);  
    }    
}
