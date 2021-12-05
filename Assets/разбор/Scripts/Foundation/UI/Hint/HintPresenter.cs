using Foundation.UI.Localization;
using UnityEngine;
using Foundation.Interactions;
using Zenject;
using Foundation.UI.Dialogs;

namespace Foundation.UI
{
    public class HintPresenter : AbstractService<IHintController>, IHintController, 
        ICollidedWithInteractableEventHolder, ILeavedInteractableEventHolder, IDialogEventsHolder
    {
        [SerializeField] private LocalizedTextWrapper _text;
        [SerializeField] private CanvasGroup _canvas;
        [Inject] private ICollidedWithInteractableEventProvider _eventProvider;
        [InjectOptional] private IDialogController _dialogEventsProvider;

        public override void Start()
        {
            base.Start();

            if (_eventProvider != null)
            {
                Observe(_eventProvider.OnCollidedWithInteractableObservers);
                Observe(_eventProvider.InteractableLeavedObservers);
            }

            if (_dialogEventsProvider != null)
                Observe(_dialogEventsProvider.DialogEventsObservers);

            Hide();
        }

        public void Show(string alias)
        {
            _text.SetLocalizationKey(alias);
            _canvas.alpha = 1;
        }

        public void Hide()
        {
            _canvas.alpha = 0;
        }

        private void Show()
        {
            _canvas.alpha = 1;
        }

        public void OnCollidedWithInteractable(IInteractable interactable)
        {
            Show();
        }

        public void OnInteractableLeaved(IInteractable interactable)
        {
            Hide();
        }

        public void OnDialogShown()
        {
            Hide();
        }

        public void OnDialogHide()
        {
        }
    }
}
