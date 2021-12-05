using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Foundation.Character.StateMachine;
using Zenject;

namespace Foundation.Sound
{
    public class PlayerFootstepsPlayer : AbstractBehaviour, IStateChangedEventHolder
    {
        [SerializeField] private float _delay = 0.5f;
        [SerializeField] private AudioClip[] _clips;

        [Inject] private ISoundsPlayer _soundPlayer;
        [Inject] private IStateMachine _stateChangedEventProvider;

        private Coroutine _playerCoroutine;
        private byte _currentClip = 0;

        private void Start()
        {
            Observe(_stateChangedEventProvider.OnStateChangedObservers);
        }

        public void OnStateChanged(IState newState)
        {
            if (newState is RunState)
                StartPlaying();
            else if (_playerCoroutine != null)
                Stop();
        }

        private void StartPlaying()
        {
            if (_playerCoroutine != null)
            {
                Debug.LogError("Trying to play sound twice");
                return;
            }

            _playerCoroutine = StartCoroutine(PlayCoroutine());
        }

        private void Stop()
        {
            StopCoroutine(_playerCoroutine);
            _playerCoroutine = null;
            _soundPlayer.Stop();
        }

        private IEnumerator PlayCoroutine()
        {
            while(true)
            {
                _soundPlayer.PlaySound(_clips[_currentClip]);

                yield return new WaitForSeconds(_delay);

                _currentClip++;
                if (_currentClip >= _clips.Length)
                    _currentClip = 0;
            }
        }
    }
}
