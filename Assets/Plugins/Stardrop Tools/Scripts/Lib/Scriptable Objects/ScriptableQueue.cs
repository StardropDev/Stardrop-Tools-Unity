using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools
{
    public class ScriptableQueue<T> : ScriptableObject, IEnumerable<T>
    {
        [SerializeField] private Queue<T> queue = new Queue<T>();

        public int Count => queue.Count;

        public void Enqueue(T item) => queue.Enqueue(item);

        public T Dequeue() => queue.Dequeue();

        public T Peek() => queue.Peek();

        public void Clear() => queue.Clear();

        public bool Contains(T item) => queue.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => queue.CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator() => queue.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => queue.GetEnumerator();
    }
}
