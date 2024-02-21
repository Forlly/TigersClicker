using TMPro;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class UIView : UIViewBase
    {
        [SerializeField] private bool _showOnStart;
        [SerializeField] private TMP_Text _descriptionText;
        public override void Awake()
        {
            base.Awake();
            
            if (_showOnStart)
            {
                InstantShow();
            }
            else
            {
                InstantHide();
            }
        }

        public void SetDescriptionText(string text)
        {
            _descriptionText.text = text;
        }
    }
}