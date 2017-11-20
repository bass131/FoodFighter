//using System;
//using System.IO;
//using System.Collections.Generic;
//using UnityEngine;

//using Hyun;

//using UniRx;

//namespace Croquette
//{
//    using Data;
//    public class DataScriptHolder : Singleton<DataScriptHolder>
//    {
//        public DT_PlayerCollection dtPlayer
//        {
//            get { return this.m_pPlayerCollectionValue; }
//        }
//        public DT_MonsterCollection dtMonster
//        {
//            get { return this.m_pMonsterCollectionValue; }
//        }
//        public DT_PlacementCollection dtPlacement
//        {
//            get { return this.m_pPlacementCollectionValue; }
//        }

//        private DT_PlayerCollection m_pPlayerCollectionValue;
//        private DT_MonsterCollection m_pMonsterCollectionValue;
//        private DT_PlacementCollection m_pPlacementCollectionValue;

//        public DT_PlayerCollection LoadPlayerScript(bool force_reload)
//        {
//            if (force_reload == false && m_pPlayerCollectionValue != null)
//                return m_pPlayerCollectionValue;

//            string path = PathHelper.mkpath_player_dt();
//            m_pPlayerCollectionValue = LoadScript<DT_PlayerCollection>(path);
//            return m_pPlayerCollectionValue;
//        }

//        public DT_MonsterCollection LoadMonsterScript(bool force_reload)
//        {
//            if (force_reload == false && m_pMonsterCollectionValue != null)
//                return m_pMonsterCollectionValue;

//            string path = PathHelper.mkpath_monster_dt();
//            m_pMonsterCollectionValue = LoadScript<DT_MonsterCollection>(path);
//            return m_pMonsterCollectionValue;
//        }

//        public DT_PlacementCollection LoadPlacementScript(bool force_reload)
//        {
//            if (force_reload == false && m_pPlacementCollectionValue != null)
//                return m_pPlacementCollectionValue;

//            string path = PathHelper.mkpath_placement_dt();
//            m_pPlacementCollectionValue = LoadScript<DT_PlacementCollection>(path);
//            return m_pPlacementCollectionValue;
//        }

//        public T LoadScript<T>(string path)
//            where T : class
//        {
//            T result = null;
//            try
//            {
//                result = Serialization.DecompileFromPath<T>(path);
//            }
//            catch (Exception ex)
//            {
//                Hyun.Debug.Log.Exception(ex);
//            }
//            finally
//            {

//            }
//            return result;
//        }


//        public DT_Player GetPlayerData(int ScIndex)
//        {
//            Hyun.Debug.Log.AssertFormat(dtPlayer.HasPlayers(), "playerCollection is Empty");

//            for (int i = 0; i < dtPlayer.GetPlayersCount(); ++i)
//            {
//                DT_Player player = dtPlayer.players[i];
//                if (ScIndex == player.id)
//                    return player;
//            }
//            return null;
//        }
//        public DT_Monster GetMonsterData(int ScIndex)
//        {
//            Hyun.Debug.Log.AssertFormat(dtMonster.HasMonsters(), "monsterCollection is Empty");

//            for (int i = 0; i < dtMonster.GetMonstersCount(); ++i)
//            {
//                DT_Monster monster = dtMonster.monsters[i];
//                if (ScIndex == monster.id)
//                    return monster;
//            }
//            return null;
//        }
//        public DT_Placement GetPlacementData(int ScIndex)
//        {
//            Hyun.Debug.Log.AssertFormat(dtPlacement.HasPlacement(), "placementCOllection is Empty");

//            for (int i = 0; i < dtPlacement.GetPlacementCount(); ++i)
//            {
//                DT_Placement placement = dtPlacement.placement[i];
//                if (ScIndex == placement.index)
//                    return placement;
//            }
//            return null;
//        }

//        public void CallDataHandler(DT_TYPE dt, bool forceReload)
//        {
//            switch (dt)
//            {
//                case DT_TYPE.DT_PLAYER:
//                    LoadPlayerScript(forceReload);
//                    break;
//                case DT_TYPE.DT_MONSTER:
//                    LoadMonsterScript(forceReload);
//                    break;
//                case DT_TYPE.DT_PLACEMENT:
//                    LoadPlacementScript(forceReload);
//                    break;
//                default:
//                    Hyun.Debug.Log.ErrorFormat("'{0}' Cannot Distrib", dt.ToString());
//                    break;
//            }
//        }
//    }
//}