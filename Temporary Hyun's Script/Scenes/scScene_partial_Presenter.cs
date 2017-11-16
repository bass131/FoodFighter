using System;

using System.Collections.Generic;
using Hyun;
using Hyun.Extensions;

namespace Croquette.Scenes
{
    public abstract partial class scScene
    {
        #region Presenter 설치
        /// <summary>
        /// AddPresenter 한것들 여기로
        /// </summary>
        readonly Dictionary<int, Presenter> allPresenters = new Dictionary<int, Presenter>();

        /// <summary>
        /// PushPresenter 하면 여기
        /// </summary>
        readonly Stack<Presenter> activePresenters = new Stack<Presenter>();
        

        public Presenter currentPresenter
        {
            get
            {
                return activePresenters.Count > 0 ? activePresenters.Peek() : null;
            }
        }

        protected TPresenter GetPresenter<TPresenter>()
            where TPresenter : Presenter
        {
            foreach (var presenter in allPresenters.Values)
            {
                if (NodeTools.IsKindOf<TPresenter>(presenter))
                {
                    return presenter as TPresenter;
                }
            }
            return null;
        }

        protected Presenter GetPresenter(string name)
        {
            int key = name.ToLower().GetHashCode();
            Presenter presenter = null;
            allPresenters.TryGetValue(key, out presenter);
            return presenter;
        }

        protected TPresenter RegPresenter<TPresenter>(string name)
            where TPresenter : Presenter
        {
            int key = name.ToLower().GetHashCode();

            Presenter ret = GetPresenter(name);

            if (!ret)
            {
                ret = gameObject.GetOrAddComponent<TPresenter>();

                if (ret != null && ret.Init())
                {
                    ret.enabled = false;

                    allPresenters.Add(key, ret);
                }
                else
                {
                    logError("Cannot Add {0} ", typeof(TPresenter).Name);
                }
            }
            return ret != null ? ret as TPresenter : null;
               
        }
        // 현재 프레즌터는 팝되고 현재 프레즌터의 지점에 푸시된다.
        // 현재 프레즌터가 교체되는 효과 를 낳는다.
        public void ReplacePresenter<TPresenter>(bool bEnableOnReplace, Action<TPresenter> injection = null)
            where TPresenter : Presenter
        {
            PopPresenter(false);
            PushPresenter<TPresenter>(bEnableOnReplace, injection);
        }

        public bool PushPresenter<TPresenter>(bool bEnableOnPush, Action<TPresenter> injection = null)
            where TPresenter : Presenter
        {

            TPresenter presenter = GetPresenter<TPresenter>();
            if (presenter != null)
            {
                // 기존 프레즌터를 disable 
                if (currentPresenter != null)
                    currentPresenter.enabled = false;

                activePresenters.Push(presenter);
                NodeTools.TryAction<TPresenter>(injection, presenter);
                currentPresenter.enabled = bEnableOnPush;
                if (currentPresenter.enabled)
                    currentPresenter.Invalidate(true);
                return true;
            }

            return false;
        }
        public bool PushPresenter(string name, bool bEnableOnPush, Action<Presenter> injection = null)
        {
            int key = name.ToLower().GetHashCode();
            // presenter 추가
            Presenter presenter = GetPresenter(name);
            if (presenter != null)
            {

                // 기존 프레즌터를 disable 
                if (currentPresenter != null)
                    currentPresenter.enabled = false;

                activePresenters.Push(presenter);
                NodeTools.TryAction<Presenter>(injection, presenter);
                currentPresenter.enabled = bEnableOnPush;
                if (currentPresenter.enabled)
                    currentPresenter.Invalidate(true);
                return true;
            }

            return false;
        }


        public void PopPresenter(bool bEnableOnPop)
        {
            if (currentPresenter != null)
            {
                // 현재 프레즌터를 disable 
                currentPresenter.enabled = false;
                // 팝 한다.
                activePresenters.Pop();

                // 팝하고 나서의 프레즌터가 있으면 보여준다.
                if (currentPresenter != null)
                {
                    currentPresenter.enabled = bEnableOnPop;
                    if (currentPresenter.enabled)
                        currentPresenter.Invalidate(true);
                }


            }
        }

        #endregion
    }
}