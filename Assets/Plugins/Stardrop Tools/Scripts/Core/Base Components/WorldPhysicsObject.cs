using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// WorldPhysicsObject is a base class for all objects that have collisions and general physics in the world.
    /// </summary>
    public class WorldPhysicsObject : WorldObject
    {
        [SerializeField] new Collider collider;
        [SerializeField] new Rigidbody rigidbody;

        public bool IsCollidable => collider != null ? collider.enabled : false;
        public bool IsKinematic => rigidbody != null ? rigidbody.isKinematic : false;

        public void AddForce(Vector3 force, ForceMode forceMode = ForceMode.Impulse)
        {
            if (rigidbody == null) return;
            rigidbody.AddForce(force, forceMode);
        }

        public void AddForce(float force, Vector3 direction, ForceMode forceMode = ForceMode.Impulse)
        {
            if (rigidbody == null) return;
            rigidbody.AddForce(direction * force, forceMode);
        }

        public void SetVelocity(Vector3 velocity)
        {
            if (rigidbody == null) return;
            rigidbody.linearVelocity = velocity;
        }

        public Vector3 GetVelocity()
        {
            if (rigidbody == null) return Vector3.zero;
            return rigidbody.linearVelocity;
        }

        public void SetConstraints(RigidbodyConstraints constraints)
        {
            if (rigidbody == null) return;
            rigidbody.constraints = constraints;
        }

        public void FreeAllConstraints()
        {
            if (rigidbody == null) return;
            rigidbody.constraints = RigidbodyConstraints.None;
        }

        // Force and torque methods
        public void AddTorque(Vector3 torque, ForceMode forceMode = ForceMode.Impulse)
        {
            if (rigidbody == null) return;
            rigidbody.AddTorque(torque, forceMode);
        }

        public void AddExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius, float upwardsModifier = 0.0f, ForceMode forceMode = ForceMode.Force)
        {
            if (rigidbody == null) return;
            rigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, upwardsModifier, forceMode);
        }

        // Rigidbody movement methods
        public void MovePosition(Vector3 position)
        {
            if (rigidbody == null) return;
            rigidbody.MovePosition(position);
        }

        public void MoveRotation(Quaternion rotation)
        {
            if (rigidbody == null) return;
            rigidbody.MoveRotation(rotation);
        }

        // Rigidbody sleep and wake methods
        public void Sleep()
        {
            if (rigidbody == null) return;
            rigidbody.Sleep();
        }

        public void WakeUp()
        {
            if (rigidbody == null) return;
            rigidbody.WakeUp();
        }

        public void SetEnabledCollisions(bool value)
        {
            if (collider == null) return;
            collider.enabled = value;
        }

        public void SetIsKinematic(bool value)
        {
            if (rigidbody == null) return;
            rigidbody.isKinematic = value;
        }

        public void EnableCollision()
        {
            SetEnabledCollisions(true);
        }

        public void DisableCollision()
        {
            SetEnabledCollisions(false);
        }

        public void EnableKinematic()
        {
            SetIsKinematic(true);
        }

        public void DisableKinematic()
        {
            SetIsKinematic(false);
        }
    }
}
