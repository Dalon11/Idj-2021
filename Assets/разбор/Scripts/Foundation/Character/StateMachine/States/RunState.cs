using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Foundation.Character.StateMachine
{
    public class RunState : BaseState
    {
        [Inject] private ICharacterStatsProvider _statsProvider;

        public override bool CanInterract => true;
        public override bool IsMoving => true;
        public override bool IsInAir => false;

        public override ObserverList<IStateChangedEventHolder> OnStateChangedObservers => _stateChangedObservers;
        private ObserverList<IStateChangedEventHolder> _stateChangedObservers = new ObserverList<IStateChangedEventHolder>();

        private Vector2 _lastUserInput = Vector2.zero;

        public override void OnMovementInput(Vector2 input)
        {
            if (input == Vector2.zero)
                TransitionToState(new IdleState());
            else if (input.y != 0)
                TransitionToState(new JumpState(input));
            else           
                _lastUserInput = input;
            
        }

        private void TransitionToState(IState newState)
        {
            foreach (var obs in _stateChangedObservers.Enumerate())
                obs.OnStateChanged(newState);
        }

        public override void Update(Rigidbody2D playerRigidbody)
        {
            _lastUserInput.y = 0;

            var pVelocity = playerRigidbody.velocity;
            pVelocity.x = _statsProvider.HorizontalVelocity * _lastUserInput.x;
            playerRigidbody.velocity = pVelocity;
        }

        public override void Dispose()
        {
        }
    }
}
