using UnityEngine;
using UnityEngine.UI;

namespace PurpleCable
{
    public class VsyncToggle : MonoBehaviour
    {
        [SerializeField] Toggle Toggle = null;

        private void Awake()
        {
            Toggle.isOn = PlayerPrefs.GetInt("Vsync", QualitySettings.vSyncCount) > 0;

            Toggle.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(bool value)
        {
            QualitySettings.vSyncCount = (value ? 1 : 0);

            PlayerPrefs.SetInt("Vsync", QualitySettings.vSyncCount);
        }
    }
}
