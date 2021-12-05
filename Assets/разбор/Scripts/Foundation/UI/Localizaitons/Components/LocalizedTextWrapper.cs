using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace Foundation.UI.Localization
{
    public class LocalizedTextWrapper : AbstractBehaviour, ILanguageChangedEventHolder
    {
        public TextMeshProUGUI InnetText;
        [SerializeField] private string _localizationKey;

        [Inject] private ILocalizationManager _localizationManager;

        private void Start()
        {
            UpdateLocale();
            Observe(_localizationManager.LanguageChangedObservers);            
        }

        public void SetLocalizationKey(string key)
        {
            _localizationKey = key;
            UpdateLocale();
        }

        private void UpdateLocale()
        {
            InnetText.text = _localizationManager.GetLocalizedString(_localizationKey);
        }

        public void OnLanguageChanged()
        {
            UpdateLocale();
        }
    }
}
