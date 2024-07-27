using UnityEngine;

namespace StardropTools.CharacterControllers
{
    [CreateAssetMenu(fileName = "CharacterControllerMovementData", menuName = "Stardrop Tools/Character Controllers/Character Controller Move Data")]
    public class CharacterControllerMovementDataSO : ScriptableObject
    {
        public CharacterControllerMovementData movementData;

        public float WalkSpeed => movementData.walkSpeed;
        public float RunSpeed => movementData.runSpeed;
        public float CrouchSpeed => movementData.crouchSpeed;
        public float JumpForce => movementData.jumpForce;
        public float Gravity => movementData.gravity;
    }
}