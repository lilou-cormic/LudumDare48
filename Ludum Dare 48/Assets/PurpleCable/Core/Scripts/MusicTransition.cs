using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PurpleCable
{
    public class MusicTransition : Singleton<MusicTransition>
    {
        [SerializeField] float _TransitionSpeed = 0.01f;
        private static float TransitionSpeed = 0.01f;

        private static Dictionary<string, AudioSource> Musics = null;

        private static AudioSource _currentMusic = null;
        private static float _volume = 0.2f;

        [SerializeField] float _TransitionTime = 1f;
        private static float TransitionTime = 1f;

        private static float _volumeStep = 0.1f;

        private static bool _isTranstioning = false;

        protected override void Awake()
        {
            base.Awake();

            TransitionSpeed = _TransitionSpeed;

            SetVolume();

            if (_TransitionSpeed > 0)
                _volumeStep = _volume / (_TransitionTime / _TransitionSpeed);

            if (Musics == null)
            {
                Musics = Resources.LoadAll<AudioClip>("Musics").ToDictionary(x => x.name, y => CreateAudioSource(y));

                foreach (var music in Musics.Values)
                {
                    music.Play();
                }
            }

            MusicPlayer.VolumeChanged += SetVolume;
        }

        private void OnDestroy()
        {
            MusicPlayer.VolumeChanged -= SetVolume;
        }

        private AudioSource CreateAudioSource(AudioClip audioClip)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.playOnAwake = false;
            audioSource.volume = 0;
            audioSource.clip = audioClip;

            return audioSource;
        }

        private static void SetVolume()
        {
            _volume = MusicPlayer.Volume;

            if (_currentMusic != null)
                _currentMusic.volume = _volume;
        }

        public static void Play(AudioSource nextMusic, float volumMultiplier = 1, bool restart = false)
        {
            AudioSource currentMusic = _currentMusic;

            if (currentMusic == nextMusic)
                return;

            _currentMusic = nextMusic;

            if (currentMusic != null || nextMusic != null)
            {
                if (Instance != null)
                {
                    Instance.StopAllCoroutines();

                    Instance.StartCoroutine(TransitionMusic(currentMusic, nextMusic, volumMultiplier, restart));
                }
                else
                {
                    if (currentMusic != null)
                        currentMusic.volume = 0;

                    if (nextMusic != null)
                        nextMusic.volume = _volume * volumMultiplier;
                }
            }
        }

        public static void Play(string musicName, float volumMultiplier = 1, bool restart = false)
        {
            AudioSource nextMusic = null;

            if (!string.IsNullOrWhiteSpace(musicName) && Musics.ContainsKey(musicName))
                nextMusic = Musics[musicName];

            Play(nextMusic, volumMultiplier, restart);
        }

        private static IEnumerator TransitionMusic(AudioSource currentMusic, AudioSource nextMusic, float volumMultiplier, bool restart)
        {
            float volume = _volume * volumMultiplier;

            if (_isTranstioning || restart)
            {
                foreach (var music in Musics)
                {
                    if (music.Value != currentMusic)
                        music.Value.volume = 0;
                }

                if (restart && nextMusic != null)
                {
                    nextMusic.Stop();
                    nextMusic.Play();
                }
            }

            _isTranstioning = true;

            if (TransitionSpeed > 0)
            {
                bool ok = true;

                do
                {
                    ok = true;

                    if (currentMusic != null && currentMusic.volume > 0)
                    {
                        currentMusic.volume -= _volumeStep;
                        ok = false;

                        if (currentMusic.volume < 0)
                            currentMusic.volume = 0;
                    }

                    if (nextMusic != null && nextMusic.volume < volume)
                    {
                        nextMusic.volume += _volumeStep;
                        ok = false;

                        if (nextMusic.volume > volume)
                            nextMusic.volume = volume;
                    }

                    yield return new WaitForSeconds(TransitionSpeed);

                } while (!ok);
            }

            if (currentMusic != null)
                currentMusic.volume = 0;

            if (nextMusic != null)
                nextMusic.volume = volume;

            _isTranstioning = false;
        }
    }
}
