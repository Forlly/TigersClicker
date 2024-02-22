using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Project
{
    [CreateAssetMenu(fileName = "Profile", menuName = "ScriptableObjects/Profile")]
    public class Profile : ScriptableObject, IProfile
    {
        public Coins Coins { get => _coins; set => _coins = value;}
        public Meat Meat { get => _meat; set => _meat = value;}
        public bool MusicIsActive { get => _musicIsActive; set => _musicIsActive = value; }
        public bool SoundsIsActive { get => _soundsIsActive; set => _soundsIsActive = value; }
        public int CountOfBanks { get => _countOfBanks; set => _countOfBanks = value; }
        public int CountOfButcheries { get => _countOfButcheries; set => _countOfButcheries = value; }
        public int CountOfTigers { get => _countOfTigers; set => _countOfTigers = value; }
        public bool IsIntroductionCompleted { get => _isIntroductionCompleted; set => _isIntroductionCompleted = value; }
        public bool IsLearningCompleted { get => _isLearningCompleted; set => _isLearningCompleted = value; }

        [SerializeField] private Coins _coins;
        [SerializeField] private Meat _meat;
        [SerializeField] private bool _isIntroductionCompleted = false;
        [SerializeField] private bool _isLearningCompleted = false;
        [SerializeField] private bool _musicIsActive = true;
        [SerializeField] private bool _soundsIsActive = true;
        [SerializeField] private int _countOfBanks = 1;
        [SerializeField] private int _countOfButcheries = 1;
        [SerializeField] private int _countOfTigers = 1;
        private ISaveSystem _saveSystem;

        private static readonly Lazy<Task<IProfile>> s_instance =
            new Lazy<Task<IProfile>>(CreateProfileInstance, LazyThreadSafetyMode.ExecutionAndPublication);public static async Task<IProfile> GetInstance()
        {
            return await s_instance.Value;
        }
        private static async Task<IProfile> CreateProfileInstance()
        {
            IProfile iProfile = await Addressables.LoadAssetAsync<Profile>("Profile").Task;
            Profile profile = (Profile)iProfile;
            
            await profile.Load();

            return iProfile;
        }

        private async Task Load()
        {
            await _coins.Load();
            await _meat.Load();
        }
    }
}