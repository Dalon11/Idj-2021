using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Foundation.Movement
{
    public class PlayerGroundStateChangedEventProvider : AbstractService<IPlayerLandedEventProvider>, 
        IPlayerLandedEventProvider, IPlayerJumpedEventProvider
    {
        [SerializeField] private Collider2D _playerGroudCollider;
        [SerializeField] private LayerMask _groundMask;

        public ObserverList<IPlayerLandedEventHolder> LandedEventObservers => _landedEventObservers;
        private ObserverList<IPlayerLandedEventHolder> _landedEventObservers = new ObserverList<IPlayerLandedEventHolder>();

        public ObserverList<IPlayerJumpedEventHolder> OnPlayerJumpedObservers => _jumpedEventObservers;
        private ObserverList<IPlayerJumpedEventHolder> _jumpedEventObservers = new ObserverList<IPlayerJumpedEventHolder>();

        private bool _isLanded = true;

        public override void InstallBindings()
        {
            base.InstallBindings();
            Container.Bind<IPlayerJumpedEventProvider>().FromInstance(this);
        }

        private void Update()
        {
            if (!_isLanded && _playerGroudCollider.IsTouchingLayers(_groundMask))
            {
                _isLanded = true;
                SendLandedEvent();                
            }
            else if (_isLanded && !_playerGroudCollider.IsTouchingLayers(_groundMask))
            {
                _isLanded = false;
                SendJumpedEvent();
            }
        }

        private void SendLandedEvent()
        {
            foreach (var obs in _landedEventObservers.Enumerate())
                obs.OnPlayerLanded();
        }

        private void SendJumpedEvent()
        {
            foreach (var obs in _jumpedEventObservers.Enumerate())
                obs.OnPlayerJump();
        }
    }
}
