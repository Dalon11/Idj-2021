using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Foundation.Character.StateMachine;

namespace Foundation.Sound
{
    public class PlayerSounds : AbstractService<ISoundsPlayer>, ISoundsPlayer
    {
        [SerializeField] private AudioSource _source;

        public float CurrentVolume => _currentVolume;
        private float _currentVolume = 0.5f;

        private string _soundsVolumeKey = "VOLUME_SOUND_KEY";

        private void Awake()
        {
            Load();
        }

        public void PlaySound(AudioClip clip)
        {
            _source.clip = clip;
            _source.Play();
        }

        public void SetVolume(float newVolume)
        {
            _currentVolume = newVolume;
            _source.volume = newVolume;

            PlayerPrefs.SetFloat(_soundsVolumeKey, _currentVolume);
            PlayerPrefs.Save();
        }

        public void Stop()
        {
            _source.Stop();
        }

        private void Load()
        {
            _currentVolume = PlayerPrefs.GetFloat(_soundsVolumeKey, _currentVolume);
        }
    }
}
