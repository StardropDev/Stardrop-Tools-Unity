using UnityEngine;

namespace StardropTools.GameKit
{
    public abstract class BaseEntityComponent : BaseComponent
    {
        [SerializeField] protected BaseEntity entity;

        public virtual void Initialize(BaseEntity entity)
        {
            Initialize();
            this.entity = entity;
        }
    }
}