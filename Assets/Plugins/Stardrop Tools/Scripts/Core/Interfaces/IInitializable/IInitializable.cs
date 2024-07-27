
namespace StardropTools
{
    public interface IInitializable
    {
        bool IsInitialized { get; }

        void Initialize();
    }
}
