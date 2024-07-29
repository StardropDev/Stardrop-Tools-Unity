
using UnityEngine;

namespace StardropTools.CharacterControllers
{
    [System.Serializable]
    public class CharacterControllerModuleComponents
    {
        [Tooltip("The character controller for movement.")]
        public CharacterController characterController;
        
        [Tooltip("The characters Animation Controller for animation.")]
        public AnimationController animationController;

        [Tooltip("The animator component attached to the character.")]
        public Animator characterAnimator;

        [Tooltip("The character's model renderer.")]
        public Renderer characterRenderer;

        [Tooltip("The character's model renderer.")]
        public Transform rootTransform;
    }
}
