using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.Interactions
{
    public interface IInteractionInputProvider
    {
        IObserverList<IInteractionInputHolder> InteractionInputObservers { get; }
    }
}
