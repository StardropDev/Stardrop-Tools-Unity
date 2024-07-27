
namespace StardropTools
{
    public interface ILateInitializable
    {
        bool IsLateInitialized { get; }

        void LateInitialize();
    }
}
