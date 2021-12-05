using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.Movement
{
    public interface IPlayerMovementInputProvider
    {
        IObserverList<IPlayerMovementInputHandler> InputObservers { get; }
    }
}