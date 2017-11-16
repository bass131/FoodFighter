using UnityEngine;
using Hyun.Extensions;
using DG.Tweening;

namespace Croquette
{
    public class LogoPresenter : Presenter
    {
        public GameObject blackOutPanel;

        public Sequence seq;
        public delegate void OnLogoAnimationComplete();
        public OnLogoAnimationComplete onLogoAnimComplete;
        public override bool Init()
        {
            return true;
        }

        public override void OnAwake()
        {
            blackOutPanel = GameObject.Find("BlackOutPanel");

        }
        public override void OnDirty()
        {
            if (blackOutPanel != null)
            {

                Hyun.Debug.Log.Message("OnDirty 들어옴");
                ColorController2D colorController2D = new ColorController2D(blackOutPanel.GetComponent<SpriteRenderer>());
                colorController2D.AlphaControl(0, 255, 2.5f, ()=>
                {
                    onLogoAnimComplete();
                });
            }
        }
        public override void OnStart()
        {

        }

        protected override void Setup()
        {
            return;
        }
    }
}