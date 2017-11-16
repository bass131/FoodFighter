using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hyun
{

    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {

        static T instance = null;
        static object objLock = new object();
        public static bool isQuitting = false;

        public static T Instance
        {
            get
            {
                if (isQuitting)
                {
                    Debug.Log.Error(typeof(T) + " [Singleton] Instance is already destroyed on application quit.");
                    return null;
                }

                lock (objLock)
                {
                    if (instance == null)
                    {
                        instance = GameObject.FindObjectOfType(typeof(T)) as T;
                        if (FindObjectsOfType(typeof(T)).Length > 1)
                        {
                            Debug.Log.Error(typeof(T) + " [Singleton] has more than one instance.");
                            return instance;
                        }

                        if (instance == null)
                        {
                            GameObject go = new GameObject();
                            instance = go.AddComponent<T>();
                            go.name = instance.GetType().Name;

                            DontDestroyOnLoad(go);
                        }
                    }

                    return instance;
                }
            }
        }

        public static bool Exists
        {
            get
            {
                return instance != null;
            }
        }

        //protected abstract void Awake();

        /// <summary>
        /// When Unity quits, it destroys objects in a random order.
        /// In principle, a Singleton is only destroyed when application quits.
        /// If any script calls Instance after it have been destroyed, 
        ///   it will create a buggy ghost object that will stay on the Editor scene
        ///   even after stopping playing the Application. Really bad!
        /// So, this was made to be sure we're not creating that buggy ghost object.
        /// </summary>
        // 	protected override void OnDestroy ()
        //     {
        // 		base.OnDestroy();
        // 		_instance = null;
        // 		applicationIsQuitting = true;
        // 	}

        protected virtual void OnApplicationQuit()
        {
            instance = null;
            isQuitting = true;
        }
    }
}