using StardropTools;
using StardropTools.CharacterControllers;
using UnityEngine;

namespace TestCharacterController
{
    public class TestCharacterControllerStateMachine : CharacterControllerBrain
    {
        [SerializeField] CharacterController characterController;
        [SerializeField] AnimationController animationController;

        private void Start()
        {
            InitializeModules();
            InitializeStateMachine();

            StartUpdate();
        }

        public override void InitializeModules()
        {
            var components = new CharacterControllerModuleComponents()
            {
                animationController = animationController,
                characterController = characterController,
                characterAnimator = animationController.AnimatorController,
                characterRenderer = characterController.GetComponentInChildren<Renderer>(),
                rootTransform = characterController.transform
            };

            var characterData = new CharacterControllerModuleCharacterTransformData()
            {
                graphicsRoot = characterController.transform,
                characterTransform = characterController.transform.GetChild(0),
            };

            modules = new CharacterControllerModules()
            {
                Components = components,
                TransformData = characterData,
                InputData = TestCharacterController.GetPCInputData(),
                MovementData = new CharacterControllerModuleMovementData()
            };
        }

        public override void InitializeStateMachine()
        {
            stateMachine.AddStates
            (
                new TestLocomotionState_Idle(TestCharacterController.Idle, modules),
                new TestLocomotionState_Walk(TestCharacterController.Walk, modules),
                new TestLocomotionState_Run(TestCharacterController.Run, modules)
            );

            stateMachine.ChangeState(0, modules.InputData);

            StartUpdate();
        }

        public override void HandleUpdate()
        {
            base.HandleUpdate();

            HandleInput();
            stateMachine.UpdateState(modules);
        }

        public override void HandleInput()
        {
            modules.InputData = TestCharacterController.GetPCInputData();
        }
    }
}
