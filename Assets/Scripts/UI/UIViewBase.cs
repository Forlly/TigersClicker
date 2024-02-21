using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public abstract class UIViewBase : MonoBehaviour
    {
        [SerializeField] private float _fadeDuration = 0.3f;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private float _backgroundFadeDuration = 1.2f;
        private RectTransform _rectTransform;
        
        public virtual void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            transform.position = transform.parent.transform.position;
        }

        public void Show(bool withBackground = false)
        {
            if (_backgroundImage != null)
            {
                _backgroundImage.DOFade(0, 0);
            }
            
            if (_backgroundImage != null)
            {
                _rectTransform.DOScale(Vector3.one, _fadeDuration).SetEase(Ease.OutBack).OnComplete(() => _backgroundImage.DOFade(0.85f, _backgroundFadeDuration));
            }
            else
            {
                _rectTransform.DOScale(Vector3.one, _fadeDuration).SetEase(Ease.OutBack);
            }
        }
        public void Hide()
        {
            if (_backgroundImage != null)
            {
                _backgroundImage.DOFade(0, _backgroundFadeDuration).OnComplete(() => _rectTransform.DOScale(Vector3.zero, _fadeDuration).SetEase(Ease.InBack));
            }
            else
            {
                _rectTransform.DOScale(Vector3.zero, _fadeDuration).SetEase(Ease.InBack);
            }
        }
        public void InstantShow()
        {
            _rectTransform.DOScale(Vector3.one, 0);
        }
        public void InstantHide()
        {
            _rectTransform.DOScale(Vector3.zero, 0);
        }
    }
}