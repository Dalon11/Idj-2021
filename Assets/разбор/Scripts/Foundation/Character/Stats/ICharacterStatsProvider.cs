using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.Character
{
    public interface ICharacterStatsProvider
    {
        float HorizontalVelocity { get; }
        float VerticalVelocity { get; }

        uint MaxAmountOfJumps { get; }
    }
}
