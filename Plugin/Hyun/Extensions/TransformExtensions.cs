using UnityEngine;

namespace Hyun.Extensions
{
    /// <summary>
    /// 원인을 알수없지만 new Vector3(인자) 가 안된다.
    /// </summary>
    public static class TransformExtensions
    {
        public static void SetLocalScale(this Transform self, float x, float y, float z)
        {
            Vector3 vec = default(Vector3);
            vec.x = x;
            vec.y = y;
            vec.z = z;
            self.localScale = vec;
        }
        public static void SetLocalPosition(this Transform self, float x, float y, float z)
        {
            Vector3 vec = default(Vector3);
            vec.x = x;
            vec.y = y;
            vec.z = z;
            self.localPosition = vec;
        }
        public static void SetPosition(this Transform self, float x, float y, float z)
        {
            Vector3 vec = default(Vector3);
            vec.x = x;
            vec.y = y;
            vec.z = z;

            self.position = vec;
        }
        public static void SetLocalPositionZ(this Transform self, float z)
        {
            Vector3 vec = default(Vector3);
            vec.x = 0;
            vec.y = 0;
            vec.z = z;
            self.localPosition = vec;
        }
    }
}