using UnityEngine;

public class ChangeWarSizeSystem : MonoBehaviour
{
    [SerializeField] private GameObject startDepoGO;
    [SerializeField] private GameObject depo_1_GO;
    [SerializeField] private GameObject depo_2_GO;
    [SerializeField] private GameObject depo_3_GO;
    [SerializeField] private OutDepository depo_start;
    [SerializeField] private OutDepository depo_1;
    [SerializeField] private OutDepository depo_2;
    [SerializeField] private OutDepository depo_3;
    [SerializeField] private AppData data;
    [SerializeField] private CarShipment carShipment;
    private GameObject currentDepoGO;
    private GameObject newDepoGO;
    private OutDepository currentDepo;
    private OutDepository newDepo;
    public int oldFishDepoPaletCount;
    private int oldUnpackStakeDepoPaletCount;
    public int oldStakeDepoPaletCount;
    private int oldUnpackFarshDepoPaletCount;
    public int oldFarshDepoPaletCount;
    private int oldUnpackFileDepoPaletCount;
    public int oldFileDepoPaletCount;
    private int fishId = 0;
    private int unpackedStakeId = 1;
    private int stakeId = 2;
    private int unpackedFarshId = 4;
    private int farshId = 3;
    private int unpackedFileId = 6;
    private int fileId = 5;
    private float timeToInitList = 2f;    
    private int outWar_1_Index = 1;
    private int outWar_2_Index = 2;
    private int outWar_3_Index = 3;

    public void InitStocks()
    {
        if (data.userData.warehaus_updated == outWar_1_Index)
        {
            startDepoGO.SetActive(false);
            depo_1_GO.SetActive(true);
        }
        else if(data.userData.warehaus_updated == outWar_2_Index)
        {
            startDepoGO.SetActive(false);
            depo_2_GO.SetActive(true);
        }
        else if (data.userData.warehaus_updated == outWar_3_Index)
        {
            startDepoGO.SetActive(false);
            depo_3_GO.SetActive(true);
        }
    }
    public void ChangeOutWarSize()
    {
        if (data.userData.warehaus_updated > 3) return;
        if(data.userData.warehaus_updated == outWar_1_Index)
        {
            currentDepoGO = startDepoGO;
            currentDepo = depo_start;
            newDepoGO = depo_1_GO;
            newDepo = depo_1;
        }
        else if(data.userData.warehaus_updated == outWar_2_Index)
        {
            currentDepoGO = depo_1_GO;
            currentDepo = depo_1;
            newDepoGO = depo_2_GO;
            newDepo = depo_2;
        }
        else if (data.userData.warehaus_updated == outWar_3_Index)
        {
            currentDepoGO = depo_2_GO;
            currentDepo = depo_2;
            newDepoGO = depo_3_GO;
            newDepo = depo_3;
        }
        oldFishDepoPaletCount = currentDepo.productBoxCount[fishId];
        oldUnpackStakeDepoPaletCount = currentDepo.productBoxCount[unpackedStakeId]; 
        oldStakeDepoPaletCount = currentDepo.productBoxCount[stakeId];
        oldFarshDepoPaletCount = currentDepo.productBoxCount[farshId];
        oldUnpackFarshDepoPaletCount = currentDepo.productBoxCount[unpackedFarshId];
        oldFileDepoPaletCount = currentDepo.productBoxCount[fileId];
        oldUnpackFileDepoPaletCount = currentDepo.productBoxCount[unpackedFileId];
        currentDepoGO.SetActive(false);
        newDepoGO.SetActive(true);
        carShipment.CheckDepo();
     
        for (int i = 0; i < newDepo.productBoxCount.Count; i++)
        {
            newDepo.productBoxCount[i] = 0;
        }

        Invoke(nameof(SetWarehouse), timeToInitList);
    }
    private void SetWarehouse()
    {
        for (int i = 0; i < oldFishDepoPaletCount; i++)
        {
            newDepo.AddBox(fishId);
        }
        for (int i = 0; i < oldUnpackStakeDepoPaletCount; i++)
        {
            newDepo.AddBox(unpackedStakeId);
        }
        for (int i = 0; i < oldStakeDepoPaletCount; i++)
        {
            newDepo.AddBox(stakeId);
        }
        for (int i = 0; i < oldUnpackFarshDepoPaletCount; i++)
        {
            newDepo.AddBox(unpackedFarshId);
        }
        for (int i = 0; i < oldFarshDepoPaletCount; i++)
        {
            newDepo.AddBox(farshId);
        }
        for (int i = 0; i < oldUnpackFileDepoPaletCount; i++)
        {
            newDepo.AddBox(unpackedFileId);
        }
        for (int i = 0; i < oldFileDepoPaletCount; i++)
        {
            newDepo.AddBox(fileId);
        }
    }
}
