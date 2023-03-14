using UnityEngine;
using UnityEngine.UI;

public enum FueState
{
    none,
    step1,
    step2,
    step3,
    step4,
    step5,
    step6,
    step7,
    step8,
    step9,
    step10,
    step11,
    step12,
    step13,
    step14


} 
public class FueSystem : BaseMonoSystem
{
    [SerializeField] private AdjustEventsSystem adjustEvents;
    [SerializeField] private ParticleSystem headCut1Particle;
    [SerializeField] private ParticleSystem headCut2Particle;
    [SerializeField] private ParticleSystem fishCleanParticle;
    [SerializeField] private ParticleSystem transportBtnParticle;
    [SerializeField] private ParticleSystem transport2BtnParticle;
    [SerializeField] private ParticleSystem machinesMenuParticle;
    [SerializeField] private ParticleSystem machines2MenuParticle;    
    [SerializeField] private GameObject btnStep4;
    [SerializeField] private GameObject btnStep7;
    [SerializeField] private Image firstScreenPanel;
    [SerializeField] private GameObject firstScreen;
    [SerializeField] private GameObject firstHand;    
    [SerializeField] private GameObject secondHand;
    [SerializeField] private GameObject firstText;
    [SerializeField] private GameObject secondText;
    [SerializeField] private GameObject thirdText;
    [SerializeField] private GameObject thirdHand;
    [SerializeField] private GameObject fourthText;
    [SerializeField] private GameObject fourthHand;
    [SerializeField] private GameObject fifthHand;
    [SerializeField] private GameObject fifthText;
    [SerializeField] private GameObject sixText;
    [SerializeField] private GameObject sevnText;
    [SerializeField] private GameObject sevnHand;
    [SerializeField] private GameObject eitHand;
    [SerializeField] private GameObject eitText;
    [SerializeField] private GameObject nineText;
    [SerializeField] private GameObject tenText;
    [SerializeField] private GameObject elevnText;
    [SerializeField] private GameObject twelvText;
    [SerializeField] private GameObject thretenText;
    [SerializeField] private GameObject fourtenText;
    [SerializeField] private SwipeControl swipeControl;
    [SerializeField] private GameObject camFollowObj;
    [SerializeField] private Transform portPos;
    [SerializeField] private Transform factoryPos;
    [SerializeField] private Transform fishCleanPos;
    [SerializeField] private GameObject talkBG;
    [SerializeField] private GameObject lady;
    [SerializeField] private UpgradesMenu upgradesMenu;
    public FueState state;
    public bool touch;
    private bool btnStep4Down;
    private bool btnStep5Down;
    private bool btnStep7Down;
    private bool btnStep8Down;
    private bool btnStep10Down;
    private bool btnStep11Down;
    private bool btnStep13Down;
    private bool step6;
    public override void Init(AppData data)
    {
        base.Init(data);        
    }
    
    public void SetBtnStep4()
    {
        btnStep4Down = true;
        touch = true;
    }
    public void SetBtnStep5()
    {
        btnStep5Down = true;
        touch = true;
    }
    public void SetBtnStep7()
    {
        btnStep7Down = true;
        touch = true;
    }
    public void SetBtnStep8()
    {
        btnStep8Down = true;
        touch = true;
    }
    public void TanStepBtn()
    {
        btnStep10Down = true;
        touch = true;
    }
    public void ElevnStepBtn()
    {
        btnStep11Down = true;
        touch = true;
    }
    public void ThretenStepBtn()
    {
        btnStep13Down = true;
        touch = true;
    }
    
    public void StartFue()
    {
        FirstStep();            
    }
    #region First_Step
    private void FirstStep()
    {
        firstScreen.SetActive(true);
        lady.SetActive(true);        
        Invoke(nameof(FirstStep_2), 0.5f);               
    }
    private void FirstStep_2()
    {
        talkBG.SetActive(true);
        firstText.SetActive(true);
        Invoke(nameof(SetState1), 1f);
    }
    private void SetState1()
    {
        state = FueState.step1;
    }
    private void ResetFirstStep()
    {
        firstText.SetActive(false);
        talkBG.SetActive(false);
    }
    #endregion
    #region Second_Step
    public void SecondStep()
    {
        talkBG.SetActive(true);
        secondText.SetActive(true);
        Invoke(nameof(SetState2), 1f);        
    }
    private void SetState2()
    {
        state = FueState.step2;
    }
    private void ResetSecondStep()
    {
        secondText.SetActive(false);
        talkBG.SetActive(false);
    }

