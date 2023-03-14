using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class FishCleaningMashineBuyPopUp : MonoBehaviour
{
    [SerializeField] private TMP_Text buyCost;
    [SerializeField] private AppData data;
    [SerializeField] private FueSystem fueSystem;
    public bool steak;
    public bool farsh;
    public bool file;
    public bool steakPacking;
    public bool farshPacking;
    public bool filePacking;
    public bool mashine_1;
    public bool mashine_2;
    public bool mashine_3;
    private void OnEnable()
    {
        if (steak)
        {
            var result = Converter.instance.ConvertMoneyView(data.upgradesData.steakMashineCost);
            buyCost.text = result.ToString();
            return;
        }
        if (farsh)
        {
            var result = Converter.instance.ConvertMoneyView(data.upgradesData.farshMashineCost);
            buyCost.text = result.ToString();
            return;
        }
        if (file)
        {
            var result = Converter.instance.ConvertMoneyView(data.upgradesData.fileMashineCost);
            buyCost.text = result.ToString();
            return;
        }
        if (steakPacking)
        {
            var result = Converter.instance.ConvertMoneyView(data.upgradesData.steakPackingMashineCost);
            buyCost.text = result.ToString();            
            return;
        }
        if (farshPacking)
        {
            var result = Converter.instance.ConvertMoneyView(data.upgradesData.farshPackingMashineCost);
            buyCost.text = result.ToString();
            return;
        }
        if (filePacking)
        {
            var result = Converter.instance.ConvertMoneyView(data.upgradesData.filePackingMashineCost);
            buyCost.text = result.ToString();
            return;
        }
        if (mashine_1)
        {
            if (data.matchData.isFue)
            {
                fueSystem.ThretenStepBtn();
            }
            var result = Converter.instance.ConvertMoneyView(data.upgradesData.fishCleaning1MashineCost);
            buyCost.text = result.ToString();            
            return;
        }
        if (mashine_2)
        {
            var result = Converter.instance.ConvertMoneyView(data.upgradesData.fishCleaning2MashineCost);
            buyCost.text = result.ToString();            
            return;
        }
        if (mashine_3)
        {
            var result = Converter.instance.ConvertMoneyView(data.upgradesData.fishCleaning3MashineCost);
            buyCost.text = result.ToString();            
            return;
        }
    }    
}
