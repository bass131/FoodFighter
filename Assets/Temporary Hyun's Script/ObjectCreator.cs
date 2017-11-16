using UnityEngine;
using Hyun;
using System;

namespace Croquette
{
    public class ObjectCreator : Singleton<ObjectCreator>
    {
        public GameObject Create<TComponent>(Transform root, string name, Action<UnityEngine.Object> addSetting = null) where TComponent : UnityEngine.Component
        {
            GameObject obj = new GameObject(name);
            if (root != null)
                obj.transform.SetParent(root);

            obj.AddComponent<TComponent>();
            if (addSetting != null)
                addSetting(obj);
            return obj;
        }
        public GameObject Create<TComponent>(Transform root, string name, string layerName, Action<UnityEngine.Object> addSetting = null) where TComponent : UnityEngine.Component
        {
            GameObject obj = new GameObject(name);
            if (root != null)
                obj.transform.SetParent(root);
            obj.layer = LayerMask.NameToLayer(layerName);
            obj.AddComponent<TComponent>();
            if (addSetting != null)
                addSetting(obj);

            return obj;
        }
    }
}