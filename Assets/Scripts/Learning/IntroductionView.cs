using DefaultNamespace.Events;
using DG.Tweening;
using Project;
using Project.DependencyInjection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class IntroductionView : UIViewBase, IPointerClickHandler
    {
        [SerializeField] private Image _imageHand;
        [SerializeField] private CanvasGroup _canvasGroup;
        private IProfile _profile;
        public override async void Awake()
        {
            base.Awake();
            _profile = await DI.GetAsync<IProfile>();
            
            if (_profile.IsIntroductionCompleted)
            {
                InstantHide(); 
            }
            else
            {
                AnimateHand();
                InstantShow();
            }
        }
        private void AnimateHand()
        {
            float startY = _imageHand.rectTransform.anchoredPosition.y;
            float endY = startY + 50; 
            _imageHand.rectTransform.DOAnchorPosY(endY, 1f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            _profile.IsIntroductionCompleted = true;
            HideWithFade(_canvasGroup);
            EventBus.Instance.Send(new OnClickInputManager());
        }
    }
}