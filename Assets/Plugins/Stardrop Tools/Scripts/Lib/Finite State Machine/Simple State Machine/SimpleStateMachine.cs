using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.FiniteStateMachines
{
    public class SimpleStateMachine : ISimpleStateMachine
    {
        private readonly List<ISimpleState> states;
        private ISimpleState currentState;

        public float TimeInState { get; protected set; }
        public ISimpleState CurrentState => currentState;

        public EventDelegate<ISimpleState> OnStateEnter { get; }
        public EventDelegate<ISimpleState> OnStateExit { get; }

        public SimpleStateMachine()
        {
            this.states = new List<ISimpleState>();

            OnStateEnter = new EventDelegate<ISimpleState>();
            OnStateExit = new EventDelegate<ISimpleState>();
        }

        public SimpleStateMachine(params ISimpleState[] states)
        {
            this.states = new List<ISimpleState>(states);

            OnStateEnter = new EventDelegate<ISimpleState>();
            OnStateExit = new EventDelegate<ISimpleState>();
        }

        public void AddState(ISimpleState state)
        {
            this.states.Add(state);
        }

        public void AddStates(params ISimpleState[] states)
        {
            this.states.AddRange(states);
        }

        public void RemoveState(ISimpleState state)
        {
            this.states.Remove(state);
        }

        public void RemoveStates(params ISimpleState[] states)
        {
            foreach (ISimpleState state in states)
            {
                this.states.Remove(state);
            }
        }

        public bool ChangeState(ISimpleState targetState)
        {
            if (targetState == null)
            {
                Debug.LogError("Target state is null.");
                return false;
            }

            if (currentState == targetState)
            {
                Debug.LogWarning("Already in the target state.");
                return false;
            }

            if (this.states.Contains(targetState))
            {
                currentState?.ExitState();
                OnStateExit?.Invoke(currentState);
                ResetTimeInState();

                currentState = targetState;
                currentState.EnterState();
                OnStateEnter?.Invoke(currentState);

                return true;
            }

            Debug.LogError("Target state is not in the state list.");
            return false;
        }

        public bool ChangeState(int index)
        {
            if (index < 0 || index >= this.states.Count)
            {
                Debug.LogError("Invalid state index.");
                return false;
            }

            return ChangeState(this.states[index]);
        }

        public ISimpleState UpdateState()
        {
            if (currentState != null)
            {
                currentState.UpdateState(TimeInState);
                UpdateTimeInState();
                return currentState;
            }

            return null;
        }

        protected virtual void UpdateTimeInState()
        {
            this.TimeInState += Time.deltaTime;
        }

        protected void ResetTimeInState()
        {
            this.TimeInState = 0;
        }
    }
}
