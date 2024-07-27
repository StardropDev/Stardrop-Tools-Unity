using System;
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Pool
{
    public class PoolManager : Singleton<PoolManager>
    {
        private Dictionary<string, object> pools = new Dictionary<string, object>();

        public T GetPool<T>(string poolKey) where T : class
        {
            if (pools.TryGetValue(poolKey, out object pool))
            {
                return pool as T;
            }

            throw new KeyNotFoundException($"Pool with poolKey {poolKey} does not exist.");
        }

        public void SetPoolMaxInstances<T>(string poolKey, int maxInstances, bool destroy = false) where T : IPoolable
        {
            if (pools.TryGetValue(poolKey, out object pool))
            {
                if (pool is Pool<T> poolT)
                {
                    poolT.SetMaxInstances(maxInstances, destroy);
                }
            }
            else
            {
                throw new KeyNotFoundException($"Pool with poolKey {poolKey} does not exist.");
            }
        }

        public void CreatePool<T>(string poolKey, int initialSize, Func<T> createFunc, Action<T> onCreateAction = null) where T : IPoolable
        {
            CreatePool<T>(poolKey, initialSize, 0, createFunc, onCreateAction);
        }

        public void CreatePool<T>(string poolKey, int initialSize, int maxInstances, Func<T> createFunc, Action<T> onCreateAction = null) where T : IPoolable
        {
            if (string.IsNullOrEmpty(poolKey))
            {
                throw new ArgumentException("Key cannot be null or empty.");
            }

            if (pools.ContainsKey(poolKey))
            {
                Debug.LogWarning($"Pool with poolKey {poolKey} already exists.");
                return;
            }

            Pool<T> pool = new Pool<T>(initialSize, maxInstances, createFunc, onCreateAction);
            pools.Add(poolKey, pool);
            Debug.Log($"Pool with poolKey {poolKey} created.");
        }

        public void CreateGameObjectPool<T>(string poolKey, int initialSize, GameObject prefab, Transform parent = null) where T : MonoBehaviour, IPoolable<T>
        {
            CreateGameObjectPool<T>(poolKey, initialSize, 0, prefab, parent);
        }

        public void CreateGameObjectPool<T>(string poolKey, int initialSize, int maxInstances, GameObject prefab, Transform parent = null) where T : MonoBehaviour, IPoolable<T>
        {
            if (string.IsNullOrEmpty(poolKey))
            {
                throw new ArgumentException("Key cannot be null or empty.");
            }

            if (pools.ContainsKey(poolKey))
            {
                Debug.LogWarning($"Pool with poolKey {poolKey} already exists.");
                return;
            }

            GameObjectPool<T> pool = new GameObjectPool<T>(prefab, initialSize, maxInstances, parent, null);
            pools.Add(poolKey, pool);
            Debug.Log($"GameObject pool with poolKey {poolKey} created.");
        }

        public T Spawn<T>(string poolKey) where T : IPoolable
        {
            if (string.IsNullOrEmpty(poolKey))
            {
                throw new ArgumentException("Key cannot be null or empty.");
            }

            if (pools.TryGetValue(poolKey, out object poolObj) && poolObj is Pool<T> pool)
            {
                return pool.Spawn();
            }

            throw new KeyNotFoundException($"Pool with poolKey {poolKey} does not exist.");
        }

        public T Spawn<T>(string poolKey, Vector3 position, Transform parent = null) where T : MonoBehaviour, IPoolable<T>
        {
            if (string.IsNullOrEmpty(poolKey))
            {
                throw new ArgumentException("Key cannot be null or empty.");
            }

            if (pools.TryGetValue(poolKey, out object poolObj))
            {
                if (poolObj is GameObjectPool<T> pool)
                {
                    return pool.Spawn(position, parent);
                }
                else
                {
                    Debug.LogError($"Pool with poolKey {poolKey} is not a GameObjectPool.");
                }
            }

            throw new KeyNotFoundException($"Pool with poolKey {poolKey} does not exist.");
        }

        public T Spawn<T>(string poolKey, Vector3 position, Quaternion rotation, Transform parent = null) where T : MonoBehaviour, IPoolable<T>
        {
            if (string.IsNullOrEmpty(poolKey))
            {
                throw new ArgumentException("Key cannot be null or empty.");
            }

            if (pools.TryGetValue(poolKey, out object poolObj))
            {
                if (poolObj is GameObjectPool<T> pool)
                {
                    return pool.Spawn(position, rotation, parent);
                }
                else
                {
                    Debug.LogError($"Pool with poolKey {poolKey} is not a GameObjectPool.");
                }
            }

            throw new KeyNotFoundException($"Pool with poolKey {poolKey} does not exist.");
        }

        public void Despawn<T>(string poolKey, T obj) where T : IPoolable
        {
            if (string.IsNullOrEmpty(poolKey))
            {
                throw new ArgumentException("Key cannot be null or empty.");
            }

            if (pools.TryGetValue(poolKey, out object poolObj) && poolObj is Pool<T> pool)
            {
                pool.Despawn(obj);
                Debug.Log($"Object despawned to pool with poolKey {poolKey}.");
            }
            else
            {
                throw new KeyNotFoundException($"Pool with poolKey {poolKey} does not exist.");
            }
        }

        public void DespawnAll<T>(string poolKey) where T : IPoolable
        {
            if (string.IsNullOrEmpty(poolKey))
            {
                throw new ArgumentException("Key cannot be null or empty.");
            }

            if (pools.TryGetValue(poolKey, out object poolObj) && poolObj is Pool<T> pool)
            {
                pool.DespawnAll();
                Debug.Log($"All objects despawned to pool with poolKey {poolKey}.");
            }
            else
            {
                throw new KeyNotFoundException($"Pool with poolKey {poolKey} does not exist.");
            }
        }

        public void DismissPool(string poolKey)
        {
            if (string.IsNullOrEmpty(poolKey))
            {
                throw new ArgumentException("Key cannot be null or empty.");
            }

            if (pools.ContainsKey(poolKey))
            {
                pools.Remove(poolKey);
                Debug.Log($"Pool with poolKey {poolKey} dismissed.");
            }
            else
            {
                Debug.LogWarning($"Pool with poolKey {poolKey} does not exist.");
            }
        }

        public void DismissAllPools()
        {
            pools.Clear();
            Debug.Log("All pools dismissed.");
        }
    }
}
