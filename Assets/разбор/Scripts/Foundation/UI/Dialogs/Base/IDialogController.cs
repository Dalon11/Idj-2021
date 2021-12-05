using Foundation.Dialogs.Configs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.UI.Dialogs
{
    public interface IDialogController: IDialogEventsProvider
    {
        void StartDialog(DialogData dialogData);

    }
}
