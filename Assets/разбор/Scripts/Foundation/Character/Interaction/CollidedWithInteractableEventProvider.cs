using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.Interactions
{
    public class CollidedWithInteractableEventProvider : AbstractService<ICollidedWithInteractableEventProvider>, 
        ICollidedWithInteractableEventProvider
    {
        public ObserverList<ICollidedWithInteractableEventHolder> OnCollidedWithInteractableObservers => _onCollidedObservers;
        private ObserverList<ICollidedWithInteractableEventHolder> _onCollidedObservers = new ObserverList<ICollidedWithInteractableEventHolder>();

        public ObserverList<ILeavedInteractableEventHolder> InteractableLeavedObservers => _onLeavedObservers;
        private ObserverList<ILeavedInteractableEventHolder> _onLeavedObservers = new ObserverList<ILeavedInteractableEventHolder>();

        public override void InstallBindings()
        {
            base.InstallBindings();
            Container.Bind<ILeavedInteractableEventProvider>().FromInstance(this);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var interactable = collision.GetComponent<IInteractable>();
            if (interactable != null && interactable.CanInteract)
                SendCollidedEvent(interactable);            
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var interactable = collision.GetComponent<IInteractable>();
            if (interactable != null)
            {
                SendLeavedEvent(interactable);
            }
        }

        private void SendCollidedEvent(IInteractable interactable)
        {
            foreach (var obs in _onCollidedObservers.Enumerate())
                obs.OnCollidedWithInteractable(interactable);
        }

        private void SendLeavedEvent(IInteractable interactable)
        {
            foreach (var obs in _onLeavedObservers.Enumerate())
                obs.OnInteractableLeaved(interactable);
        }
    }
}
