using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hyun;

namespace Croquette
{
    using Logic;
    using Data;
    public static class DeclrHelper {

        public static EntityDeclr GetPlayerDeclr()
        {

            PlayerInfo info = new PlayerInfo();
            info.objectID = DataScriptHolder.Instance.GetPlayerData(0).id;
            info.uid = Hyun.Id64.MakeLong(info.objectID, Hyun.UniqueID.GetNextId32());
            info.name = DataScriptHolder.Instance.GetPlayerData(0).name;
            info.tag = DataScriptHolder.Instance.GetPlayerData(0).tag;
            info.moveSpeed = DataScriptHolder.Instance.GetPlayerData(0).moveSpeed;
            info.jumpSpeed = DataScriptHolder.Instance.GetPlayerData(0).jumpSpeed;
            info.prefabPath = DataScriptHolder.Instance.GetPlayerData(0).prefabPath;
            //// 세팅

            return info;
        }

        //public static EntityDeclr GetObstacleDeclr()
        //{
        //    //Obstacle
        //}
        public static EntityDeclr GetMonsterDeclr(int index)
        {
            //Debug.Log(index);
            //MonsterInfo info = new MonsterInfo();
            MonsterInfo monsterInfo = new MonsterInfo();
            monsterInfo.name = DataScriptHolder.Instance.GetMonsterData(index).name;
            monsterInfo.tag = DataScriptHolder.Instance.GetMonsterData(index).tag;
            monsterInfo.objectID = DataScriptHolder.Instance.GetMonsterData(index).id;
            monsterInfo.type = kEntityType.MONSTER;
            monsterInfo.uid = Id64.MakeLong(monsterInfo.objectID, UniqueID.GetNextId32());
            monsterInfo.hp = DataScriptHolder.Instance.GetMonsterData(index).hp;
            monsterInfo.atk = DataScriptHolder.Instance.GetMonsterData(index).atk;
            monsterInfo.atkDelayForSec = DataScriptHolder.Instance.GetMonsterData(index).atkDelayForSec;
            monsterInfo.moveSpeed = DataScriptHolder.Instance.GetMonsterData(index).moveSpeed;
            monsterInfo.jumpSpeed = DataScriptHolder.Instance.GetMonsterData(index).jumpSpeed;
            monsterInfo.prefabPath = DataScriptHolder.Instance.GetMonsterData(index).prefabPath;

            return monsterInfo;
        }
    }
}