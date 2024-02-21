using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Project.Services
{
    public class SceneLoader : MonoBehaviour
    {
        public static Action<string> LoadSceneEvent;
        public static Action MainSceneLoadedEvent;
        public static SceneLoader Instance;
        public static AsyncOperationHandle<SceneInstance> CurrentSceneInstanceHandle { get; set; }

        [SerializeField]
        private bool _loadOnStart;

        [SerializeField]
        private string _sceneKey = "My Scene";
        private static AsyncOperationHandle<SceneInstance> _currentMechanicScene;

        public static async Task UnloadCurrentScene()
        {
            await Addressables.UnloadSceneAsync(CurrentSceneInstanceHandle).Task;
        }

        public static async Task LoadScene(string key)
        {
            CurrentSceneInstanceHandle = Addressables.LoadSceneAsync(key);
            await CurrentSceneInstanceHandle.Task;
            
            if (GetLoadedScene(CurrentSceneInstanceHandle).name == "Main Menu")
            {
                await Task.Delay(1000);
                MainSceneLoadedEvent?.Invoke();
            }
        }

        public static async Task<AsyncOperationHandle<SceneInstance>> LoadSceneAdditiveAsync(string key)
        {
            AsyncOperationHandle<SceneInstance> sceneLoadHandle = Addressables.LoadSceneAsync(key,  LoadSceneMode.Additive);
            await sceneLoadHandle.Task;
            SceneManager.SetActiveScene(sceneLoadHandle.Result.Scene);
            _currentMechanicScene = sceneLoadHandle;
            return sceneLoadHandle;
        }
        
        public static async Task UnloadScenesAsync(AsyncOperationHandle<SceneInstance> scene)
        {
            AsyncOperationHandle<SceneInstance> unloadOperation = Addressables.UnloadSceneAsync(scene);
            await unloadOperation.Task;
        }
        public static async Task UnloadCurrentMechanicScenesAsync()
        {
            AsyncOperationHandle<SceneInstance> unloadOperation = Addressables.UnloadSceneAsync(_currentMechanicScene);
            await unloadOperation.Task;
        }

        public async Task LoadScene()
        {
            await LoadScene(_sceneKey);
        }
        
        
        
        public static Scene GetLoadedScene(AsyncOperationHandle<SceneInstance> handle)
        {
            Scene loadedScene = handle.Result.Scene;

            return loadedScene;
        }

        private async void Start()
        {
            if (Instance == null)
                Instance = this;

            if (_loadOnStart)
                await LoadScene();
        }
    }
}
