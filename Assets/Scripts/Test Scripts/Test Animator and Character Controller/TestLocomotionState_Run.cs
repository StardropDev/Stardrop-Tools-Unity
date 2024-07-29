using StardropTools;
using StardropTools.CharacterControllers;
using UnityEngine;

namespace TestCharacterController
{
    public class TestLocomotionState_Run : CharacterLocomotionState
    {
        public TestLocomotionState_Run(CharacterControllerModuleStateID stateID, CharacterControllerModules modules)
            : base(stateID, modules)
        {
        }

        public override void EnterState()
        {
            CrossfadeAnimation(StateData.Name);
        }

        public override void ExitState()
        {
        }

        public override void UpdateState(float timeInState, CharacterControllerModules modules)
        {
            Move(modules.Components.characterController, modules.InputData.Direction, modules.MovementData.runSpeed, Time.deltaTime);
            SmoothLookAt(modules.TransformData.graphicsRoot, modules.InputData.Direction, modules.MovementData.rotationSpeed);
        }

        public override void HandleInput(CharacterControllerModuleInput input)
        {
            if (!input.IsMoving)
            {
                ChangeState(TestCharacterController.Idle, input);
            }

            if (input.IsMoving && !input.sprint)
            {
                ChangeState(TestCharacterController.Walk, input);
            }
        }
    }
}