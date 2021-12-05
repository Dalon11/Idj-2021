using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Foundation.Sound
{
    public class OptionsMenu : AbstractBehaviour
    {
        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private Slider _soundsVolumeSlider;

        [Inject] private ISoundsPlayer _soundOptions;
        [Inject] private IMusicPlayer _musicOptions;

        private void Start()
        {
            _musicVolumeSlider.value = _soundOptions.CurrentVolume;
            _soundsVolumeSlider.value = _musicOptions.CurrentVolume;

            _musicVolumeSlider.onValueChanged.AddListener(volume =>
            {
                _musicOptions.SetVolume(volume);
            });

            _soundsVolumeSlider.onValueChanged.AddListener(volume =>
            {
                _soundOptions.SetVolume(volume);
            });
        }
    }
}
