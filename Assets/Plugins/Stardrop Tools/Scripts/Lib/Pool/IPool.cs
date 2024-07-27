
namespace StardropTools.Pool
{
    public interface IPool<T> where T : IPoolable<T>
    {
        T Spawn();
        void Despawn(T poolable);
    }
}
