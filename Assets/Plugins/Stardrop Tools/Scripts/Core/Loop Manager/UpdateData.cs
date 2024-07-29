namespace StardropTools
{
    public struct UpdateData
    {
        public float updateFrequency;
        public float nextUpdateTime;
        public IUpdateable updateable;
        public UpdateType updateType;
        
        public bool IsScheduledForRemoval { get; set; }
        //public bool HasDuration { get; set; }

        public UpdateData(float updateFrequency, IUpdateable updateable)
        {
            this.updateFrequency = updateFrequency;
            this.updateable = updateable;
            this.nextUpdateTime = UnityEngine.Time.time + updateFrequency;
            this.updateType = UpdateType.Update;

            IsScheduledForRemoval = false;
        }

        public UpdateData(float updateFrequency, IUpdateable updateable, UpdateType updateType)
        {
            this.updateFrequency = updateFrequency;
            this.updateable = updateable;
            this.nextUpdateTime = updateFrequency > 0 ? (updateType == UpdateType.FixedUpdate ? UnityEngine.Time.fixedTime + updateFrequency : UnityEngine.Time.time + updateFrequency) : 0;
            this.updateType = updateType;

            IsScheduledForRemoval = false;
        }

        public void CalculateNextUpdateTime()
        {
            nextUpdateTime = updateFrequency > 0 ? (updateType == UpdateType.FixedUpdate ? UnityEngine.Time.fixedTime + updateFrequency : UnityEngine.Time.time + updateFrequency) : 0;
        }
    }
}
