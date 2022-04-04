/*
 * Jaden Pleasants
 * Project 5/6
 * Generic singleton
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubmarineBigfish
{

    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T instance;

        public static T Instance => instance;

        public static bool IsInitialized => instance != null;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                Debug.LogError("Trying to instantiate a second instance of a singleton class.");
            }
        }

        protected virtual void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }
    }
}
