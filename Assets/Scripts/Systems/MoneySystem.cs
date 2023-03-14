using UnityEngine;
using TMPro;
using System;

public class MoneySystem : BaseMonoSystem
{
    [SerializeField] private TextMeshProUGUI coins;
    
    public bool Buy(double _price)
    {
        data.moneyData.Clear();
        if (_price > data.userData.coins.Value)
        {
            data.moneyData.state.Value = MoneyData.State.Failed;
            return false;
        }
        var price = (double)Math.Round((double)_price, 1);
        data.userData.coins.Value -= price;
        SaveDataSystem.Instance.SaveMoney();
        ConvertMoneyView();
        data.moneyData.state.Value = MoneyData.State.Accept;
        return true;
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
    //TODO убрать после отладки
    public void AddMoreMoney()
    {
        data.userData.coins.Value += 100000000000000;
        ConvertMoneyView();
    }
    public void AddMoreGold()
    {
        if (data.userData.gold.Value > 800) return;
        data.userData.gold.Value += 100;
    }
    public void AddMoreCrystals()
    {
        if (data.userData.cristalls.Value > 800) return;
        data.userData.cristalls.Value += 100;
    }
    public void ClearHistory()
    {
        PlayerPrefs.DeleteAll();
    }
}
