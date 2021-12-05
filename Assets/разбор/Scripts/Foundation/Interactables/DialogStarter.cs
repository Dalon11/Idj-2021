using Foundation.Dialogs.Configs;
using Foundation.UI.Dialogs;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Foundation.Interactions
{
    public class DialogStarter : AbstractBehaviour, IInteractable, IDialogEventsHolder
    {
        [SerializeField] private UnityEvent _onDialogCompletedEvent;
        [SerializeField] private DialogData _dialogData;

        [Inject] private IDialogController _popupController;

        public bool CanInteract => _canInteract;
        private bool _canInteract = true;

        private void Start()
        {
            Observe(_popupController.DialogEventsObservers);
        }

        public void OnInteracted()
        {
            _popupController.StartDialog(_dialogData);
            _canInteract = false;
        }

        public void OnDialogShown()
        {
            _onDialogCompletedEvent?.Invoke();
            this.enabled = false;
        }

        public void OnDialogHide()
        {
        }
    }
}
