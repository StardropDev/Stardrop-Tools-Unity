using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using StardropTools.Pool;

public class TestPool : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Transform parentTransform;
    [SerializeField] private int initialPoolSize = 10;
    [SerializeField] private int maxPoolSize = 10;
    [SerializeField] private float spawnRange = 5f;

    private GameObjectPool<CubePoolable> pool;
    private List<CubePoolable> spawnedCubes = new List<CubePoolable>();
    private const string PoolKey = "CubePool";

    private void Start()
    {
        // Create the pool using PoolManager
        PoolManager.Instance.CreateGameObjectPool<CubePoolable>(PoolKey, initialPoolSize, maxPoolSize, cubePrefab, parentTransform);

        // Retrieve the pool instance from PoolManager
        pool = PoolManager.Instance.GetPool<GameObjectPool<CubePoolable>>(PoolKey);

        // Initialize the local pool instance if necessary
        InitializePool();
    }

    private void InitializePool()
    {
        if (pool == null)
        {
            Debug.LogError("Failed to retrieve pool from PoolManager.");
            return;
        }

        foreach (var cube in pool.GetAllObjects())
        {
            InitializeCube(cube);
        }
    }

    private void InitializeCube(CubePoolable cube)
    {
        cube.InitializeWithPool(pool);
    }

    [Button("Spawn Cube")]
    private void SpawnCube()
    {
        // Use PoolManager to spawn a cube
        CubePoolable cube = PoolManager.Instance.Spawn<CubePoolable>(PoolKey);
        cube.transform.position = GetRandomPosition();
        spawnedCubes.Add(cube);
    }

    [Button("Despawn All Cubes")]
    private void DespawnAllCubes()
    {
        foreach (CubePoolable cube in spawnedCubes)
        {
            // Use PoolManager to despawn the cube
            //PoolManager.Instance.Despawn(PoolKey, cube);
            cube.Despawn();
        }
        spawnedCubes.Clear();
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(
            Random.Range(-spawnRange, spawnRange),
            Random.Range(-spawnRange, spawnRange),
            Random.Range(-spawnRange, spawnRange)
        );
    }

    [Button("Refresh Max Pool Instances")]
    private void RefreshMaxPoolInstances()
    {
        if (maxPoolSize > 0)
        {
            PoolManager.Instance.SetPoolMaxInstances<CubePoolable>(PoolKey, maxPoolSize, true);
        }
    }
}
