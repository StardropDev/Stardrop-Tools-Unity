using System;
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Pool
{
    public class Pool<T> where T : IPoolable
    {
        protected Queue<T> objectQueue;
        protected List<T> activeObjectsList;
        private readonly Func<T> createFunc;
        private readonly Action<T> onCreateAction;
        private int maxInstances;

        public int MaxInstances
        {
            get => maxInstances;
            set => SetMaxInstances(value);
        }

        public int TotalInstances => objectQueue.Count + activeObjectsList.Count;
        public int AvailableInstances => objectQueue.Count;
        public int ActiveInstances => activeObjectsList.Count;

        public Pool(int initialSize, int maxInstances, Func<T> createFunc, Action<T> onCreateAction = null)
        {
            this.objectQueue = new Queue<T>();
            this.activeObjectsList = new List<T>();
            this.createFunc = createFunc;
            this.onCreateAction = onCreateAction;
            this.maxInstances = maxInstances;

            for (int i = 0; i < initialSize; i++)
            {
                T obj = createFunc();
                onCreateAction?.Invoke(obj);
                obj.OnDespawn();
                objectQueue.Enqueue(obj);
                activeObjectsList.Add(obj);
            }
        }

        public virtual T Spawn()
        {
            // Recycle the oldest object
            if (objectQueue.Count == 0 && maxInstances > 0 && activeObjectsList.Count >= maxInstances)
            {
                T oldestObj = activeObjectsList[0];
                activeObjectsList.RemoveAt(0);
                oldestObj.OnDespawn();
                objectQueue.Enqueue(oldestObj);
            }

            T obj;
            do
            {
                obj = objectQueue.Count > 0 ? objectQueue.Dequeue() : createFunc();
            } while (obj == null);

            if (!activeObjectsList.Contains(obj))
            {
                activeObjectsList.Add(obj);
            }

            obj.OnSpawn();

            return obj;
        }

        public virtual void Despawn(T obj)
        {
            obj.OnDespawn();
            if (TotalInstances < maxInstances && !objectQueue.Contains(obj))
            {
                objectQueue.Enqueue(obj);
            }
            else
            {
                if (obj is MonoBehaviour monoBehaviour)
                {
                    UnityEngine.Object.Destroy(monoBehaviour.gameObject);
                }
            }
        }

        public void DespawnAll()
        {
            foreach (T obj in activeObjectsList)
            {
                if (!objectQueue.Contains(obj))
                {
                    Despawn(obj);
                }
            }
        }

        public List<T> GetAllObjects()
        {
            return activeObjectsList;
        }

        public Pool<T> SetMaxInstances(int maxInstances, bool destroy = false)
        {
            this.maxInstances = maxInstances;
            OnMaxInstancesChanged(destroy);
            return this;
        }

        protected virtual void OnMaxInstancesChanged(bool destroy)
        {
            // Are there LESS instances?
            if (TotalInstances < maxInstances)
            {
                Debug.Log("There are LESS Pool Instances!");

                for (int i = 0; i < maxInstances; i++)
                {
                    var obj = createFunc();
                    objectQueue.Enqueue(obj);

                    if (TotalInstances == maxInstances)
                    {
                        break;
                    }
                }
            }
            
            // Are there MORE instances?
            if (TotalInstances > maxInstances)
            {
                Debug.Log("There are MORE Pool Instances!");

                for (int i = 0; i < maxInstances; i++)
                {
                    if (objectQueue.Count == 0)
                    {
                        Debug.Log("Queue is empty!");
                        break;
                    }

                    T obj = objectQueue.Dequeue();
                    if (destroy && obj is MonoBehaviour monoBehaviour)
                    {
                        UnityEngine.Object.Destroy(monoBehaviour.gameObject);
                    }

                    if (TotalInstances == maxInstances)
                    {
                        break;
                    }
                }
            }
        }
    }
}
