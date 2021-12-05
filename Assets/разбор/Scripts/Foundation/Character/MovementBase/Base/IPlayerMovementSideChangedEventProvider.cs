using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.Character
{
    public interface IPlayerSideChangedEventProvider
    {
        ObserverList<IPlayerSideChangedEventHolder> PlayerSideChangedObservers { get; }
    }
}
