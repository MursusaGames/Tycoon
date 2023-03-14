using UnityEngine;

public class BlueFish : MonoBehaviour
{
    public NarezkaMashineAnimation narezka;
    [SerializeField] private GameObject fishWithOutHead;
    [SerializeField] private GameObject stake;
    public Transform startPosFishWithOutHead;
    [SerializeField] private float timeToHideFish;
    public bool withOutHead;
    public bool inStake;
    public bool move;
    public float speed = 1f;
    private float startZPos;
    private float zTreshold = 0.2f;
    private float zBound = 1.7f;
    public AppData data;
    public bool masine1;

    void OnEnable()
    {
        if (withOutHead)
        {
            startZPos = gameObject.transform.localPosition.z;
            move = true;
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }            
    }
    public void StartCount()
    {
        gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        Invoke(nameof(HideFish), timeToHideFish);
    }

    private void Update()
    {
        if (inStake)
        {
            var pos = gameObject.transform.localPosition;
            pos.y -= speed * Time.deltaTime;
            gameObject.transform.localPosition = pos;
            if (Mathf.Abs(gameObject.transform.localPosition.y) > Mathf.Abs(zBound))
            {
                SetStake();
            }
            return;
        }
        if (move)
        {
            var pos = gameObject.transform.localPosition;
            pos.y += speed * Time.deltaTime;
            gameObject.transform.localPosition = pos;
            if (Mathf.Abs(gameObject.transform.localPosition.z) > Mathf.Abs(startZPos + zTreshold))
            {
                move = false;
            }               
        }
    }
    private void SetStake()
    {
        var fish = Instantiate(stake, startPosFishWithOutHead.position, Quaternion.identity, startPosFishWithOutHead).GetComponent<Stake>();
        fish.speed = speed;
        fish.narezka = narezka;
        Destroy(gameObject);
    }

    private void HideFish()
    {
        var fish = Instantiate(fishWithOutHead, startPosFishWithOutHead.position, Quaternion.identity, startPosFishWithOutHead).
            GetComponent<BlueFish>();
        fish.withOutHead = true;
        if(masine1) fish.speed = 1.0f + (data.userData.headCutingMashineSpeedLevel * Constant.speedDecreeseKoeff*data.userData.ADSpeedMultiplier);
        else fish.speed = 1.0f + (data.userData.headCutingMashine2SpeedLevel * Constant.speedDecreeseKoeff*data.userData.ADSpeedMultiplier);
        Destroy(gameObject);
    }


}
