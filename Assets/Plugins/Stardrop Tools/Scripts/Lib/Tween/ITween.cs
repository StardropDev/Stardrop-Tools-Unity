
namespace StardropTools.Tween
{
    public interface ITween : IPlayableState, IPlayable<Tween>, IPlayableCallback<Tween>, IPausable<Tween>, IResumable<Tween>, IStoppable<Tween>, IExecutable
    {
        int ID { get; }
        EaseType EaseType { get; }
        LoopType LoopType { get; }
        bool IsScheduledForRemoval { get; }
    }
}
