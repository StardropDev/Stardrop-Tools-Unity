
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// Objects 
    /// </summary>
    public class WorldEntity : WorldObject
    {
        [SerializeField] new Collider collider;
        [SerializeField] new Rigidbody rigidbody;


        public bool IsCollidable => collider != null ? collider.enabled : false;

        public bool IsKinematic => rigidbody != null ? rigidbody.isKinematic : false;


        public void SetEnabledCollisions(bool value)
        {
            if (collider == null)
            {
                return;
            }

            collider.enabled = value;
        }

        public void EnableCollision()
        {
           SetEnabledCollisions(true);
        }

        public void DisableCollision()
        {
            SetEnabledCollisions(false);
        }


        public void SetIsKinematic(bool value)
        {
            if (rigidbody == null)
            {
                return;
            }
            
            rigidbody.isKinematic = value;
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