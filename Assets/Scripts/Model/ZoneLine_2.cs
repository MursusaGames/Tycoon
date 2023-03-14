
public class ZoneLine_2 : TransportMenuContent
{
    private void OnEnable()
    {
        CheckInfo();
    }

    public void CheckInfo()
    {
        count.text = data.userData.zone2Car.ToString();
        cost.text = data.upgradesData.zone2CarCost.ToString();        
    }
}
