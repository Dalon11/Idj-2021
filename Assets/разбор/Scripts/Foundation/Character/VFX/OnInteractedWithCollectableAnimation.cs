using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Foundation.Character.Vfx
{
    public class OnInteractedWithCollectableAnimation : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _onCollectedParticles;
        [SerializeField] private Light2D _onCollectedLight;
        [SerializeField] private float _lightAnimationDuration = 0.3f;
        [SerializeField] private float _lightRadius = 3f;

        private Coroutine _animationCoroutine;

        
        public void Play()
        {
            _onCollectedParticles.Play();

            if (_animationCoroutine == null)
                StartCoroutine(LightAnimationCoroutine());
        }

        private IEnumerator LightAnimationCoroutine()
        {
            var step = _lightRadius / _lightAnimationDuration * 2;

            while(_onCollectedLight.pointLightOuterRadius <= _lightRadius)
            {
                _onCollectedLight.pointLightOuterRadius += step * Time.deltaTime;
                yield return null;
            }

            while(_onCollectedLight.pointLightOuterRadius > 0)
            {
                _onCollectedLight.pointLightOuterRadius -= step * Time.deltaTime;
                yield return null;
            }

            _animationCoroutine = null;
        }
    }
}
