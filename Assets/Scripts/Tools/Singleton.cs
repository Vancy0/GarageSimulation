using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace generation
{
    public class MonoBehaviourSingleton<T> : MonoBehaviour
        where T : MonoBehaviourSingleton<T>
    {
        private static T instance;
        public static T Instance
        {
            get { return instance; }
        }

        public void Awake()
        {
            instance = this as T;
        }
    }

    public class Singleton<T>
        where T : Singleton<T>
    {
        private static T instance;
        public static T Instance
        {
            get { return instance; }
        }

        public void Awake()
        {
            instance = this as T;
        }
    }
}

