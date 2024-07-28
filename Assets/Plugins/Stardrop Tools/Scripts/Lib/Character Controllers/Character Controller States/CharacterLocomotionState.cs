
using StardropTools.FiniteStateMachines;
using UnityEngine;

namespace StardropTools.CharacterControllers
{
    public abstract class CharacterLocomotionState : CharacterControllerState, IState, IStateUpdate<float, CharacterControllerModules>, IStateInput<CharacterControllerModuleInput>
    {
        protected CharacterLocomotionState(CharacterControllerModuleStateID stateID, CharacterControllerModules modules)
            : base(stateID, modules)
        {
        }

        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState(float timeInState, CharacterControllerModules modules);
        public abstract void HandleInput(CharacterControllerModuleInput input);
    }
}
