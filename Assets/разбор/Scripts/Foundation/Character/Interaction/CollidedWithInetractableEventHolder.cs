using Zenject;
using Foundation.Character.StateMachine;

namespace Foundation.Interactions
{
    public class CollidedWithInetractableEventHolder : AbstractBehaviour, 
        ICollidedWithInteractableEventHolder, IStateChangedEventHolder, IInteractionInputHolder
    {
        [Inject] private ICollidedWithInteractableEventProvider _eventProvider;
        [Inject] private IInteractionInputProvider _interactionInputProvider;

        private bool _canInteract = true;
        private IInteractable _interactable;

        private void Start()
        {
            Observe(_eventProvider.OnCollidedWithInteractableObservers);
            Observe(_interactionInputProvider.InteractionInputObservers);
        }

        public void OnCollidedWithInteractable(IInteractable interactable)
        {
            _interactable = interactable;
        }

        public void OnStateChanged(IState newState)
        {
            _canInteract = newState.CanInterract;
        }

        public void OnInteraction()
        {
            if (_canInteract && _interactable != null && _interactable.CanInteract)
                _interactable.OnInteracted();
        }
    }
}
