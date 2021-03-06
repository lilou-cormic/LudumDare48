using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PurpleCable
{
    public abstract class UISlider : MonoBehaviour
    {
        [SerializeField]
        private Slider Slider = null;

        [SerializeField]
        private TextMeshProUGUI LabelText = null;

        [SerializeField]
        private string Label = null;

        protected float Value => Slider.value;

        protected virtual void Awake()
        {
            Slider.onValueChanged.AddListener(delegate { OnValueChanged(); });
        }

        protected virtual void Start()
        {
            Slider.value = GetStartValue();
        }

        protected virtual void OnValidate()
        {
            if (LabelText != null)
                LabelText.text = Label;
        }

        protected abstract float GetStartValue();

        protected abstract void OnValueChanged();
    }
}
