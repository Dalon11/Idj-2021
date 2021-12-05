using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.UI.Dialogs
{
    public interface IDialogEventsHolder
    {
        void OnDialogShown();
        void OnDialogHide();
    }
}