using System;
using UnityEngine;

namespace StardropTools.Pool
{
    public class GameObjectPool<T> : Pool<T>, IPool<T> where T : MonoBehaviour, IPoolable<T>
    {
        private readonly GameObject prefab;
        private readonly Transform parent;

        public GameObjectPool(GameObject prefab, int initialSize, int maxInstances, Transform parent = null, Action<T> onCreateAction = null)
            : base(initialSize, maxInstances, () => CreateInstance(prefab, parent), onCreateAction)
        {
            this.prefab = prefab;
            this.parent = parent;
        }

        public override T Spawn()
        {
            T obj = base.Spawn();

            if (obj.gameObject != null)
            {
                obj.gameObject.SetActive(true);
            }
            else
            {
                throw new ArgumentNullException("object");
            }
            
            return obj;
        }

        public T Spawn(Vector3 position, Transform parent = null)
        {
            T obj = Spawn();

            obj.gameObject.SetActive(true);
            obj.transform.position = position;
            obj.transform.SetParent(parent != null ? parent : this.parent);

            return obj;
        }

        public T Spawn(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            T obj = Spawn();

            obj.gameObject.SetActive(true);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.transform.SetParent(parent != null ? parent : this.parent);

            return obj;
        }

        public override void Despawn(T obj)
        {
            base.Despawn(obj);
            obj.gameObject.SetActive(false);
        }

        private static T CreateInstance(GameObject prefab, Transform parent)
        {
            GameObject instance = UnityEngine.Object.Instantiate(prefab, parent);
            T component = instance.GetComponent<T>();
            instance.SetActive(false);

            if (component == null)
            {
                throw new ArgumentException($"Prefab does not have the required component {typeof(T).Name}");
            }
            return component;
        }

        protected override void OnMaxInstancesChanged(bool destroy)
        {
            base.OnMaxInstancesChanged(destroy);

            for (int i = 0; i < activeObjectsList.Count; i++)
            {
                if (activeObjectsList[i] == null || activeObjectsList[i].gameObject == null)
                {
                    activeObjectsList.RemoveAt(i);
                    Debug.Log("Removed null objects from active list!");
                }
            }
        }
    }
}
