using UnityEngine;

namespace StardropTools
{
    [System.Serializable]
    public class AnimationState
    {
#if UNITY_EDITOR
        public string EditorName;
#endif
        public int id;
        public string stateName;
        public int stateHash;
        public string clipName;
        public float duration;
        public float crossFade;
        public bool loop;

        #nullable enable
        public AnimationClip? clip;
    }
}
