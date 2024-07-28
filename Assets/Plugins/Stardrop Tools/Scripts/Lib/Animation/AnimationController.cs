using System.Collections;
using UnityEngine;

namespace StardropTools
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] Animator controller;
        [SerializeField] RuntimeAnimatorController runtimeController;
        [SerializeField] private AnimationState[] states;

        public RuntimeAnimatorController AnimatorController => runtimeController;
    }
}