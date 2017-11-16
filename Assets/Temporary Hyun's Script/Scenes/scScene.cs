using System;
using System.Collections.Generic;
using UnityEngine;
using Hyun;
using Hyun.Extensions;
using UnityEngine.UI;

namespace Croquette.Scenes
{
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [ScriptOrder(ScriptOrders.SceneMain)]
    public abstract partial class scScene : scBehaviour,  IScene
        ,IResourceEventListener
    {
        public scScene() { }

        public override void Awake()
        {

            RESMGR.AddListener(name, this);

            OnInitOnAwake();

            // 요기서 스크립트 요청을 수집하고 
            OnRequestDataScript();

            // 여기서 로드를 검증 
            AcquireRequiredDataScripts();

            OnPreloadUIResources();
            //OnRequestDataScript();


            base.Awake();
        }

        public virtual void OnInitOnAwake()
        {
            // Manager Initialize

        }

        public override void OnPostEnable()
        {
            PrepareUIEnvironments();
        }
        public override void OnValidate()
        {
            OnRequestDataScript();
            AcquireRequiredDataScripts();

            base.OnValidate();


        }



        #region Scene 전용 디버그 출력 함수 
        public void logError(string template, params object[] args)
        {
            Hyun.Debug.Log.ErrorFormat(string.Format("{0} >> {1}", name, template), args);
        }
        public void logWarning(string template, params object[] args)
        {
            Hyun.Debug.Log.WarningFormat(string.Format("{0} >> {1}", name, template), args);
        }
        public void logException(Exception ex)
        {
            Hyun.Debug.Log.Exception(ex);
        }
        public void logMessage(string template, params object[] args)
        {
            Hyun.Debug.Log.MessageFormat(string.Format("{0} >> {1}", name, template), args, args);
        }
        #endregion
    }
}