using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.Interactions
{
    public interface IInteractable
    {
        bool CanInteract { get; }
        void OnInteracted();
    }
}
