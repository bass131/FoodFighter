﻿using UnityEngine;

namespace Hyun.Extensions
{
    /// <summary>
    /// Extension methods for UnityEngine.Component.
    /// </summary>
    public static class ComponentExtensions
    {
        /// <summary>
        /// Attaches a component to the given component's game object.
        /// </summary>
        /// <param name="component">Component.</param>
        /// <returns>Newly attached component.</returns>
        public static T AddComponent<T>(this Component component) where T : Component
        {
            var newComponent = component.gameObject.AddComponent<T>();
            return newComponent;
        }
        public static Component AddComponent(this Component component, System.Type tar)
        {
            var newComponent = component.gameObject.AddComponent(tar);
            return newComponent;
        }

        /// <summary>
        /// Gets a component attached to the given component's game object.
        /// If one isn't found, a new one is attached and returned.
        /// </summary>
        /// <param name="component">Component.</param>
        /// <returns>Previously or newly attached component.</returns>
        public static T GetOrAddComponent<T>(this Component component) where T : Component
        {
            var newOrExistingComponent = component.GetComponent<T>() ?? component.AddComponent<T>();
            return newOrExistingComponent;
        }
        public static Component GetOrAddComponent(this Component component, System.Type TyComponent)
        {
            Component newOrExistingComponent = null;
            if (component.HasComponent(TyComponent))
            {
                newOrExistingComponent = component.GetComponent(TyComponent);
            }
            else
            {
                newOrExistingComponent = component.AddComponent(TyComponent);
            }

            return newOrExistingComponent;
        }

        /// <summary>
        /// Checks whether a component's game object has a component of type T attached.
        /// </summary>
        /// <param name="component">Component.</param>
        /// <returns>True when component is attached.</returns>
        public static bool HasComponent<T>(this Component component) where T : Component
        {
            var hasComponent = component.GetComponent<T>() != null;
            return hasComponent;
        }
        public static bool HasComponent(this Component component, System.Type tar)
        {
            var hasComponent = component.GetComponent(tar) != null;
            return hasComponent;

        }
    }
}