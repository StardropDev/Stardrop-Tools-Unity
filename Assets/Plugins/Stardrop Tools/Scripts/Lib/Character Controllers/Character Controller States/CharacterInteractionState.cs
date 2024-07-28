
using StardropTools.FiniteStateMachines;
using UnityEngine;

namespace StardropTools.CharacterControllers
{
    public abstract class CharacterInteractionState : CharacterControllerState, IState, IStateUpdate<float, CharacterControllerModules>, IStateInput<CharacterControllerModuleInput>
    {
        private readonly Transform lookAtTarget;
        private readonly Transform interactionTarget;

        private readonly Vector3 lookAtTargetPosition;
        private readonly Vector3 interactionTargetPosition;
        
        private readonly float interactionRange;

        protected CharacterInteractionState(CharacterControllerModuleStateID stateID, CharacterControllerModules modules, Transform lookAtTarget, Transform interactionTarget, float interactionRange)
            : this(stateID, modules, lookAtTarget.position, interactionTarget.position, interactionRange)
        {
            this.lookAtTarget = lookAtTarget;
            this.interactionTarget = interactionTarget;
        }

        protected CharacterInteractionState(CharacterControllerModuleStateID stateID, CharacterControllerModules modules, Vector3 lookAtTarget, Vector3 interactionTarget, float interactionRange)
            : base(stateID, modules)
        {
            this.lookAtTargetPosition = lookAtTarget;
            this.interactionTargetPosition = interactionTarget;

            this.interactionRange = interactionRange;
        }

        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState(float timeInState, CharacterControllerModules modules);
        public abstract void HandleInput(CharacterControllerModuleInput input);
    }
}
