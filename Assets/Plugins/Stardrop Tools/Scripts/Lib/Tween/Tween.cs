using System;
using UnityEngine;

namespace StardropTools
{
    public abstract class Tween : ITween
    {
        private float deltaTime;
        private float duration;
        private float delay;
        private AnimationCurve curve;
        private bool isReversed;
        private int loopCount;
        private int currentLoopCount;

        private Action onCompleteCallback;

        public int ID { get; private set; }
        public EaseType EaseType { get; private set; }
        public LoopType LoopType { get; private set; }
        public PlayableState PlayableState { get; private set; } = PlayableState.None;
        public bool IsScheduledForRemoval => PlayableState == PlayableState.Stopped || PlayableState == PlayableState.Completed;

        public bool IsPlaying => PlayableState == PlayableState.Playing;
        public float Percent => Mathf.Clamp01(deltaTime / duration);
        public float EasedPercent => EasePercent(Percent);
        public float DeltaTime => deltaTime;
        public int LoopCount => loopCount;
        public int CurrentLoopCount => currentLoopCount;

        #region Get Tween As
        public TweenFloat AsFloat => this as TweenFloat;
        public TweenInt AsInt => this as TweenInt;
        public TweenVector2 AsVector2 => this as TweenVector2;
        public TweenVector3 AsVector3 => this as TweenVector3;
        public TweenVector4 AsVector4 => this as TweenVector4;
        public TweenQuaternion AsQuaternion => this as TweenQuaternion;
        public TweenColor AsColor => this as TweenColor;

        public TweenPosition AsPosition => this as TweenPosition;
        public TweenLocalPosition AsLocalPosition => this as TweenLocalPosition;
        public TweenRotation AsRotation => this as TweenRotation;
        public TweenLocalRotation AsLocalRotation => this as TweenLocalRotation;
        public TweenEulerAngles AsEulerAngles => this as TweenEulerAngles;
        public TweenLocalEulerAngles AsLocalEulerAngles => this as TweenLocalEulerAngles;
        public TweenScale AsScale => this as TweenScale;


        #endregion // Get Tween As


        public Tween(int id, float duration, float delay = 0, EaseType easeType = EaseType.EaseInOutSine, LoopType loopType = LoopType.None, int loopCount = 0, AnimationCurve animationCurve = null, Action onCompleteCallback = null)
        {
            this.ID = id;
            this.duration = duration;
            this.delay = delay;
            this.EaseType = easeType;
            this.LoopType = loopType;
            this.loopCount = loopCount;
            this.curve = animationCurve;
            this.onCompleteCallback = onCompleteCallback;

            this.deltaTime = 0;
            this.isReversed = false;
            this.currentLoopCount = loopCount;
        }

        public Tween SetDuration(float duration)
        {
            this.duration = duration;
            return this;
        }

        public Tween SetDelay(float delay)
        {
            this.delay = delay;
            return this;
        }

        public Tween SetDurationAndDelay(float duration, float delay)
        {
            this.duration = duration;
            this.delay = delay;
            return this;
        }

        public Tween SetEaseType(EaseType easeType)
        {
            this.EaseType = easeType;
            return this;
        }

        public Tween SetAnimationCurve(AnimationCurve curve, bool updateEaseType = false)
        {
            if (updateEaseType)
            {
                this.EaseType = EaseType.AnimationCurve;
            }

            this.curve = curve;
            return this;
        }

        public Tween SetLoopType(LoopType loopType, int loopCounts)
        {
            this.LoopType = loopType;
            SetLoopCounts(loopCounts);
            return this;
        }

        public Tween SetLoopCounts(int loopCounts)
        {
            this.loopCount = loopCounts;
            this.currentLoopCount = loopCount;
            return this;
        }

        public Tween SetOnCompleteCallback(Action onCompleteCallback)
        {
            this.onCompleteCallback = onCompleteCallback;
            return this;
        }

        public Tween Play()
        {
            deltaTime = 0;
            isReversed = false;
            currentLoopCount = loopCount;
            ChangePlayableState(PlayableState.Waiting);
            AddToTweenManager();
            return this;
        }

        public Tween Play(Action onCompleteCallback)
        {
            this.onCompleteCallback = onCompleteCallback;
            return Play();
        }

        public Tween Pause()
        {
            ChangePlayableState(PlayableState.Paused);
            return this;
        }

        public Tween Resume()
        {
            ChangePlayableState(PlayableState.Playing);
            return this;
        }

        public Tween Stop()
        {
            ChangePlayableState(PlayableState.Stopped);
            RemoveFromTweenManager();
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
                deltaTime += Time.deltaTime;
                if (deltaTime >= delay)
                {
                    ChangePlayableState(PlayableState.Playing);
                    deltaTime = 0;
                }
            }

            if (PlayableState == PlayableState.Playing)
            {
                deltaTime += Time.deltaTime * (isReversed ? -1 : 1);

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

                HandleTween(EasedPercent);
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
            RemoveFromTweenManager();
        }

        private void AddToTweenManager()
        {
            TweenManager.Instance.AddTween(this);
        }

        private void RemoveFromTweenManager()
        {
            TweenManager.Instance.RemoveTween(this);
        }

        private float EasePercent(float percent)
        {
            return EaseType == EaseType.AnimationCurve ? curve.Evaluate(percent) : EaseValue.Ease(EaseType, percent);
        }

        protected abstract void HandleTween(float percent);
    }
}
