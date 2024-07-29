using System;

namespace StardropTools
{
    public struct Timer : ITimer
    {
        // Fields
        private float deltaTime;
        private readonly float duration;
        private readonly float delay;

        private readonly int loopCount;
        private int currentLoopCount;
        private bool isReversed;

        private Action onCompleteCallback;
        private Action<float> onUpdateCallback;

        public readonly EventDelegate OnTimerComplete;

        // Properties
        public int ID { get; private set; }
        public LoopType LoopType { get; private set; }
        public PlayableState PlayableState { get; private set; }
        public bool IsScheduledForRemoval => PlayableState == PlayableState.Stopped || PlayableState == PlayableState.Completed;

        public bool IsPlaying => PlayableState == PlayableState.Playing;
        public float Percent => deltaTime / duration;
        public float Duration => duration;
        public float DeltaTime => deltaTime;
        public int LoopCount => loopCount;
        public int CurrentLoopCount => currentLoopCount;

        // Constructors
        public Timer(float duration, Action onCompleteCallback = null, LoopType loopType = LoopType.None, int loopCount = 0)
            : this(-1, duration, 0, onCompleteCallback, null, loopType, loopCount) { }

        public Timer(float duration, LoopType loopType = LoopType.None, int loopCount = 0)
            : this(-1, duration, 0, null, null, loopType, loopCount) { }

        public Timer(int id, float duration, Action onCompleteCallback = null, LoopType loopType = LoopType.None, int loopCount = 0)
            : this(id, duration, 0, onCompleteCallback, null, loopType, loopCount) { }

        public Timer(float duration, float delay, Action onCompleteCallback = null, LoopType loopType = LoopType.None, int loopCount = 0)
            : this(-1, duration, delay, onCompleteCallback, null, loopType, loopCount) { }

        public Timer(float duration, float delay, LoopType loopType = LoopType.None, int loopCount = 0)
            : this(-1, duration, delay, null, null, loopType, loopCount) { }

        public Timer(int id, float duration, float delay, Action onCompleteCallback = null, Action<float> onUpdateCallback = null, LoopType loopType = LoopType.None, int loopCount = 0)
        {
            this.ID = id;
            this.duration = duration;
            this.delay = delay;
            this.deltaTime = 0;
            this.isReversed = false;
            this.LoopType = loopType;
            this.loopCount = loopCount;
            this.currentLoopCount = loopCount;

            this.PlayableState = PlayableState.None;

            this.onCompleteCallback = onCompleteCallback;
            this.onUpdateCallback = onUpdateCallback;
            OnTimerComplete = new EventDelegate();
        }

        // Methods
        public ITimer Play()
        {
            ResetTimer();
            ChangePlayableState(PlayableState.Waiting);
            AddToTimerManager();
            return this;
        }

        public ITimer Play(Action onCompleteCallback, Action<float> onUpdateCallback = null)
        {
            this.onCompleteCallback = onCompleteCallback;
            this.onUpdateCallback = onUpdateCallback;
            return Play();
        }

        public ITimer Play(Action onCompleteCallback)
        {
            this.onCompleteCallback = onCompleteCallback;
            return Play();
        }

        public ITimer Pause()
        {
            ChangePlayableState(PlayableState.Paused);
            return this;
        }

        public ITimer Resume()
        {
            ChangePlayableState(PlayableState.Playing);
            return this;
        }

        public ITimer Stop()
        {
            ChangePlayableState(PlayableState.Stopped);
            RemoveFromTimerManager();
            return this;
        }

        public void Execute()
        {
            if (PlayableState == PlayableState.Waiting)
            {
                HandleWaitingState();
            }

            if (PlayableState == PlayableState.Playing)
            {
                HandlePlayingState();
            }
        }

        // Helper Methods
        private void ChangePlayableState(PlayableState targetState)
        {
            if (targetState == PlayableState)
                return;

            PlayableState = targetState;
        }

        private void HandleWaitingState()
        {
            deltaTime += UnityEngine.Time.deltaTime;
            if (deltaTime >= delay)
            {
                ChangePlayableState(PlayableState.Playing);
                deltaTime = 0;
            }
        }

        private void HandlePlayingState()
        {
            deltaTime += UnityEngine.Mathf.Clamp(UnityEngine.Time.deltaTime * (isReversed ? -1 : 1), 0, duration);
            onUpdateCallback?.Invoke(deltaTime);

            if (deltaTime >= duration || deltaTime <= 0)
            {
                HandleLooping();
            }
        }

        private void HandleLooping()
        {
            if (LoopType == LoopType.Loop)
            {
                deltaTime = 0;
                DecrementLoopCount();
            }
            else if (LoopType == LoopType.PingPong)
            {
                isReversed = !isReversed;
                deltaTime = isReversed ? duration : 0;
                DecrementLoopCount();
            }
            else
            {
                Complete();
            }
        }

        private void DecrementLoopCount()
        {
            if (currentLoopCount > 0)
            {
                currentLoopCount--;
                if (currentLoopCount == 0)
                {
                    Complete();
                }
            }
        }

        private void Complete()
        {
            ChangePlayableState(PlayableState.Completed);
            deltaTime = 0;

            onCompleteCallback?.Invoke();
            OnTimerComplete?.Invoke();

            RemoveFromTimerManager();
        }

        private void ResetTimer()
        {
            deltaTime = 0;
            isReversed = false;
            currentLoopCount = loopCount;
        }

        private void AddToTimerManager()
        {
            TimerManager.Instance.AddTimer(this);
        }

        private void RemoveFromTimerManager()
        {
            TimerManager.Instance.RemoveTimer(this);
        }
    }
}
