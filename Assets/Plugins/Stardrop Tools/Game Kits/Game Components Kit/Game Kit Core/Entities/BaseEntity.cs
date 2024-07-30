using UnityEngine;

namespace StardropTools.GameComponentKit
{
    public abstract class BaseEntity : WorldObject
    {
        [SerializeField] protected EntityPhysicsComponent physics;

        public int ID { get; set; }
        public string Name { get; set; }

        public void SetPhysicsModule<T>() where T : EntityPhysicsComponent
        {
            if (physics != null)
            {
                Destroy(physics);
            }

            physics = gameObject.AddComponent<T>();
            physics.Initialize(this);
        }

        public void Move(Vector3 direction, float speed)
        {
            if (physics != null)
            {
                physics.Move(direction, speed, Time.deltaTime);
            }
        }

        public void ApplyForce(Vector3 force)
        {
            if (physics != null)
            {
                physics.ApplyForce(force);
            }
        }

        public void ApplyGravity(Vector3 gravity)
        {
            if (physics != null)
            {
                physics.ApplyGravity(gravity);
            }
        }

        public void Jump(float jumpForce)
        {
            if (physics != null)
            {
                physics.Jump(jumpForce);
            }
        }

        public void JumpTo(Transform targetPoint)
        {
            if (physics != null)
            {
                physics.JumpTo(targetPoint);
            }
        }

        public void JumpTo(Vector3 targetPosition)
        {
            if (physics != null)
            {
                physics.JumpTo(targetPosition);
            }
        }
    }
}
