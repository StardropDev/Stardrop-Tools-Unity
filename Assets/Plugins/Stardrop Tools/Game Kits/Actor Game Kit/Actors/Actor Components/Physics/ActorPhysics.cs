using UnityEngine;

namespace StardropTools.GameKit.Actors
{
    public abstract class ActorPhysics : BaseActorComponent
    {
        [SerializeField] protected new Collider collider;

        public override void Initialize(BaseActor actor)
        {
            base.Initialize(actor);
            collider = actor.GetComponent<Collider>();
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