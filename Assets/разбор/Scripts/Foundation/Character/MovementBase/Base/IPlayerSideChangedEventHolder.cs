using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.Character
{
    public interface IPlayerSideChangedEventHolder
    {
        void OnPlayerMovementSideChanged(bool isLeft);
    }
}
