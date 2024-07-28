
namespace StardropTools.FiniteStateMachines
{
    public interface IBaseStateMachine<TState, TStateData> where TState : IBaseState<TStateData>
    {
        void AddState(TState state);
        void AddStates(params TState[] states);


        void RemoveState(TState state);
        void RemoveStates(params TState[] states);


        bool ChangeState(TState targetState, TStateData data);
        bool ChangeState(int stateID, TStateData data);
        bool ChangeState(string stateName, TStateData data);


        TState UpdateState(TStateData data);
    }
}
