using System.Collections;
using UnityEngine;

namespace StardropTools
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] RuntimeAnimatorController controller;
        [SerializeField] private AnimationState[] states;
    }
}