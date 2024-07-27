using System;
using UnityEngine;

namespace StardropTools
{
    public class Timer : ITimer
    {
        private float deltaTime;
        private readonly float duration;
        private readonly float delay;
        private bool isReversed;
        private readonly int loopCount;
        private int currentLoopCount;

        private Action onCompleteCallback;

        public readonly EventDelegate OnTimerComplete;

        public int ID { get; private set; }
        public LoopType LoopType { get; private set; }
        public PlayableState PlayableState { get; private set; } = PlayableState.None;
        public bool IsScheduledForRemoval => PlayableState == PlayableState.Stopped || PlayableState == PlayableState.Completed;

        public bool IsPlaying => PlayableState == PlayableState.Playing;
        public float Percent => deltaTime / duration;
        public float DeltaTime => deltaTime;
        public int LoopCount => loopCount;
        public int CurrentLoopCount => currentLoopCount;

        public Timer(float duration, Action onCompleteCallback = null, LoopType loopType = LoopType.None, int loopCount = 0)
            : this(-1, duration, 0, onCompleteCallback, loopType, loopCount) { }

        public Timer(float duration, LoopType loopType = LoopType.None, int loopCount = 0)
            : this(-1, duration, 0, null, loopType, loopCount) { }

        public Timer(int id, float duration, Action onCompleteCallback = null, LoopType loopType = LoopType.None, int loopCount = 0)
            : this(id, duration, 0, onCompleteCallback, loopType, loopCount) { }

        public Timer(float duration, float delay, Action onCompleteCallback = null, LoopType loopType = LoopType.None, int loopCount = 0)
            : this(-1, duration, delay, onCompleteCallback, loopType, loopCount) { }

        public Timer(float duration, float delay, LoopType loopType = LoopType.None, int loopCount = 0)
            : this(-1, duration, delay, null, loopType, loopCount) { }

        public Timer(int id, float duration, float delay, Action onCompleteCallback = null, LoopType loopType = LoopType.None, int loopCount = 0)
        {
            this.ID = id;
            this.duration = duration;
            this.delay = delay;
            this.deltaTime = 0;
            this.isReversed = false;
            this.LoopType = loopType;
            this.loopCount = loopCount;
            this.currentLoopCount = loopCount;

            this.onCompleteCallback = onCompleteCallback;
            OnTimerComplete = new EventDelegate();
        }

        public ITimer Play()
        {
            deltaTime = 0;
            isReversed = false;
            currentLoopCount = loopCount;
            ChangePlayableState(PlayableState.Waiting);
            AddToTimerManager();
            return this;
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

        private void ChangePlayableState(PlayableState targetState)
        {
            if (targetState == PlayableState)
                return;

            PlayableState = targetState;
        }

        public void Execute()
        {
            if (PlayableState == PlayableState.Waiting)
            {
                deltaTime += UnityEngine.Time.deltaTime;
                if (deltaTime >= delay)
                {
                    ChangePlayableState(PlayableState.Playing);
                    deltaTime = 0;
                }
            }

            if (PlayableState == PlayableState.Playing)
            {
                deltaTime += UnityEngine.Time.deltaTime * (isReversed ? -1 : 1);

                if (deltaTime >= duration || deltaTime <= 0)
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
                        return;
                    }
                }
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
