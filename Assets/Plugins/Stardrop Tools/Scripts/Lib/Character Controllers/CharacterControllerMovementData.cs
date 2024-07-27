
namespace StardropTools.CharacterControllers
{
    public class CharacterControllerMovementData
    {
        public float walkSpeed = 4f;
        public float runSpeed = 12f;
        public float crouchSpeed = 2f;
        public float jumpForce = 5f;
        public float gravity = -9.81f;

        public CharacterControllerMovementData() { }

        public CharacterControllerMovementData(float walkSpeed, float runSpeed, float crouchSpeed, float jumpForce, float gravity)
        {
            this.walkSpeed = walkSpeed;
            this.runSpeed = runSpeed;
            this.crouchSpeed = crouchSpeed;
            this.jumpForce = jumpForce;
            this.gravity = gravity;
        }
    }
}