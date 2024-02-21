using Newtonsoft.Json.Linq;
using Project.Security;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Project
{
    [CreateAssetMenu(fileName = "SaveSystem", menuName = "ScriptableObjects/SaveSystem")]
    public class SaveSystem : ScriptableObject, ISaveSystem
    {
        private static readonly Lazy<Task<ISaveSystem>> s_instance = new Lazy<Task<ISaveSystem>>(CreateSaveSystemInstance, LazyThreadSafetyMode.ExecutionAndPublication);

        [SerializeField]
        private pstring _encryptionKey = "abcd451d5d1f5s";

        private string _mainDirectory;
        private string _fileFormat;

        private FastEncryption _fastEncryption;

        public static async Task<ISaveSystem> GetInstance()
        {
            return await s_instance.Value;
        }

        public async Task Save<T>(string saveId, T saveData)
        {
            CheckMainDirectory();

            string path = GetPathBySaveId(saveId);
            JObject json = JObject.FromObject(saveData);
            pstring encryptedJson = json.ToString();

            bool wasWritten = false;

            while (!wasWritten)
            {
                try
                {
                    await File.WriteAllTextAsync(path, encryptedJson);

                    wasWritten = true;
                }
                catch { }
            }
        }

        public async Task<T> Load<T>(string saveId)
        {
            string path = GetPathBySaveId(saveId);

            if (!HasSave(saveId))
                return default;

            using StreamReader reader = new StreamReader(path);

            pstring json = await reader.ReadToEndAsync();
            //pstring decryptedJson = _fastEncryption.DecryptString(json);
            pstring decryptedJson = json;

            return JObject.Parse(decryptedJson).ToObject<T>();
        }

        public bool HasSave(string saveId)
        {
            string path = GetPathBySaveId(saveId);

            return File.Exists(path);
        }

        public bool ClearSave(string saveId)
        {
            if (!HasSave(saveId))
                return false;

            string path = GetPathBySaveId(saveId);

            File.Delete(path);

            return true;
        }

        private static async Task<ISaveSystem> CreateSaveSystemInstance()
        {
            SaveSystem saveSystem = await Addressables.LoadAssetAsync<SaveSystem>("SaveSystem").Task;

#if UNITY_EDITOR
            saveSystem._mainDirectory = Path.Combine(Application.dataPath, "Saves");
            saveSystem._fileFormat = ".txt";
#else
            saveSystem._mainDirectory = Path.Combine(Application.persistentDataPath, "Cat-Pictures");
            saveSystem._fileFormat = ".png";
#endif

            saveSystem._fastEncryption = new FastEncryption(saveSystem._encryptionKey);

            return saveSystem;
        }

        private void CheckMainDirectory()
        {
            if (Directory.Exists(_mainDirectory))
                return;

            Directory.CreateDirectory(_mainDirectory);
        }

        private string GetPathBySaveId(string saveId)
        {
            string encryptedSaveId = EncryptSaveId(saveId);

            return $"{Path.Combine(_mainDirectory, encryptedSaveId)}{_fileFormat}";
        }

        private string EncryptSaveId(string saveId)
        {
            return saveId;
            saveId = saveId.ToLower();

            string encryptedSaveId = string.Empty;

            for (int i = 0; i < saveId.Length - 1; i += 2)
            {
                encryptedSaveId += saveId[i + 1];
                encryptedSaveId += saveId[i];
            }

            return encryptedSaveId;
        }
    }
}
