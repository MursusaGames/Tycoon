using UnityEngine;

public class UpgradesMenu : MonoBehaviour
{
    [SerializeField] private FueSystem fueSystem;
    [SerializeField] private SwipeControl swipeControl;
    [SerializeField] private WorkMenu workMenu;
    [SerializeField] private GameObject workBuyPopUp;
    [SerializeField] private GameObject headCut_1_upgrade_PopUp;
    [SerializeField] private GameObject headCut_2_upgrade_PopUp;
    [SerializeField] private GameObject headCut_2_Buy_PopUp;
    [SerializeField] private GameObject fishCleaning_1_upgrade_PopUp;
    [SerializeField] private GameObject fishCleaning_1_Buy_PopUp;
    [SerializeField] private GameObject fishCleaning_2_upgrade_PopUp;
    [SerializeField] private GameObject fishCleaning_2_Buy_PopUp;
    [SerializeField] private GameObject fishCleaning_3_upgrade_PopUp;
    [SerializeField] private GameObject fishCleaning_3_Buy_PopUp;
    [SerializeField] private GameObject steakMashinZoneUpgrade_PopUp;
    [SerializeField] private GameObject steakMashinZoneBuy_PopUp;
    [SerializeField] private GameObject farshMashinZoneUpgrade_PopUp;
    [SerializeField] private GameObject farshMashinZoneBuy_PopUp;
    [SerializeField] private GameObject fileMashinZoneUpgrade_PopUp;
    [SerializeField] private GameObject fileMashinZoneBuy_PopUp;
    [SerializeField] private GameObject steakPackingUpgrade_PopUp;
    [SerializeField] private GameObject steakPackingBuy_PopUp;
    [SerializeField] private GameObject farshPackingUpgrade_PopUp;
    [SerializeField] private GameObject farshPackingBuy_PopUp;
    [SerializeField] private GameObject filePackingUpgrade_PopUp;
    [SerializeField] private GameObject filePackingBuy_PopUp;
    [SerializeField] private TransportMenu transportMenu;
    [SerializeField] private GameObject pirsContent;
    [SerializeField] private GameObject carInWarUpgrade_PopUp;    
    [SerializeField] private GameObject shipUpgrade_PopUp;
    [SerializeField] private AppData data;
    [SerializeField] private GameObject contentPortTransportScroll;
    [SerializeField] private GameObject contentMashinesTransportScroll;
    [SerializeField] private GameObject cameraFollowObject;
    [SerializeField] private Transform headCuting1Camera;
    [SerializeField] private Transform headCuting2Camera;
    [SerializeField] private Transform fishCleaning1Camera;
    [SerializeField] private Transform fishCleaning2Camera;
    [SerializeField] private Transform fishCleaning3Camera;
    [SerializeField] private Transform stakeMashineCamera;
    [SerializeField] private Transform farshMashineCamera;
    [SerializeField] private Transform fileMashineCamera;
    [SerializeField] private Transform packing1MashineCamera;
    [SerializeField] private Transform packing2MashineCamera;
    [SerializeField] private Transform packing3MashineCamera;
    private GameObject currentGO;
    private float cameraOrtoUpSize = 13f;
    private float cameraOrtoDownSize = 35f;
    private bool fueMessage;
    private void Start()
    {
        currentGO = headCut_1_upgrade_PopUp;
    }
    public void HeadCut_1_PopUpShow()
    {
        if (data.matchData.isFue)
        {
            if (fueMessage)
            {
                return;
            }
            else
            {
                fueSystem.TanStepBtn();
                fueMessage = true;
            }
            
        }
            
        currentGO.SetActive(false);
        currentGO = headCut_1_upgrade_PopUp;
        headCut_1_upgrade_PopUp.SetActive(true);
        cameraFollowObject.transform.position = headCuting1Camera.position;
        CameraOrtoUp();        
    }
    public void HeadCut_1_PopUpHide()
    {
        if (data.matchData.isFue) return;
        headCut_1_upgrade_PopUp.SetActive(false);
        CameraOrtoDown();
    }
    public void FueHideHeadCut1()
    {
        headCut_1_upgrade_PopUp.SetActive(false);
        CameraOrtoDown();
    }
    public void HeadCut_2_PopUpShow()
    {
        if (data.userData.headCutingMashine > 1)
        {
            currentGO.SetActive(false);
            currentGO = headCut_2_upgrade_PopUp;
            headCut_2_upgrade_PopUp.SetActive(true);
            cameraFollowObject.transform.position = headCuting2Camera.position;
            CameraOrtoUp();            
        }

        else
        {
            currentGO.SetActive(false);
            currentGO = headCut_2_Buy_PopUp;
            headCut_2_Buy_PopUp.SetActive(true);
            cameraFollowObject.transform.position = headCuting2Camera.position;
            CameraOrtoUp();            
        } 
            
    }
    private void CameraOrtoUp()
    {
        cameraOrtoDownSize = Camera.main.orthographicSize;
        Camera.main.orthographicSize = cameraOrtoUpSize;
        swipeControl.inMenu = true;
    }
    private void CameraOrtoDown()
    {
        Camera.main.orthographicSize = cameraOrtoDownSize;
        swipeControl.inMenu = false;
    }
    public void HeadCut_2_PopUpHide()
    {
        headCut_2_upgrade_PopUp.SetActive(false);
        headCut_2_Buy_PopUp.SetActive(false);
        CameraOrtoDown();
    }
    public void FishCleaning_1_PopUpShow()
    {
        if (data.userData.fishCleaningMashine > 0)
        {
            currentGO.SetActive(false);
            currentGO = fishCleaning_1_upgrade_PopUp;
            fishCleaning_1_upgrade_PopUp.SetActive(true);
            cameraFollowObject.transform.position = fishCleaning1Camera.position;
            CameraOrtoUp();
        }

        else
        {
            currentGO.SetActive(false);
            currentGO = fishCleaning_1_Buy_PopUp;
            fishCleaning_1_Buy_PopUp.SetActive(true);
            cameraFollowObject.transform.position = fishCleaning1Camera.position;
            CameraOrtoUp();
        }
            
    }
    public void FishCleaning_1_PopUpHide()
    {
        fishCleaning_1_upgrade_PopUp.SetActive(false);
        fishCleaning_1_Buy_PopUp.SetActive(false);
        CameraOrtoDown();        
    }
    public void FishCleaning_2_PopUpShow()
    {
        if (data.userData.fishCleaningMashine > 1)
        {
            currentGO.SetActive(false);
            currentGO = fishCleaning_2_upgrade_PopUp;
            fishCleaning_2_upgrade_PopUp.SetActive(true);
            cameraFollowObject.transform.position = fishCleaning2Camera.position;
            CameraOrtoUp();
        }

        else
        {
            currentGO.SetActive(false);
            currentGO = fishCleaning_2_Buy_PopUp;
            fishCleaning_2_Buy_PopUp.SetActive(true);
            cameraFollowObject.transform.position = fishCleaning2Camera.position;
            CameraOrtoUp();
        }
            
    }
    public void FishCleaning_2_PopUpHide()
    {
        fishCleaning_2_upgrade_PopUp.SetActive(false);
        fishCleaning_2_Buy_PopUp.SetActive(false);
        CameraOrtoDown();
    }
    public void FishCleaning_3_PopUpShow()
    {
        if (data.userData.fishCleaningMashine > 2)
        {
            currentGO.SetActive(false);
            currentGO = fishCleaning_3_upgrade_PopUp;
            fishCleaning_3_upgrade_PopUp.SetActive(true);
            cameraFollowObject.transform.position = fishCleaning3Camera.position;
            CameraOrtoUp();
        }

        else
        {
            currentGO.SetActive(false);
            currentGO = fishCleaning_3_Buy_PopUp;
            fishCleaning_3_Buy_PopUp.SetActive(true);
            cameraFollowObject.transform.position = fishCleaning3Camera.position;
            CameraOrtoUp();
        }
            
    }
    public void FishCleaning_3_PopUpHide()
    {
        fishCleaning_3_upgrade_PopUp.SetActive(false);
        fishCleaning_3_Buy_PopUp.SetActive(false);
        CameraOrtoDown();
    }
    public void SteakZonePopUpShow()
    {
        if (data.userData.steakMashine > 0)
        {
            currentGO.SetActive(false);
            currentGO = steakMashinZoneUpgrade_PopUp;
            steakMashinZoneUpgrade_PopUp.SetActive(true);
            cameraFollowObject.transform.position = stakeMashineCamera.position;
            CameraOrtoUp();
        }

        else
        {
            currentGO.SetActive(false);
            currentGO = steakMashinZoneBuy_PopUp;
            steakMashinZoneBuy_PopUp.SetActive(true);
            cameraFollowObject.transform.position = stakeMashineCamera.position;
            CameraOrtoUp();
        }
            
    }
    public void SteakZonePopUpHide()
    {
        steakMashinZoneUpgrade_PopUp.SetActive(false);
        steakMashinZoneBuy_PopUp.SetActive(false);
        CameraOrtoDown();
    }
    public void FarshZonePopUpShow()
    {
        if (data.userData.farshMashine > 0)
        {
            currentGO.SetActive(false);
            currentGO = farshMashinZoneUpgrade_PopUp;
            farshMashinZoneUpgrade_PopUp.SetActive(true);
            cameraFollowObject.transform.position = farshMashineCamera.position;
            CameraOrtoUp();
        }

        else
        {
            currentGO.SetActive(false);
            currentGO = farshMashinZoneBuy_PopUp;
            farshMashinZoneBuy_PopUp.SetActive(true);
            cameraFollowObject.transform.position = farshMashineCamera.position;
            CameraOrtoUp();
        }

    }
    public void FarshZonePopUpHide()
    {
        farshMashinZoneUpgrade_PopUp.SetActive(false);
        farshMashinZoneBuy_PopUp.SetActive(false);
        CameraOrtoDown();
    }
    public void FileZonePopUpShow()
    {
        if (data.userData.fileMashine > 0)
        {
            currentGO.SetActive(false);
            currentGO = fileMashinZoneUpgrade_PopUp;
            fileMashinZoneUpgrade_PopUp.SetActive(true);
            cameraFollowObject.transform.position = fileMashineCamera.position;
            CameraOrtoUp();
        }

        else
        {
            currentGO.SetActive(false);
            currentGO = fileMashinZoneBuy_PopUp;
            fileMashinZoneBuy_PopUp.SetActive(true);
            cameraFollowObject.transform.position = fileMashineCamera.position;
            CameraOrtoUp();
        }

    }
    public void FileZonePopUpHide()
    {
        fileMashinZoneUpgrade_PopUp.SetActive(false);
        fileMashinZoneBuy_PopUp.SetActive(false);
        CameraOrtoDown();
    }
    public void Packing_1_ZonePopUpShow()
    {
        if (data.userData.packingMashine > 0)
        {
            currentGO.SetActive(false);
            currentGO = steakPackingUpgrade_PopUp;
            steakPackingUpgrade_PopUp.SetActive(true);
            cameraFollowObject.transform.position = packing1MashineCamera.position;
            CameraOrtoUp();
        }

        else
        {
            currentGO.SetActive(false);
            currentGO = steakPackingBuy_PopUp;
            steakPackingBuy_PopUp.SetActive(true);
            cameraFollowObject.transform.position = packing1MashineCamera.position;
            CameraOrtoUp();
        }
            
    }
    public void Packing_1_ZonePopUpHide()
    {
        steakPackingUpgrade_PopUp.SetActive(false);
        steakPackingBuy_PopUp.SetActive(false);
        CameraOrtoDown();
    }
    public void Packing_2_ZonePopUpShow()
    {
        if (data.userData.packingMashine > 1)
        {
            currentGO.SetActive(false);
            currentGO = farshPackingUpgrade_PopUp;
            farshPackingUpgrade_PopUp.SetActive(true);
            cameraFollowObject.transform.position = packing2MashineCamera.position;
            CameraOrtoUp();
        }
        else
        {
            currentGO.SetActive(false);
            currentGO = farshPackingBuy_PopUp;
            farshPackingBuy_PopUp.SetActive(true);
            cameraFollowObject.transform.position = packing2MashineCamera.position;
            CameraOrtoUp();
        }

    }
    public void Packing_2_ZonePopUpHide()
    {
        farshPackingUpgrade_PopUp.SetActive(false);
        farshPackingBuy_PopUp.SetActive(false);
        CameraOrtoDown();
    }
    public void Packing_3_ZonePopUpShow()
    {
        if (data.userData.packingMashine > 2)
        {
            currentGO.SetActive(false);
            currentGO = filePackingUpgrade_PopUp;
            filePackingUpgrade_PopUp.SetActive(true);
            cameraFollowObject.transform.position = packing3MashineCamera.position;
            CameraOrtoUp();
        }

        else
        {
            currentGO.SetActive(false);
            currentGO = filePackingBuy_PopUp;
            filePackingBuy_PopUp.SetActive(true);
            cameraFollowObject.transform.position = packing3MashineCamera.position;
            CameraOrtoUp();
        }

    }
    public void Packing_3_ZonePopUpHide()
    {
        filePackingUpgrade_PopUp.SetActive(false);
        filePackingBuy_PopUp.SetActive(false);
        CameraOrtoDown();
    }
    public void CarInPortZonePopUpShow()
    {
        if (data.matchData.isFue) return;
        shipUpgrade_PopUp.SetActive(true);
        transportMenu.ChangeMenuScroll(0);
        var pos = contentPortTransportScroll.transform.localPosition;
        pos.y = 1850;
        contentPortTransportScroll.transform.localPosition = pos;
    }
    public void PirsZonePopUpShow()
    {
        if (data.matchData.isFue) return;
        shipUpgrade_PopUp.SetActive(true);
        transportMenu.ChangeMenuScroll(1);
    }
    public void Pirs1ZonePopUpShow()
    {
        if (data.matchData.isFue) return;
        shipUpgrade_PopUp.SetActive(true);
        transportMenu.ChangeMenuScroll(1);
        var pos = pirsContent.transform.localPosition;
        pos.y = 900;
        pirsContent.transform.localPosition = pos;
    }
    public void Pirs2ZonePopUpShow()
    {
        if (data.matchData.isFue) return;
        shipUpgrade_PopUp.SetActive(true);
        transportMenu.ChangeMenuScroll(1);
        var pos = pirsContent.transform.localPosition;
        pos.y = 1900;
        pirsContent.transform.localPosition = pos;
    }

