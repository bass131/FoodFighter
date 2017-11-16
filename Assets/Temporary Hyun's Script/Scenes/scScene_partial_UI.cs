using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Hyun;
using Hyun.Extensions;
namespace Croquette.Scenes
{

    public abstract partial class scScene
    {
        public abstract void OnUICreated(Transform uiRoot);
        public abstract void OnUICameraCreated(Camera uiCal);
        public abstract void OnPreloadUIResources();

        public Transform ui_Root;

        public Camera ui_Camera;

        public Physics2DRaycaster phy2dRayCaster;

        public TText BindText<TText>(string hierachy) where TText : UnityEngine.UI.Text
        {
            Transform tr = ui_Root.Find(hierachy);
            if (tr != null)
            {
                TText ctrl = tr.GetComponent<TText>();
                return ctrl;
            }
            else
                Hyun.Debug.Log.ErrorFormat("ui_root/{0} not found", hierachy);

            return null;
        }

        public TControl BindUIControl<TControl>(string hierachy, Action clickHandler = null) where TControl : UnityEngine.UI.Button
        {
            Transform tr = ui_Root.Find(hierachy);

            if (tr != null)
            {
                TControl ctrl = tr.GetComponent<TControl>();

                ctrl.onClick.RemoveAllListeners();

                ctrl.onClick.AddListener(()=>
                {
                    OnUIClick(ctrl, hierachy.ToLower().GetHashCode());

                    if (clickHandler != null)
                        clickHandler();
                });
                return ctrl;
            }
            else
            {
                Hyun.Debug.Log.ErrorFormat("ui_root/{0} not found", hierachy);
            }
            return null;
        }

        public virtual void OnUIClick(UnityEngine.UI.Selectable sender, int hashCode)
        {
            Hyun.Debug.Log.MessageFormat("{0} Clicked", sender.name);
        }

        /// <summary>
        /// 이걸 통해서 똑같은 UI설정이 들어간다
        /// </summary>
        private void PrepareUIEnvironments()
        {
            try
            {
                int uiLayer = LayerMask.NameToLayer("UI");
                int uiLayerMask = 1 << uiLayer;

                GameObject go_uiCamera = GameObject.Find("UI Camera");

                if (go_uiCamera == null)
                    go_uiCamera = new GameObject("UI Camera");

                go_uiCamera.layer = uiLayer;

                var uiCam = go_uiCamera.GetOrAddComponent<Camera>();
                uiCam.clearFlags = CameraClearFlags.Depth;
                uiCam.orthographic = true;
                uiCam.orthographicSize = 100f;
                uiCam.cullingMask = uiLayerMask;
                uiCam.nearClipPlane = 1;
                uiCam.farClipPlane = 100;
                uiCam.depth = 70f;
                uiCam.allowHDR = false;
                uiCam.allowMSAA = false;

                var audioListener = go_uiCamera.GetOrAddComponent<AudioListener>();


                OnUICameraCreated(uiCam);

                ui_Camera = uiCam;

                phy2dRayCaster = go_uiCamera.GetOrAddComponent<Physics2DRaycaster>();
                phy2dRayCaster.eventMask = LayerMask.GetMask("UI");

                GameObject go_ui = GameObject.Find("UI");
                if (go_ui == null)
                    go_ui = new GameObject("UI", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));

                go_ui.layer = uiLayer;

                var canvas = go_ui.GetOrAddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
                canvas.pixelPerfect = false;
                canvas.worldCamera = uiCam;

                var scaler = go_ui.GetOrAddComponent<CanvasScaler>();
                scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

                // 1280 x 720
                scaler.referenceResolution = scConfigure.SCREEN_RESOLUTION;
                scaler.referencePixelsPerUnit = scConfigure.DEFAULT_PIXEL_PER_UNIT;

                var rayCaster = go_ui.GetOrAddComponent<GraphicRaycaster>();
                rayCaster.ignoreReversedGraphics = true;
                rayCaster.blockingObjects = GraphicRaycaster.BlockingObjects.TwoD;

                ui_Root = go_ui.transform;

                OnUICreated(ui_Root);
            }
            catch(Exception ex)
            {
                logException(ex);
            }
        }


    }
}