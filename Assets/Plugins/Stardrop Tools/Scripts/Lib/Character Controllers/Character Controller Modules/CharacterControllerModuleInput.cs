using UnityEngine;
using NaughtyAttributes;

namespace StardropTools.CharacterControllers
{
    [System.Serializable]
    public class CharacterControllerModuleInput
    {
        [ReadOnly]
        public float horizontal;

        [ReadOnly]
        public float vertical;

        [ReadOnly]
        public bool jump;

        [ReadOnly]
        public bool sprint;

        [ReadOnly]
        public bool slide;

        [ReadOnly]
        public bool crouch;

        [ReadOnly]
        public bool dodge;

        [ReadOnly]
        public bool attack;

        [ReadOnly]
        public bool block;

        [ReadOnly]
        public bool ability;

        [ReadOnly]
        public bool interact;

        [ReadOnly]
        public bool cancelInteract;

        [ReadOnly]
        public bool pause;



        [ShowNativeProperty]
        public Vector3 Direction => new Vector3(horizontal, 0, vertical);

        [ShowNativeProperty]
        public bool IsMoving => Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f;
    }
}
