using UnityEngine;
using UnityEngine.Localization.Settings;
using VPackages.Core.SingletonPattern;
using Cysharp.Threading.Tasks;

namespace VPackages.MultiLanguages
{
    public class LanguageManagerSingleton : ManualSingletonMono<LanguageManagerSingleton>, ILanguageControl
    {
        private ILanguageControl _control;

        public override void Awake()
        {
            base.Awake();
            _control = new LanguageControlBase();
            Initialize();
        }

        public void Initialize()
        {
            _control.Initialize();
        }

        public async UniTask SetLocaleAsync(int localeID)
        {
            await _control.SetLocaleAsync(localeID);
        }

        public async UniTask SetLocaleAsync(string localeCode)
        {
            await _control.SetLocaleAsync(localeCode);
        }

        public async UniTask ChangeNextLanguageAsync()
        {
            await _control.ChangeNextLanguageAsync();
        }

        public string GetStringFromDatabase(string table, string key, params object[] args)
        {
            return _control.GetStringFromDatabase(table, key, args);
        }

        public T GetAssetFromDatabase<T>() where T : Object
        {
            return _control.GetAssetFromDatabase<T>();
        }

        public bool TryGetLocaleIndex(string localeCode, out int index)
        {
            return _control.TryGetLocaleIndex(localeCode, out index);
        }
    }
}