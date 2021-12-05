using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.UI.Localization
{
    public interface ILocalizationManager: ILocalizaedStringProvider
    {
        string CurrentLanguage { get; }
        void SetLanguage(string language);
        ObserverList<ILanguageChangedEventHolder> LanguageChangedObservers { get; }
    }
}
