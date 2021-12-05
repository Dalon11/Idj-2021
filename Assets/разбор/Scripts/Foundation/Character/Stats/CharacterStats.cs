using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.Character
{
    public class CharacterStats : AbstractService<ICharacterStatsProvider>, ICharacterStatsProvider
    {
        [SerializeField] private float _horizontalVelocity = 5f;
        [SerializeField] private float _verticalVelocity = 5f;
        [SerializeField] private uint _maxAmountOfJumps = 2;

        public float HorizontalVelocity => _horizontalVelocity;
        public float VerticalVelocity => _verticalVelocity;
        public uint MaxAmountOfJumps => _maxAmountOfJumps;
    }
}
