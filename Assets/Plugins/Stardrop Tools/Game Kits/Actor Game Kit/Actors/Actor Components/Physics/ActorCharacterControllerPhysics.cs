using UnityEngine;

namespace StardropTools.GameKit.Actors
{
    public class ActorCharacterControllerPhysics : ActorPhysics
    {
        private CharacterController characterController;
        private Vector3 velocity;

        public override void Initialize(BaseActor actor)
        {
            base.Initialize(actor);
            characterController = actor.GetComponent<CharacterController>();
            if (characterController == null)
            {
                characterController = actor.gameObject.AddComponent<CharacterController>();
            }
        }

        public override void ApplyForce(Vector3 force)
        {
            velocity += force * Time.deltaTime;
        }

        public override void ApplyGravity(Vector3 gravity)
        {
            velocity += gravity * Time.deltaTime;
        }

        public override bool IsGrounded()
        {
            return characterController.isGrounded;
        }

        public override void Jump(float jumpForce)
        {
            if (IsGrounded())
            {
                velocity.y = jumpForce;
            }
        }

        public override void JumpTo(Transform targetPoint)
        {
            JumpTo(targetPoint.position);
        }

        public override void JumpTo(Vector3 targetPosition)
        {
            Vector3 direction = targetPosition - characterController.transform.position;
            float distance = direction.magnitude;
            float gravity = Physics.gravity.magnitude;
            float jumpForce = Mathf.Sqrt(2 * gravity * distance);

            Jump(jumpForce);
        }

        public override void Move(Vector3 direction, float speed, float deltaTime)
        {
            characterController.Move((direction * speed + velocity) * deltaTime);
            if (IsGrounded())
            {
                velocity.y = 0;
            }
        }
    }
}
