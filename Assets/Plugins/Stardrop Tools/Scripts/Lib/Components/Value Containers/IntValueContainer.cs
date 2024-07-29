using UnityEngine;

namespace StardropTools
{
    public class IntValueContainer : BaseComponent
    {
        [SerializeField] int value = 0;
        [SerializeField] int minValue = 0;
        [SerializeField] int maxValue = 0;

        public int Value => value;
        public int MinValue => minValue;
        public int MaxValue => maxValue;
        public float Percent => (maxValue > minValue) ? (float)(value - minValue) / (maxValue - minValue) : 0f;
        public bool IsMinValue => value == minValue;

        public EventDelegate<int, int> OnValueChanged { get; private set; }
        public EventDelegate<int> OnMinValueChanged { get; private set; }
        public EventDelegate<int> OnMaxValueChanged { get; private set; }
        public EventDelegate<float> OnValuePercentChanged { get; private set; }
        public EventDelegate<int> OnMinValueReached { get; private set; }



        public override void Initialize()
        {
            base.Initialize();

            OnValueChanged = new EventDelegate<int, int>();
            OnValuePercentChanged = new EventDelegate<float>();

            OnMinValueChanged = new EventDelegate<int>();
            OnMaxValueChanged = new EventDelegate<int>();

            OnMinValueReached = new EventDelegate<int>();
        }



        public void SetValue(int value)
        {
            if (this.value == value)
                return;

            ValueChanged();
        }

        public void SetMinValue(int minValue)
        {
            if (this.minValue == minValue)
                return;

            this.minValue = minValue;

            OnMinValueChanged?.Invoke(minValue);
            OnValuePercentChanged?.Invoke(Percent);
        }

        public void SetMaxValue(int maxValue)
        {
            if (this.maxValue == maxValue)
                return;

            this.maxValue = maxValue;

            OnMaxValueChanged?.Invoke(maxValue);
            OnValuePercentChanged?.Invoke(Percent);
        }



        public void SetValueAndMaxValue(int value, int maxValue)
        {
            SetMinValue(0);
            SetMaxValue(maxValue);
            SetValue(value);
        }

        public void SetValueAndMinMaxValues(int value, int minValue, int maxValue)
        {
            SetMinValue(minValue);
            SetMaxValue(maxValue);
            SetValue(value);
        }



        public int AddValue(int value)
        {
            SetValue(this.value + value);
            return this.value;
        }

        public int SubtractValue(int value)
        {
            SetValue(this.value - value);
            return this.value;
        }



        protected virtual void ValueChanged()
        {
            if (value <= minValue)
            {
                value = minValue;
                OnMinValueReached?.Invoke(minValue);
            }
            else if (value > maxValue)
                value = maxValue;

            OnValueChanged?.Invoke(value, maxValue);
            OnValuePercentChanged?.Invoke(Percent);
        }
    }
}
