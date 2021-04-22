using UnityEngine;
using UnityEngine.Audio;

namespace PurpleCable
{
    public class AudioMixing : Singleton<AudioMixing>
    {
        [SerializeField] AudioMixer AudioMixer = null;

        protected override void Awake()
        {
            base.Awake();

            AudioMixer.SetVolume("Master", 1);
            AudioMixer.SetVolume("Music", 0.2f);
            AudioMixer.SetVolume("Sound", 0.5f);
            AudioMixer.SetVolume("Voice", 0.5f);
        }
    }
}
