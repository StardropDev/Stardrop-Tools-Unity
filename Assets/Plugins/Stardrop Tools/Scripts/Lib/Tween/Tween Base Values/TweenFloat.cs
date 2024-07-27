
namespace StardropTools.Tween
{
    public class TweenFloat : TweenValue<float>
    {
        public TweenFloat(float startValue, float endValue) : base(startValue, endValue)
        {
        }

        public TweenFloat(int id, float startValue, float endValue) : base(id, startValue, endValue)
        {
        }

        public TweenFloat(int id, float startValue, float endValue, float duration, float delay = 0, EaseType easeType = EaseType.EaseInOutSine, LoopType loopType = LoopType.None, int loopCount = 0, UnityEngine.AnimationCurve animationCurve = null, System.Action onCompleteCallback = null, System.Action<float> onValueChangedCallback = null) : base(id, startValue, endValue, duration, delay, easeType, loopType, loopCount, animationCurve, onCompleteCallback, onValueChangedCallback)
        {
        }

        protected override float Interpolate(float startValue, float endValue, float percent)
        {
            return UnityEngine.Mathf.LerpUnclamped(startValue, endValue, percent);
        }
    }
}
