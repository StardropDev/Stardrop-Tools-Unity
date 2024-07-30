
using UnityEngine;

namespace StardropTools.GameKit.Actors
{
    public class CreatureActor : LivingActor, IAnimated
    {
        [SerializeField] protected ActorAnimation animationHandler;

        public void SetAnimationHandler<T>() where T : ActorAnimation
        {
            if (animationHandler != null)
            {
                Destroy(animationHandler);
            }

            animationHandler = gameObject.AddComponent<T>();
            animationHandler.Initialize(this);
        }

        public void PlayAnimation(string animationName)
        {
            if (animationHandler == null)
            {
                Debug.LogWarning("AnimationHandler is not assigned.");
                return;
            }

            animationHandler.PlayAnimation(animationName);
        }

        public void PlayAnimation(int animationID)
        {
            if (animationHandler == null)
            {
                Debug.LogWarning("AnimationHandler is not assigned.");
                return;
            }

            animationHandler.PlayAnimation(animationID);
        }

        public void PlayAnimation(AnimState animationState)
        {
            if (animationHandler == null)
            {
                Debug.LogWarning("AnimationHandler is not assigned.");
                return;
            }

            animationHandler.PlayAnimation(animationState);
        }

        public void CrossFadeAnimation(string animationName, float fadeLength = 0.15f)
        {
            if (animationHandler == null)
            {
                Debug.LogWarning("AnimationHandler is not assigned.");
                return;
            }

            animationHandler.CrossFadeAnimation(animationName, fadeLength);
        }

        public void CrossFadeAnimation(int animationID, float fadeLength = 0.15f)
        {
            if (animationHandler == null)
            {
                Debug.LogWarning("AnimationHandler is not assigned.");
                return;
            }

            animationHandler.CrossFadeAnimation(animationID, fadeLength);
        }

        public void CrossFadeAnimation(AnimState animationState, float fadeLength = 0.15f)
        {
            if (animationHandler == null)
            {
                Debug.LogWarning("AnimationHandler is not assigned.");
                return;
            }

            animationHandler.CrossFadeAnimation(animationState, fadeLength);
        }
    }
}
