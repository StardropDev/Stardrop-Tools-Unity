
namespace StardropTools
{
    public interface IFixedUpdateable
    {
        bool IsFixedUpdating { get; }

        void StartFixedUpdate();

        void HandleFixedUpdate();

        void StopFixedUpdate();



        // Example implementation:

        /*
        public bool IsFixedUpdating { get; private set; }

        public void StartFixedUpdate()
        {
            if (IsFixedUpdating)
            {
                return;
            }

            LoopManager.AddToUpdate(this);
            IsFixedUpdating = true;
        }

        public void StopFixedUpdate()
        {
            if (!IsFixedUpdating)
            {
                return;
            }

            LoopManager.RemoveFromUpdate(this);
            IsFixedUpdating = false;
        }

        public void HandleFixedUpdate()
        {
            if (!IsFixedUpdating)
            {
                return;
            }
        }
        */
    }
}
