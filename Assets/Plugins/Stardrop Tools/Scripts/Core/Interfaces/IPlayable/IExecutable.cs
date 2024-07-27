
namespace StardropTools
{
    public interface IExecutable
    {
        void Execute();
    }

    public interface IExecutable<T>
    {
        T Execute();
    }
}
