
namespace StardropTools.GameComponentKit
{
    public class EntityModuleValueFloat : EntityModuleValueContainer<float>
    {
        public override float ValuePercentage => value / maxValue * 100;

        public override void IncreaseValue(float amount)
        {
            value += amount;
            if (value >= maxValue)
            {
                value = maxValue;
                OnMaxValueReached.Invoke();
            }
            ValueChanged();
        }

        public override void DecreaseValue(float amount)
        {
            value -= amount;
            if (value < 0)
            {
                value = 0;
            }
            ValueChanged();
        }

        public override void SetValue(float amount)
        {
            value = amount;
            if (value >= maxValue)
            {
                value = maxValue;
                OnMaxValueReached.Invoke();
            }
            else if (value < 0)
            {
                value = 0;
            }
            ValueChanged();
        }

        public override void SetMaxValue(float amount)
        {
            maxValue = amount;
            if (value > maxValue)
            {
                value = maxValue;
                OnMaxValueReached.Invoke();
            }
            ValueChanged();
        }

        protected override void ValueChanged()
        {
            OnValueChanged.Invoke(value);
        }
    }
}
