using UnityEngine;

namespace StardropTools.GameKit.Actors
{
    public class ActorAnimatorController : ActorAnimation
    {
        [SerializeField] new private Animation animation;
        [SerializeField] private AnimationClip[] animationClips;

        public override void Initialize(BaseActor actor)
        {
            base.Initialize(actor);
            
            animation = actor.GetComponent<Animation>();
            if (animation == null)
                Debug.LogWarning("Animation component is not assigned.");

            GetAnimationClips();
        }

        public AnimationClip[] GetAnimationClips()
        {
            if (animation == null)
            {
                Debug.LogWarning("Animation component is not assigned.");
                return null;
            }

            animationClips = AnimationUtils.GetAnimationClips(animation);
            return animationClips;
        }

        public AnimationClip GetClipByIndex(int clipIndex)
        {
            if (clipIndex >= 0 && clipIndex < animationClips.Length)
                return animationClips[clipIndex];

            Debug.LogWarning("Invalid clip ID.");
            return null;
        }

        public AnimationClip GetClipByName(string clipName)
        {
            foreach (var clip in animationClips)
            {
                if (clip.name == clipName)
                    return clip;
            }

            Debug.LogWarning("Clip not found.");
            return null;
        }

        public override void PlayAnimation(string animationName)
        {
            if (animation == null)
            {
                Debug.LogWarning("Animation component is not assigned.");
                return;
            }

            if (currentAnimationData != null && currentAnimationData.animationName == animationName)
                return;

            animation.Play(animationName);
            currentAnimationData = new AnimationHandlerData(animationName);
        }

        public override void PlayAnimation(int animationID)
        {
            if (animation == null)
            {
                Debug.LogWarning("Animation component is not assigned.");
                return;
            }

            if (currentAnimationData != null && currentAnimationData.animationID == animationID)
                return;

            // Assuming animationID corresponds to the index in the animation clips
            if (animationID >= 0 && animationID < animation.GetClipCount())
            {
                var clip = GetClipByIndex(animationID);
                animation.Play(clip.name);
                currentAnimationData = new AnimationHandlerData(animationID);
            }
            else
            {
                Debug.LogWarning("Invalid animation ID.");
            }
        }

        public override void PlayAnimation(AnimState animationState)
        {
            if (animation == null)
            {
                Debug.LogWarning("Animation component is not assigned.");
                return;
            }

            if (currentAnimationData != null && currentAnimationData.animationState == animationState)
                return;

            animation.Play(animationState.clipName);
            currentAnimationData = new AnimationHandlerData(animationState);
        }

        public override void CrossFadeAnimation(string animationName, float fadeLength = 0.15F)
        {
            if (animation == null)
            {
                Debug.LogWarning("Animation component is not assigned.");
                return;
            }

            if (currentAnimationData != null && currentAnimationData.animationName == animationName)
                return;

            animation.CrossFade(animationName, fadeLength);
            currentAnimationData = new AnimationHandlerData(animationName);
        }

        public override void CrossFadeAnimation(int animationID, float fadeLength = 0.15F)
        {
            if (animation == null)
            {
                Debug.LogWarning("Animation component is not assigned.");
                return;
            }

            if (currentAnimationData != null && currentAnimationData.animationID == animationID)
                return;

            // Assuming animationID corresponds to the index in the animation clips
            if (animationID >= 0 && animationID < animation.GetClipCount())
            {
                var clip = GetClipByIndex(animationID); ;
                animation.CrossFade(clip.name, fadeLength);
                currentAnimationData = new AnimationHandlerData(animationID);
            }
            else
            {
                Debug.LogWarning("Invalid animation ID.");
            }
        }

        public override void CrossFadeAnimation(AnimState animationState, float fadeLength = 0.15F)
        {
            if (animation == null)
            {
                Debug.LogWarning("Animation component is not assigned.");
                return;
            }

            if (currentAnimationData != null && currentAnimationData.animationState == animationState)
                return;

            animation.CrossFade(animationState.clipName, fadeLength);
            currentAnimationData = new AnimationHandlerData(animationState);
        }
    }
}
