using UnityEngine;

namespace StardropTools
{
    public static class CharacterControllerUtils
    {
        public static void ApplyGravity(CharacterController controller, ref Vector3 velocity, float gravity = -9.81f)
        {
            if (!controller.isGrounded)
            {
                velocity.y += gravity * Time.deltaTime;
                controller.Move(velocity * Time.deltaTime);
            }
            else if (velocity.y < 0)
            {
                velocity.y = 0f;
            }
        }

        public static void Jump(CharacterController controller, ref Vector3 velocity, float jumpHeight = 2.0f, float gravity = -9.81f)
        {
            if (controller.isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        public static void AddForce(CharacterController controller, ref Vector3 velocity, Vector3 force)
        {
            velocity += force * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        /*
        public static Timer AddForceOverTime(CharacterController controller, ref Vector3 velocity, Vector3 force, float duration)
        {
            float time = 0f;
            while (time < duration)
            {
                AddForce(controller, ref velocity, force);
                time += Time.deltaTime;
                yield return null;
            }
        }
        */
    }
}
