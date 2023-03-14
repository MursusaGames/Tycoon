using TMPro;
using UnityEngine;

public class TimeTravelScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyFor10minText;
    [SerializeField] private TextMeshProUGUI moneyFor30minText;
    [SerializeField] private TextMeshProUGUI moneyFor1hourText;
    [SerializeField] private AppData data;
    public double moneyFor10min;
    public double moneyFor30min;
    public double moneyFor1hour;

    private void OnEnable()
    {
        var currenmtIncomeInMinut = GetCurrentAllIncome();
        moneyFor10min = currenmtIncomeInMinut * 10;
        moneyFor30min = currenmtIncomeInMinut * 30;
        moneyFor1hour = currenmtIncomeInMinut * 60;
        moneyFor10minText.text = Converter.instance.ConvertMoneyView(moneyFor10min);
        moneyFor30minText.text = Converter.instance.ConvertMoneyView(moneyFor30min);
        moneyFor1hourText.text = Converter.instance.ConvertMoneyView(moneyFor1hour);
    }
    private double GetCurrentAllIncome()
    {
        var result = data.userData.headCutingMashinePromice;
        if (data.userData.fishCleaningMashine > 0) result += data.userData.fishCleaningMashinePromice;
        if (data.userData.fishCleaningMashine > 1) result += data.userData.fishCleaningMashine2Promice;
        if (data.userData.fishCleaningMashine > 2) result += data.userData.fishCleaningMashine3Promice;
        if (data.userData.headCutingMashine > 1) result += data.userData.headCutingMashine2Promice;
        if (data.userData.fileMashine > 0) result += data.userData.fileMashinePromice;
        if (data.userData.packingMashine > 0) result += data.userData.steakPackingMashinePromice;
        if (data.userData.packingMashine > 1) result += data.userData.farshPackingMashinePromice;
        if (data.userData.packingMashine > 2) result += data.userData.filePackingMashinePromice;
        return result;
    }    
}
