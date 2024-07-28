
namespace StardropTools.FiniteStateMachines
{
    public abstract class BaseState<TStateData> : IBaseState<TStateData>
    {
        public int ID { get; }
        public string Name { get; }

        public BaseStateMachine<BaseState<TStateData>, TStateData> StateMachine { get; }

        public BaseState(int id, string name, BaseStateMachine<BaseState<TStateData>, TStateData> stateMachine)
        {
            ID = id;
            Name = name;
            StateMachine = stateMachine;
        }

        public abstract void EnterState(TStateData data);
        public abstract void ExitState(TStateData data);
        public abstract void UpdateState(float timeInState, TStateData data);

        public bool ChangeState(int stateID, TStateData data)
        {
            return StateMachine.ChangeState(stateID, data);
        }

        public bool ChangeState(string stateName, TStateData data)
        {
            return StateMachine.ChangeState(stateName, data);
        }
    }
}
