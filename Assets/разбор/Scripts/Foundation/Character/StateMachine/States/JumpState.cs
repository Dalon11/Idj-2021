using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Foundation.Movement;
using Zenject;

namespace Foundation.Character.StateMachine
{
    public class JumpState : BaseState, IPlayerLandedEventHolder
    {
        [Inject] private ICharacterStatsProvider _statsProvider;

        public override bool CanInterract => false;
        public override bool IsMoving => true;
        public override bool IsInAir => true;

        public override ObserverList<IStateChangedEventHolder> OnStateChangedObservers => _stateObservers;
        private ObserverList<IStateChangedEventHolder> _stateObservers = new ObserverList<IStateChangedEventHolder>();

        private byte _currentAmountOfJumps = 0;

        private Vector2 _lastUserInput;
        private IPlayerLandedEventProvider _landedEventProvider;

        [Inject]
        private void InjectLandedEventProvider(IPlayerLandedEventProvider landedEventProvider)
        {
            _landedEventProvider = landedEventProvider;
            Observe(landedEventProvider.LandedEventObservers);
        }

        public JumpState(Vector2 input)
        {
            _lastUserInput = input;
        }

        public override void OnMovementInput(Vector2 input)
        {
            _lastUserInput = input;
        }

        public void OnPlayerLanded()
        {
            _currentAmountOfJumps = 0;
            foreach (var obs in _stateObservers.Enumerate())
                obs.OnStateChanged(_lastUserInput.x == 0 ? (IState)new IdleState() : new RunState());
        }

        public override void Update(Rigidbody2D playerRigidbody)
        {
            if (_lastUserInput != Vector2.zero)
            {
                var pVelocity = playerRigidbody.velocity;

                if (_lastUserInput.y != 0)
                {
                    if (_statsProvider.MaxAmountOfJumps > _currentAmountOfJumps)
                    {
                        _currentAmountOfJumps++;
                        pVelocity.y = _statsProvider.VerticalVelocity;
                    }
                }

                pVelocity.x = _statsProvider.HorizontalVelocity * _lastUserInput.x;
                playerRigidbody.velocity = pVelocity;
            }

        }

        public override void Dispose()
        {
            _landedEventProvider.LandedEventObservers.Remove(this);
            Observers.Clear();
        }
    }
}
