using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : BaseMenu
{
    private const int ON = 1;

    [SerializeField] private SaveDataSystem _saveDataSystem;

    [Space]
    [SerializeField] private Sprite _soundOnSprite;
    [SerializeField] private Sprite _soundOffSprite;
    [SerializeField] private Sprite _musicOnSprite;
    [SerializeField] private Sprite _musicOffSprite;
    [SerializeField] private Sprite _vibrateOnSprite;
    [SerializeField] private Sprite _vibrateOffSprite;
    [SerializeField] private Sprite _autorunOnSprite;
    [SerializeField] private Sprite _autorunOffSprite;

    [Space]
    [SerializeField] private Image _musicBtn;
    [SerializeField] private Image _soundBtn;
    [SerializeField] private Image _vibrateBtn;
    [SerializeField] private Image _autorunBtn;

    public override void SetData(AppData data)
    {
        base.SetData(data);
        SoundDesignerSystem.SoundMuted = (PlayerPrefs.GetInt("Sound", 0) == ON);
        SoundDesignerSystem.MusicMuted = (PlayerPrefs.GetInt("Music", 0) == ON);
        CheckSoundSettings();
        CheckMusicSettings();
        CheckVibroSettings();
        CheckAutoRunSettings();
    }

    public void ChangeSoundSettings()
    {
        SoundDesignerSystem.SoundMuted = !SoundDesignerSystem.SoundMuted;
        PlayerPrefs.SetInt("Sound", System.Convert.ToInt32(SoundDesignerSystem.SoundMuted));
        CheckSoundSettings();
    }

    public void ChangeMusicSettings()
    {
        SoundDesignerSystem.MusicMuted = !SoundDesignerSystem.MusicMuted;
        PlayerPrefs.SetInt("Music", System.Convert.ToInt32(SoundDesignerSystem.MusicMuted));
        CheckMusicSettings();
    }

    public void ChangeVibroSettings()
    {
        var value = PlayerPrefs.GetInt("Vibrate", 0) == ON ? 0 : 1;
        PlayerPrefs.SetInt("Vibrate", value);
        CheckVibroSettings();
    }

    public void ChangeAutoRunSettings()
    {
        var value = PlayerPrefs.GetInt("Autorun", 0) == ON ? 0 : 1;
        PlayerPrefs.SetInt("Autorun", value);
        CheckAutoRunSettings();
    }

    private void CheckSoundSettings()
    {
        _soundBtn.sprite = PlayerPrefs.GetInt("Sound",1) == 1 ? _soundOnSprite : _soundOffSprite;
        SoundDesignerSystem.SetMuteByBaseType(SoundBaseType.Sound, SoundDesignerSystem.SoundMuted);
    }

    private void CheckMusicSettings()
    {
        _musicBtn.sprite = SoundDesignerSystem.MusicMuted ? _musicOffSprite : _musicOnSprite;
        SoundDesignerSystem.SetMuteByBaseType(SoundBaseType.Music, SoundDesignerSystem.MusicMuted);
    }

    private void CheckVibroSettings()
    {
        _vibrateBtn.sprite = (PlayerPrefs.GetInt("Vibrate", 0) == ON) ? _vibrateOffSprite : _vibrateOnSprite;
        //Handheld.Vibrate();
        // [TODO] ѕосмотреть, где вообще будем использовать вибрацию, вынести в отдельное доступное поле или мб оставить в PlayerPrefs
    }

    private void CheckAutoRunSettings()
    {
        _autorunBtn.sprite = (PlayerPrefs.GetInt("Autorun", 0) == ON) ? _autorunOffSprite : _autorunOnSprite;
        // [TODO]
    }

    public void OnRemoveAdButton()
    {
        InterfaceManager.SetCurrentMenu(MenuName.Shop);
    }

    public void OnBackButton()
    {
        InterfaceManager.SetCurrentMenu(MenuName.Main);        
    }
}
