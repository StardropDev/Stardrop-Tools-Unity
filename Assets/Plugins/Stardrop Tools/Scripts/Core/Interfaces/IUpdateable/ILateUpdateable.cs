
namespace StardropTools
{
    public interface ILateUpdateable
    {
        bool IsLateUpdating { get; }

        void StartLateUpdate();

        void HandleLateUpdate();

        void StopLateUpdate();



        // Example implementation:

        /*
        public bool IsLateUpdating { get; private set; }

        public void StartLateUpdate()
        {
            if (IsLateUpdating)
            {
                return;
            }

            LoopManager.AddToUpdate(this);
            IsLateUpdating = true;
        }

        public void StopLateUpdate()
        {
            if (!IsLateUpdating)
            {
                return;
            }

            LoopManager.RemoveFromUpdate(this);
            IsLateUpdating = false;
        }

        public void HandleLateUpdate()
        {
            if (!IsLateUpdating)
            {
                return;
            }
        }
        */
    }
}
