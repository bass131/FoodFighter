using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Croquette
{
    public static class IndexHelper
    {
        public static int GetMonsterXmlIndex(int monsterID)
        {
            return monsterID - 100; // monsterID = 100, 101, 102 ~~ 
        }

        public static int GetPlayerXmlIndex(int playerID)
        {
            return playerID;
        }

        public static int GetObstacleXmlIndex(int obstacleID)
        {
            return obstacleID - 1000; // obstacleID = 1000, 1001, 1002 ~~ 
        }

    }
}