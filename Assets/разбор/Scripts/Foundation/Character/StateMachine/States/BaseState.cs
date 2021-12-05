using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.Character.StateMachine
{
    public abstract class BaseState : IState
    {
        public abstract bool CanInterract { get; }

        public abstract bool IsMoving { get; }

        public abstract bool IsInAir { get; }

        public abstract ObserverList<IStateChangedEventHolder> OnStateChangedObservers { get; }

        public abstract void OnMovementInput(Vector2 input);

        public abstract void Update(Rigidbody2D playerRigidbody);

        ObserverHandleManager observers;
        protected ObserverHandleManager Observers
        {
            get
            {
                if (observers == null)
                    observers = new ObserverHandleManager();
                return observers;
            }
        }

        protected void Observe<O>(IObserverList<O> observable)
            where O : class
        {
            Observers.Observe(observable, this as O);
        }

        public abstract void Dispose();
    }
}
