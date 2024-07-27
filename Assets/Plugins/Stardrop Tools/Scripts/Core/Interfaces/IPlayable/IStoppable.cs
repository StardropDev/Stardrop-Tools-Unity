
namespace StardropTools
{
    public interface IStoppable
    {
        void Stop();
    }
    public interface IStoppable<T>
    {
        T Stop();
    }
}
