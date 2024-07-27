
namespace StardropTools
{
    public interface IPlayable
    {
        void Play();
    }

    public interface IPlayable<T>
    {
        T Play();
    }
}
