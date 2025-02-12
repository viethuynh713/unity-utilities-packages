using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace VPackages.MultiLanguages
{
    public class LanguageControlBase : ILanguageControl
    {
        protected int currentIndexLanguage;
        
        public void Initialize()
        {
            if (TryGetLocaleIndex(LocalizationSettings.ProjectLocale.Identifier.Code, out int index))
            {
                currentIndexLanguage = index;
            }
        }

        public async UniTask SetLocaleAsync(int localeID)
        {
            await LocalizationSettings.InitializationOperation.Task;
            if (localeID >= 0 && localeID < LocalizationSettings.AvailableLocales.Locales.Count)
            {
                currentIndexLanguage = localeID;
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
            }
            else
            {
                Debug.LogWarning($"Invalid localeID: {localeID}");
            }
        }

        public async UniTask SetLocaleAsync(string localeCode)
        {
            await LocalizationSettings.InitializationOperation.Task;
            if (TryGetLocaleIndex(localeCode, out int index))
            {
                currentIndexLanguage = index;
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[currentIndexLanguage];
            }
            else
            {
                Debug.LogWarning($"Locale code '{localeCode}' not found.");
            }
        }

        public async UniTask ChangeNextLanguageAsync()
        {
            await LocalizationSettings.InitializationOperation.Task;
            currentIndexLanguage = (currentIndexLanguage + 1) % LocalizationSettings.AvailableLocales.Locales.Count;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[currentIndexLanguage];
        }

        public string GetStringFromDatabase(string table, string key)
        {
            return LocalizationSettings.StringDatabase.GetLocalizedString(table, key);
        }

        public T GetAssetFromDatabase<T>() where T : Object
        {
            // TODO: Implement asset retrieval
            return default;
        }

        public bool TryGetLocaleIndex(string localeCode, out int index)
        {
            var locales = LocalizationSettings.AvailableLocales.Locales;
            for (int i = 0; i < locales.Count; i++)
            {
                if (locales[i].Identifier.Code == localeCode)
                {
                    index = i;
                    return true;
                }
            }
            index = -1;
            return false;
        }
    }
}
