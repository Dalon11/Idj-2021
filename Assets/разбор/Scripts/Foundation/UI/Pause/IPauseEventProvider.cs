using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.UI
{
    public interface IPauseEventProvider
    {
        ObserverList<IPauseEventHolder> OnPauseObservers { get; }
    }
}
