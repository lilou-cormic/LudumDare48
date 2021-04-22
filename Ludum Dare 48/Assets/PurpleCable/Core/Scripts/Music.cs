using UnityEngine;

namespace PurpleCable
{
    [DisallowMultipleComponent]
    public class Music : MonoBehaviour
    {
        [SerializeField] string _MusicName = null;
        string MusicName => _MusicName;

        [SerializeField] AudioSource _AudioSource = null;
        AudioSource AudioSource => _AudioSource;

        [SerializeField] float VolumeMultiplier = 1;

        [SerializeField] bool HasLowPassFilter = false;

        [SerializeField] bool Restart = false;

        private void Start()
        {
            if (!string.IsNullOrWhiteSpace(MusicName))
                MusicTransition.Play(MusicName, VolumeMultiplier, Restart);

            if (AudioSource != null && AudioSource.clip != null)
                MusicTransition.Play(AudioSource, VolumeMultiplier, Restart);
            
            var lowPassFilter = FindObjectOfType<AudioLowPassFilter>();

            if (lowPassFilter != null)
                FindObjectOfType<AudioLowPassFilter>().enabled = HasLowPassFilter;
        }
    }
}
