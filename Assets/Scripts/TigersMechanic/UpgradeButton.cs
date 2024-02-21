using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.TigersMechanic
{
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField] private UpgradeData _data;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _priceText;

        private void Start()
        {
            UpdatePriceText();
        }

        public UpgradeData GetData()
        {
            return _data;
        }
        public void UpgradeData()
        {
            _data.UpgradeIndex++;
            UpdatePriceText();
        }
        public Button GetButton()
        {
            return _button;
        }

        private void UpdatePriceText()
        {
            if (_data.UpgradeIndex < _data.Price.Count)
            {
                _priceText.text = _data.Price[_data.UpgradeIndex].ToString();
            }
            else
            {
                _priceText.text = "MAX";
            }
        }
    }
}