using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.Movement
{
    public interface IPlayerJumpedEventProvider
    {
        ObserverList<IPlayerJumpedEventHolder> OnPlayerJumpedObservers { get; }
    }
}
