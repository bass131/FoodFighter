using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hyun;

namespace Croquette
{
    using Data;
    using Logic;

    public class Placement
    {
        public int index;
        public int stageNumber;
        public int objectType;
        public int objectID;
        public Vector2 startPosition;
    }

    public class scMonoPlacementManager : scMonoManager
    {
        // 배치하려는 씬
        public enum PlacementScene { Stage1 = 0, Stage2, Stage3 };
        public PlacementScene placementScene;

        readonly List<Placement> placements = new List<Placement>();

        public override bool LoadData()
        {
            if (DataScriptHolder.Instance.dtPlacement == null)
            {
                Hyun.Debug.Log.Error("Data : dtPlacement가 등록되지않았습니다. Scene에 등록이 되어있는지 확인해주세요.");
                return false;
            }
            int placementCnt = DataScriptHolder.Instance.dtPlacement.GetPlacementCount();

            for (int i = 0; i < placementCnt; ++i)
            {
                DT_Placement dtPlacement = DataScriptHolder.Instance.GetPlacementData(i);
                if (dtPlacement == null)
                {
                    Hyun.Debug.Log.ErrorFormat("DT+Placement : {0}, null이 들어가있습니다 Xml index값이 0 ~ max인지 확인해주세요", dtPlacement);
                    return false;
                }
                if ((int)placementScene == dtPlacement.stageNumber)
                {
                    Placement placement = new Placement();
                    placement.index = dtPlacement.index;
                    placement.objectID = dtPlacement.objectId;
                    placement.objectType = dtPlacement.objectType;
                    placement.stageNumber = dtPlacement.stageNumber;
                    placement.startPosition = new Vector2(dtPlacement.startPositionX, dtPlacement.startPositionY);

                    placements.Add(placement);
                }
            }
            return true;
        }

        public override bool Initialize()
        {
            CreateObjects();
            return true;
        }

        public bool CreateObjects()
        {
            for (int i = 0; i < placements.Count; ++i)
            {
                switch (placements[i].objectType)
                {
                    case (int)ObjectType.PLAYER:
                        CreatePlayer(placements[i]);
                        break;
                    case (int)ObjectType.MONSTER:
                        CreateMonster(placements[i]);
                        break;
                    case (int)ObjectType.OBSTACLE:
                        break;
                }
            }
            return true;
        }
        public override void OnAwake()
        {

        }

        public override void OnStart()
        {

        }

        #region CreateObject
        /*
         *----------------------설명!---------------------------------- 
         * 1. Info 설정 DeclrHelper.GetPlayerDeclr()이렇게 설정해준다
         * 2. 엔티티 생성 EntityManager.Instance.CreateEntity를 통해 엔티티를 생성해준다.
         * + CreateEntity내부에서 InitWithDeclr, CreateModel을 호출해줌
         * 3. Position을 placement position으로 세팅해준다
         * 4. createmodel을 통해 생성된 엔티티속 오브젝트를 불러와 
         *    BehaviourObj스크립트를 불러온다 ex) scPlayer
         *    불러온 후에 BehaviourScript 속 theEntity에 생성된 엔티티를 세팅해준다
         * 5. 엔티티 매니저에서 플레이어에게 바로 접근할 수 있게 
         *    SetPlayer를 설정해준다. -> Player만 해당
         *    
         * 이렇게 필요한 정보값을 갖고있는 하나의 오브젝트를 
         * 원하는 위치에 생성하는데 성공!
         */
        public void CreatePlayer(Placement placement)
        {
            EntityDeclr playerInfo = DeclrHelper.GetPlayerDeclr();

            EntityPlayer entityPlayer = EntityManager.Instance.CreateEntity<EntityPlayer>(playerInfo);
            if (entityPlayer != null)
            {
                entityPlayer.mTransform.position = placement.startPosition;
  

                EntityManager.Instance.SetPlayer(entityPlayer);
            }
        }

        public void CreateMonster(Placement placement)
        {
            EntityDeclr declr = DeclrHelper.GetMonsterDeclr(placement.objectID);
            EntityMonster entityMonster = EntityManager.Instance.CreateEntity<EntityMonster>(declr);
            if (entityMonster != null)
            {
                entityMonster.SetPosition(placement.startPosition.x, placement.startPosition.y, 0);
                
            }
        }
        #endregion

    }
}