using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.Sound
{
    public interface ISoundsPlayer
    {
        float CurrentVolume { get; }
        void SetVolume(float newVolume);

        void PlaySound(AudioClip clip);
        void Stop();
    }
}