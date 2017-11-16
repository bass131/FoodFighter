using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Croquette.Scenes
{
    public class LogoScene : scScene
    {
        LogoPresenter presenter { get; set; }
        public override void OnAwake()
        {

        }
        public override void OnRequestDataScript()
        {

        }
        public override void OnStart()
        {
            presenter = RegPresenter<LogoPresenter>("logoPresenter");
            presenter.onLogoAnimComplete += OnLogoAnimComplete;
            ReplacePresenter<LogoPresenter>(true);

        }
        public void OnLogoAnimComplete()
        {
            scSceneManager.Instance.SwitchScene("Title");
        }
        public override void OnUICameraCreated(Camera uiCam)
        {

        }
        public override void OnPreloadUIResources()
        {

        }

        public override void OnUICreated(Transform uiRoot)
        {

        }

        public override void OnPreDisable()
        {

        }

        protected override void Setup()
        {

        }
    }
}