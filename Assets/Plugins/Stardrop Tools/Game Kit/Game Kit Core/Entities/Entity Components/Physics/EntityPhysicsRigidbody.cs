using UnityEngine;

namespace StardropTools.GameKit
{
    public class EntityPhysicsRigidbody : EntityPhysicsComponent
    {
        [SerializeField] new Rigidbody rigidbody;
        [SerializeField] private float groundCheckDistance = 0.1f;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private bool showGroundCheckGizmo = true;
        [SerializeField] private Vector3 boxCastSize = new Vector3(1, .2f, 1);

        public override void Initialize(BaseEntity entity)
        {
            base.Initialize(entity);
            rigidbody = entity.GetComponent<Rigidbody>();
        }

        public override void ApplyForce(Vector3 force)
        {
            rigidbody.AddForce(force, ForceMode.Impulse);
        }

        public override void ApplyGravity(Vector3 gravity)
        {
            rigidbody.AddForce(gravity * Time.deltaTime, ForceMode.Acceleration);
        }

        public override bool IsGrounded()
        {
            return Physics.BoxCast(
                Transform.position,
                boxCastSize * 0.5f,
                Vector3.down,
                out RaycastHit hit,
                Quaternion.identity,
                groundCheckDistance,
                groundLayer
            );
        }

        public override void Jump(float jumpForce)
        {
            if (IsGrounded())
            {
                rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        public override void JumpTo(Transform targetPoint)
        {
            JumpTo(targetPoint.position);
        }

        public override void JumpTo(Vector3 targetPosition)
        {
            Vector3 direction = targetPosition - rigidbody.position;
            float distance = direction.magnitude;
            float gravity = Physics.gravity.magnitude;
            float jumpForce = Mathf.Sqrt(2 * gravity * distance);

            Jump(jumpForce);
        }

        public override void Move(Vector3 direction, float speed, float deltaTime)
        {
            rigidbody.MovePosition(rigidbody.position + direction * speed * deltaTime);
        }

        private void OnDrawGizmosSelected()
        {
            if (showGroundCheckGizmo && collider != null)
            {
                Gizmos.color = Color.red;
                Vector3 center = Transform.position;
                Vector3 halfExtents = boxCastSize * 0.5f;
                Gizmos.DrawWireCube(center - Vector3.up * groundCheckDistance, halfExtents);
            }
        }
    }
}
