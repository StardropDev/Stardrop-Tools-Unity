using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// BaseComponent is a base class for all components with custom initialization and update logic.
    /// </summary>
    public class BaseComponent : MonoBehaviour, IInitializable, ILateInitializable, IUpdateable
    {
        protected GameObject cachedGameObject;

        protected Transform cachedTransform;


        public GameObject GameObject => cachedGameObject != null ? cachedGameObject : cachedGameObject = gameObject;

        public Transform Transform => cachedTransform != null ? cachedTransform : cachedTransform = transform;
        

        public bool IsInitialized { get; private set; }

        public bool IsLateInitialized { get; private set; }


        public bool IsUpdating { get; private set; }

        public bool IsLateUpdating { get; private set; }

        public bool IsFixedUpdating { get; private set; }



        public virtual void Initialize()
        {
            if (IsInitialized)
            {
                return;
            }

            IsInitialized = true;
        }

        public void LateInitialize()
        {
            if (IsInitialized)
            {
                return;
            }

            IsLateInitialized = true;
        }



        public void StartUpdate()
        {
            if (IsUpdating)
            {
                return;
            }

            LoopManager.AddToUpdate(this);
            IsUpdating = true;
        }

        public void StopUpdate()
        {
            if (!IsUpdating)
            {
                return;
            }

            LoopManager.RemoveFromUpdate(this);
            IsUpdating = false;
        }

        public virtual void HandleUpdate() { }



        public void StartLateUpdate()
        {
            if (IsLateUpdating)
            {
                return;
            }

            LoopManager.AddToUpdate(this, 0, 0, UpdateType.LateUpdate);
            IsLateUpdating = false;
        }

        public void StopLateUpdate()
        {
            if (!IsLateUpdating)
            {
                return;
            }

            LoopManager.RemoveFromUpdate(this);
            IsLateUpdating = false;
        }

        public void HandleLateUpdate() { }



        public void StartFixedUpdate()
        {
            if (IsFixedUpdating)
            {
                return;
            }

            LoopManager.AddToUpdate(this, 0, 0, UpdateType.FixedUpdate);
            IsFixedUpdating = true;
        }

        public void StopFixedUpdate()
        {
            if (!IsFixedUpdating)
            {
                return;
            }

            LoopManager.AddToUpdate(this);
            IsFixedUpdating = false;
        }

        public void HandleFixedUpdate() { }



        public void SetActive(bool active)
        {
            GameObject.SetActive(active);
        }

        public void Activate()
        {
            SetActive(true);
        }

        public void Deactivate()
        {
            SetActive(false);
        }



        public void SetEnabled(bool enabled)
        {
            this.enabled = enabled;
        }

        public void Enable()
        {
            SetEnabled(true);
        }

        public void Disable()
        {
            SetEnabled(false);
        }

        public Vector3 GetPosition()
        {
            return Transform.position;
        }
    }
}
