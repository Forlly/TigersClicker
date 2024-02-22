using DefaultNamespace.Events;
using Project;
using Project.DependencyInjection;
using Sounds.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class SettingsView : MonoBehaviour
    {
        [SerializeField] private SettingsButton _musicOn;
        [SerializeField] private SettingsButton _musicOff;
        [SerializeField] private SettingsButton _soundOn;
        [SerializeField] private SettingsButton _soundOff;
        [SerializeField] private Sprite _backgroundTurnOn;
        [SerializeField] private Sprite _backgroundTurnOff;
        [SerializeField] private bool _isPanelInMainMenu = false;
        private ISoundManager _soundManager;
        private IProfile _profile;

        private async void Start()
        {
            _musicOn.GetComponent<Button>().onClick.AddListener(() => OnMusicToggleChanged(true));
            _musicOff.GetComponent<Button>().onClick.AddListener(() => OnMusicToggleChanged(false));
            
            _soundOn.GetComponent<Button>().onClick.AddListener(() => OnSoundsToggleChanged(true));
            _soundOff.GetComponent<Button>().onClick.AddListener(() => OnSoundsToggleChanged(false));

            _soundManager ??= DI.Get<ISoundManager>();

            _profile = await DI.GetAsync<IProfile>();
            SetButtonsValue();
            
            if (_isPanelInMainMenu)
            {
                EventBus.Instance.AddListener<OnChangeMusicValue>(ChangeMusicValue);
                EventBus.Instance.AddListener<OnChangeSoundsValue>(ChangeSoundsValue);
            }
        }
        private void ChangeMusicValue(OnChangeMusicValue e)
        {
            OnMusicToggleChanged(e.Value);
        }
        private void ChangeSoundsValue(OnChangeSoundsValue e)
        {
            OnSoundsToggleChanged(e.Value);
        }

        private void SetButtonsValue()
        {
            if (_profile.MusicIsActive)
            {
                UpdateButtonUI(_musicOn, _musicOff, true);
                OnMusicToggleChanged(true);
            }
            else
            {
                UpdateButtonUI(_musicOn, _musicOff, false);
                OnMusicToggleChanged(false);
            }

            if (_profile.SoundsIsActive)
            {
                UpdateButtonUI(_soundOn, _soundOff, true);
                OnSoundsToggleChanged(true);
            }
            else
            {
                UpdateButtonUI(_soundOn, _soundOff, false);
                OnSoundsToggleChanged(false);
            }
        }

        private void UpdateButtonUI(SettingsButton buttonOn,SettingsButton buttonOff, bool state)
        {
            if (state)
            {
                buttonOn.SetBackground(_backgroundTurnOn);
                buttonOff.SetBackground(_backgroundTurnOff);
            }
            else
            {
                buttonOn.SetBackground(_backgroundTurnOff);
                buttonOff.SetBackground(_backgroundTurnOn);
            }
        }

        private void OnMusicToggleChanged(bool newValue)
        {
            if (newValue)
            {
                _soundManager.TurnOnMusic();
            }
            else
            {
                _soundManager.TurnOffMusic();
            }
            UpdateButtonUI(_musicOn, _musicOff, newValue);
           
            if (!_isPanelInMainMenu)
            {
                EventBus.Instance.Send(new OnChangeMusicValue(newValue));
            }
        }

        private void OnSoundsToggleChanged(bool newValue)
        {
            if (newValue)
            {
                _soundManager.TurnOnSounds();
            }
            else
            {
                _soundManager.TurnOffSounds();
            }
            UpdateButtonUI(_soundOn, _soundOff, newValue);
            if (!_isPanelInMainMenu)
            {
                EventBus.Instance.Send(new OnChangeSoundsValue(newValue));
            }
        }
    }
}