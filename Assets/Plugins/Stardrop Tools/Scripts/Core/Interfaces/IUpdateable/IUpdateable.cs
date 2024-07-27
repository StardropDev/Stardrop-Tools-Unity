
namespace StardropTools
{
    public interface IUpdateable
    {
        bool IsUpdating { get; }

        void StartUpdate();

        void HandleUpdate();

        void StopUpdate();



        // Example implementation:
        
        /*
        public bool IsUpdating { get; private set; }

        public void StartUpdate()
        {
            if (IsUpdating)
            {
                return;
            }

            LoopManager.AddToUpdate(this);
            IsUpdating = true;
        }

        public void StopUpdate()
        {
            if (!IsUpdating)
            {
                return;
            }

            LoopManager.RemoveFromUpdate(this);
            IsUpdating = false;
        }

        public void HandleUpdate()
        {
            if (!IsUpdating)
            {
                return;
            }
        }
        */
    }
}
