using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hyun;
namespace Croquette
{
    using Data;
    using Logic;
    public class EntityManager : Singleton<EntityManager>
    {
        public readonly Dictionary<int, EntityParent> theEntities = new Dictionary<int, EntityParent>();

        public EntityPlayer m_entityPlayer
        {
            get; private set;
        }

        public void SetPlayer(EntityPlayer entityPlayer)
        {
            m_entityPlayer = entityPlayer;
        }
        public EntityManager()
        {

        }

        public void Init()
        { }

        public TEntity CreateEntity<TEntity>(EntityDeclr declr)
            where TEntity : EntityParent
        {
            EntityParent entity = null;
            switch (declr.type)
            {
                case kEntityType.MONSTER:
                    entity = new EntityMonster();
                    break;
                case kEntityType.PLAYER:
                    entity = new EntityPlayer();
                    break;
            }

            if (entity != null && entity.InitWithDeclr(declr))
            {
                entity.CreateModel();
                AddEntity(entity.mGameObject.GetInstanceID(), entity);
            }

            return entity as TEntity;
        }

        public bool DestroyEntity(GameObject go)
        {
            int nID = go.GetInstanceID();
            bool bRet = RemoveEntity(nID);
            NodeTools.SafeDestroy(go);
            return bRet;
        }
        public bool AddEntity(int nID, EntityParent pEntity)
        {
            if (!theEntities.ContainsKey(nID))
            {
                theEntities.Add(nID, pEntity);
                return true;
            }
            return false;
        }

        public bool RemoveEntity(int nID)
        {
            return theEntities.Remove(nID);
        }

        public EntityParent GetEntity(int nID)
        {
            EntityParent pEntity = null;
            theEntities.TryGetValue(nID, out pEntity);
            return pEntity;
        }

        public int EnumEntities<TEntity>(ref List<TEntity> results)
            where TEntity : EntityParent
        {
            results.Clear();
            foreach (EntityParent it in theEntities.Values)
            {
                if (NodeTools.IsKindOf<TEntity>(it))
                {
                    results.Add(it as TEntity);
                }
            }
            return results.Count;
        }

        public int GetEntityCount<TEntity>()
            where TEntity : EntityParent
        {
            List<TEntity> entities = new List<TEntity>();
            return EnumEntities<TEntity>(ref entities);
        }
    }
}