
using UnityEngine;

namespace StardropTools
{
    public class TweenVector2 : TweenValue<Vector2>
    {
        public TweenVector2(Vector2 startValue, Vector2 endValue) : base(startValue, endValue)
        {
        }

        public TweenVector2(int id, Vector2 startValue, Vector2 endValue) : base(id, startValue, endValue)
        {
        }

        public TweenVector2(int id, Vector2 startValue, Vector2 endValue, float duration, float delay = 0, EaseType easeType = EaseType.EaseInOutSine, LoopType loopType = LoopType.None, int loopCount = 0, UnityEngine.AnimationCurve animationCurve = null, System.Action onCompleteCallback = null, System.Action<Vector2> onValueChangedCallback = null) : base(id, startValue, endValue, duration, delay, easeType, loopType, loopCount, animationCurve, onCompleteCallback, onValueChangedCallback)
        {
        }

        protected override Vector2 Interpolate(Vector2 startValue, Vector2 endValue, float percent)
        {
            return Vector2.LerpUnclamped(startValue, endValue, percent);
        }
    }
}
