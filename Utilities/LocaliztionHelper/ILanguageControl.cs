using Cysharp.Threading.Tasks;
using UnityEngine;

namespace VPackages.MultiLanguages
{
    public interface ILanguageControl
    {
        void Initialize();
        UniTask SetLocaleAsync(int localeID);
        UniTask SetLocaleAsync(string localeCode);
        UniTask ChangeNextLanguageAsync();
        string GetStringFromDatabase(string table, string key, params object[] args);
        public T GetAssetFromDatabase<T>() where T : Object;
        bool TryGetLocaleIndex(string localeCode, out int index);
    }
}