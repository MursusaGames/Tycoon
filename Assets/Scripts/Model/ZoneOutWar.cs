
public class ZoneOutWar : TransportMenuContent
{
    private void OnEnable()
    {
        CheckInfo();
    }

    public void CheckInfo()
    {
        count.text = data.userData.zone3Car.ToString();
        cost.text = data.upgradesData.zone3CarCost.ToString();        
    }
}