    #endregion
    #region Third_Step
    public void ThirdStep()
    {
        talkBG.SetActive(true);
        thirdText.SetActive(true);
        Invoke(nameof(SetState3), 1f);
    }
    private void SetState3()
    {
        state = FueState.step3;
    }
    private void ResetThirdStep()
    {
        thirdText.SetActive(false);
        talkBG.SetActive(false);
    }
    #endregion
    #region Fourth_Step
    public void FourthStep()
    {
        talkBG.SetActive(true);
        fourthText.SetActive(true);
        btnStep4.SetActive(true);
        firstHand.SetActive(true);        
        transportBtnParticle.Play();
        Invoke(nameof(FourthStep_2), 3f);
        Invoke(nameof(SetState4), 1f);
    }
    private void SetState4()
    {
        state = FueState.step4;
    }
    private void FourthStep_2()
    {
        fourthText.SetActive(false);
        talkBG.SetActive(false);
        
    }
    private void ResetFourthStep()
    {
        FourthStep_2();
        firstHand.SetActive(false);        
        transportBtnParticle.Stop();
        btnStep4.SetActive(false);
        InterfaceManager.SetCurrentMenu(MenuName.Transport);        
    }
    #endregion
    #region Fifth_Step
    public void FifthStep()
    {
        lady.SetActive(true);
        talkBG.SetActive(true);
        fifthText.SetActive(true);
        secondHand.SetActive(true);        
        transport2BtnParticle.Play();
        firstScreenPanel.raycastTarget = false;
        Invoke(nameof(FifthStep_2), 3f);
        Invoke(nameof(SetState5), 1f);
    }
    private void SetState5()
    {
        state = FueState.step5;
    }
    private void FifthStep_2()
    {
        fifthText.SetActive(false);
        talkBG.SetActive(false);        
    }
    private void ResetFifthStep()
    {
        FifthStep_2();
        secondHand.SetActive(false);        
        transport2BtnParticle.Stop();        
        firstScreenPanel.raycastTarget = true;        
    }
    public void SixtStep()
    {
        talkBG.SetActive(true);
        sixText.SetActive(true);
        Invoke(nameof(SetState6), 1f);
    }
    private void SetState6()
    {
        state = FueState.step6;
    }
    private void ResetSixtStep()
    {
        sixText.SetActive(false);
        talkBG.SetActive(false);
    }
    public void SevnStep()
    {
        talkBG.SetActive(true);
        sevnText.SetActive(true);
        thirdHand.SetActive(true);
        lady.SetActive(true);
        btnStep7.SetActive(true);
        machinesMenuParticle.Play();
        Invoke(nameof(SetState7), 2f);
        Invoke(nameof(SevnStep_2), 3f);
    }
    private void SetState7()
    {
        state = FueState.step7;
    }
    private void SevnStep_2()
    {
        sevnText.SetActive(false);
        talkBG.SetActive(false);        
    }
    private void ResetSevnStep()
    {
        SevnStep_2();
        firstScreenPanel.raycastTarget = false;
        thirdHand.SetActive(false);
        machinesMenuParticle.Stop();
    }
    public void EitStep()
    {
        btnStep7.SetActive(false);
        InterfaceManager.SetCurrentMenu(MenuName.Work);
        fourthHand.SetActive(true);
        machines2MenuParticle.Play();
        Invoke(nameof(SetState8), 1f);
    }
    private void SetState8()
    {
        state = FueState.step8;
    }

