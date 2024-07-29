
namespace StardropTools.GameKit
{
    public abstract class EntityModuleValueContainer<T> : BaseEntityComponent where T : struct
    {
        protected T value;
        protected T maxValue;

        public T Value => value;
        public T MaxValue => maxValue;
        public abstract float ValuePercentage { get; }

        public readonly EventDelegate<T> OnValueChanged = new EventDelegate<T>();
        public readonly EventDelegate OnMaxValueReached = new EventDelegate();

        public abstract void IncreaseValue(T amount);
        public abstract void DecreaseValue(T amount);
        public abstract void SetValue(T amount);
        public abstract void SetMaxValue(T amount);
        protected abstract void ValueChanged();
    }
}
