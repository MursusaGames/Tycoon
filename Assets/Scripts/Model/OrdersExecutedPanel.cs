using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class OrdersExecutedPanel : MonoBehaviour
{
    [SerializeField] private OrdersSystem ordersSystem;
    [SerializeField] private OutDepository outDepository;
    [SerializeField] private OutDepository outDepositoryBig;
    [SerializeField] private AppData data;
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private TextMeshProUGUI productName;
    [SerializeField] private Image imgProduct;
    [SerializeField] private Image imgValute;
    [SerializeField] private Image bar;
    [SerializeField] private TextMeshProUGUI boxInWarehouse;
    [SerializeField] private TextMeshProUGUI boxAll;
    [SerializeField] private Sprite gold;
    [SerializeField] private Sprite money;
    private Vector2 goldSize = new Vector2(50, 50);
    private Vector2 moneySize = new Vector2(70, 40);
    private float boxMax;
    private float productCount;

    private void OnEnable()
    {
        /*if (ordersSystem.orderFishOn)
        {
            productCount = boxMax - ordersSystem.currentOrderFishNumber;
        }
        else if (ordersSystem.orderUnpackedStakeOn)
        {
            productCount = boxMax - ordersSystem.currentUnpStakeOrderNumber;
        }
        else if (ordersSystem.orderStakeOn)
        {
            productCount = boxMax - ordersSystem.currentOrderStakeNumber;
        }
        boxInWarehouse.text = productCount.ToString();
        SetBar(productCount);*/
    }
    public void SetPanel(int orderID, int productID)
    {
        boxMax = (float)data.matchData.orderContainer.orders[orderID].productCount.Value;
        cost.text = data.matchData.orderContainer.orders[orderID].orderCost.ToString();
        productName.text = data.matchData.orderContainer.orders[orderID].productName;
        imgProduct.sprite = data.matchData.orderContainer.orders[orderID].productSprite;
        boxAll.text = data.matchData.orderContainer.orders[orderID].productCount.ToString();        
        boxInWarehouse.text = productCount.ToString();
        if(data.matchData.orderContainer.orders[orderID].orderValute == "money")
        {
            imgValute.sprite = money;
            imgValute.gameObject.GetComponent<RectTransform>().sizeDelta = moneySize;
        }
        else
        {
            imgValute.sprite = gold;
            imgValute.gameObject.GetComponent<RectTransform>().sizeDelta = goldSize;
        }
        SetBar(productCount);
        
    }
    public void SetBar(float boxCount)
    {
        bar.fillAmount = boxCount/boxMax;
    }
}
