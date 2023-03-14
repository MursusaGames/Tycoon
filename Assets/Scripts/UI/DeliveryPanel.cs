using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class DeliveryPanel : MonoBehaviour
{
    [SerializeField] private GameObject claimBtnGO;
    [SerializeField] private GameObject barGO;
    [SerializeField] private AppData data;
    [SerializeField] private Image btnImg;
    [SerializeField] private Image bar;
    [SerializeField] private Image productImg;
    [SerializeField] private TextMeshProUGUI _money;
    [SerializeField] private TextMeshProUGUI _gold;
    [SerializeField] private TextMeshProUGUI productNumber;
    [SerializeField] private TextMeshProUGUI currentProduct;
    [SerializeField] private TextMeshProUGUI maxProduct;
    private int maxCount = 1;

    
    public void SetPanel(Sprite product, Sprite deliver, double money, double gold, int productCount)
    {
        productImg.sprite = product;
        btnImg.sprite = deliver;
        _money.text = Converter.instance.ConvertMoneyView(money);
        _gold.text = gold.ToString("F0");
        productNumber.text = productCount.ToString();
        currentProduct.text = "0";
        maxProduct.text = productCount.ToString();
        maxCount = productCount;
        ShowBar(data.userData.currentProductCount);
        
    }

    public void ResetDeliverMode()
    {
        barGO.SetActive(true);
        claimBtnGO.SetActive(false);
    }
    
    public void ShowBar(int count)
    {
        bar.fillAmount = (float)count/(float)maxCount;
        currentProduct.text = count.ToString();
        if (data.userData.currentProductCount == data.userData.currentMaxProductCount)
        {
            barGO.SetActive(false);
            claimBtnGO.SetActive(true);
        }
    }
    
}
