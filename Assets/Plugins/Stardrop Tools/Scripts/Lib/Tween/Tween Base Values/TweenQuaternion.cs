using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenQuaternion : TweenValue<Quaternion>
    {
        public TweenQuaternion(Quaternion startValue, Quaternion endValue) : base(startValue, endValue)
        {
        }

        public TweenQuaternion(int id, Quaternion startValue, Quaternion endValue) : base(id, startValue, endValue)
        {
        }

        public TweenQuaternion(int id, Quaternion startValue, Quaternion endValue, float duration, float delay = 0, EaseType easeType = EaseType.EaseInOutSine, LoopType loopType = LoopType.None, int loopCount = 0, AnimationCurve animationCurve = null, System.Action onCompleteCallback = null, System.Action<Quaternion> onValueChangedCallback = null)
            : base(id, startValue, endValue, duration, delay, easeType, loopType, loopCount, animationCurve, onCompleteCallback, onValueChangedCallback)
        {
        }

        protected override Quaternion Interpolate(Quaternion startValue, Quaternion endValue, float percent)
        {
            return Quaternion.SlerpUnclamped(startValue, endValue, percent);
        }
    }
}
