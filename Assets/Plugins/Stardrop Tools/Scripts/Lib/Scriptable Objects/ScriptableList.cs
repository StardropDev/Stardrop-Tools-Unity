﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools
{
    public class ScriptableList<T> : ScriptableObject, IList<T>
    {
        [SerializeField] private List<T> list = new List<T>();

        public T this[int index]
        {
            get => list[index];
            set => list[index] = value;
        }

        public int Count => list.Count;

        public bool IsReadOnly => ((IList<T>)list).IsReadOnly;

        public void Add(T item) => list.Add(item);

        public void Clear() => list.Clear();

        public bool Contains(T item) => list.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator() => list.GetEnumerator();

        public int IndexOf(T item) => list.IndexOf(item);

        public void Insert(int index, T item) => list.Insert(index, item);

        public bool Remove(T item) => list.Remove(item);

        public void RemoveAt(int index) => list.RemoveAt(index);

        IEnumerator IEnumerable.GetEnumerator() => list.GetEnumerator();
    }
}
