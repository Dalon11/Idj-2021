using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Foundation.Movement;
using Foundation.Interactions;
using System;

namespace Foundation.Character.StateMachine
{
    public interface IState: IPlayerMovementInputHandler, IStateChangedEventProvider, IDisposable
    {
        bool CanInterract { get; }
        bool IsMoving { get; }
        bool IsInAir { get; }

        void Update(Rigidbody2D playerRigidbody);
    }
}
