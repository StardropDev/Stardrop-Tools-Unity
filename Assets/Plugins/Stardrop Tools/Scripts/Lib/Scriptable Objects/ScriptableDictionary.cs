using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools
{
    public class ScriptableDictionary<TKey, TValue> : ScriptableObject, IDictionary<TKey, TValue>
    {
        [SerializeField] private Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

        public TValue this[TKey key]
        {
            get => dictionary[key];
            set => dictionary[key] = value;
        }

        public ICollection<TKey> Keys => dictionary.Keys;

        public ICollection<TValue> Values => dictionary.Values;

        public int Count => dictionary.Count;

        public bool IsReadOnly => ((IDictionary<TKey, TValue>)dictionary).IsReadOnly;

        public void Add(TKey key, TValue value) => dictionary.Add(key, value);

        public void Add(KeyValuePair<TKey, TValue> item) => ((IDictionary<TKey, TValue>)dictionary).Add(item);

        public void Clear() => dictionary.Clear();

        public bool Contains(KeyValuePair<TKey, TValue> item) => ((IDictionary<TKey, TValue>)dictionary).Contains(item);

        public bool ContainsKey(TKey key) => dictionary.ContainsKey(key);

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => ((IDictionary<TKey, TValue>)dictionary).CopyTo(array, arrayIndex);

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => dictionary.GetEnumerator();

        public bool Remove(TKey key) => dictionary.Remove(key);

        public bool Remove(KeyValuePair<TKey, TValue> item) => ((IDictionary<TKey, TValue>)dictionary).Remove(item);

        public bool TryGetValue(TKey key, out TValue value) => dictionary.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => dictionary.GetEnumerator();
    }
}
