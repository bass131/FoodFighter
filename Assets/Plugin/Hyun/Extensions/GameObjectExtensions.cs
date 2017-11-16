using System.Collections.Generic;
using UnityEngine;

namespace Hyun.Extensions
{
    /// <summary>
    /// Extension methods for UnityEngine.GameObject.
    /// </summary>
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Gets a component attached to the given game object.
        /// If one isn't found, a new one is attached and returned.
        /// </summary>
        /// <param name="gameObject">Game object.</param>
        /// <returns>Previously or newly attached component.</returns>
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            var newOrExistingComponent = gameObject.HasComponent<T>() ? gameObject.GetComponent<T>() : gameObject.AddComponent<T>();
            return newOrExistingComponent;
        }

        /// <summary>
        /// Checks whether a game object has a component of type T attached.
        /// </summary>
        /// <param name="gameObject">Game object.</param>
        /// <returns>True when component is attached.</returns>
        public static bool HasComponent<T>(this GameObject gameObject) where T : Component
        {
            var hasComponent = gameObject.GetComponent<T>() != null;
            return hasComponent;
        }

        public static string GetPath(this GameObject go)
        {
            List<string> path = new List<string>();

            Transform current = go.transform;
            path.Add(current.name);

            while (current.parent != null)
            {
                path.Insert(0, current.parent.name);
                current = current.parent;
            }

            return string.Join("/", path.ToArray());
        }
        /// <summary>
        /// Checks if a GameObject has been destroyed.
        /// </summary>
        /// <param name="gameObject">GameObject reference to check for destructedness</param>
        /// <returns>If the game object has been marked as destroyed by UnityEngine</returns>
        public static bool IsDestroyed(this GameObject gameObject)
        {
            // UnityEngine overloads the == opeator for the GameObject type
            // and returns null when the object has been destroyed, but 
            // actually the object is still there but has not been cleaned up yet
            // if we test both we can determine if the object has been destroyed.
            return gameObject == null && !ReferenceEquals(gameObject, null);
        }
    }
}