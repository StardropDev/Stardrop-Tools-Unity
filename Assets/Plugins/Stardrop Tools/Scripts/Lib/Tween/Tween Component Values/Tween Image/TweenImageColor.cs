using UnityEngine;
using UnityEngine.UI;

namespace StardropTools
{
    public class TweenImageColor : TweenComponentValues<Image, Color>
    {
        public TweenImageColor(Image targetComponent, Color endValue) : base(targetComponent, endValue)
        {
            startValue = targetComponent.color;
        }

        public TweenImageColor(Image targetComponent, Color startValue, Color endValue)
            : base(targetComponent, startValue, endValue)
        {
        }

        public TweenImageColor(int id, Image targetComponent, Color startValue, Color endValue)
            : base(id, targetComponent, startValue, endValue)
        {
        }

        public TweenImageColor(int id, Image targetComponent, Color startValue, Color endValue, float duration, float delay = 0, EaseType easeType = EaseType.EaseInOutSine, LoopType loopType = LoopType.None, int loopCount = 0, AnimationCurve animationCurve = null, System.Action onCompleteCallback = null, System.Action<Color> onValueChangedCallback = null)
            : base(id, targetComponent, startValue, endValue, duration, delay, easeType, loopType, loopCount, animationCurve, onCompleteCallback, onValueChangedCallback)
        {
        }

        protected override Color Interpolate(Color startValue, Color endValue, float percent)
        {
            return Color.LerpUnclamped(startValue, endValue, percent);
        }

        protected override void ApplyValue(Image component, Color value)
        {
            component.color = value;
        }
    }
}
