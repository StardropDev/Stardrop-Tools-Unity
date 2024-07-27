using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenSizeDeltaX : TweenComponentValues<RectTransform, float>
    {
        public TweenSizeDeltaX(RectTransform targetComponent, float endValue) : base(targetComponent, endValue)
        {
            startValue = targetComponent.sizeDelta.x;
        }

        public TweenSizeDeltaX(RectTransform targetComponent, float startValue, float endValue)
            : base(targetComponent, startValue, endValue)
        {
        }

        public TweenSizeDeltaX(int id, RectTransform targetComponent, float startValue, float endValue)
            : base(id, targetComponent, startValue, endValue)
        {
        }

        public TweenSizeDeltaX(int id, RectTransform targetComponent, float startValue, float endValue, float duration, float delay = 0, EaseType easeType = EaseType.EaseInOutSine, LoopType loopType = LoopType.None, int loopCount = 0, AnimationCurve animationCurve = null, System.Action onCompleteCallback = null, System.Action<float> onValueChangedCallback = null)
            : base(id, targetComponent, startValue, endValue, duration, delay, easeType, loopType, loopCount, animationCurve, onCompleteCallback, onValueChangedCallback)
        {
        }

        protected override float Interpolate(float startValue, float endValue, float percent)
        {
            return Mathf.LerpUnclamped(startValue, endValue, percent);
        }

        protected override void ApplyValue(RectTransform component, float value)
        {
            var size = component.sizeDelta;
            size.x = value;
            component.sizeDelta = size;
        }
    }
}
