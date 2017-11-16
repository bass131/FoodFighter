using UnityEngine;

namespace Croquette.Scenes
{

    public class NowLoading : SceneTransition
    {
        public string targetSceneName;
        public float m_progress = 0.0f;
        SceneTransitionComposer m_composer;

        public override void SetParams(params object[] args)
        {
            this.targetSceneName = args[0] as string;
        }

        public void onProgress(float fRadio)
        {
            m_progress = fRadio;
            m_composer.SetProgress(m_progress);
        }

        public override bool OnTransitionInit(GameObject owner)
        {
            base.OnTransitionInit(owner);
            m_composer = owner.GetComponent<SceneTransitionComposer>();

            return true;
        }

        public override bool OnTransitionEnter()
        {
            bool ok = scSceneManager.Instance.BeginLoadScene(targetSceneName, onProgress);

            return ok;
        }

        public override bool OnTransitionUpdate()
        {
            if (scSceneManager.Instance.isFall)
                return false;
            if (scSceneManager.Instance.isComplete)
                return false;
            return true;
        }
        public override void OnTransitionLeave()
        {
            scSceneManager.Instance.EndLoadScene(onProgress);
        }
        public override void OnTransitionDestroy()
        {

        }

    }
}