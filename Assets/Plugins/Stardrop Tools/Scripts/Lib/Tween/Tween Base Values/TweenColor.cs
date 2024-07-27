using UnityEngine;

namespace StardropTools
{
    public class TweenColor : TweenValue<Color>
    {
        public TweenColor(Color startValue, Color endValue) : base(startValue, endValue)
        {
        }

        public TweenColor(int id, Color startValue, Color endValue) : base(id, startValue, endValue)
        {
        }

        public TweenColor(int id, Color startValue, Color endValue, float duration, float delay = 0, EaseType easeType = EaseType.EaseInOutSine, LoopType loopType = LoopType.None, int loopCount = 0, AnimationCurve animationCurve = null, System.Action onCompleteCallback = null, System.Action<Color> onValueChangedCallback = null)
            : base(id, startValue, endValue, duration, delay, easeType, loopType, loopCount, animationCurve, onCompleteCallback, onValueChangedCallback)
        {
        }

        protected override Color Interpolate(Color startValue, Color endValue, float percent)
        {
            return Color.LerpUnclamped(startValue, endValue, percent);
        }
    }
}
