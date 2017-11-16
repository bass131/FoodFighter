using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hyun;
namespace Croquette.Logic
{
    public class EntityMonster : EntityParent
    {
        MonsterInfo monsterinfo;
        public override float GetAttackRange()
        {
            return 1.0f;
        }

        public override float GetAttackSpeed()
        {
            return 1.0f;
        }

        public override void OnDeath()
        {

        }

        public override void OnRevive()
        {

        }

        protected override void SetInfo(EntityDeclr pDeclr)
        {
            monsterinfo = pDeclr as MonsterInfo;
        }
        public override bool InitWithDeclr(EntityDeclr pDeclr)
        {
            SetInfo(pDeclr);
            return Init();
        }
        protected override bool Init()
        {
            return true;
        }


        public override void CreateModel()
        {
            this.isCreatingModel = true;

            GameObject parent = GameObject.Find("Actors");
            if (parent == null)
            {
                Hyun.Debug.Log.Message("Hierarchy에 Actors가 없어서 새로 생성합니다.");
                parent = new GameObject("Actors");
            }
            GameObject goMonsterGroup = NodeTools.StableGameObject(parent.transform, "Monsters");

            if(monsterinfo == null)
                Hyun.Debug.Log.ErrorFormat("monsterinfo : {0}, 값이 null입니다 InitWithDeclr 호출 유무를 확인해주세요.");


            string prefabPath = monsterinfo.prefabPath;
            Debug.Log(monsterinfo.objectID);
            Debug.Log(prefabPath);
            mGameObject = RESMGR.InstantiateFromPrefab(goMonsterGroup.transform, prefabPath);

            mTransform = mGameObject.transform;

            mActor = mGameObject.GetComponent<ActorMonster>();
            //mActor가 ActorParent이기때문에 형 변환 시킨다
            (mActor as ActorMonster).theEntity = this;

            mTransform.name = monsterinfo.name;
            mTransform.tag = monsterinfo.tag;

            base.CreateModel();
        }


    }
}