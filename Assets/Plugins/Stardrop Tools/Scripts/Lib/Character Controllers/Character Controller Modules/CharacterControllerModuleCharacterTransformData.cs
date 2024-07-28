using UnityEngine;

namespace StardropTools.CharacterControllers
{
    [System.Serializable]
    public class CharacterControllerModuleCharacterTransformData
    {
        [Tooltip("The root transform of the character's graphics, typically used for orientation")]
        public Transform graphicsRoot;

        [Tooltip("The main character transform.")]
        public Transform characterTransform;

        [Tooltip("The character's head transform, useful for head look or camera follow.")]
        public Transform headTransform;

        [Tooltip("The character's right hand transform, useful for holding weapons or other objects.")]
        public Transform handTransformR;

        [Tooltip("The character's left hand transform, useful for holding weapons or other objects.")]
        public Transform handTransformL;

        [Tooltip("The character's right foot transform, useful for footstep sounds or ground detection.")]
        public Transform footTransformR;

        [Tooltip("The character's left foot transform, useful for footstep sounds or ground detection.")]
        public Transform footTransformL;
    }
}
