
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.FiniteStateMachines
{
    public class BaseStateMachine<TState, TStateData> : IBaseStateMachine<TState, TStateData> where TState : BaseState<TStateData>
    {
        private readonly List<TState> states;
        private TState currentState;

        public readonly EventDelegate<TState> OnStateEnter;
        public readonly EventDelegate<TState> OnStateExit;

        public int ID { get; private set; }
        public string Name { get; private set; }
        public float TimeInState { get; protected set; }
        public TState CurrentState => currentState;

        public BaseStateMachine(int id, string name)
        {
            this.ID = id;
            this.Name = name;

            this.states = new List<TState>();
        }

        public BaseStateMachine(int id, string name, params TState[] states)
        {
            this.ID = id;
            this.Name = name;

            this.states = new List<TState>(states);
        }

        public void AddState(TState state)
        {
            this.states.Add(state);
        }

        public void AddStates(params TState[] states)
        {
            this.states.AddRange(states);
        }

        public void RemoveState(TState state)
        {
            this.states.Remove(state);
        }

        public void RemoveStates(params TState[] states)
        {
            foreach (TState state in states)
            {
                this.states.Remove(state);
            }
        }

        public bool ChangeState(TState targetState, TStateData data)
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
                currentState?.ExitState(data);
                OnStateExit?.Invoke(currentState);
                ResetTimeInState();

                currentState = targetState;
                currentState.EnterState(data);
                OnStateEnter?.Invoke(currentState);

                return true;
            }

            Debug.LogError("Target state is not in the state list.");
            return false;
        }

        public bool ChangeState(int stateID, TStateData data)
        {
            TState targetState = this.states.Find(s => s.ID == stateID);
            if (targetState != null)
            {
                return ChangeState(targetState, data);
            }

            Debug.LogError($"State with ID: '{stateID}' not found.");
            return false;
        }

        public bool ChangeState(string stateName, TStateData data)
        {
            TState targetState = this.states.Find(s => s.Name == stateName);
            if (targetState != null)
            {
                return ChangeState(targetState, data);
            }

            Debug.LogError($"State with name: '{stateName}' not found.");
            return false;
        }

        public TState UpdateState(TStateData data)
        {
            if (currentState != null)
            {
                currentState.UpdateState(TimeInState, data);
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
