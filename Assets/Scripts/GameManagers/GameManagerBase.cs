using Sirenix.OdinInspector;

namespace DefaultNamespace.GameManagers
{
    public abstract class GameManagerBase : SerializedMonoBehaviour
    {
        private void Awake()
        {
            OnInitialized();
        }

        public virtual void OnInitialized()
        {
            
        }
    }
}