using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace PurpleCable
{
    public class UIVolumeSlider : UISlider
    {
        [SerializeField] AudioMixer AudioMixer = null;

        [SerializeField] string GroupName = null;

        [SerializeField] float DefaultVolume = 0.2f;

        [SerializeField] Toggle MuteToggle = null;

        protected override void Awake()
        {
            base.Awake();

            MuteToggle.isOn = PlayerPrefs.GetInt("Mute" + GroupName, 0) == 1;

            MuteToggle.onValueChanged.AddListener(OnMuteToggleValueChanged);
        }

        private void OnDestroy()
        {
            MuteToggle.onValueChanged.RemoveListener(OnMuteToggleValueChanged);
        }

        protected override float GetStartValue()
        {
            return PlayerPrefs.GetFloat(GroupName + "Volume", DefaultVolume);
        }

        protected override void OnValueChanged()
        {
            PlayerPrefs.SetFloat(GroupName + "Volume", Value);

            SetVolume();
        }

        private void OnMuteToggleValueChanged(bool value)
        {
            PlayerPrefs.SetInt("Mute" + GroupName, (value ? 1 : 0));

            SetVolume();
        }

        private void SetVolume()
        {
            AudioMixer.SetVolume(GroupName, DefaultVolume);
        }
    }
}
