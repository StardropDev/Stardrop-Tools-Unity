using UnityEngine;

namespace StardropTools.GameComponentKit
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