
namespace StardropTools.GameKit
{
    public interface IAnimated
    {
        void PlayAnimation(string animationName);
        void PlayAnimation(int animationID);
        void PlayAnimation(AnimState animationState);

        void CrossFadeAnimation(string animationName, float fadeLength = 0.15f);
        void CrossFadeAnimation(int animationID, float fadeLength = 0.15f);
        void CrossFadeAnimation(AnimState animationState, float fadeLength = 0.15f);
    }
}
