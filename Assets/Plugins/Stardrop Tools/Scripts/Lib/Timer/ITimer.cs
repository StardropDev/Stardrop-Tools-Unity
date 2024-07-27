
namespace StardropTools
{
    public interface ITimer : IPlayableState, IPlayable<ITimer>, IPlayableCallback<ITimer>, IPausable<ITimer>, IResumable<ITimer>, IStoppable<ITimer>, IExecutable
    {
        int ID { get; }
        LoopType LoopType { get; }
        bool IsScheduledForRemoval { get; }
    }
}
