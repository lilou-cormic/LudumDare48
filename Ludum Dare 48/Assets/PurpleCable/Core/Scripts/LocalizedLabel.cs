using TMPro;
using UnityEngine;

namespace PurpleCable
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedLabel : MonoBehaviour
    {
        private TextMeshProUGUI LabelText = null;

        [SerializeField] string Label = null;

        private void Awake()
        {
            LabelText = GetComponent<TextMeshProUGUI>();
            TextManager.LanguageChanged += TextManager_LanguageChanged;
        }

        private void OnDestroy()
        {
            TextManager.LanguageChanged -= TextManager_LanguageChanged;
        }

        private void Start()
        {
            SetLabelText();
        }

        private void OnValidate()
        {
            LabelText = GetComponent<TextMeshProUGUI>();

            if (string.IsNullOrWhiteSpace(LabelText.text) && !string.IsNullOrWhiteSpace(Label))
                LabelText.text = Label;
            else if (!string.IsNullOrWhiteSpace(LabelText.text) && string.IsNullOrWhiteSpace(Label))
                Label = LabelText.text;
        }

        private void SetLabelText()
        {
            if (!string.IsNullOrWhiteSpace(Label))
                LabelText.text = TextManager.GetText(Label);
            else
                LabelText.text = Label;
        }

        private void TextManager_LanguageChanged()
        {
            SetLabelText();
        }
    }
}
