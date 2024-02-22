using System.Collections.Generic;
using DefaultNamespace.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.TigersMechanic
{
    public class TigersUpgradeView : UIViewBase
    {
        [SerializeField] private List<Image> _stars = new List<Image>();
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Sprite _fullStar;
        [SerializeField] private Sprite _emptyStar;
        public override void Awake()
        {
            base.Awake();
            InstantHide();
        }

        public void SetStars(int countOfStars)
        {
            if (countOfStars >= _stars.Count || countOfStars < 0)
            {
                return;
            }
            for (int i = 0; i < countOfStars; i++)
            {
                _stars[i].sprite = _fullStar;
            }
            for (int i = countOfStars; i < _stars.Count; i++)
            {
                _stars[i].sprite = _emptyStar;
            }

            UpdateText(countOfStars);
        }

        private void UpdateText(int countOfStars)
        {
            _text.text = $"tiger boost for {countOfStars} stars!";
        }
    }
}