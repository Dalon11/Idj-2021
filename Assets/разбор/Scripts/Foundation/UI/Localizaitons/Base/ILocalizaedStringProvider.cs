using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.UI.Localization
{
    public interface ILocalizaedStringProvider
    {
        string GetLocalizedString(string key);
    }
}
