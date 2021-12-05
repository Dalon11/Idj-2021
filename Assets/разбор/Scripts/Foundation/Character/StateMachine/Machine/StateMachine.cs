using Foundation.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Foundation.Character.StateMachine
{
    public class StateMachine : AbstractService<IStateMachine>, 
        IStateMachine, IStateChangedEventHolder
    {
        [SerializeField] private Rigidbody2D _playerRigidbody;

        [Inject] private IPlayerMovementInputProvider _movementInputProvider;

        public IState CurrentState { get; private set; } = new IdleState();

        public ObserverList<IStateChangedEventHolder> OnStateChangedObservers => _onStateChangedObservers;
        private ObserverList<IStateChangedEventHolder> _onStateChangedObservers = new ObserverList<IStateChangedEventHolder>();

        ObserverList<IPlayerSideChangedEventHolder> IPlayerSideChangedEventProvider.PlayerSideChangedObservers => _sideChangedObs;
        private ObserverList<IPlayerSideChangedEventHolder> _sideChangedObs = new ObserverList<IPlayerSideChangedEventHolder>();


        private void Awake()
        {
            Observe(_movementInputProvider.InputObservers);
            Observe(CurrentState.OnStateChangedObservers);
        }

        private void Update()
        {
            CurrentState.Update(_playerRigidbody);
        }

        public void OnMovementInput(Vector2 input)
        {
            CurrentState.OnMovementInput(input);
        }

        public void OnStateChanged(IState newState)
        {
            Observers.Clear();
            CurrentState.Dispose();

            Container.Inject(newState);

            CurrentState = newState;
            Observe(CurrentState.OnStateChangedObservers);
            

            foreach (var obs in _onStateChangedObservers.Enumerate())
                obs.OnStateChanged(newState);
        }
    }
}