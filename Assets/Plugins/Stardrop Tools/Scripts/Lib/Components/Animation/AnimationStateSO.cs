
using UnityEngine;

namespace StardropTools
{
    [CreateAssetMenu(fileName = "AnimationState", menuName = "Stardrop Tools/Animation State")]
    public class AnimationStateSO : ScriptableObject
    {
        public AnimState animationState;
        public AnimationClip clip;

        public int ID => animationState.id;
        public string StateName => animationState.stateName;
        public int StateHash => animationState.stateHash;
        public string ClipName => animationState.clipName;
        public float Duration => animationState.duration;
        public float CrossFade => animationState.crossFade;
    }
}