// From http://www.huwyss.com/level-it/localization-textmanager-for-unity3d

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class TextManager : MonoBehaviour
{
    [SerializeField] TMPro.TMP_FontAsset FontDefault = null;

    private const string English = "English";

    private static readonly IDictionary<string, string> TextTable = new Dictionary<string, string>();
    private static TextAsset _language;

    private static TextManager _instance;

    public static string CurrLanguage { get; private set; } = English;
    public static TMPro.TMP_FontAsset CurrFont { get; private set; } = _instance?.FontDefault;

    public static bool IsEnglish => CurrLanguage.Equals(English, StringComparison.InvariantCultureIgnoreCase);

    public static event Action LanguageChanged;

    private void Awake()
    {
        if (_instance == null)
        {
            // If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad(this);
            SetDefaultLanguage();
        }
        else
        {
            // If a Singleton already exists and you find
            // another reference in scene, destroy it!
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

    public static void SetDefaultLanguage()
    {
        string language = PlayerPrefs.GetString("Language");

        if (string.IsNullOrWhiteSpace(language))
        {
            var osLanguage = Application.systemLanguage;
            language = osLanguage.ToString();
        }

        SetLanguage(language);
    }

    public static void SetLanguage(string langFilename)
    {
        // Load a asset by its AssetName.
        // The text file must be located within 'Resources' subfolder in Unity ('Assets/Resources' in Visual Studio).
        TextAsset newLanguage = Resources.Load(@"Languages/" + langFilename) as TextAsset;

        CurrLanguage = langFilename;

        if (newLanguage == null)
        {
            newLanguage = Resources.Load<TextAsset>(@"Languages/" + English);

            CurrLanguage = English;
        }

        PlayerPrefs.SetString("Language", CurrLanguage);

        CurrFont = _instance.FontDefault;

        if (newLanguage == null)
            return;

        LoadLanguage(newLanguage);
        _language = newLanguage;
    }

    private static void LoadLanguage(TextAsset asset)
    {
        TextTable.Clear();

        var lineNumber = 1;
        using (var reader = new StringReader(asset.text))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                // Ignore comments and empty lines
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("'"))
                    continue;

                int firstSpaceIndex = line.IndexOf('=');
                if (firstSpaceIndex > 0)
                {
                    var key = line.Substring(0, firstSpaceIndex);
                    var val = line.Substring(firstSpaceIndex + 1);

                    if (key != null && val != null)
                    {
                        if (TextTable.ContainsKey(key))
                        {
                            Debug.LogWarning(String.Format("Duplicate key '{1}' in language file '{0}' detected.", asset.text, key));
                        }
                        TextTable.Add(key, val);
                    }
                }
                else
                {
                    Debug.LogWarning(String.Format("Format error in language file '{0}' on line {1}. Each line must be of format: key = value", asset.text, lineNumber));
                }

                lineNumber++;
            }
        }

        LanguageChanged?.Invoke();
    }

    public static string GetText(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            return key;

        string result;
        if (TextTable.ContainsKey(key))
        {
            result = TextTable[key];
        }
        else
        {
            Debug.LogWarning(String.Format("Couldn't find key '{0}' in dictionary.", key));
            result = key;
        }

        return result;
    }
}
