using TMPro;
using UnityEngine;

namespace PurpleCable
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedFont : MonoBehaviour
    {
        private TextMeshProUGUI Text = null;

        private void Awake()
        {
            Text = GetComponent<TextMeshProUGUI>();
            TextManager.LanguageChanged += TextManager_LanguageChanged;
        }

        private void OnDestroy()
        {
            TextManager.LanguageChanged -= TextManager_LanguageChanged;
        }

        private void Start()
        {
            SetTextFont();
        }

        private void SetTextFont()
        {
            Text.font = TextManager.CurrFont;
        }

        private void TextManager_LanguageChanged()
        {
            SetTextFont();
        }
    }
}
