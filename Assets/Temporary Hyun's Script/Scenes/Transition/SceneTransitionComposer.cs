using UnityEngine;
using UnityEngine.UI;

namespace Croquette.Scenes
{
    public class SceneTransitionComposer : MonoBehaviour
    {
        public ISceneTransition m_transition;
        UIWidgets.Progressbar progressBar;
        Text tipText;

        public void SetProgress(float per)
        {
            if (progressBar)
                progressBar.Value = (int)(per * 100f);
        }

        public void SetTip(string text)
        {
            if (tipText != null)
            {
                tipText.text = text;
            }
        }

        public virtual void Awake()
        {
            progressBar = gameObject.transform.Find("Loading_Progressbar").GetComponent<UIWidgets.Progressbar>();
           // tipText = gameObject.transform.Find("Loading_Tip").GetComponent<Text>();
        }

        public void FixedUpdate()
        {
            switch (m_transition.GetState())
            {
                case TransitionState.TS_INIT:
                    if (m_transition.OnTransitionInit(gameObject))
                    {
                        m_transition.SetState(TransitionState.TS_ENTER);
                    }
                    break;
                case TransitionState.TS_ENTER:
                    if (m_transition.OnTransitionEnter())
                    {
                        m_transition.SetState(TransitionState.TS_UPDATE);
                    }
                    break;
                case TransitionState.TS_UPDATE:
                    if (!m_transition.OnTransitionUpdate())
                    {
                        m_transition.SetState(TransitionState.TS_LEAVE);
                    }
                    break;
                case TransitionState.TS_LEAVE:
                    m_transition.OnTransitionLeave();
                    {
                        m_transition.SetState(TransitionState.TS_DESTROY);
                    }
                    break;
                case TransitionState.TS_DESTROY:
                    m_transition.OnTransitionDestroy();
                    Hyun.NodeTools.SafeDestroy(gameObject);
                    break;
            }
        }

    }
}