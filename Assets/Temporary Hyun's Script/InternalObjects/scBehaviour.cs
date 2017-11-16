using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Hyun;
using Hyun.Extensions;
using Hyun.Collections;

using UniRx;
namespace Croquette
{
    [ExecuteInEditMode]
    public abstract class scBehaviour : MonoBehaviour
    {

        public bool? is_Dirty
        {
            get; private set;
        }
        public void SetDirty(bool? b)
        {
            this.is_Dirty = b;

            if (this.is_Dirty.HasValue)
            {
                if (Application.platform == RuntimePlatform.Android)
                {
                    UniRx.MainThreadDispatcher.StartUpdateMicroCoroutine(CO_RedrawUpdate());
                }
            }
        }

        public abstract void OnAwake();
        public abstract void OnStart();

        public virtual void OnReset()
        {

        }

        public abstract void OnPostEnable();
        public abstract void OnPreDisable();

        //강제 재설정 딜레이 없이 바로 업데이트 하도록 변경
        public virtual void Invalidate(bool? bDirty)
        {
            this.is_Dirty = bDirty;

            if (this.is_Dirty.HasValue)
            {
                if (this.is_Dirty == true)
                    this.Setup();

                this.OnDirty();
            }
            this.is_Dirty = null;
        }


        public virtual void OnValidate()
        {

        }

        /// <summary>
        /// 쎗업은 OnVliadate 에서도 호출된다. 
        /// </summary>
        protected abstract void Setup();

        public virtual void OnDirty()
        {

        }

        public virtual void Awake()
        {
            OnAwake();
        }
        public virtual void Start()
        {
            OnStart();
        }

        public virtual void Reset()
        {
            OnReset();
        }


        private IEnumerator CO_RedrawUpdate()
        {

            if (this.is_Dirty.HasValue)
            {
                if (this.is_Dirty == true)
                    this.Setup();

                this.OnDirty();
            }
            this.is_Dirty = null;

            yield return null;


        }

        public void OnEnable()
        {
            OnPostEnable();
        }
        public void OnDisable()
        {
            OnPreDisable();
        }
    }
}