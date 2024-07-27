using UnityEngine;

namespace StardropTools
{
    public class TweenLocalRotation : TweenComponentValues<Transform, Quaternion>
    {
        public TweenLocalRotation(Transform targetComponent, Quaternion endValue) : base(targetComponent, endValue)
        {
            startValue = targetComponent.localRotation;
        }

        public TweenLocalRotation(Transform targetComponent, Quaternion startValue, Quaternion endValue)
            : base(targetComponent, startValue, endValue)
        {
        }

        public TweenLocalRotation(int id, Transform targetComponent, Quaternion startValue, Quaternion endValue)
            : base(id, targetComponent, startValue, endValue)
        {
        }

        public TweenLocalRotation(int id, Transform targetComponent, Quaternion startValue, Quaternion endValue, float duration, float delay = 0, EaseType easeType = EaseType.EaseInOutSine, LoopType loopType = LoopType.None, int loopCount = 0, AnimationCurve animationCurve = null, System.Action onCompleteCallback = null, System.Action<Quaternion> onValueChangedCallback = null)
            : base(id, targetComponent, startValue, endValue, duration, delay, easeType, loopType, loopCount, animationCurve, onCompleteCallback, onValueChangedCallback)
        {
        }

        protected override Quaternion Interpolate(Quaternion startValue, Quaternion endValue, float percent)
        {
            return Quaternion.LerpUnclamped(startValue, endValue, percent);
        }

        protected override void ApplyValue(Transform component, Quaternion value)
        {
            component.localRotation = value;
        }
    }
}
