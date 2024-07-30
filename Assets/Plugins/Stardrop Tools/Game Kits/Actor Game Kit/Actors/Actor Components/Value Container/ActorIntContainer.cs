
namespace StardropTools.GameKit.Actors
{
    public class ActorIntContainer : ActorValueContainer<int>
    {
        public override float ValuePercentage => (float)value / maxValue * 100;

        public override void IncreaseValue(int amount)
        {
            value += amount;
            if (value >= maxValue)
            {
                value = maxValue;
                OnMaxValueReached.Invoke();
            }
            ValueChanged();
        }

        public override void DecreaseValue(int amount)
        {
            value -= amount;
            if (value < 0)
            {
                value = 0;
            }
            ValueChanged();
        }

        public override void SetValue(int amount)
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

        public override void SetMaxValue(int amount)
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
