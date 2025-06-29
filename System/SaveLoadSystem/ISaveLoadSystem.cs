using System;
using Cysharp.Threading.Tasks;


namespace VPackages.System.SaveLoadSystem
{
    public interface ISaveLoadSystem
    {
        string CustomKey { set; }
        UniTask<SaveLoadResult> SaveData<T>(string key, T value);
        UniTask<T> GetData<T>(string key);
        void ClearData(string key);
        void ClearAllData();
    }
}