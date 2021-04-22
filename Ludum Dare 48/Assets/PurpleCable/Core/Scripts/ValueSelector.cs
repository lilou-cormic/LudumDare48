using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PurpleCable
{
    public abstract class ValueSelector<T> : MonoBehaviour
        where T : struct
    {
        [SerializeField] TextMeshProUGUI ValueText = null;

        [SerializeField] protected T[] Values = null;

        [SerializeField] int _SelectedIndex = 0;
        public int SelectedIndex
        {
            get => _SelectedIndex;

            set
            {
                _SelectedIndex = value;
                SetText();
            }
        }

        private float _coolDown = 0f;

        protected virtual void Start()
        {
            SetText();
        }

        private void Update()
        {
            if (_coolDown > 0)
            {
                _coolDown -= Time.deltaTime;
                return;
            }

            if (FindObjectOfType<EventSystem>().currentSelectedGameObject == gameObject)
            {
                var horizontal = GetRawHorizontalAxis();

                if (Mathf.Abs(horizontal) > 0.01f)
                {
                    ChangeValue(horizontal);

                    _coolDown = 0.5f;
                }
            }
        }

        protected virtual float GetRawHorizontalAxis()
        {
            return Input.GetAxisRaw("Horizontal");
        }

        public void SetValue(T value)
        {
            SelectedIndex = Values.ToList().IndexOf(value);
        }

        public void ChangeValue(float horizontal)
        {
            if (horizontal > 0)
                SelectedIndex = Mathf.Clamp(SelectedIndex + 1, 0, Values.Length - 1);
            else
                SelectedIndex = Mathf.Clamp(SelectedIndex - 1, 0, Values.Length - 1);

            OnValueChanged(Values[SelectedIndex]);

            SetText();
        }

        protected abstract void OnValueChanged(T value);

        private void SetText()
        {
            string text = string.Empty;
            if (SelectedIndex > 0)
                text += "◄ ";

            text += GetDisplayText(Values[SelectedIndex]);

            if (SelectedIndex < Values.Length - 1)
                text += " ►";

            ValueText.text = text;
        }

        protected virtual string GetDisplayText(T value)
        {
            return TextManager.GetText(value.ToString());
        }
    }
}
