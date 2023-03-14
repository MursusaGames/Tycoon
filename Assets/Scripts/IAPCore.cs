using System;
using UnityEngine;
using TMPro;
using UnityEngine.Purchasing; //библиотека с покупками, будет доступна когда активируем сервисы
using System.Collections.Generic;
using UnityEngine.Purchasing.Security;

public class IAPCore : MonoBehaviour//, IStoreListener//для получения сообщений из Unity Purchasing
{
    [SerializeField] private TextMeshProUGUI screenText;
    public void OnPurchaseComplete(Product product)
    {
        var result = product.receipt;
    }

}
