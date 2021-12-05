using System;
using UnityEngine;

namespace Foundation.UI.Localization
{
    public class LocalizedStringsProvider: AbstractService<ILocalizationManager>, ILocalizationManager
    {
        [SerializeField] private LocalizationData _localizationData;

        public string CurrentLanguage => _currentLanguage;

        public ObserverList<ILanguageChangedEventHolder> LanguageChangedObservers => _languageChangedObservers;
        private ObserverList<ILanguageChangedEventHolder> _languageChangedObservers = new ObserverList<ILanguageChangedEventHolder>();

        private string _currentLanguage = "English";
        private string _languageKey = "LANGUAGE_KEY";

        public Action OnLocalizationChanged;

        private void Awake()
        {
            LoadSelectedLanguage();
        }

        public void SetLanguage(string language)
        {
            if (_currentLanguage == language)
                return;

            _currentLanguage = language;
            SaveSelectedLanguage();
            SentLocalizationChangedEvent();
        }

        public string GetLocalizedString(string key)
        {
            return _localizationData.GetLocalizationFor(_currentLanguage, key);
        }

        private void SaveSelectedLanguage()
        {
            PlayerPrefs.SetString(_languageKey, _currentLanguage);
        }

        private void LoadSelectedLanguage()
        {
            _currentLanguage = PlayerPrefs.GetString(_languageKey, "English");
        }

        private void SentLocalizationChangedEvent()
        {
            foreach (var obs in _languageChangedObservers.Enumerate())
                obs.OnLanguageChanged();
        }
    }
}