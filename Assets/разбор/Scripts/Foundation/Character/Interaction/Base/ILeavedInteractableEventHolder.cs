using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.Interactions
{
    public interface ILeavedInteractableEventHolder
    {
        void OnInteractableLeaved(IInteractable interactable);
    }
}
