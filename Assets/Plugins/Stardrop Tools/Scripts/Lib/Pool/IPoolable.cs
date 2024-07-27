
namespace StardropTools.Pool
{
    public interface IPoolable
    {
        void OnSpawn();
        void OnDespawn();
    }

    public interface IPoolable<T> : IPoolable where T : IPoolable<T>
    {
        void InitializeWithPool(IPool<T> pool);
    }
}
