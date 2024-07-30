using UnityEngine;

namespace StardropTools.GameKit.Actors
{
    public abstract class BaseActorComponent : BaseComponent
    {
        [SerializeField] protected BaseActor actor;

        public virtual void Initialize(BaseActor actor)
        {
            Initialize();
            this.actor = actor;
        }
    }
}