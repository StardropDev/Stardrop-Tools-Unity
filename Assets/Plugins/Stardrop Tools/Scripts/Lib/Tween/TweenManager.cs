using System.Collections.Generic;

namespace StardropTools.Tween
{
    public class TweenManager : Singleton<TweenManager>
    {
        private readonly List<ITween> tweenList = new List<ITween>();

        [NaughtyAttributes.ShowNativeProperty]
        public int TweenCount => tweenList.Count;

        public override void HandleUpdate()
        {
            base.HandleUpdate();

            if (tweenList.Count == 0)
            {
                StopUpdate();
                return;
            }

            // Execute all tweens
            for (int i = 0; i < tweenList.Count; i++)
            {
                ITween tween = tweenList[i];
                if (!tween.IsScheduledForRemoval)
                {
                    tween.Execute();
                }
            }

            // Remove tweens marked for removal
            for (int i = tweenList.Count - 1; i >= 0; i--)
            {
                ITween tween = tweenList[i];
                if (tween.IsScheduledForRemoval)
                {
                    tweenList.RemoveAt(i);
                }
            }
        }

        public void AddTween(ITween tween)
        {
            if (tween == null || (tween.ID != -1 && tweenList.Exists(t => t.ID == tween.ID)))
            {
                return;
            }

            tweenList.Add(tween);

            if (tweenList.Count > 0 && !IsUpdating)
            {
                StartUpdate();
            }
        }

        internal void RemoveTween(ITween tween)
        {
            if (tween == null || !tweenList.Contains(tween))
            {
                return;
            }

            tweenList.Remove(tween);

            if (tweenList.Count == 0 && IsUpdating)
            {
                StopUpdate();
            }
        }
    }
}