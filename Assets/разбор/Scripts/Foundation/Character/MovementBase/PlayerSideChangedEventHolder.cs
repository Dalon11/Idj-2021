using Foundation.Movement;
using Foundation.Character.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace Foundation.Character
{
    public class PlayerSideChangedEventHolder : AbstractBehaviour, IPlayerMovementInputHandler
    {
        [SerializeField] private Transform _playerTransform;

        [Inject] private IPlayerMovementInputProvider _inputProvider;
        private bool _prevSideLeft = false;

        private void Start()
        {
            Observe(_inputProvider.InputObservers);
        }

        private void OnPlayerMovementSideChanged(bool isLeft)
        {
            var currentScale = _playerTransform.localScale;
            var multiplyer = isLeft != _prevSideLeft ? -1 : 1;
            currentScale.x *= multiplyer;

            _prevSideLeft = isLeft;
            _playerTransform.localScale = currentScale;
        }

        public void OnMovementInput(Vector2 input)
        {
            if (input.x > 0 && _prevSideLeft)
                OnPlayerMovementSideChanged(false);
            else if (input.x < 0 && !_prevSideLeft)
                OnPlayerMovementSideChanged(true);
        }
    }

}