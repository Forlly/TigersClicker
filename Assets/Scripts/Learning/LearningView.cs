using System.Collections.Generic;
using Project;
using Project.DependencyInjection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class LearningView : UIViewBase, IPointerClickHandler
    {
        [SerializeField] private Image _imageHero;
        [SerializeField] private TMP_Text _spokenText;
        [SerializeField] private List<LearningData> _learningDatas = new List<LearningData>();
        [SerializeField] private CanvasGroup _canvasGroup;
        private int _currentIndex = 0;
        private IProfile _profile;
        public override async void Awake()
        {
            base.Awake();
            _profile = await DI.GetAsync<IProfile>();
            
            if (_profile.IsLearningCompleted)
            {
               InstantHide(); 
            }
            else
            {
                SetNextData();
                InstantShow();
            }
        }

        private void SetNextData()
        {
            if (_currentIndex < _learningDatas.Count)
            {
                _imageHero.sprite = _learningDatas[_currentIndex].Hero;
                _spokenText.text = _learningDatas[_currentIndex].Text;
            }
            else
            {
                _profile.IsLearningCompleted = true;
                HideWithFade(_canvasGroup);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _currentIndex++;
            SetNextData();
        }
    }
}