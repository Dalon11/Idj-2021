using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.Movement
{
    public interface IPlayerLandedEventProvider
    {
        ObserverList<IPlayerLandedEventHolder> LandedEventObservers { get; }
    }
}
