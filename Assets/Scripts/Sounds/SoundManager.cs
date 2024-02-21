using System.Threading.Tasks;
using Project;
using Project.DependencyInjection;
using Sounds.Interface;
using UnityEngine;

namespace Sounds
{
    public class SoundManager : MonoBehaviour, ISoundManager
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioSource _backgroundMusicAudioSource;
        private static ISoundManager s_instance;
        public static ISoundManager GetInstance() => s_instance;
        private IProfile _profile;
        private async void Awake()
        {
            s_instance = this;
            _profile = await DI.GetAsync<IProfile>();
            SetSettingsFromProfile();
        }

        private void SetSettingsFromProfile()
        {
            if ( _profile.MusicIsActive)
            {
               TurnOnMusic(); 
            }
            else
            {
                TurnOffMusic();
            }

            if (_profile.SoundsIsActive)
            {
                TurnOnSounds(); 
            }
            else
            {
                TurnOffSounds();
            }
        }
        public void PlaySound(AudioClip sound)
        {
            _audioSource.clip = sound;
            _audioSource.Play();
        }

        public void PlayBackgroundMusic(bool isPlaying)
        {
            if (isPlaying)
            {
                _backgroundMusicAudioSource.Play();
            }
            else
            {
                _backgroundMusicAudioSource.Pause();
            }

        }
        
        public void TurnOnSounds()
        {
            _audioSource.enabled = true;
            _profile.SoundsIsActive = true;
        }
        public void TurnOffSounds()
        {
            _audioSource.enabled = false;
            _profile.SoundsIsActive = false;
        }
        public void TurnOnMusic()
        {
            _backgroundMusicAudioSource.enabled = true;
            _profile.MusicIsActive = true;
        }
        public void TurnOffMusic()
        {
            _backgroundMusicAudioSource.enabled = false;
            _profile.MusicIsActive = false;
        }
        

        public float GetSoundsSliderValue()
        {
            return _audioSource.volume;
        }

        public float GetMusicSliderValue()
        {
            return _backgroundMusicAudioSource.volume;
        }

        private void SetBackgroundMusic(AudioClip clip)
        {
            if (_backgroundMusicAudioSource.clip == clip)
                return;

            _backgroundMusicAudioSource.clip = clip;
            _backgroundMusicAudioSource.Play();
        }

        public async Task WaitForAudioToFinish()
        {
            while (_audioSource.isPlaying)
            {
                await Task.Yield();
            }
        }
    }
}