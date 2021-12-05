using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.Character.StateMachine
{
    public interface IStateChangedEventProvider
    {
        ObserverList<IStateChangedEventHolder> OnStateChangedObservers { get; }
    }
}
