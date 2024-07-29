using StardropTools;
using StardropTools.CharacterControllers;
using UnityEngine;

namespace TestCharacterController
{
    public class TestLocomotionState_Idle : CharacterLocomotionState
    {
        public TestLocomotionState_Idle(CharacterControllerModuleStateID stateID, CharacterControllerModules modules)
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

        }

        public override void HandleInput(CharacterControllerModuleInput data)
        {
            if (data.IsMoving && data.sprint)
            {
                ChangeState(TestCharacterController.Run, data);
            }

            if (data.IsMoving && !data.sprint)
            {
                ChangeState(TestCharacterController.Walk, data);
            }
        }
    }
}