using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenScaleY : TweenComponentValues<Transform, float>
    {
        public TweenScaleY(Transform targetComponent, float endValue) : base(targetComponent, endValue)
        {
            startValue = targetComponent.localScale.y;
        }

        public TweenScaleY(Transform targetComponent, float startValue, float endValue)
            : base(targetComponent, startValue, endValue)
        {
        }

        public TweenScaleY(int id, Transform targetComponent, float startValue, float endValue)
            : base(id, targetComponent, startValue, endValue)
        {
        }

        public TweenScaleY(int id, Transform targetComponent, float startValue, float endValue, float duration, float delay = 0, EaseType easeType = EaseType.EaseInOutSine, LoopType loopType = LoopType.None, int loopCount = 0, AnimationCurve animationCurve = null, System.Action onCompleteCallback = null, System.Action<float> onValueChangedCallback = null)
            : base(id, targetComponent, startValue, endValue, duration, delay, easeType, loopType, loopCount, animationCurve, onCompleteCallback, onValueChangedCallback)
        {
        }

        protected override float Interpolate(float startValue, float endValue, float percent)
        {
            return Mathf.LerpUnclamped(startValue, endValue, percent);
        }

        protected override void ApplyValue(Transform component, float value)
        {
            var scale = component.localScale;
            scale.y = value;
            component.localScale = scale;
        }
    }
}
