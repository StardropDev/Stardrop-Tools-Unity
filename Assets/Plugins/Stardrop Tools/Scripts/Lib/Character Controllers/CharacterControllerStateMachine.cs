
using UnityEngine;
using System.Collections.Generic;

namespace StardropTools.CharacterControllers
{
    public class CharacterControllerStateMachine : MonoBehaviour
    {
        private List<CharacterControllerState> states;
        private CharacterControllerState currentState;

        public bool IsStateActive => currentState != null;
        public bool IsLocomotionStateActive => currentState is CharacterLocomotionState;
        public bool IsInteractionStateActive => currentState is CharacterInteractionState;
        public bool IsAttackStateActive => currentState is CharacterAttackState;
        public float TimeInState { get; private set; }


#if UNITY_EDITOR
        [NaughtyAttributes.ShowNativeProperty]
        public string CurrentStateID => currentState != null ? $"{currentState.ID}, {currentState.Name}" : $"None";
#endif

        private void InitializeStateList()
        {
            if (states == null)
            {
                states = new List<CharacterControllerState>();
            }
        }

        public void AddState(CharacterControllerState state)
        {
            InitializeStateList();

            if (states.Contains(state) == false)
            {
                states.Add(state);
                state.SetStateMachine(this);
            }
        }

        public void AddStates(params CharacterControllerState[] states)
        {
            foreach (CharacterControllerState state in states)
            {
                AddState(state);
            }
        }

        public void RemoveState(CharacterControllerState state)
        {
            InitializeStateList();

            if (states.Contains(state))
            {
                states.Remove(state);
                state.SetStateMachine(null);
            }
        }

        public void RemoveStates(params CharacterControllerState[] states)
        {
            foreach (CharacterControllerState state in states)
            {
                RemoveState(state);
            }
        }

        private bool HasAnyStates()
        {
            if (states == null || states.Count == 0)
            {
                Debug.LogError("CharacterControllerStateMachine: No states available to change to.");
                return false;
            }

            return true;
        }

        public bool ChangeState(CharacterControllerState state, CharacterControllerModuleInput inputData)
        {
            if (!HasAnyStates())
            {
                return false;
            }

            if (state == null || inputData == null || states.Contains(state) == false)
            {
                return false;
            }

            if (currentState != null)
            {
                if (currentState is CharacterLocomotionState locomotionState)
                {
                    locomotionState.ExitState();
                }
                else if (currentState is CharacterInteractionState interactionState)
                {
                    interactionState.ExitState();
                }
                else if (currentState is CharacterAttackState attackState)
                {
                    attackState.ExitState();
                }
            }

            currentState = state;

            if (currentState != null)
            {
                if (currentState is CharacterLocomotionState locomotionState)
                {
                    locomotionState.EnterState();
                }
                else if (currentState is CharacterInteractionState interactionState)
                {
                    interactionState.EnterState();
                }
                else if (currentState is CharacterAttackState attackState)
                {
                    attackState.EnterState();
                }

                TimeInState = 0;
                return true;
            }

            return false;
        }

        public bool ChangeState(CharacterControllerModuleStateID stateID, CharacterControllerModuleInput inputData)
        {
            if (!HasAnyStates())
            {
                return false;
            }

            CharacterControllerState state = states.Find(s => s.StateData.Equals(stateID));

            return ChangeState(state, inputData);
        }

        public bool ChangeState(int stateID, CharacterControllerModuleInput inputData)
        {
            if (!HasAnyStates())
            {
                return false;
            }

            CharacterControllerState state = states.Find(s => s.ID == stateID);

            return ChangeState(state, inputData);
        }

        public bool ChangeState(string stateName, CharacterControllerModuleInput inputData)
        {
            if (!HasAnyStates())
            {
                return false;
            }

            CharacterControllerState state = states.Find(s => s.Name == stateName);

            return ChangeState(state, inputData);
        }

        public CharacterControllerState UpdateState(CharacterControllerModules modules)
        {
            if (currentState == null)
            {
                return null;
            }

            if (currentState is CharacterLocomotionState locomotionState)
            {
                locomotionState.UpdateState(TimeInState, modules);
                locomotionState.HandleInput(modules.InputData);
            }
            else if (currentState is CharacterInteractionState interactionState)
            {
                interactionState.UpdateState(TimeInState, modules);
                interactionState.HandleInput(modules.InputData);
            }
            else if (currentState is CharacterAttackState attackState)
            {
                attackState.UpdateState(TimeInState, modules);
                attackState.HandleInput(modules.InputData);
            }

            TimeInState += Time.deltaTime;
            return currentState;
        }
    }
}