    public void CarInWarZonePopUpShow()
    {
        if (data.matchData.isFue) return;
        carInWarUpgrade_PopUp.SetActive(true);
        workMenu.ChangeMenuScroll(1);
    }
    public void CarInWarZonePopUpHide()
    {
        carInWarUpgrade_PopUp.SetActive(false);
    }
    public void CarZone_1_PopUpShow()
    {
        if (data.matchData.isFue) return;
        carInWarUpgrade_PopUp.SetActive(true);
        workMenu.ChangeMenuScroll(1);
        var pos = contentMashinesTransportScroll.transform.localPosition;
        pos.y = 1200;
        contentMashinesTransportScroll.transform.localPosition = pos;
    }
    
    public void CarZone_2_PopUpShow()
    {
        if (data.matchData.isFue) return;
        carInWarUpgrade_PopUp.SetActive(true);
        workMenu.ChangeMenuScroll(1);
        var pos = contentMashinesTransportScroll.transform.localPosition;
        pos.y = 1500;
        contentMashinesTransportScroll.transform.localPosition = pos;
    }
    
    public void CarZone_3_PopUpShow()
    {
        if (data.matchData.isFue) return;
        carInWarUpgrade_PopUp.SetActive(true);
        workMenu.ChangeMenuScroll(1);
        var pos = contentMashinesTransportScroll.transform.localPosition;
        pos.y = 1900;
        contentMashinesTransportScroll.transform.localPosition = pos;
    }    
    public void CarOutPopUpShow()
    {
        if (data.matchData.isFue) return;
        carInWarUpgrade_PopUp.SetActive(true);
        workMenu.ChangeMenuScroll(1);
        var pos = contentMashinesTransportScroll.transform.localPosition;
        pos.y = 2300;
        contentMashinesTransportScroll.transform.localPosition = pos;
    }
    public void CarShipmentPopUpShow()
    {
        if (data.matchData.isFue) return;
        carInWarUpgrade_PopUp.SetActive(true);
        workMenu.ChangeMenuScroll(1);
        var pos = contentMashinesTransportScroll.transform.localPosition;
        pos.y = 3000;
        contentMashinesTransportScroll.transform.localPosition = pos;
    }
    public void OutWarehouseShowPopUp()
    {
        if (data.matchData.isFue) return;
        carInWarUpgrade_PopUp.SetActive(true);
        workMenu.ChangeMenuScroll(0);
        var pos = contentMashinesTransportScroll.transform.localPosition;
        pos.y = 0;
        contentMashinesTransportScroll.transform.localPosition = pos;
    }
    public void ShowStatisticWindow()
    {

    }
    public void ShipPopUpShow()
    {
        if (data.matchData.isFue) return;
        shipUpgrade_PopUp.SetActive(true);
    }
    public void Ship1PopUpShow()
    {
        if (data.matchData.isFue) return;
        shipUpgrade_PopUp.SetActive(true);
        transportMenu.ChangeMenuScroll(0);
        var pos = contentPortTransportScroll.transform.localPosition;
        pos.y = 0;
        contentPortTransportScroll.transform.localPosition = pos;
    }
    public void ShipPopUpHide()
    {
        shipUpgrade_PopUp.SetActive(false);
    }
    public void ShowWorkBuyWindow()
    {
        workBuyPopUp.SetActive(true);
    }
    public void HideWorkBuyWindow()
    {
        workBuyPopUp.SetActive(false);
    }
    public void CloseAllWindow()
    {
        headCut_1_upgrade_PopUp.SetActive(false);
        shipUpgrade_PopUp.SetActive(false);                        
        carInWarUpgrade_PopUp.SetActive(false);        
        steakPackingUpgrade_PopUp.SetActive(false);
        steakPackingBuy_PopUp.SetActive(false);
        steakMashinZoneUpgrade_PopUp.SetActive(false);
        steakMashinZoneBuy_PopUp.SetActive(false);
        farshMashinZoneUpgrade_PopUp.SetActive(false);
        farshMashinZoneBuy_PopUp.SetActive(false);
        fileMashinZoneUpgrade_PopUp.SetActive(false);
        fileMashinZoneBuy_PopUp.SetActive(false);
        fishCleaning_3_upgrade_PopUp.SetActive(false);
        fishCleaning_3_Buy_PopUp.SetActive(false);
        fishCleaning_2_upgrade_PopUp.SetActive(false);
        fishCleaning_2_Buy_PopUp.SetActive(false);
        fishCleaning_1_upgrade_PopUp.SetActive(false);
        fishCleaning_1_Buy_PopUp.SetActive(false);
        headCut_2_upgrade_PopUp.SetActive(false);
        headCut_2_Buy_PopUp.SetActive(false);
        InterfaceManager.SetCurrentMenu(MenuName.Main);
    }
}
