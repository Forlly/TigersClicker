using System;
using System.Threading.Tasks;
using Project.DependencyInjection;
using Project.Security;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project
{
    [Serializable, HideLabel, Title("Meat")]
    public class Meat
    {
        public int Count
        {
            get => _count;
            set => _count = value;
        }

        private const string SAVE_ID = "Profile-Meat";

        [SerializeField] private int _count = 0;
        private ISaveSystem _saveSystem;

        public async void Save()
        {
            _saveSystem ??= await DI.GetAsync<ISaveSystem>();
            MeatSaveData data = new MeatSaveData
            {
                Count = Count
            };

            await _saveSystem.Save(SAVE_ID, data);
        }

        public async Task Load()
        {
            _saveSystem ??= await DI.GetAsync<ISaveSystem>();
            
            if (!_saveSystem.HasSave(SAVE_ID))
                return;
            MeatSaveData data = await _saveSystem.Load<MeatSaveData>(SAVE_ID);

            Count = data.Count;
            OnChanged(false);
        }

        public void Add(pint value)
        {
            Count += value;

            OnChanged(true);
        }

        public void Subtract(pint value)
        {
            Count -= value;

            OnChanged(true);
        }

        public void ChangeCount(pint value)
        {
            Count = value;

            OnChanged(true);
        }

        private void OnChanged(bool save)
        {
            EventBus.Instance.Send(new MeatChangedEvent(this));

            if (save)
                Save();
        }
    }
}