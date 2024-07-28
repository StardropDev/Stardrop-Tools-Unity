
namespace StardropTools.FiniteStateMachines
{
    public interface ISimpleState : IState, IStateUpdate<float>
    {
        SimpleStateMachine StateMachine { get; }

        bool ChangeState(int stateIndex);
    }
}
