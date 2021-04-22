using UnityEngine;
using UnityEngine.Audio;

namespace PurpleCable
{
    public static class VolumeHelper
    {
        public static float ToDecibels(float volume, int mute)
        {
            if (mute == 1)
                return -80;

            return Mathf.Lerp(-80, 0, volume);
        }

        public static void SetVolume(this AudioMixer audioMixer, string groupName, float defaultVolume)
        {
            audioMixer.SetFloat(groupName + "Volume", ToDecibels(PlayerPrefs.GetFloat(groupName + "Volume", defaultVolume), PlayerPrefs.GetInt("Mute" + groupName, 0)));
        }
    }
}
