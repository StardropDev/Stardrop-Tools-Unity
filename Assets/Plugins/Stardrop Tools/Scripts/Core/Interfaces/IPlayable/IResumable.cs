
namespace StardropTools
{
    public interface IResumable
    {
        void Resume();
    }

    public interface IResumable<T>
    {
        T Resume();
    }
}
