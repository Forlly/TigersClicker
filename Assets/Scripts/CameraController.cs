using UnityEngine;

namespace DefaultNamespace
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private AudioListener _audioListener;

        public void DeactivateCamera()
        {
            _audioListener.enabled = false;
        }
        
        public void ActivateCamera()
        {
            _audioListener.enabled = true;
        }
    }
}