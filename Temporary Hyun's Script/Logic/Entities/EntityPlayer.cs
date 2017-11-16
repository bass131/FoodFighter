using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hyun.Extensions;

namespace Croquette.Logic
{
    public class EntityPlayer : EntityParent
    {

        PlayerInfo playerInfo;
        public override float GetAttackRange()
        {
            return 1.0f;
        }

        public override float GetAttackSpeed()
        {
            return 1.0f;
        }

        public override bool InitWithDeclr(EntityDeclr pDeclr)
        {
            SetInfo(pDeclr);
            return Init();

        }
        protected override void SetInfo(EntityDeclr pDeclr)
        {
            playerInfo = pDeclr as PlayerInfo;
        }
        protected override bool Init()
        {

            return true;
        }
        public override void OnDeath()
        {

        }

        public override void OnRevive()
        {

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

            string prefabPath = playerInfo.prefabPath;
            mGameObject = RESMGR.InstantiateFromPrefab(parent.transform, prefabPath);
            
            mTransform = mGameObject.transform;

            mActor = mGameObject.GetComponent<ActorPlayer>();
            // mActor가 ActorParent이기 때문에 형변환시킴
            (mActor as ActorPlayer).theEntity = this;

            if (playerInfo != null)
            {
                mTransform.name = playerInfo.name;
                mTransform.tag = playerInfo.tag;
            }

            base.CreateModel();
        }

       
    }
}