    private void ResetEitStep()
    {
        firstScreenPanel.raycastTarget = true;
        fourthHand.SetActive(false);
        machinesMenuParticle.Stop();
    }
    public void NineStep()
    {
        talkBG.SetActive(true);
        eitText.SetActive(true);
        Invoke(nameof(SetState9), 1f);
        lady.SetActive(true);
    }
    private void SetState9()
    {
        state = FueState.step9;
    }
    private void NineStep_2()
    {
        eitText.SetActive(false);
        talkBG.SetActive(false);        
    }
    public void TanStep()
    {
        NineStep_2();
        firstScreenPanel.raycastTarget = false;        
        talkBG.SetActive(true);
        nineText.SetActive(true);        
        fifthHand.SetActive(true);
        headCut1Particle.Play();
        Invoke(nameof(SetState10), 2f);        
    }
    private void SetState10()
    {
        state = FueState.step10;
    }
    private void TenStep_2()
    {
        fifthHand.SetActive(false);
        headCut1Particle.Stop();
        nineText.SetActive(false);
        talkBG.SetActive(false);        
    }
    public void ElevnStep()
    {
        talkBG.SetActive(true);
        tenText.SetActive(true);
        Invoke(nameof(SetState11), 1f);
        Invoke(nameof(ElevenStep_2), 4f);
    }
    private void SetState11()
    {
        sevnHand.SetActive(true);
        headCut2Particle.Play();
        state = FueState.step11;
    }
    private void ElevenStep_2()
    {
        tenText.SetActive(false);
        talkBG.SetActive(false);
        lady.SetActive(false);
    }
    public void TwelvStep()
    {
        sevnHand.SetActive(false);
        headCut2Particle.Stop();        
        upgradesMenu.FueHideHeadCut1();        
        Invoke(nameof(TwelvStep2), 0.1f);        
    }
    private void TwelvStep2()
    {
        lady.SetActive(true);
        talkBG.SetActive(true);
        elevnText.SetActive(true);
        Invoke(nameof(SetState12), 1f);
    }
    private void SetState12()
    {
        state = FueState.step12;
    }
    public void ThritenStep()
    {
        CameraGoToFishClean1();
        elevnText.SetActive(false);
        talkBG.SetActive(false);
        Invoke(nameof(TretenStep_1), 1f);        
    }
    private void TretenStep_1()
    {
        talkBG.SetActive(true);
        twelvText.SetActive(true);
        Invoke(nameof(TretenStep_2), 2f);
    }
    private void TretenStep_2()
    {
        eitHand.SetActive(true);
        fishCleanParticle.Play();        
        Invoke(nameof(SetState13), 1f);
    }
    private void SetState13()
    {
        state = FueState.step13;        
    }
    public void FourtenStep()
    {
        twelvText.SetActive(false);
        talkBG.SetActive(false);
        lady.SetActive(false);
        eitHand.SetActive(false);
        fishCleanParticle.Stop();       
        Invoke(nameof(EndMessage0), 1f);
    }
    private void EndMessage0()
    {
        lady.SetActive(true);
        talkBG.SetActive(true);
        thretenText.SetActive(true);
        Invoke(nameof(EndMessage), 3f);
    }
    private void EndMessage()
    {
        firstScreenPanel.raycastTarget = true;
        thretenText.SetActive(false);
        talkBG.SetActive(false);
        lady.SetActive(false);
        firstScreen.SetActive(false);
        data.matchData.isFue = false;
        //swipeControl.inMenu = false;
        adjustEvents.AdustEventForTutorial(14, "End Tutorial");
    }
    #endregion
    private void Update()
    {
        if (!data.matchData.isFue) return;
        if (Input.touchCount == 1&& state != FueState.none || touch && state != FueState.none)
        {
            switch (state)
            {
                case FueState.step1:
                    state = FueState.none;
                    ResetFirstStep();
                    Invoke(nameof(SecondStep), 0.2f);                    
                    touch = false;
                    adjustEvents.AdustEventForTutorial(1, "Open next page_1");
                    break;
                case FueState.step2:
                    state = FueState.none;
                    ResetSecondStep();
                    Invoke(nameof(ThirdStep), 0.2f);                                     
                    touch = false;
                    adjustEvents.AdustEventForTutorial(2, "Open next page_2");
                    break;
                case FueState.step3:
                    state = FueState.none;
                    ResetThirdStep();
                    CameraGoToPort();
                    Invoke(nameof(FourthStep), 0.2f);
                    touch = false;
                    adjustEvents.AdustEventForTutorial(3, "Move to port");
                    break;
                case FueState.step4:
                    if (btnStep4Down)
                    {
                        state = FueState.none;
                        ResetFourthStep();                        
                        Invoke(nameof(FifthStep),0.2f);
                        touch = false;
                        adjustEvents.AdustEventForTutorial(4, "Open port menu");
                    }                    
                    break;
                case FueState.step5:
                    if (btnStep5Down)
                    {
                        state = FueState.none;
                        ResetFifthStep();
                        Invoke(nameof(SixtStep), 0.2f);
                        touch = false;
                        adjustEvents.AdustEventForTutorial(5, "Buy ship");
                    }
                    break;
                case FueState.step6:
                    state = FueState.none;
                    ResetSixtStep();
                    Invoke(nameof(SevnStep), 0.2f);
                    touch = false;
                    adjustEvents.AdustEventForTutorial(6, "Open next page_6");
                    break;
                case FueState.step7:
                    if (btnStep7Down)
                    {
                        state = FueState.none;
                        ResetSevnStep();
                        Invoke(nameof(EitStep), 0.2f);
                        touch = false;
                        adjustEvents.AdustEventForTutorial(7, "Open transport menu");
                    }
                    break;
                case FueState.step8:
                    if (btnStep8Down)
                    {
                        state = FueState.none;
                        ResetEitStep();
                        InterfaceManager.SetCurrentMenu(MenuName.Main);
                        Invoke(nameof(NineStep), 0.2f);
                        touch = false;
                        adjustEvents.AdustEventForTutorial(8, "Buy port warehouse transport");
                    }
                    break;
                case FueState.step9:
                    state = FueState.none;
                    NineStep_2();
                    CameraGoToFactory();
                    Invoke(nameof(TanStep),0.2f);
                    touch = false;
                    adjustEvents.AdustEventForTutorial(9, "Move to factory");
                    break;
                case FueState.step10:
                    if (btnStep10Down)
                    {
                        TenStep_2();
                        state = FueState.none;
                        Invoke(nameof(ElevnStep), 0.2f);
                        touch = false;
                        adjustEvents.AdustEventForTutorial(10, "Open Carving machine upgrade window");
                    }                    
                    break;
                case FueState.step11:
                    if (btnStep11Down)
                    {
                        state = FueState.none;
                        Invoke(nameof(TwelvStep), 0.2f);
                        touch = false;
                        adjustEvents.AdustEventForTutorial(11, "Upgrade Carving Machine 1");
                    }
                    break;
                case FueState.step12:
                    state = FueState.none;
                    Invoke(nameof(ThritenStep),0.2f);
                    touch = false;
                    adjustEvents.AdustEventForTutorial(12, "Open next page_12");
                    break;
                case FueState.step13:
                    if (btnStep13Down)
                    {
                        state = FueState.none;
                        Invoke(nameof(FourtenStep), 0.2f);
                        touch = false;
                        adjustEvents.AdustEventForTutorial(13, "Open Scaling Machine upgrade window");
                    }
                    break;
            }
        }
    }
    private void CameraGoToPort()
    {
        camFollowObj.transform.position = portPos.position;
    }
    private void CameraGoToFactory()
    {
        camFollowObj.transform.position = factoryPos.position;
    }
    private void CameraGoToFishClean1()
    {
        camFollowObj.transform.position = fishCleanPos.position;
    }
    public void OrdersIn()
    {
        firstScreen.SetActive(true);
        lady.SetActive(true);
        talkBG.SetActive(true);
        fourtenText.SetActive(true);
        Invoke(nameof(HideOrdersFue), 4f);
    }

    private void HideOrdersFue()
    {
        fourtenText.SetActive(false);
        talkBG.SetActive(false);
        lady.SetActive(false);
        firstScreen.SetActive(false);        
    }

}
