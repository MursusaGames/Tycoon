using System;
using UnityEngine;
using TMPro;
using UnityEngine.Purchasing; //���������� � ���������, ����� �������� ����� ���������� �������
using System.Collections.Generic;
using UnityEngine.Purchasing.Security;

public class IAPCore : MonoBehaviour//, IStoreListener//��� ��������� ��������� �� Unity Purchasing
{
    [SerializeField] private TextMeshProUGUI screenText;
    public void OnPurchaseComplete(Product product)
    {
        var result = product.receipt;
    }

}
