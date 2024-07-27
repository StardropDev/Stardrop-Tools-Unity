using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenPosition : TweenComponentValues<Transform, Vector3>
    {
        private WorldSpaceTarget worldSpaceTarget;

        public TweenPosition(Transform targetComponent, Vector3 endValue) : base(targetComponent, endValue)
        {
            startValue = targetComponent.position;
        }

        public TweenPosition(Transform targetComponent, Vector3 startValue, Vector3 endValue)
            : base(targetComponent, startValue, endValue)
        {
        }

        public TweenPosition(int id, Transform targetComponent, Vector3 startValue, Vector3 endValue)
            : base(id, targetComponent, startValue, endValue)
        {
        }

        public TweenPosition(int id, Transform targetComponent, WorldSpaceTarget worldSpaceTarget, Vector3 startValue, Vector3 endValue, float duration, float delay = 0, EaseType easeType = EaseType.EaseInOutSine, LoopType loopType = LoopType.None, int loopCount = 0, AnimationCurve animationCurve = null, System.Action onCompleteCallback = null, System.Action<Vector3> onValueChangedCallback = null)
            : base(id, targetComponent, startValue, endValue, duration, delay, easeType, loopType, loopCount, animationCurve, onCompleteCallback, onValueChangedCallback)
        {
        }

        protected override Vector3 Interpolate(Vector3 startValue, Vector3 endValue, float percent)
        {
            return Vector3.LerpUnclamped(startValue, endValue, percent);
        }

        protected override void ApplyValue(Transform component, Vector3 value)
        {
            
            component.position = value;
        }
    }
}
