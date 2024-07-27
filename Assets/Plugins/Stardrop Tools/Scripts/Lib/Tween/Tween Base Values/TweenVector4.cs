using UnityEngine;

namespace StardropTools
{
    public class TweenVector4 : TweenValue<Vector4>
    {
        public TweenVector4(Vector4 startValue, Vector4 endValue) : base(startValue, endValue)
        {
        }

        public TweenVector4(int id, Vector4 startValue, Vector4 endValue) : base(id, startValue, endValue)
        {
        }

        public TweenVector4(int id, Vector4 startValue, Vector4 endValue, float duration, float delay = 0, EaseType easeType = EaseType.EaseInOutSine, LoopType loopType = LoopType.None, int loopCount = 0, AnimationCurve animationCurve = null, System.Action onCompleteCallback = null, System.Action<Vector4> onValueChangedCallback = null)
            : base(id, startValue, endValue, duration, delay, easeType, loopType, loopCount, animationCurve, onCompleteCallback, onValueChangedCallback)
        {
        }

        protected override Vector4 Interpolate(Vector4 startValue, Vector4 endValue, float percent)
        {
            return Vector4.LerpUnclamped(startValue, endValue, percent);
        }
    }
}
