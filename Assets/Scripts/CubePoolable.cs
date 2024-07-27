
using StardropTools.Pool;
using UnityEngine;

public class CubePoolable : MonoBehaviour, IPoolable<CubePoolable>
{
    private GameObjectPool<CubePoolable> pool;

    public void InitializeWithPool(IPool<CubePoolable> pool)
    {
        this.pool = pool as GameObjectPool<CubePoolable>;
    }

    public void OnSpawn()
    {
        Debug.Log("Cube spawned");
    }

    public void OnDespawn()
    {
        Debug.Log("Cube despawned");
    }

    public void Despawn()
    {
        Debug.Log("Cube Despawned Itself!");
        pool?.Despawn(this);
    }
}
