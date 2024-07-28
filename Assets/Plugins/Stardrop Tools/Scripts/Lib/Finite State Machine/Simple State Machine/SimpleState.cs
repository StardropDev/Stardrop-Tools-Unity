
namespace StardropTools.FiniteStateMachines
{
    public abstract class SimpleState : ISimpleState
    {
        public SimpleStateMachine StateMachine { get; private set; }

        public SimpleState(SimpleStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState(float timeInState);

        public bool ChangeState(int stateIndex)
        {
            if (StateMachine == null)
            {
                UnityEngine.Debug.LogError("State machine is null.");
                return false;
            }

            return StateMachine.ChangeState(stateIndex);
        }
    }
}
