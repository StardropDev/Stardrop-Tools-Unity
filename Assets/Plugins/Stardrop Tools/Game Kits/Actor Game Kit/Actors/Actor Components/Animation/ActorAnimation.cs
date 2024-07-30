
namespace StardropTools.GameKit.Actors
{
    public abstract class ActorAnimation : BaseActorComponent, IAnimated
    {
        [UnityEngine.SerializeField] internal AnimationHandlerData currentAnimationData;

        public abstract void PlayAnimation(string animationName);
        public abstract void PlayAnimation(int animationID);
        public abstract void PlayAnimation(AnimState animationState);

        public abstract void CrossFadeAnimation(string animationName, float fadeLength = 0.15f);
        public abstract void CrossFadeAnimation(int animationID, float fadeLength = 0.15f);
        public abstract void CrossFadeAnimation(AnimState animationState, float fadeLength = 0.15f);



        [System.Serializable]
        internal class AnimationHandlerData
        {
            public string animationName;
            public int animationID;
            public AnimState animationState;

            public AnimationHandlerData(string animationName)
            {
                this.animationName = animationName;
            }

            public AnimationHandlerData(int animationID)
            {
                this.animationID = animationID;
            }

            public AnimationHandlerData(AnimState animationState)
            {
                this.animationState = animationState;
            }
        }
    }
}
