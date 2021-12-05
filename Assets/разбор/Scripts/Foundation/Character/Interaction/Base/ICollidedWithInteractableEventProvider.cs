using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.Interactions
{
    public interface ICollidedWithInteractableEventProvider: ILeavedInteractableEventProvider
    {
        ObserverList<ICollidedWithInteractableEventHolder> OnCollidedWithInteractableObservers { get; }
    }
}
