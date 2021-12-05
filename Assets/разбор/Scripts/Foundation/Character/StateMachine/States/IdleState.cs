using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.Character.StateMachine
{
    public class IdleState : BaseState, IStateChangedEventProvider
    {
        public override bool CanInterract => true;
        public override bool IsMoving => false;
        public override bool IsInAir => false;

        public override ObserverList<IStateChangedEventHolder> OnStateChangedObservers => _observers;
        private ObserverList<IStateChangedEventHolder> _observers = new ObserverList<IStateChangedEventHolder>();

        public override void OnMovementInput(Vector2 input)
        {
            if (input.y != 0)
                TransitionToState(new JumpState(input));
            else if (input.x != 0)
                TransitionToState(new RunState());
        }

        private void TransitionToState(IState newState)
        {
            foreach (var obs in _observers.Enumerate())
                obs.OnStateChanged(newState);
        }

        public override void Update(Rigidbody2D playerRigidbody)
        {
        }

        public override void Dispose()
        {
        }
    }
}
