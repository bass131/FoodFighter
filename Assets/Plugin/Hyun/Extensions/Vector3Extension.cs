using System;
using UnityEngine;

namespace Hyun.Extensions
{
    public static class Vector3Extension
    {
        //         public static Point ToPoint( this Vector3 v)
        //         {
        //             Point pt = new Point();
        //             pt.x = (int) v.x;
        //             pt.y = (int) v.y;
        //             return pt; 
        //         }
        public static Vector2[] toVector2(this Vector3[] v3)
        {
            return System.Array.ConvertAll<Vector3, Vector2>(v3, getV3fromV2);
        }

        public static Vector2 getV3fromV2(Vector3 v3)
        {
            return new Vector2(v3.x, v3.y);
        }
        public static Vector3[] toVector3(this Vector2[] v2)
        {
            return System.Array.ConvertAll<Vector2, Vector3>(v2, getV2fromV3);
        }
        public static Vector3 getV2fromV3(Vector2 v2)
        {
            return new Vector3(v2.x, v2.y);
        }
    }
}