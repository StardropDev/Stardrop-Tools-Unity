using System;
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools
{
    public class AnimationHandler : MonoBehaviour
    {
        [SerializeField] new Animation animation;
        [SerializeField] private List<AnimationState> states = new List<AnimationState>();
        [SerializeField] private int currentStateId;

        public Animation Animation => animation;

        public AnimationState GetStateByID(int id)
        {
            return states.Find(s => s.id == id);
        }

        public AnimationState GetStateByIndex(int index)
        {
            if (index < 0 || index >= states.Count)
            {
                Debug.Log("Index out of range!");
                return null;
            }

            return states[index];
        }

        public AnimationState GetStateByName(string stateName)
        {
            return states.Find(s => s.stateName == stateName);
        }

        public AnimationState GetStateByClipName(string clipName)
        {
            return states.Find(s => s.clipName == clipName);
        }

        private void Start()
        {
            if (animation == null)
            {
                GetAnimation();
            }
        }

        public void PlayAnimation(AnimationState animationState)
        {
            if (animationState == null)
            {
                Debug.Log("Animation State is null!");
                return;
            }

            if (currentStateId == animationState.id)
                return;

            currentStateId = animationState.id;
            animation.Play(animationState.clipName);
        }

        public void PlayAnimation(string clipName)
        {
            AnimationState state = GetStateByName(clipName);
            if (state != null)
            {
                PlayAnimation(state);
            }
        }

        public void PlayAnimation(int stateId)
        {
            AnimationState state = GetStateByID(stateId);
            if (state != null)
            {
                PlayAnimation(state);
            }
        }

        public void PlayAnimationByIndex(int index)
        {
            AnimationState state = GetStateByIndex(index);
            if (state != null)
            {
                PlayAnimation(state);
            }
        }

        public void PlayByClipName(string clipName)
        {
            AnimationState state = GetStateByClipName(clipName);
            if (state != null)
            {
                PlayAnimation(state);
            }
        }

        public void CrossfadeAnimation(AnimationState animationState, float crossFadeDuration = 0.3f)
        {
            if (animationState == null)
            {
                Debug.Log("Animation State is null!");
                return;
            }

            if (currentStateId == animationState.id)
                return;

            currentStateId = animationState.id;
            animation.CrossFade(animationState.clipName, crossFadeDuration);
        }

        public void CrossfadeAnimation(string stateName, float crossFadeDuration = 0.3f)
        {
            AnimationState state = GetStateByName(stateName);
            if (state != null)
            {
                CrossfadeAnimation(state, crossFadeDuration);
            }
        }

        public void CrossfadeAnimation(int stateId, float crossFadeDuration = 0.3f)
        {
            AnimationState state = GetStateByID(stateId);
            if (state != null)
            {
                CrossfadeAnimation(state, crossFadeDuration);
            }
        }

        public void CrossfadeAnimationByIndex(int index, float crossFadeDuration = 0.3f)
        {
            AnimationState state = GetStateByIndex(index);
            if (state != null)
            {
                CrossfadeAnimation(state, crossFadeDuration);
            }
        }

        public void CrossfadeByClipName(string clipName, float crossFadeDuration = 0.3f)
        {
            AnimationState state = GetStateByClipName(clipName);
            if (state != null)
            {
                CrossfadeAnimation(state, crossFadeDuration);
            }
        }

        [NaughtyAttributes.Button("Get Animation")]
        public void GetAnimation()
        {
            animation = GetComponent<Animation>();

            if (animation == null)
            {
                animation = GetComponentInChildren<Animation>();
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            RefreshNames();
        }

        [NaughtyAttributes.Button("Generate Anim States")]
        public void GenerateAnimStates()
        {
            if (animation == null)
            {
                Debug.Log("Animation component not found!");
                return;
            }

            var animClips = AnimationUtils.GetAnimationClips(animation);
            states.Clear();

            for (int i = 0; i < animClips.Length; i++)
            {
                var animClip = animClips[i];
                AnimationState newState = new AnimationState()
                {
                    id = i,
                    stateName = animClip.name,
                    clipName = animClip.name,
                    duration = animClip.length,
                    crossFade = 0.15f
                };

                states.Add(newState);
            }

            RefreshNames();
        }

        private void RefreshNames()
        {
            for (int i = 0; i < states.Count; i++)
            {
                var state = states[i];
                states[i].EditorName = $"{i} - {state.stateName}";
            }
        }
#endif
    }
}
