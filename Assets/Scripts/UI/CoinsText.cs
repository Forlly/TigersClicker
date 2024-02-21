using Project;
using Project.DependencyInjection;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class CoinsText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinsText;
        private IProfile _profile;
        private async void Awake()
        {
            EventBus.Instance.AddListener<CoinsChangedEvent>(Refresh);
            
            _profile = await DI.GetAsync<IProfile>();
            UpdateText(_profile.Coins.Count.ToString());
        }

        private void UpdateText(string coinsText)
        {
            _coinsText.text = ShortenNumber(coinsText);
        }

        private string ShortenNumber(string numberStr)
        {
            if (!double.TryParse(numberStr, out double number))
            {
                return numberStr; 
            }
            
            string[] suffixes = { "", "K", "M", "B", "T", "P", "E" };
            int suffixIndex = 0;
            
            while (number >= 1000 && suffixIndex < suffixes.Length - 1)
            {
                number /= 1000;
                suffixIndex++;
            }
            
            return $"{number:0.#}{suffixes[suffixIndex]}";
        }


        public void Refresh(CoinsChangedEvent e)
        {
            UpdateText(e.Coins.Count.ToString());
        }
    }
}