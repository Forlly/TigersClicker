using DefaultNamespace.Events;
using DefaultNamespace.UI;
using Project;
using Project.DependencyInjection;
using UnityEngine;

namespace DefaultNamespace.GameManagers
{
    public class BootGameManager : GameManagerBase
    {
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private GameObject _eventSystem;
        [SerializeField] private UIViewBase _gameViewBase;
        private IProfile _profile;
        
        public override async void OnInitialized()
        {
            _profile ??= await DI.GetAsync<IProfile>();
            _cameraController.ActivateCamera();

            EventBus.Instance.AddListener<TransitionBetweenScenes>(DeactivateCamera, false);
        }
        
        private void DeactivateCamera(TransitionBetweenScenes e)
        {
            if (e.NameScene == "MenuScene")
            {
                _cameraController.ActivateCamera();
                _eventSystem.gameObject.SetActive(true);
                _gameViewBase.gameObject.SetActive(true);
            }
            else
            {
                _cameraController.DeactivateCamera();
                _eventSystem.gameObject.SetActive(false);
                _gameViewBase.gameObject.SetActive(false);
            }
        }
    }
}