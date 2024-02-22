using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.TigersMechanic
{
    public class ResourceUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void SetText(string text)
        {
            _text.text = text;
        }
        public RectTransform GetRectTransform()
        {
            return _rectTransform;
        }
    }
}