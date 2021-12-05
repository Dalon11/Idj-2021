using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class LocalizationData : ScriptableObject, ISerializationCallbackReceiver
{
    [System.Serializable]
    private struct LocalizationTempStruct
    {
        public string Language;
        public string[] IDs;
        public string[] Values;
    }

    [SerializeField] private List<LocalizationTempStruct> Strings;

    public Dictionary<string, Dictionary<string, string>> LocalizationValues;

    public const string AssetPath = "Assets/Config/LocalizationData.asset";

    public string GetLocalizationFor(string language, string key)
    {
        if (LocalizationValues == null)
        {
            Debug.LogError("Need to load localizations");
            return "error_no_localization_load";
        }

        if (LocalizationValues.ContainsKey(language))
        {
            var strings = LocalizationValues[language];
            if (strings.ContainsKey(key))
            {
                return strings[key];
            }
            else
            {
                Debug.LogError($"Localization data doesn't contain such key: {key}");
                return "no_such_key";
            }
        }
        else
        {
            Debug.LogError($"Localization data doesn't contain such language: {language}");
            return "no_such_language";
        }
    }

    public void OnBeforeSerialize()
    {
        if (LocalizationValues == null)
            return;

        Strings = new List<LocalizationTempStruct>();

        foreach(var key in LocalizationValues.Keys)
        {
            var idAndValues = LocalizationValues[key];

            var keys = new string[idAndValues.Keys.Count];
            var values = new string[idAndValues.Values.Count];

            for(int i = 0; i < keys.Length; i++)
            {
                keys[i] = idAndValues.Keys.ElementAt(i);
                values[i] = idAndValues.Values.ElementAt(i);
            }

            var localizaedInfo = new LocalizationTempStruct
            {
                Language = key,
                IDs = keys,
                Values = values
            };

            Strings.Add(localizaedInfo);
        }
    }

    public void OnAfterDeserialize()
    {
        LocalizationValues = new Dictionary<string, Dictionary<string, string>>();

        foreach (var str in Strings)
        {
            var lang = str.Language;

            var idAndValues = new Dictionary<string, string>();

            for(var i = 0; i < str.IDs.Length; i++)
            {
                idAndValues.Add(str.IDs[i], str.Values[i]);
            }

            LocalizationValues.Add(lang, idAndValues);
        }
    }
}
