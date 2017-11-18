using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Croquette.Logic
{
    public abstract partial class EntityParent 
    {

        /// <summary>
        /// 이 엔티티에 대한 프리팹 모델이 현재 생성된 상태인가 
        /// </summary>
        public bool isCreatingModel = false;

        public abstract bool InitWithDeclr(EntityDeclr pDeclr);

        protected abstract bool Init();
        
        protected abstract void SetInfo(EntityDeclr pDeclr);

        /// <summary>
        /// 인식 거리
        /// </summary>
        /// <returns></returns>
        public float GetDetectDistance()
        {
            
            return 10.0f;
        }

        public abstract float GetAttackRange();

        public abstract float GetAttackSpeed();

        public abstract void OnDeath();
        public abstract void OnRevive();
        
        /// <summary>
        /// 공통적으로 CreateModel이 불린다
        /// </summary>
        public virtual void CreateModel()
        {

        }
        
    }
}