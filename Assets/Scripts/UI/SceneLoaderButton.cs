using DefaultNamespace.Events;
using Project;
using Project.DependencyInjection;
using Project.Services;
using Sounds.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class SceneLoaderButton : MonoBehaviour
    {
        [SerializeField] private bool _isMechanicScene;
        [SerializeField] private bool _isReloadMechanic;
        [SerializeField] private Sprite _background;
        [SerializeField] private string _sceneKey;
        [SerializeField] private AudioClip _audioClip;
        
        private ISoundManager _soundManager;
        private IProfile _profile;
        private async void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
            _soundManager =  DI.Get<ISoundManager>();
            _profile = await DI.GetAsync<IProfile>();

            if (_isMechanicScene)
            {
                _sceneKey = $"Scene-Machanics-{_sceneKey}";
            }
        }

        private void OnClick()
        {
            _soundManager.PlaySound(_audioClip);
            LoadScene();
        }

        private async void LoadScene()
        {
            EventBus.Instance.Send(new TransitionBetweenScenes(_sceneKey));
            if (_isMechanicScene)
            {
                await SceneLoader.LoadSceneAdditiveAsync(_sceneKey);
            }
            else if (_isReloadMechanic)
            {
                await SceneLoader.UnloadCurrentMechanicScenesAsync();
                await SceneLoader.LoadSceneAdditiveAsync(_sceneKey);
            }
            else
            {
                await SceneLoader.UnloadCurrentMechanicScenesAsync();
            }
        }
    }
}