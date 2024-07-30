using UnityEngine;

namespace StardropTools.GameComponentKit
{
    public class EntityAnimatorController : EntityAnimationHandler
    {
        [SerializeField] private Animator animator;

        public override void Initialize(BaseEntity entity)
        {
            base.Initialize(entity);

            animator = entity.GetComponent<Animator>();
            if (animator == null)
                Debug.LogWarning("Animator component is not assigned.");
        }

        public override void PlayAnimation(string animationName)
        {
            if (animator == null)
            {
                Debug.LogWarning("Animator component is not assigned.");
                return;
            }

            if (currentAnimationData != null && currentAnimationData.animationName == animationName)
                return;

            animator.Play(animationName);
            currentAnimationData = new AnimationHandlerData(animationName);
        }

        public override void PlayAnimation(int animationID)
        {
            if (animator == null)
            {
                Debug.LogWarning("Animator component is not assigned.");
                return;
            }

            if (currentAnimationData != null && currentAnimationData.animationID == animationID)
                return;

            animator.Play(animationID);
            currentAnimationData = new AnimationHandlerData(animationID);
        }

        public override void PlayAnimation(AnimState animationState)
        {
            if (animator == null)
            {
                Debug.LogWarning("Animator component is not assigned.");
                return;
            }

            if (currentAnimationData != null && currentAnimationData.animationState == animationState)
                return;

            animator.Play(animationState.stateHash);
            currentAnimationData = new AnimationHandlerData(animationState);
        }

        public override void CrossFadeAnimation(string animationName, float fadeLength = 0.15F)
        {
            if (animator == null)
            {
                Debug.LogWarning("Animator component is not assigned.");
                return;
            }

            if (currentAnimationData != null && currentAnimationData.animationName == animationName)
                return;

            animator.CrossFade(animationName, fadeLength);
            currentAnimationData = new AnimationHandlerData(animationName);
        }

        public override void CrossFadeAnimation(int animationID, float fadeLength = 0.15F)
        {
            if (animator == null)
            {
                Debug.LogWarning("Animator component is not assigned.");
                return;
            }

            if (currentAnimationData != null && currentAnimationData.animationID == animationID)
                return;

            animator.CrossFade(animationID, fadeLength);
            currentAnimationData = new AnimationHandlerData(animationID);
        }

        public override void CrossFadeAnimation(AnimState animationState, float fadeLength = 0.15F)
        {
            if (animator == null)
            {
                Debug.LogWarning("Animator component is not assigned.");
                return;
            }

            if (currentAnimationData != null && currentAnimationData.animationState == animationState)
                return;

            animator.CrossFade(animationState.stateHash, fadeLength);
            currentAnimationData = new AnimationHandlerData(animationState);
        }
    }
}
