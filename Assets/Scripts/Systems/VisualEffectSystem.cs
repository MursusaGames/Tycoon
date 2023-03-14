using UnityEngine;
using UnityEngine.UI;

public class VisualEffectSystem : MonoBehaviour
{
    [SerializeField] private ParticleSystem headCut1Particle;
    [SerializeField] private ParticleSystem headCut1SpeedParticle;
    [SerializeField] private ParticleSystem headCut2Particle;
    [SerializeField] private ParticleSystem headCut2SpeedParticle;
    [SerializeField] private ParticleSystem fishClean1Particle;
    [SerializeField] private ParticleSystem fishClean1SpeedParticle;
    [SerializeField] private ParticleSystem fishClean2Particle;
    [SerializeField] private ParticleSystem fishClean2SpeedParticle;
    [SerializeField] private ParticleSystem fishClean3Particle;
    [SerializeField] private ParticleSystem fishClean3SpeedParticle;
    [SerializeField] private ParticleSystem steakMachinParticle;
    [SerializeField] private ParticleSystem steakMachinSpeedParticle;
    [SerializeField] private ParticleSystem farshMachinParticle;
    [SerializeField] private ParticleSystem farshMachinSpeedParticle;
    [SerializeField] private ParticleSystem fileMachinParticle;
    [SerializeField] private ParticleSystem fileMachinSpeedParticle;
    [SerializeField] private ParticleSystem packingMachinParticle;
    [SerializeField] private ParticleSystem packingMachinSpeedParticle;
    [SerializeField] private ParticleSystem farshPackingMachinParticle;
    [SerializeField] private ParticleSystem farshPackingMachinSpeedParticle;
    [SerializeField] private ParticleSystem filePackingMachinParticle;
    [SerializeField] private ParticleSystem filePackingMachinSpeedParticle;
    [SerializeField] private AudioSource increaseLevel;
    [SerializeField] private AudioSource increaseSpeed;
    [SerializeField] private Slider slider;

    #region HeadCutParticles
    public void ShowHeadCut1Particle()
    {
        headCut1Particle.Play();
        increaseLevel.volume = (0.04f  * slider.value);
        if (PlayerPrefs.GetInt("Sound", 1) == 1) 
            increaseLevel.Play();

    }
    public void ShowHeadCut1SpeedParticle()
    {
        headCut1SpeedParticle.Play();
        increaseSpeed.volume = (0.04f * slider.value);
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
            increaseSpeed.Play();
    }

    public void ShowHeadCut2Particle()
    {
        headCut2Particle.Play();
        increaseLevel.volume = (0.04f * slider.value);
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
            increaseLevel.Play();
    }
    public void ShowHeadCut2SpeedParticle()
    {
        headCut2SpeedParticle.Play();
        increaseSpeed.volume = (0.04f * slider.value);
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
            increaseSpeed.Play();
    }
    #endregion

    #region FishCleanParticles
    public void ShowFishClean1Particle()
    {
        fishClean1Particle.Play();
        increaseLevel.volume = (0.04f * slider.value);
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
            increaseLevel.Play();
    }
    public void ShowFishClean1SpeedParticle()
    {
        fishClean1SpeedParticle.Play();
        increaseSpeed.volume = (0.04f * slider.value);
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
            increaseSpeed.Play();
    }
    public void ShowFishClean2Particle()
    {
        fishClean2Particle.Play();
        increaseLevel.volume = (0.04f * slider.value);
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
            increaseLevel.Play();
    }
    public void ShowFishClean2SpeedParticle()
    {
        fishClean2SpeedParticle.Play();
        increaseSpeed.volume = (0.04f * slider.value);
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
            increaseSpeed.Play();
    }
    public void ShowFishClean3Particle()
    {
        fishClean3Particle.Play();
        increaseLevel.volume = (0.04f * slider.value);
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
            increaseLevel.Play();
    }
    public void ShowFishClean3SpeedParticle()
    {
        fishClean3SpeedParticle.Play();
        increaseSpeed.volume = (0.04f * slider.value);
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
            increaseSpeed.Play();
    }

    #endregion

    #region SteakMachineParticle
    public void ShowSteakMachinParticle()
    {
        steakMachinParticle.Play();
        increaseLevel.volume = (0.04f * slider.value);
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
            increaseLevel.Play();
    }
    public void ShowSteakMachinSpeedParticle()
    {
        steakMachinSpeedParticle.Play();
        increaseSpeed.volume = (0.04f * slider.value);
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
            increaseSpeed.Play();
    }
    #endregion

    #region FarshMachineParticle
    public void ShowFarshMachinParticle()
    {
        farshMachinParticle.Play();
        increaseLevel.volume = (0.04f * slider.value);
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
            increaseLevel.Play();
    }
    public void ShowFarshMachinSpeedParticle()
    {
        farshMachinSpeedParticle.Play();
        increaseSpeed.volume = (0.04f * slider.value);
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
            increaseSpeed.Play();
    }
    #endregion

    #region FileMachineParticle
    public void ShowFileMachinParticle()
    {
        fileMachinParticle.Play();
        increaseLevel.volume = (0.04f * slider.value);
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
            increaseLevel.Play();
    }
    public void ShowFileMachinSpeedParticle()
    {
        fileMachinSpeedParticle.Play();
        increaseSpeed.volume = (0.04f * slider.value);
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
            increaseSpeed.Play();
    }
    #endregion

    #region PackingMachineParticle
    public void ShowPackingMachinParticle()
    {
        packingMachinParticle.Play();
        increaseLevel.volume = (0.04f * slider.value);
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
            increaseLevel.Play();
    }
    public void ShowPackingMachinSpeedParticle()
    {
        packingMachinSpeedParticle.Play();
        increaseSpeed.volume = (0.04f * slider.value);
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
            increaseSpeed.Play();
    }
    public void ShowFarshPackingMachinParticle()
    {
        farshPackingMachinParticle.Play();
        increaseLevel.volume = (0.04f * slider.value);
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
            increaseLevel.Play();
    }
    public void ShowFarshPackingMachinSpeedParticle()
    {
        farshPackingMachinSpeedParticle.Play();
        increaseSpeed.volume = (0.04f * slider.value);
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
            increaseSpeed.Play();
    }
    public void ShowFilePackingMachinParticle()
    {
        filePackingMachinParticle.Play();
        increaseLevel.volume = (0.04f * slider.value);
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
            increaseLevel.Play();
    }
    public void ShowFilePackingMachinSpeedParticle()
    {
        filePackingMachinSpeedParticle.Play();
        increaseSpeed.volume = (0.04f * slider.value);
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
            increaseSpeed.Play();
    }

    #endregion
}
