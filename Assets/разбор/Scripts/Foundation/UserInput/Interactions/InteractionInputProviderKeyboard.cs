using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.Interactions
{
    public class InteractionInputProviderKeyboard : AbstractService<IInteractionInputProvider>, IInteractionInputProvider
    {
        public IObserverList<IInteractionInputHolder> InteractionInputObservers => _observers;
        private ObserverList<IInteractionInputHolder> _observers = new ObserverList<IInteractionInputHolder>();

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
                SendUserInput();
        }

        private void SendUserInput()
        {
            foreach (var obs in _observers.Enumerate())
                obs.OnInteraction();
        }
    }
}
