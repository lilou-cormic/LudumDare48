using System.Collections;
using UnityEngine;

namespace PurpleCable
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicWithIntro : MonoBehaviour
    {
        private AudioSource AudioSource = null;

        [SerializeField] AudioClip Intro = null;

        [SerializeField] AudioClip Loop = null;

        private void Awake()
        {
            AudioSource = GetComponent<AudioSource>();
            AudioSource.clip = Loop;

            SetVolume();

            MusicPlayer.VolumeChanged += SetVolume;
        }

        private void OnDestroy()
        {
            MusicPlayer.VolumeChanged -= SetVolume;
        }

        private IEnumerator Start()
        {
            AudioSource.PlayOneShot(Intro);

            yield return new WaitForSecondsRealtime(Intro.length);

            AudioSource.Play();
        }

        private void SetVolume()
        {
            AudioSource.volume = MusicPlayer.Volume;
        }

        public void Stop()
        {
            StopAllCoroutines();
            AudioSource.Stop();
        }
    }
}
