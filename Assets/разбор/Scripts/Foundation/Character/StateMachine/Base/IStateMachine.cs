using Foundation.Movement;

namespace Foundation.Character.StateMachine
{
    public interface IStateMachine: IStateChangedEventProvider, IPlayerMovementInputHandler, IPlayerSideChangedEventProvider
    {
        IState CurrentState { get; }
    }
}
