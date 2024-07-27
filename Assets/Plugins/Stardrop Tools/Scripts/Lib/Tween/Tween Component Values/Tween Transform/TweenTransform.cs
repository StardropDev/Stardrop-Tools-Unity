using System;
using UnityEngine;

namespace StardropTools.Tween
{
    public abstract class TweenTransform<T> : TweenComponentValues<Transform, T>
    {
        protected WorldSpaceTarget worldSpaceTarget;

        public Tween SetWorldSpaceTarget(WorldSpaceTarget worldSpaceTarget)
        {
            this.worldSpaceTarget = worldSpaceTarget;
            return this;
        }

        public TweenTransform(Transform targetComponent, T endValue, WorldSpaceTarget worldSpaceTarget) : base(targetComponent, endValue)
        {
            this.worldSpaceTarget = worldSpaceTarget;
        }

        public TweenTransform(Transform targetComponent, T startValue, T endValue, WorldSpaceTarget worldSpaceTarget) : base(targetComponent, startValue, endValue)
        {
            this.worldSpaceTarget = worldSpaceTarget;
        }

        public TweenTransform(int id, Transform targetComponent, T startValue, T endValue, WorldSpaceTarget worldSpaceTarget) : base(id, targetComponent, startValue, endValue)
        {
            this.worldSpaceTarget = worldSpaceTarget;
        }

        public TweenTransform(int id, Transform targetComponent, T startValue, T endValue, WorldSpaceTarget worldSpaceTarget, float duration, float delay = 0, EaseType easeType = EaseType.EaseInOutSine, LoopType loopType = LoopType.None, int loopCount = 0, AnimationCurve animationCurve = null, Action onCompleteCallback = null, Action<T> onValueChangedCallback = null) : base(id, targetComponent, startValue, endValue, duration, delay, easeType, loopType, loopCount, animationCurve, onCompleteCallback, onValueChangedCallback)
        {
            this.worldSpaceTarget = worldSpaceTarget;
        }
    }
}