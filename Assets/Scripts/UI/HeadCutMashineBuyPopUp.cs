using UnityEngine;
using TMPro;

public class HeadCutMashineBuyPopUp : MonoBehaviour
{
    [SerializeField] private TMP_Text buyCost;
    [SerializeField] private AppData data;
    private void OnEnable()
    {
        var result = Converter.instance.ConvertMoneyView(data.upgradesData.headCutingMashineCost);
        buyCost.text = result.ToString();        
    }
    
}
