using System;
using UnityEngine;

namespace Croquette
{
    [Serializable]
    public class scConfigure
    {
        public const float DEFAULT_PIXEL_PER_UNIT = 100f;

        public const float SCREEN_RESOLUTION_WIDTH = 1280f;
        public const float SCREEN_RESOLUTION_HEIGHT = 720f;

        public readonly static Vector2 SCREEN_RESOLUTION = new Vector2(SCREEN_RESOLUTION_WIDTH, SCREEN_RESOLUTION_HEIGHT);


        /// <summary>
        /// 오소카메라 사이즈 
        /// </summary>
    //public const float OTHOGRAPHIC_SIZE = (SCREEN_RESOLUTION_HEIGHT / 2f) / DEFAULT_PIXEL_PER_UNIT;
        public const float OTHOGRAPHIC_SIZE = SCREEN_RESOLUTION_HEIGHT / 64f;///2; ///3.6f * 3f;//(SCREEN_RESOLUTION_HEIGHT / 2f) / DEFAULT_PIXEL_PER_UNIT;




    }
}