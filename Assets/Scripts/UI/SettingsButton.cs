using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class SettingsButton : MonoBehaviour
    {
        [SerializeField] private Image _backgroundImage;

        public void SetBackground(Sprite sprite)
        {
            _backgroundImage.sprite = sprite;
        }
    }
}