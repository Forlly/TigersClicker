using System.Collections.Generic;
using DefaultNamespace.TigersMechanic;
using DefaultNamespace.UI;
using Project;
using Project.DependencyInjection;
using UnityEngine;

namespace DefaultNamespace.GameManagers
{
    public class TigersGameManager : GameManagerBase
    {
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private UIView _infoView;
        [SerializeField] private TigersObjectPool _objectPool;
        [Header("Tigers")]
        [SerializeField] private List<TigerControls> _tigers = new List<TigerControls>();
        [Header("Upgrade Buttons")]
        [SerializeField] private UpgradeButton _addBankButton;
        [SerializeField] private UpgradeButton _addButcheryButton;
        [SerializeField] private UpgradeButton _addTigerButton;
        private IProfile _profile;
        public override async void OnInitialized()
        {
            _objectPool.Init();
            SubscribeToEvents();
            _profile = await DI.GetAsync<IProfile>();
        }

        private void SubscribeToEvents()
        {
            _addBankButton.GetButton().onClick.AddListener(() => TryAddBank(_addBankButton.GetData(), _addBankButton));
            _addButcheryButton.GetButton().onClick.AddListener(() => TryAddButchery(_addButcheryButton.GetData(), _addButcheryButton));
            _addTigerButton.GetButton().onClick.AddListener(() => TryAddTiger(_addTigerButton.GetData(), _addTigerButton));
        }

        private void Update()
        {
            _tigers.ForEach(controls => controls.Move());
        }

        private void TryAddBank(UpgradeData data, UpgradeButton button)
        {
            if (data.UpgradeIndex < data.Price.Count && CanBuyUpgradeByMeat(data.Price[data.UpgradeIndex]))
            {
                SubtractMeatFromProfile(data.Price[data.UpgradeIndex]);
                button.UpgradeData();
            }
            else
            {
                ShowInfoView(data);
            }
        }
        private void TryAddTiger(UpgradeData data, UpgradeButton button)
        {
            if (data.UpgradeIndex < data.Price.Count && CanBuyUpgradeByCoins(data.Price[data.UpgradeIndex]))
            {
                SubtractCoinsFromProfile(data.Price[data.UpgradeIndex]);
                button.UpgradeData();
                SpawnTiger();
            }
            else
            {
                ShowInfoView(data);
            }
        }

        private void SpawnTiger()
        {
            TigerControls newTiger = _objectPool.GetPooledObject();
            _tigers.Add(newTiger);
        }
        private void TryAddButchery(UpgradeData data, UpgradeButton button)
        {
            if (data.UpgradeIndex < data.Price.Count && CanBuyUpgradeByCoins(data.Price[data.UpgradeIndex]))
            {
                SubtractCoinsFromProfile(data.Price[data.UpgradeIndex]);
                button.UpgradeData();
            }
            else
            {
                ShowInfoView(data);
            }
        }
        private void ShowInfoView(UpgradeData data)
        {
            if (data.UpgradeIndex >= data.Price.Count)
            {
                _infoView.Show();
                _infoView.SetDescriptionText("You have reached maximum improvement!");
                return;
            }
            if (CanBuyUpgradeByMeat(data.Price[data.UpgradeIndex]))
            {
                _infoView.Show();
                _infoView.SetDescriptionText("You don't have enough resources to improve!");
            }
        }
        private void SubtractCoinsFromProfile(int value)
        {
            _profile.Coins.Subtract(value);
        }
        private void SubtractMeatFromProfile(int value)
        {
            _profile.Meat.Subtract(value);
        }
        private bool CanBuyUpgradeByCoins(int price)
        {
            if (_profile.Coins.Count >= price)
            {
                return true;
            }

            return false;
        }
        private bool CanBuyUpgradeByMeat(int price)
        {
            if (_profile.Meat.Count >= price)
            {
                return true;
            }

            return false;
        }
    }
}