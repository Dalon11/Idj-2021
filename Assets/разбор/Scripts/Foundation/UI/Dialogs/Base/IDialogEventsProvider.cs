using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.UI.Dialogs
{
    public interface IDialogEventsProvider
    {
        ObserverList<IDialogEventsHolder> DialogEventsObservers { get; }
    }
}
