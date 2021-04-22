using UnityEngine;

namespace PurpleCable
{
    public class UIMusicVolumeSlider : UISlider
    {
        protected override float GetStartValue()
        {
            return MusicPlayer.Volume;
        }

        protected override void OnValueChanged()
        {
            MusicPlayer.Volume = Value;
        }
    }
}
