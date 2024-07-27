using UnityEngine;
using UnityEngine.UI;

namespace StardropTools
{
    public class TweenImageAlpha : TweenComponentValues<Image, float>
    {
        public TweenImageAlpha(Image targetComponent, float endValue) : base(targetComponent, endValue)
        {
            startValue = targetComponent.color.a;
        }

        public TweenImageAlpha(Image targetComponent, float startValue, float endValue)
            : base(targetComponent, startValue, endValue)
        {
        }

        public TweenImageAlpha(int id, Image targetComponent, float startValue, float endValue)
            : base(id, targetComponent, startValue, endValue)
        {
        }

        public TweenImageAlpha(int id, Image targetComponent, float startValue, float endValue, float duration, float delay = 0, EaseType easeType = EaseType.EaseInOutSine, LoopType loopType = LoopType.None, int loopCount = 0, AnimationCurve animationCurve = null, System.Action onCompleteCallback = null, System.Action<float> onValueChangedCallback = null)
            : base(id, targetComponent, startValue, endValue, duration, delay, easeType, loopType, loopCount, animationCurve, onCompleteCallback, onValueChangedCallback)
        {
        }

        protected override float Interpolate(float startValue, float endValue, float percent)
        {
            return Mathf.LerpUnclamped(startValue, endValue, percent);
        }

        protected override void ApplyValue(Image component, float value)
        {
            Color color = component.color;
            color.a = value;
            component.color = color;
        }
    }
}
