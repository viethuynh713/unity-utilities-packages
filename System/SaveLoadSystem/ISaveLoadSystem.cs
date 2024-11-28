using System;

namespace VPackages.System.SaveLoadSystem
{
    public interface ISaveLoadSystem
    {
        string CustomKey { set; }
        void SaveData<T>(string key, T value, Action<SaveLoadResult> callback = null);
        void GetData<T>(string key, Action<SaveLoadResult,T> callback);
        void ClearData(string key);
        void ClearAllData();
    }
}