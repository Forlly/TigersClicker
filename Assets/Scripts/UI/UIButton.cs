using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class UIButton : MonoBehaviour
    {
        [SerializeField] private bool _isShowButton;
        [SerializeField] private UIViewBase _view;
        [SerializeField] private List<UIViewBase> _closedView = new List<UIViewBase>();
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            
            if (_isShowButton)
            {
                _button.onClick.AddListener(ShowUIView);
            }
            else
            {
                _button.onClick.AddListener(HideUIView);
            }
           
        }

        private void ShowUIView()
        {
            _view.Show();
            HideAllView();
        }
        private void HideUIView()
        {
            _view.Hide();
            HideAllView();
        }

        private void HideAllView()
        {
            if (_closedView.Count > 0)
            {
                _closedView.ForEach(view => view.Hide());
            }
        }
    }
}