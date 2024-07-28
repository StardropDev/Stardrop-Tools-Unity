using UnityEngine;

namespace StardropTools.CharacterControllers
{
    [RequireComponent(typeof(CharacterControllerStateMachine), typeof(CharacterController))]
    public abstract class CharacterControllerBrain : BaseComponent
    {
        [SerializeField] protected CharacterControllerModules modules;
        [SerializeField] protected CharacterControllerStateMachine stateMachine;

        public virtual void InitializeModules()
        {
            var componentModule = new CharacterControllerModuleComponents();
            var characterTransformModule = new CharacterControllerModuleCharacterTransformData();
            var inputModule = new CharacterControllerModuleInput();
            var movementModule = new CharacterControllerModuleMovementData();

            modules = new CharacterControllerModules()
            {
                Components = componentModule,
                TransformData = characterTransformModule,
                InputData = inputModule,
                MovementData = movementModule
            };
        }

        public abstract void InitializeStateMachine();

        public void UpdateStateMachine()
        {
            if (stateMachine != null)
            {
                HandleInput();
                stateMachine.UpdateState(modules);
            }
        }

        public abstract void HandleInput();
    }
}