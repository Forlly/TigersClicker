using UnityEngine;

namespace DefaultNamespace.UI
{
    public class UIView : UIViewBase
    {
        [SerializeField] private bool _showOnStart;
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
    }
}