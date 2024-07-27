using UnityEngine;

namespace StardropTools
{
    public class TweenVector3 : TweenValue<Vector3>
    {
        public TweenVector3(Vector3 startValue, Vector3 endValue) : base(startValue, endValue)
        {
        }

        public TweenVector3(int id, Vector3 startValue, Vector3 endValue) : base(id, startValue, endValue)
        {
        }

        public TweenVector3(int id, Vector3 startValue, Vector3 endValue, float duration, float delay = 0, EaseType easeType = EaseType.EaseInOutSine, LoopType loopType = LoopType.None, int loopCount = 0, AnimationCurve animationCurve = null, System.Action onCompleteCallback = null, System.Action<Vector3> onValueChangedCallback = null)
            : base(id, startValue, endValue, duration, delay, easeType, loopType, loopCount, animationCurve, onCompleteCallback, onValueChangedCallback)
        {
        }

        protected override Vector3 Interpolate(Vector3 startValue, Vector3 endValue, float percent)
        {
            return Vector3.LerpUnclamped(startValue, endValue, percent);
        }
    }
}
