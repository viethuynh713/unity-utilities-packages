using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace VPackages.System.SaveLoadSystem.impl
{
    public class PlayerPrefSaveLoad : ISaveLoadSystem
    {
        private string _privateKey;

        public string CustomKey
        {
            set => _privateKey = value;
        }

        private string HashKey(string key) => _privateKey + key;

        public async UniTask<SaveLoadResult> SaveData<T>(string key, T value)
        {
            try
            {
                // Handle primary types separately
                if (value is int || value is float || value is bool || value is string)
                {
                    PlayerPrefs.SetString(HashKey(key), value.ToString());
                }
                else
                {
                    // For non-primary types, use JSON serialization
                    string json = JsonUtility.ToJson(value);
                    PlayerPrefs.SetString(HashKey(key), json);
                }
                
                // Ensure PlayerPrefs are saved
                PlayerPrefs.Save();
                
                return SaveLoadResult.Success;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to save data for key '{key}': {ex.Message}");
                return SaveLoadResult.Fail;
            }
        }

        public async UniTask<T> GetData<T>(string key)
        {
            try
            {
                if (PlayerPrefs.HasKey(HashKey(key)))
                {
                    string storedValue = PlayerPrefs.GetString(HashKey(key));

                    // Handle primary types separately
                    if (typeof(T) == typeof(int))
                    {
                        return (T)(object)int.Parse(storedValue);
                    }
                    else if (typeof(T) == typeof(float))
                    {
                        return (T)(object)float.Parse(storedValue);
                    }
                    else if (typeof(T) == typeof(bool))
                    {
                        return (T)(object)bool.Parse(storedValue);
                    }
                    else if (typeof(T) == typeof(string))
                    {
                        return (T)(object)storedValue;
                    }
                    else
                    {
                        // For non-primary types, use JSON deserialization
                        T value = JsonUtility.FromJson<T>(storedValue);
                        return value;
                    }
                }
                else
                {
                    // No data found, return default value
                    return default;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load data for key '{key}': {ex.Message}");
                return default;
            }
        }

        public void ClearData(string key)
        {
            if (PlayerPrefs.HasKey(HashKey(key)))
            {
                PlayerPrefs.DeleteKey(HashKey(key));
            }
            else
            {
                Debug.LogWarning($"Attempted to clear data for key '{key}', but it does not exist.");
            }
        }

        public void ClearAllData()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
