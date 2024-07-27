using UnityEngine;

namespace StardropTools
{
    public class TweenOffsetMinMax : TweenComponentValues<RectTransform, Vector2>
    {
        private bool isMin;

        public TweenOffsetMinMax(RectTransform targetComponent, Vector2 endValue, bool isMin) : base(targetComponent, endValue)
        {
            this.isMin = isMin;
            startValue = isMin ? targetComponent.offsetMin : targetComponent.offsetMax;
        }

        public TweenOffsetMinMax(RectTransform targetComponent, Vector2 startValue, Vector2 endValue, bool isMin)
            : base(targetComponent, startValue, endValue)
        {
            this.isMin = isMin;
        }

        public TweenOffsetMinMax(int id, RectTransform targetComponent, Vector2 startValue, Vector2 endValue, bool isMin)
            : base(id, targetComponent, startValue, endValue)
        {
            this.isMin = isMin;
        }

        public TweenOffsetMinMax(int id, RectTransform targetComponent, Vector2 startValue, Vector2 endValue, bool isMin, float duration, float delay = 0, EaseType easeType = EaseType.EaseInOutSine, LoopType loopType = LoopType.None, int loopCount = 0, AnimationCurve animationCurve = null, System.Action onCompleteCallback = null, System.Action<Vector2> onValueChangedCallback = null)
            : base(id, targetComponent, startValue, endValue, duration, delay, easeType, loopType, loopCount, animationCurve, onCompleteCallback, onValueChangedCallback)
        {
            this.isMin = isMin;
        }

        protected override Vector2 Interpolate(Vector2 startValue, Vector2 endValue, float percent)
        {
            return Vector2.LerpUnclamped(startValue, endValue, percent);
        }

        protected override void ApplyValue(RectTransform component, Vector2 value)
        {
            if (isMin)
            {
                component.offsetMin = value;
            }
            else
            {
                component.offsetMax = value;
            }
        }
    }
}
