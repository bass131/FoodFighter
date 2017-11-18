/*
 * Usage

    There are two main points of interest:

    A pool manager class for Unity Game Objects that allows you to easily pool scene objects

    A generic object pool collection that can be used for non Unity Game Objects.

    ###Pooling Unity Game Objects: PoolManager.cs is a very flexible class that co-ordinates all your pooling requirements in a scene.

    It requires no initialization, can be called from anywhere, and dynamically accomodates any prefab game object.

    //Optional: Warm the pool and preallocate memory
    void Start()
    {
	    PoolManager.WarmPool(bulletPrefab, 50);
	
	    //Notes
	    // Make sure the prefab is inactive, or else it will run update before first use
    }

    //Spawn pooled objects
    void FireBullet(Vector3 position, Quaternion rotation)
    {
	    var bullet = PoolManager.SpawnObject(bulletPrefab, position, rotation).GetComponent<Bullet>();
		
	    //Notes:
	    // bullet.gameObject.SetActive(true) is automatically called on spawn 
	    // When done with the instance, you MUST release it!
	    // if the number of objects in use exceeds the pool size, new objects will be created
	
    }

    //In Bullet.cs
    void Finish()
    {
        PoolManager.ReleaseObject(this.gameObject);
    
        //Notes
        // This takes the gameObject instance, and NOT the prefab instance.
        // Without this call the object will never be available for re-use!
        // gameObject.SetActive(false) is automatically called;
    }
    ###Pooling C# objects: This allows you to pool objects not derived from the Unity engine. In fact if you replaced the Debug statemetns you could use this in any other .NET or C# project.

    This is the backbone of the PoolManager.cs class, but you can use it directly. For instance you could use it to pool events in a memory friendly observer pattern:

    //The factoryFunc (first arg) is the crux of the ObjectPool class. 
    //It privodes a way for the ObjectPool to dynamically create new objects
    eventPool = new ObjectPool<DelayedEvent>(()=> new DelayedEvent(), 5);

    var evt = eventPool.GetItem();
    evt.Start(Time.time, delay); //Configure object

    //On event done:
    eventPool.ReleaseItem(evt);
	
 */
using System;
using System.Collections.Generic;

using UnityEngine;

namespace Hyun
{
    using Collections;
    public class PoolManager : Singleton<PoolManager>
    {
        public bool logStatus;
        public Transform root;

        private Dictionary<GameObject, ObjectPool<GameObject>> prefabLookup = new Dictionary<GameObject, ObjectPool<GameObject>>();
        private Dictionary<GameObject, ObjectPool<GameObject>> instanceLookup = new Dictionary<GameObject, ObjectPool<GameObject>>();

        private bool dirty = false;
        private bool isInitialized = false;

        public static void Initialize()
        {
            PoolManager.Instance.Uninitialize();


        }

        public void Uninitialize()
        {
            prefabLookup.Clear();
            instanceLookup.Clear();
        }

        public void Update()
        {
            if (logStatus && dirty)
            {
                PrintStatus();
                dirty = false;
            }
        }

        void warmPool(GameObject prefab, int size)
        {

            if (prefabLookup.ContainsKey(prefab))
            {
                throw new Exception("Pool for prefab " + prefab.name + " has already been created");
            }
            var pool = new ObjectPool<GameObject>(() => { return InstantiatePrefab(prefab); }, size);
            prefabLookup[prefab] = pool;

            dirty = true;
        }

        GameObject spawnObject(GameObject prefab, bool isManualActive = true)
        {
            return spawnObject(prefab, Vector3.zero, Quaternion.identity);
        }

        GameObject spawnObject(GameObject prefab, Vector3 position, Quaternion rotation, bool isManualActive = true)
        {
            if (!prefabLookup.ContainsKey(prefab))
            {
                WarmPool(prefab, 1);
            }

            var pool = prefabLookup[prefab];

            var clone = pool.GetItem();
            clone.transform.position = position;
            clone.transform.rotation = rotation;

            if (isManualActive == false)
                clone.SetActive(true);

            instanceLookup.Add(clone, pool);
            dirty = true;
            return clone;
        }

        void releaseObject(GameObject clone)
        {
            clone.SetActive(false);

            if (instanceLookup.ContainsKey(clone))
            {
                instanceLookup[clone].ReleaseItem(clone);
                instanceLookup.Remove(clone);
                dirty = true;
            }
            else
            {
                Debug.Log.Warning("No pool contains the object: " + clone.name);
            }
        }


        private GameObject InstantiatePrefab(GameObject prefab)
        {
            var go = GameObject.Instantiate(prefab) as GameObject;
            if (root != null) go.transform.parent = root;
            return go;
        }

        public void PrintStatus()
        {
            foreach (KeyValuePair<GameObject, ObjectPool<GameObject>> keyVal in prefabLookup)
            {
                Debug.Log.Message(string.Format("Object Pool for Prefab: {0} In Use: {1} Total {2}", keyVal.Key.name, keyVal.Value.CountUsedItems, keyVal.Value.Count));
            }
        }

        #region Static API

        public static void WarmPool(GameObject prefab, int size)
        {
            Instance.warmPool(prefab, size);
        }

        public static GameObject SpawnObject(GameObject prefab, bool isManualActive)
        {
            return Instance.spawnObject(prefab, isManualActive);
        }

        public static GameObject SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation, bool isManualActive)
        {
            return Instance.spawnObject(prefab, position, rotation, isManualActive);
        }

        public static void ReleaseObject(GameObject clone)
        {
            Instance.releaseObject(clone);
        }

        #endregion
    }

}