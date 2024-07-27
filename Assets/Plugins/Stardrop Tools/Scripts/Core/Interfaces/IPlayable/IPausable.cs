
namespace StardropTools
{
    public interface IPausable
    {
        void Pause();
    }

    public interface IPausable<T>
    {
        T Pause();
    }
}
