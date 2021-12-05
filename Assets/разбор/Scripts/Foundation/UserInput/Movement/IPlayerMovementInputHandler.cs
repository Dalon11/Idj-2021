using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.Movement
{
    public interface IPlayerMovementInputHandler
    {
        void OnMovementInput(Vector2 input);
    }
}
