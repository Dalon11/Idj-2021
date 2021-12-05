using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Foundation.UI.Localization
{
    public class LanguageChangeHandler : AbstractBehaviour, ILanguageChangedEventHolder
    {
        [SerializeField] private Button _russianLangButton;
        [SerializeField] private Button _engLangButton;
        [SerializeField] private LayerMask _layer;

        [Space]
        [SerializeField] private Color _selectedLangColor = Color.red;
        [SerializeField] private Color _unselectedLangColor = Color.grey;

        [Space]
        [SerializeField] private string _russianLanguage = "Russian";
        [SerializeField] private string _englishLanguage = "English";

        [Inject] private ILocalizationManager _localizationManager;

        private void Start()
        {
            _russianLangButton.onClick.AddListener(() => _localizationManager.SetLanguage(_russianLanguage));
            _engLangButton.onClick.AddListener(() => _localizationManager.SetLanguage(_englishLanguage));

            ColorSelectedLang();

            Observe(_localizationManager.LanguageChangedObservers);
        }

        private void ColorSelectedLang()
        {
            if (_localizationManager.CurrentLanguage == _russianLanguage)
            {
                _russianLangButton.GetComponent<Image>().color = _selectedLangColor;
                _engLangButton.GetComponent<Image>().color = _unselectedLangColor;
            }
            else
            {
                _russianLangButton.GetComponent<Image>().color = _unselectedLangColor;
                _engLangButton.GetComponent<Image>().color = _selectedLangColor;
            }
        }

        public void OnLanguageChanged()
        {
            ColorSelectedLang();
        }
    }
}
