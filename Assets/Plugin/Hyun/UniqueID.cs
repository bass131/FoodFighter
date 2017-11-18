using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hyun
{
    public static class UniqueID
    {
        public static long lastTick = 0;

        public static long GetNextId()
        {
            lock (idGenLock)
            {
                long tick = System.DateTime.UtcNow.Ticks;
                if (lastTick == tick)
                {
                    tick = lastTick + 1;
                }
                lastTick = tick;
                return tick;
            }
        }

        public static int GetNextId32()
        {
            return (int)((GetNextId() << 32) >> 32);
        }
    }
}