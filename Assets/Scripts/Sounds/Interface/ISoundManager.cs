using System.Threading.Tasks;
using Project.DependencyInjection;
using UnityEngine;

namespace Sounds.Interface
{
    public interface ISoundManager: IDIModule
    {
        void PlaySound(AudioClip sound);
        public void PlayBackgroundMusic(bool isPlaying);
        public void TurnOnSounds();
        public void TurnOffSounds();
        public void TurnOnMusic();
        public void TurnOffMusic();
        public float GetSoundsSliderValue();
        public float GetMusicSliderValue();
        Task WaitForAudioToFinish();
    }
}