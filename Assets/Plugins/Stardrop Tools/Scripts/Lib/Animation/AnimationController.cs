﻿using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor.Animations;
#endif

namespace StardropTools
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private List<AnimationState> states = new List<AnimationState>();
        [SerializeField] private int currentStateId;

        public Animator Animator => animator;

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


        private void Start()
        {
            if (animator == null)
            {
                GetAnimator();
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
            animator.Play(animationState.stateHash);
        }

        public void PlayAnimation(string stateName)
        {
            AnimationState state = GetStateByName(stateName);
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



        public void CrosfadeAnimation(AnimationState animationState, float crossFade = 0)
        {
            if (animationState == null)
            {
                Debug.Log("Animation State is null!");
                return;
            }

            if (currentStateId == animationState.id)
                return;

            currentStateId = animationState.id;
            animator.CrossFade(animationState.stateHash, crossFade == 0 ? animationState.crossFade : crossFade);
        }

        public void CrosfadeAnimation(string stateName, float crossFade = 0)
        {
            AnimationState state = GetStateByName(stateName);
            if (state != null)
            {
                CrosfadeAnimation(state, crossFade);
            }
        }

        public void CrosfadeAnimation(int stateId, float crossFade = 0)
        {
            AnimationState state = GetStateByID(stateId);
            if (state != null)
            {
                CrosfadeAnimation(state, crossFade);
            }
        }

        public void CrosfadeAnimationByIndex(int index, float crossFade = 0)
        {
            if (index < 0 || index >= states.Count)
            {
                Debug.Log("Index out of range!");
                return;
            }

            AnimationState state = GetStateByIndex(index);
            if (state != null)
            {
                CrosfadeAnimation(state, crossFade);
            }
        }



        [NaughtyAttributes.Button("Get Animator")]
        public void GetAnimator()
        {
            animator = GetComponent<Animator>();

            if (animator == null)
            {
                animator = GetComponentInChildren<Animator>();
            }
        }

        private void OnValidate()
        {
#if UNITY_EDITOR
            RefreshNames();
#endif
        }

        #region Animation Utils
#if UNITY_EDITOR
        /// <summary>
        /// 
        /// 1) Get Animator Controller Reference
        /// 2) Get Animator Controller States
        /// 3) Get Animation Clips from Animator
        /// 4) Check if States.Length == AnimClips.Length
        /// 5) Loop through states
        /// 5.1) Create AnimState based on state & animClip info
        /// 
        /// </summary>
        [NaughtyAttributes.Button("Generate Anim States")]
        protected void GenerateAnimStates()
        {
            if (animator == null)
            {
                Debug.Log("Animator not found!");
                return;
            }

            // 1 & 2) Get Animator Controller States
            // 3) Get Animation Clips from Animator
            ChildAnimatorState[] controllerStates = AnimationUtils.GetAnimatorStates(AnimationUtils.GetAnimatorController(animator), 0);
            AnimationClip[] animClips = AnimationUtils.GetAnimationClips(animator);

            // 4) Check if States.Length == AnimClips.Length
            if (controllerStates.Length != animClips.Length)
            {
                Debug.Log("States.Length != Animation Clips.Length");
                return;
            }

            var animStateList = new List<AnimationState>();

            // 5) Loop through states
            // 5.1) Create AnimState based on state & animClip info
            for (int i = 0; i < controllerStates.Length; i++)
            {
                AnimatorState controllerState = controllerStates[i].state;
                AnimationState newState = new AnimationState()
                {
                    id = i,
                    stateName = controllerState.name,
                    clipName = animClips[i].name,
                    duration = animClips[i].length * controllerState.speed,
                    clip = animClips[i],
                    stateHash = controllerState.nameHash,
                    crossFade = .15f
                };

                animStateList.Add(newState);
                Debug.Log("State: " + controllerStates[i].state.name);
            }

            states = animStateList;
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
        #endregion
    }

    [System.Serializable]
    public class AnimationState
    {
#if UNITY_EDITOR
        public string EditorName;
#endif
        public int id;
        public string stateName;
        public string clipName;
        public float duration;
        public AnimationClip clip;
        public int stateHash;
        public float crossFade;
    }
}
