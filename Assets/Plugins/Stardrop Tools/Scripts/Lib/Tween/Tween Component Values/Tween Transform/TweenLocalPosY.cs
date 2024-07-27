﻿using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenLocalPosY : TweenComponentValues<Transform, float>
    {
        public TweenLocalPosY(Transform targetComponent, float endValue) : base(targetComponent, endValue)
        {
            startValue = targetComponent.localPosition.y;
        }

        public TweenLocalPosY(Transform targetComponent, float startValue, float endValue)
            : base(targetComponent, startValue, endValue)
        {
        }

        public TweenLocalPosY(int id, Transform targetComponent, float startValue, float endValue)
            : base(id, targetComponent, startValue, endValue)
        {
        }

        public TweenLocalPosY(int id, Transform targetComponent, float startValue, float endValue, float duration, float delay = 0, EaseType easeType = EaseType.EaseInOutSine, LoopType loopType = LoopType.None, int loopCount = 0, AnimationCurve animationCurve = null, System.Action onCompleteCallback = null, System.Action<float> onValueChangedCallback = null)
            : base(id, targetComponent, startValue, endValue, duration, delay, easeType, loopType, loopCount, animationCurve, onCompleteCallback, onValueChangedCallback)
        {
        }

        protected override float Interpolate(float startValue, float endValue, float percent)
        {
            return Mathf.LerpUnclamped(startValue, endValue, percent);
        }

        protected override void ApplyValue(Transform component, float value)
        {
            var position = component.localPosition;
            position.y = value;
            component.localPosition = position;
        }
    }
}
