using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenInt : TweenValue<int>
    {
        public TweenInt(int startValue, int endValue) : base(startValue, endValue)
        {
        }

        public TweenInt(int id, int startValue, int endValue) : base(id, startValue, endValue)
        {
        }

        public TweenInt(int id, int startValue, int endValue, float duration, float delay = 0, EaseType easeType = EaseType.EaseInOutSine, LoopType loopType = LoopType.None, int loopCount = 0, AnimationCurve animationCurve = null, System.Action onCompleteCallback = null, System.Action<int> onValueChangedCallback = null)
            : base(id, startValue, endValue, duration, delay, easeType, loopType, loopCount, animationCurve, onCompleteCallback, onValueChangedCallback)
        {
        }

        protected override int Interpolate(int startValue, int endValue, float percent)
        {
            return Mathf.RoundToInt(Mathf.LerpUnclamped(startValue, endValue, percent));
        }
    }
}
