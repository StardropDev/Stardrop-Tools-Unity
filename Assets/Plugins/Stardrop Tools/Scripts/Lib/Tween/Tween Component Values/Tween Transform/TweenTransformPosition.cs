
using System;
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenTransformPosition : TweenTransform<Vector3>
    {
        public TweenTransformPosition(Transform targetComponent, Vector3 endValue, WorldSpaceTarget worldSpaceTarget) : base(targetComponent, endValue, worldSpaceTarget)
        {
        }

        public TweenTransformPosition(Transform targetComponent, Vector3 startValue, Vector3 endValue, WorldSpaceTarget worldSpaceTarget) : base(targetComponent, startValue, endValue, worldSpaceTarget)
        {
        }

        public TweenTransformPosition(int id, Transform targetComponent, Vector3 startValue, Vector3 endValue, WorldSpaceTarget worldSpaceTarget) : base(id, targetComponent, startValue, endValue, worldSpaceTarget)
        {
        }

        public TweenTransformPosition(int id, Transform targetComponent, Vector3 startValue, Vector3 endValue, WorldSpaceTarget worldSpaceTarget, float duration, float delay = 0, EaseType easeType = EaseType.EaseInOutSine, LoopType loopType = LoopType.None, int loopCount = 0, AnimationCurve animationCurve = null, Action onCompleteCallback = null, Action<Vector3> onValueChangedCallback = null) : base(id, targetComponent, startValue, endValue, worldSpaceTarget, duration, delay, easeType, loopType, loopCount, animationCurve, onCompleteCallback, onValueChangedCallback)
        {
        }

        protected override Vector3 Interpolate(Vector3 startValue, Vector3 endValue, float percent)
        {
            return Vector3.LerpUnclamped(startValue, endValue, percent);
        }

        protected override void ApplyValue(Transform component, Vector3 value)
        {
            if (worldSpaceTarget == WorldSpaceTarget.Local)
            {
                component.localPosition = value;
            }
            else
            {
                component.position = value;
            }
        }
    }
}