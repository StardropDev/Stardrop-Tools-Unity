
using StardropTools.CharacterControllers;
using UnityEngine;

namespace TestCharacterController
{
    public static class TestCharacterController
    {
        // Locomotion
        public static CharacterControllerModuleStateID Idle = new CharacterControllerModuleStateID(0, "Idle");
        public static CharacterControllerModuleStateID Walk = new CharacterControllerModuleStateID(1, "Walk");
        public static CharacterControllerModuleStateID Run = new CharacterControllerModuleStateID(2, "Run");
        public static CharacterControllerModuleStateID Jump = new CharacterControllerModuleStateID(3, "Jump");

        // Combat
        public static CharacterControllerModuleStateID Attack = new CharacterControllerModuleStateID(4, "Attack");
        public static CharacterControllerModuleStateID Block = new CharacterControllerModuleStateID(5, "Block");
        public static CharacterControllerModuleStateID Dodge = new CharacterControllerModuleStateID(6, "Dodge");

        // Input
        private static CharacterControllerModuleInput inputData = new CharacterControllerModuleInput();

        public static CharacterControllerModuleInput GetPCInputData()
        {
            inputData.horizontal = Input.GetAxisRaw("Horizontal");
            inputData.vertical = Input.GetAxisRaw("Vertical");

            inputData.sprint = Input.GetKey(KeyCode.LeftShift);
            inputData.jump = Input.GetKeyDown(KeyCode.Space);
            inputData.crouch = Input.GetKeyDown(KeyCode.LeftControl);
            inputData.slide = Input.GetKey(KeyCode.C);
            inputData.dodge = Input.GetKey(KeyCode.V);
            inputData.attack = Input.GetMouseButtonDown(0);
            inputData.block = Input.GetMouseButton(1);
            inputData.ability = Input.GetKey(KeyCode.E);
            inputData.interact = Input.GetKeyDown(KeyCode.F);
            inputData.cancelInteract = Input.GetKeyDown(KeyCode.G);
            inputData.pause = Input.GetKeyDown(KeyCode.Escape);

            return inputData;
        }
    }
}
