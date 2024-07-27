using System.Collections.Generic;

namespace StardropTools
{
    public class TimerManager : Singleton<TimerManager>
    {
        private readonly List<ITimer> timerList = new List<ITimer>();

        [NaughtyAttributes.ShowNativeProperty]
        public int TimerCount => timerList.Count;

        public override void HandleUpdate()
        {
            base.HandleUpdate();

            if (timerList.Count == 0)
            {
                StopUpdate();
                return;
            }

            // Execute all timers
            for (int i = 0; i < timerList.Count; i++)
            {
                ITimer timer = timerList[i];
                if (!timer.IsScheduledForRemoval)
                {
                    timer.Execute();
                }
            }

            // Remove timers marked for removal
            for (int i = timerList.Count - 1; i >= 0; i--)
            {
                ITimer timer = timerList[i];
                if (timer.IsScheduledForRemoval)
                {
                    timerList.RemoveAt(i);
                }
            }
        }

        public void AddTimer(ITimer timer)
        {
            if (timer == null || (timer.ID != -1 && timerList.Exists(t => t.ID == timer.ID)))
            {
                return;
            }

            timerList.Add(timer);

            if (timerList.Count > 0 && !IsUpdating)
            {
                StartUpdate();
            }
        }

        internal void RemoveTimer(ITimer timer)
        {
            if (timer == null || !timerList.Contains(timer))
            {
                return;
            }

            timerList.Remove(timer);

            if (timerList.Count == 0 && IsUpdating)
            {
                StopUpdate();
            }
        }
    }
}
