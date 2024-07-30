using UnityEngine;

namespace StardropTools.GameComponentKit
{
    public abstract class EntityPhysicsComponent : BaseEntityComponent
    {
        [SerializeField] protected new Collider collider;

        public override void Initialize(BaseEntity entity)
        {
            base.Initialize(entity);
            collider = entity.GetComponent<Collider>();
        }

        public abstract void Move(Vector3 direction, float speed, float deltaTime);
        public abstract void ApplyForce(Vector3 force);
        public abstract void ApplyGravity(Vector3 gravity);
        public abstract void Jump(float jumpForce);
        public abstract void JumpTo(Transform targetPoint);
        public abstract void JumpTo(Vector3 targetPosition);
        public abstract bool IsGrounded();
    }
}