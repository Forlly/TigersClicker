using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefaultNamespace.Events;
using DefaultNamespace.TigersMechanic;
using DefaultNamespace.UI;
using Project;
using Project.DependencyInjection;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.GameManagers
{
    public class TigersGameManager : GameManagerBase
    {
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private UIView _infoView;
        [SerializeField] private TigersObjectPool _objectPool;
        [Header("Tigers")]
        [SerializeField] private List<TigerControls> _tigers = new List<TigerControls>();
        [SerializeField] private TigersUpgradeView _tigersUpgrade;
        [Header("Upgrade Buttons")]
        [SerializeField] private Button _speedUpButton;
        [SerializeField] private UpgradeButton _addBankButton;
        [SerializeField] private UpgradeButton _addButcheryButton;
        [SerializeField] private UpgradeButton _addTigerButton;
        [Header("Banks")]
        [SerializeField] private List<GameObject> _banks = new List<GameObject>();
        [Header("Butcheries")]
        [SerializeField] private List<GameObject> _butcheries = new List<GameObject>();
        private IProfile _profile;
        public override async void OnInitialized()
        {
            _objectPool.Init();
            SubscribeToEvents();
            _profile = await DI.GetAsync<IProfile>();
            LoadProfileData();
        }
        private void Update()
        {
            _tigers.ForEach(controls => controls.Move());
        }

        private async void LoadProfileData()
        {
            for (int i = 0; i < _profile.CountOfBanks; i++)
            {
                _banks[i].SetActive(true);
            }
            for (int i = 0; i < _profile.CountOfButcheries; i++)
            {
                _butcheries[i].SetActive(true);
            }

            float angle = 90;
            for (int i = 1; i < _profile.CountOfTigers; i++)
            {
                await Task.Delay(10);
                SpawnTiger(angle);
                angle += 50;
            }
        }
        private void SubscribeToEvents()
        {
            _speedUpButton.onClick.AddListener(() => SpeedUp());
            _addBankButton.GetButton().onClick.AddListener(() => TryAddBank(_addBankButton.GetData(), _addBankButton));
            _addButcheryButton.GetButton().onClick.AddListener(() => TryAddButchery(_addButcheryButton.GetData(), _addButcheryButton));
            _addTigerButton.GetButton().onClick.AddListener(() => TryAddTiger(_addTigerButton.GetData(), _addTigerButton));
            EventBus.Instance.AddListener<OnGetBonusFromBuilding>(GetBonus);
        }
        
        private void GetBonus(OnGetBonusFromBuilding e)
        {
            switch (e.BonusData.PaymentCategory)
            {
                case PaymentCategory.Meat:
                    _profile.Meat.Add(e.BonusData.RewardCount);
                    break;
                case PaymentCategory.Coin:
                    _profile.Coins.Add(e.BonusData.RewardCount);
                    break;
            }
        }

        private void TryAddBank(UpgradeData data, UpgradeButton button)
        {
            if (data.UpgradeIndex < data.Price.Count && CanBuyUpgradeByMeat(data.Price[data.UpgradeIndex]))
            {
                SubtractMeatFromProfile(data.Price[data.UpgradeIndex]);
                foreach (GameObject bank in _banks.Where(bank => !bank.activeInHierarchy))
                {
                    bank.SetActive(true);
                    _profile.CountOfBanks++;
                    button.UpgradeData();
                    return;
                }
                
                _profile.CountOfBanks++;
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
                
                _profile.CountOfTigers++;
                SpawnTiger();
                _tigersUpgrade.SetStars(_tigers.Count);
                _tigersUpgrade.Show();
            }
            else
            {
                ShowInfoView(data);
            }
        }

        private void SpawnTiger(float angle = 35)
        {
            TigerControls newTiger = _objectPool.GetPooledObject();
            newTiger.SetAngle(angle);
            _tigers.Add(newTiger);
        }

        private void SpeedUp()
        {
            EventBus.Instance.Send(new OnClickInputManager());
        }
        private void TryAddButchery(UpgradeData data, UpgradeButton button)
        {
            if (data.UpgradeIndex < data.Price.Count && CanBuyUpgradeByCoins(data.Price[data.UpgradeIndex]))
            {
                SubtractCoinsFromProfile(data.Price[data.UpgradeIndex]);
                foreach (GameObject butchery in _butcheries.Where(butchery => !butchery.activeInHierarchy))
                {
                    butchery.SetActive(true);
                    _profile.CountOfButcheries++;
                    button.UpgradeData();
                    return;
                }

                _profile.CountOfButcheries++;
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
            if (!CanBuyUpgradeByMeat(data.Price[data.UpgradeIndex]))
            {
                _infoView.Show();
                _infoView.SetDescriptionText("You don't have enough resources to improve!");
            }
            if (!CanBuyUpgradeByCoins(data.Price[data.UpgradeIndex]))
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