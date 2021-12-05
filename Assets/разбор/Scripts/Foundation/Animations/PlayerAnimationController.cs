using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Foundation.Movement;
using Foundation.Character.StateMachine;
using Zenject;

namespace Foundation.Animations
{
    public class PlayerAnimationController : AbstractBehaviour, IStateChangedEventHolder, IPlayerLandedEventHolder
    {
        [SerializeField] private Animator _playerAnimator;

        [Inject] private IStateMachine _stateChangedEventProvider;
        [Inject] private IPlayerLandedEventProvider _landedEventProvider;

        private void Start()
        {
            Observe(_stateChangedEventProvider.OnStateChangedObservers);
            Observe(_landedEventProvider.LandedEventObservers);
            PlayIdleAnimation();
        }

        public void OnStateChanged(IState newState)
        {
            if (newState is IdleState)
                PlayIdleAnimation();
            else if (newState is RunState)
                PlayRunAnimation();
            else if (newState is JumpState)
                PlayJumpAnimation();
            else
                Debug.LogError($"animation not implemented for state {newState}");
        }

        private void PlayIdleAnimation()
        {
            _playerAnimator.SetBool("idle", true);
            _playerAnimator.SetBool("run", false);
        }

        private void PlayRunAnimation()
        {
            _playerAnimator.SetBool("idle", false);
            _playerAnimator.SetBool("run", true);
        }

        private void PlayJumpAnimation()
        {
            _playerAnimator.SetBool("idle", false);
            _playerAnimator.SetBool("run", false);
            _playerAnimator.SetTrigger("jump");
            _playerAnimator.ResetTrigger("land");
        }

        public void OnPlayerLanded()
        {
            _playerAnimator.SetTrigger("land");
        }
    }
}
