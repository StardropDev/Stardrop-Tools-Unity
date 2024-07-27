
using System;
using UnityEngine;

namespace StardropTools
{
    public abstract class TweenValue<T> : Tween
    {
        protected T startValue;
        protected T endValue;
        protected T currentValue;

        private System.Action<T> onValueChangedCallback;

        public T StartValue => startValue;
        public T EndValue => endValue;
        public T CurrentValue => currentValue;

        public readonly EventDelegate<T> OnValueChanged;

        public TweenValue(T startValue, T endValue)
            : this(-1, startValue, endValue, 0) { }

        public TweenValue(int id, T startValue, T endValue)
            : this(id, startValue, endValue, 0) { }

        public TweenValue(int id, T startValue, T endValue, float duration, float delay = 0, EaseType easeType = EaseType.EaseInOutSine, LoopType loopType = LoopType.None, int loopCount = 0, AnimationCurve animationCurve = null, System.Action onCompleteCallback = null, System.Action<T> onValueChangedCallback = null)
            : base(id, duration, delay, easeType, loopType, loopCount, animationCurve, onCompleteCallback)
        {
            this.startValue = startValue;
            this.endValue = endValue;
            this.currentValue = startValue;

            this.onValueChangedCallback = onValueChangedCallback;
            OnValueChanged = new EventDelegate<T>();
        }

        public TweenValue<T> SetStartValue(T startValue)
        {
            this.startValue = startValue;
            return this;
        }

        public TweenValue<T> SetEndValue(T endValue)
        {
            this.endValue = endValue;
            return this;
        }

        protected override void HandleTween(float percent)
        {
            currentValue = Interpolate(startValue, endValue, percent);
            onValueChangedCallback?.Invoke(currentValue);
            OnValueChanged?.Invoke(currentValue);
        }

        protected abstract T Interpolate(T startValue, T endValue, float percent);
    }
}
