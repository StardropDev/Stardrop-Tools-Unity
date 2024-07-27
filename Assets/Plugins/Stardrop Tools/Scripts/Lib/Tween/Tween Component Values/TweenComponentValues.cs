using UnityEngine;

namespace StardropTools.Tween
{
    public abstract class TweenComponentValues<TComponent, TValue> : Tween where TComponent : Component
    {
        protected TComponent targetComponent;
        protected TValue startValue;
        protected TValue endValue;
        protected TValue currentValue;

        private System.Action<TValue> onValueChangedCallback;

        public TComponent TargetComponent => targetComponent;
        public TValue StartValue => startValue;
        public TValue EndValue => endValue;
        public TValue CurrentValue => currentValue;

        public readonly EventDelegate<TValue> OnValueChanged;

        public TweenComponentValues(TComponent targetComponent, TValue endValue)
            : this(-1, targetComponent, default, endValue, 0) { }

        public TweenComponentValues(TComponent targetComponent, TValue startValue, TValue endValue)
            : this(-1, targetComponent, startValue, endValue, 0) { }

        public TweenComponentValues(int id, TComponent targetComponent, TValue startValue, TValue endValue)
            : this(id, targetComponent, startValue, endValue, 0) { }

        public TweenComponentValues(int id, TComponent targetComponent, TValue startValue, TValue endValue, float duration, float delay = 0, EaseType easeType = EaseType.EaseInOutSine, LoopType loopType = LoopType.None, int loopCount = 0, AnimationCurve animationCurve = null, System.Action onCompleteCallback = null, System.Action<TValue> onValueChangedCallback = null)
            : base(id, duration, delay, easeType, loopType, loopCount, animationCurve, onCompleteCallback)
        {
            this.targetComponent = targetComponent;
            this.startValue = startValue;
            this.endValue = endValue;
            this.currentValue = startValue;

            this.onValueChangedCallback = onValueChangedCallback;
            OnValueChanged = new EventDelegate<TValue>();
        }

        public TweenComponentValues<TComponent, TValue> SetStartValue(TValue startValue)
        {
            this.startValue = startValue;
            return this;
        }

        public TweenComponentValues<TComponent, TValue> SetEndValue(TValue endValue)
        {
            this.endValue = endValue;
            return this;
        }

        public TweenComponentValues<TComponent, TValue> SetOnValueChangedCallback(System.Action<TValue> onValueChangedCallback)
        {
            this.onValueChangedCallback = onValueChangedCallback;
            return this;
        }

        protected override void HandleTween(float percent)
        {
            currentValue = Interpolate(startValue, endValue, percent);
            ApplyValue(targetComponent, currentValue);
            onValueChangedCallback?.Invoke(currentValue);
            OnValueChanged?.Invoke(currentValue);
        }

        protected abstract TValue Interpolate(TValue startValue, TValue endValue, float percent);

        protected abstract void ApplyValue(TComponent component, TValue value);
    }
}
