using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.Interactions
{
    public interface ILeavedInteractableEventProvider
    {
        ObserverList<ILeavedInteractableEventHolder> InteractableLeavedObservers { get; }
    }
}
