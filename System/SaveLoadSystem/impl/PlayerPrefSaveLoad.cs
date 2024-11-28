using System;
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

        public void SaveData<T>(string key, T value, Action<SaveLoadResult> callback = null)
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
                callback?.Invoke(SaveLoadResult.Success);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to save data for key '{key}': {ex.Message}");
                callback?.Invoke(SaveLoadResult.Fail);
            }
        }

        public void GetData<T>(string key, Action<SaveLoadResult, T> callback)
        {
            if (callback == null)
            {
                Debug.LogError("GetData callback is null.");
                return;
            }

            try
            {
                if (PlayerPrefs.HasKey(HashKey(key)))
                {
                    string storedValue = PlayerPrefs.GetString(HashKey(key));

                    // Handle primary types separately
                    if (typeof(T) == typeof(int))
                    {
                        callback.Invoke(SaveLoadResult.Success, (T)(object)int.Parse(storedValue));
                    }
                    else if (typeof(T) == typeof(float))
                    {
                        callback.Invoke(SaveLoadResult.Success, (T)(object)float.Parse(storedValue));
                    }
                    else if (typeof(T) == typeof(bool))
                    {
                        callback.Invoke(SaveLoadResult.Success, (T)(object)bool.Parse(storedValue));
                    }
                    else if (typeof(T) == typeof(string))
                    {
                        callback.Invoke(SaveLoadResult.Success, (T)(object)storedValue);
                    }
                    else
                    {
                        // For non-primary types, use JSON deserialization
                        T value = JsonUtility.FromJson<T>(storedValue);
                        callback.Invoke(SaveLoadResult.Success, value);
                    }
                }
                else
                {
                    // No data found, return default value
                    callback.Invoke(SaveLoadResult.Fail, default);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load data for key '{key}': {ex.Message}");
                callback.Invoke(SaveLoadResult.Fail, default);
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
