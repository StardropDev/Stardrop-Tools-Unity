using UnityEngine;

namespace StardropTools
{
    public class FloatValueContainer : BaseComponent
    {
        [SerializeField] float value = 0;
        [SerializeField] float minValue = 0;
        [SerializeField] float maxValue = 0;

        public float Value => value;
        public float MinValue => minValue;
        public float MaxValue => maxValue;
        public float Percent => (maxValue > minValue) ? (value - minValue) / (maxValue - minValue) : 0f;
        public bool IsMinValue => value == minValue;

        public EventDelegate<float, float> OnValueChanged { get; private set; }
        public EventDelegate<float> OnMinValueChanged { get; private set; }
        public EventDelegate<float> OnMaxValueChanged { get; private set; }
        public EventDelegate<float> OnValuePercentChanged { get; private set; }
        public EventDelegate OnMinValueReached { get; private set; }



        public override void Initialize()
        {
            base.Initialize();

            OnValueChanged = new EventDelegate<float, float>();
            OnValuePercentChanged = new EventDelegate<float>();

            OnMinValueChanged = new EventDelegate<float>();
            OnMaxValueChanged = new EventDelegate<float>();

            OnMinValueReached = new EventDelegate();
        }



        public void SetValue(float value)
        {
            if (this.value == value)
                return;

            ValueChanged();
        }

        public void SetMinValue(float minValue)
        {
            if (this.minValue == minValue)
                return;

            this.minValue = minValue;

            OnMinValueChanged?.Invoke(minValue);
            OnValuePercentChanged?.Invoke(Percent);
        }

        public void SetMaxValue(float maxValue)
        {
            if (this.maxValue == maxValue)
                return;

            this.maxValue = maxValue;

            OnMaxValueChanged?.Invoke(maxValue);
            OnValuePercentChanged?.Invoke(Percent);
        }



        public void SetValueAndMaxValue(float value, float maxValue)
        {
            SetMinValue(0);
            SetMaxValue(maxValue);
            SetValue(value);
        }

        public void SetValueAndMinMaxValues(float value, float minValue, float maxValue)
        {
            SetMinValue(minValue);
            SetMaxValue(maxValue);
            SetValue(value);
        }



        public float AddValue(float value)
        {
            SetValue(this.value + value);
            return this.value;
        }

        public float SubtractValue(float value)
        {
            SetValue(this.value - value);
            return this.value;
        }



        protected virtual void ValueChanged()
        {
            if (value <= minValue)
            {
                value = minValue;
                OnMinValueReached?.Invoke();
            }
            else if (value > maxValue)
                value = maxValue;

            OnValueChanged?.Invoke(value, maxValue);
            OnValuePercentChanged?.Invoke(Percent);
        }
    }
}
