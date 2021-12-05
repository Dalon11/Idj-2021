using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.Interactions
{
    public interface ICollidedWithInteractableEventHolder
    {
        void OnCollidedWithInteractable(IInteractable interactable);
    }
}
