﻿using UnityEngine;

namespace StardropTools
{
    public abstract class Singleton<T> : BaseComponent where T : MonoBehaviour
    {
        private static T _instance;
        private static readonly object _lock = new object();
        private static bool _applicationIsQuitting = false;

        public static T Instance
        {
            get
            {
                if (_applicationIsQuitting)
                {
                    Debug.LogWarning($"[Singleton] Instance '{typeof(T)}' already destroyed on application quit. Won't create again - returning null.");
                    return null;
                }

                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = (T)FindAnyObjectByType(typeof(T));

                        if (FindObjectsByType(typeof(T), FindObjectsSortMode.None).Length > 1)
                        {
                            Debug.LogError("[Singleton] Something went really wrong - there should never be more than 1 singleton! Reopening the scene might fix it.");
                            return _instance;
                        }

                        if (_instance == null)
                        {
                            GameObject singleton = new GameObject();
                            _instance = singleton.AddComponent<T>();
                            singleton.name = $"[Singleton] {typeof(T)}";

                            DontDestroyOnLoad(singleton);

                            Debug.Log($"[Singleton] An instance of {typeof(T)} is needed in the scene, so '{singleton.name}' was created with DontDestroyOnLoad.");
                        }
                        else
                        {
                            Debug.Log($"[Singleton] Using instance already created: {_instance.gameObject.name}");
                        }
                    }

                    return _instance;
                }
            }
        }

        protected virtual void OnApplicationQuit()
        {
            _applicationIsQuitting = true;
        }
    }
}