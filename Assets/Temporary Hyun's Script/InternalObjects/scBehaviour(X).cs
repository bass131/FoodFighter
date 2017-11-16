using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Hyun;
using Hyun.Extensions;
using Hyun.Collections;
namespace Croquette
{
//    [ExecuteInEditMode]
//    public abstract class scBehaviour : MonoBehaviour
//    {

//        private float m_LastEditorUpdateTime;

//        public bool? is_Dirty
//        {
//            get; private set;
//        }

//        public void SetDirty(bool? b)
//        {
//            this.is_Dirty = b;
//            if (this.is_Dirty.HasValue)
//            {
//                if (Application.platform == RuntimePlatform.Android)
//                {
//                    UniRx.MainThreadDispatcher.StartUpdateMicroCoroutine(CO_RedrawUpdate());
//                }
//                else
//                {
//#if UNITY_EDITOR

//#endif
//                }
//            }
//        }

//        public scBehaviour()
//        {
//            this.is_Dirty = null;
//        }

//        public abstract void OnAwake();
//        public abstract void OnStart();

//        public virtual void OnReset()
//        {

//        }

//        public abstract void OnPostEnable();
//        public abstract void OnPreDisable();

//        public virtual void Invalidate(bool? bDirty)
//        {
//            this.is_Dirty = bDirty;
//            if (this.is_Dirty.HasValue)
//            {
//                if (this.is_Dirty == true)
//                    this.Setup();
//                this.OnDirty();
//            }
//            this.is_Dirty = null;
//        }
//        public virtual void OnValidate()
//        {

//        }

//        protected abstract void Setup();

//        public virtual void OnDirty()
//        {

//        }

//        public virtual void Awake()
//        {
//            this.is_Dirty = null;

//            OnAwake();
//        }

//        public virtual void Start()
//        {
//            OnStart();
//        }

//        public virtual void Reset()
//        {
//            OnReset();
//        }

//#if UNITY_EDITOR
//        public virtual void UpdateInEditor()
//        {
//            try
//            {
//                if (!bIsShouldBeDestroy)
//                {
//                    RedrawUpdateInEditor();
//                }
//            }
//            catch (MissingComponentException ex)
//            {
//                Hyun.Debug.Log.Exception(ex);
//            }
//        }
//#endif
//        private IEnumerator CO_RedrawUpdate()
//        {
//            if (this.is_Dirty.HasValue)
//            {
//                if (this.is_Dirty == true)
//                    this.Setup();

//                this.OnDirty();
//            }
//            this.is_Dirty = null;

//            yield return null;
//        }
//#if UNITY_EDITOR
//        private void RedrawUpdateInEditor()
//        {
//            if (this.is_Dirty.HasValue)
//            {
//                if (this.is_Dirty == true)
//                    this.Setup();

//                this.OnDirty();
//            }
//            this.is_Dirty = null;
//        }
//#endif

//        public void BindUpdateInEditor()
//        {
//#if UNITY_EDITOR
//            UnityEditor.EditorApplication.update += UpdateInEditor;
//#endif
//        }

//        public void UnbindUpdateInEditor()
//        {
//#if UNITY_EDITOR
//            UnityEditor.EditorApplication.update -= UpdateInEditor;
//#endif
//        }


//        public void OnEnable()
//        {
//            m_LastEditorUpdateTime = Time.realtimeSinceStartup;

//            BindUpdateInEditor();

//            OnPostEnable();
//        }

//        public void OnDisable()
//        {
//            UnbindUpdateInEditor();

//            this.is_Dirty = null;

//            OnPreDisable();
//        }

//        public virtual void OnDestroy()
//        {
//            UnbindUpdateInEditor();
//        }
//        public bool bIsShouldBeDestroy = false;

//        public void SetShouldBeDestroy(bool b)
//        {
//            UnbindUpdateInEditor();
//            this.bIsShouldBeDestroy = b;
//        }
//    }

   
}