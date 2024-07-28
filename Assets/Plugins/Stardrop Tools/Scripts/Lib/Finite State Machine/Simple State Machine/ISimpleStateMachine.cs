
namespace StardropTools.FiniteStateMachines
{
    public interface ISimpleStateMachine
    {
        float TimeInState { get; }
        ISimpleState CurrentState { get; }


        EventDelegate<ISimpleState> OnStateEnter { get; }
        EventDelegate<ISimpleState> OnStateExit { get; }


        void AddState(ISimpleState state);
        void AddStates(params ISimpleState[] states);


        void RemoveState(ISimpleState state);
        void RemoveStates(params ISimpleState[] states);


        bool ChangeState(ISimpleState targetState);
        bool ChangeState(int index);


        ISimpleState UpdateState();
    }
}
