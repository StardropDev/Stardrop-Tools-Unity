using UnityEngine;

namespace StardropTools
{
    public class TweenPivot : TweenComponentValues<RectTransform, Vector2>
    {
        public TweenPivot(RectTransform targetComponent, Vector2 endValue) : base(targetComponent, endValue)
        {
            startValue = targetComponent.pivot;
        }

        public TweenPivot(RectTransform targetComponent, Vector2 startValue, Vector2 endValue)
            : base(targetComponent, startValue, endValue)
        {
        }

        public TweenPivot(int id, RectTransform targetComponent, Vector2 startValue, Vector2 endValue)
            : base(id, targetComponent, startValue, endValue)
        {
        }

        public TweenPivot(int id, RectTransform targetComponent, Vector2 startValue, Vector2 endValue, float duration, float delay = 0, EaseType easeType = EaseType.EaseInOutSine, LoopType loopType = LoopType.None, int loopCount = 0, AnimationCurve animationCurve = null, System.Action onCompleteCallback = null, System.Action<Vector2> onValueChangedCallback = null)
            : base(id, targetComponent, startValue, endValue, duration, delay, easeType, loopType, loopCount, animationCurve, onCompleteCallback, onValueChangedCallback)
        {
        }

        protected override Vector2 Interpolate(Vector2 startValue, Vector2 endValue, float percent)
        {
            return Vector2.LerpUnclamped(startValue, endValue, percent);
        }

        protected override void ApplyValue(RectTransform component, Vector2 value)
        {
            component.pivot = value;
        }
    }
}
