using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using Hyun;

using UnityEngine;

namespace Croquette
{
    //using Data;


    public class RESMGR
    {
        public RESMGR() {}

        public static readonly Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();

        // 텍스쳐 로딩 캐시
        public static readonly Dictionary<string, UnityEngine.Object> textures = new Dictionary<string, UnityEngine.Object>();

        public static readonly Dictionary<string, Material> materials = new Dictionary<string, Material>();

        public static List<IResourceEventListener> mListeners = new List<IResourceEventListener>();

        public static void AddListener(string name, IResourceEventListener listener)
        {
            if (!mListeners.Contains(listener))
            {
                mListeners.Add(listener);
                Hyun.Debug.Log.MessageFormat("{0} listener added.", name);
            }
        }
        public delegate void onResourceLoaded(int handle);

        public onResourceLoaded OnResourceLoadComplete;

        public static T GetMaterial<T>(string path) where T : UnityEngine.Material
        {
            UnityEngine.Material mat = null;

            materials.TryGetValue(path, out mat);

            if (mat == null)
            {
                mat = Resources.Load(path, typeof(T)) as T;
                if (mat != null)
                    materials.Add(path, mat);
                else
                    Hyun.Debug.Log.ErrorFormat("'{0}' Cannot found Material", path);
            }

            return mat as T;
        }

        public static T GetTexture<T>(string path) where T : UnityEngine.Object
        {
            UnityEngine.Object texr = null;

            textures.TryGetValue(path, out texr);

            if (texr == null)
            {
                texr = Resources.Load(path, typeof(T)) as T;
                if (texr != null)
                    textures.Add(path, texr);
                else
                    Hyun.Debug.Log.ErrorFormat("'{0}' Cannot Found", path);
            }
            return texr as T;
        }

        public static GameObject GetPrefab(string path)
        {
            GameObject goPrefab = null;

            prefabs.TryGetValue(path, out goPrefab);

            if (goPrefab == null)
            {
                goPrefab = Resources.Load(path) as GameObject;
                if (goPrefab != null)
                    prefabs.Add(path, goPrefab);
                else
                    Hyun.Debug.Log.ErrorFormat("'{0}' Cannot Found", path);
            }
            return goPrefab;
        }

        public static GameObject InstantiateFromPrefab(Transform parent, string path)
        {
            GameObject goPrefab = GetPrefab(path);
            return Instantiate(goPrefab, parent);
        }

        public static GameObject Instantiate(GameObject goPrefab, Transform parent)
        {
            GameObject instance = null;

            instance = UnityEngine.Object.Instantiate(goPrefab);

            instance.name = goPrefab.name;
            instance.layer = goPrefab.layer;

            instance.transform.SetParent(parent);
            instance.transform.localPosition = goPrefab.transform.localPosition;
            instance.transform.localScale = goPrefab.transform.localScale;
            instance.transform.localRotation = goPrefab.transform.localRotation;

            return instance;
        }

        public static GameObject InstantiateFromPrefab(string path)
        {
            return InstantiateFromPrefab(null, path);
        }

        public static T LoadPreset<T>(string relative_path) where T : ScriptableObject
        {
            T ret = Resources.Load(relative_path, typeof(T)) as T;
            return ret;
        }
    }
}