
using StardropTools.FiniteStateMachines;
using UnityEngine;

namespace StardropTools.CharacterControllers
{
    public abstract class CharacterAttackState : CharacterControllerState, IState, IStateUpdate<float, CharacterControllerModules>, IStateInput<CharacterControllerModuleInput>
    {
        private readonly Transform attackTarget;
        private readonly Vector3 attackPosition;
        private readonly float attackRange;

        protected CharacterAttackState(CharacterControllerModuleStateID stateID, CharacterControllerModules modules, Vector3 attackPosition, float attackRange)
            : this(stateID, modules, null, attackPosition, attackRange)
        {
        }

        protected CharacterAttackState(CharacterControllerModuleStateID stateID, CharacterControllerModules modules, Transform attackTarget, Vector3 attackPosition, float attackRange)
            : base(stateID, modules)
        {
            this.attackTarget = attackTarget;
            this.attackPosition = attackPosition;
            this.attackRange = attackRange;
        }

        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState(float timeInState, CharacterControllerModules modules);
        public abstract void HandleInput(CharacterControllerModuleInput input);
    }
}
