using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Hyun;

namespace Croquette
{
    using Scenes;

    [ScriptOrder(ScriptOrders.SceneMgr)]
    public class scSceneManager : SingletonMonoBehaviour<scSceneManager>
    {
        public scSceneManager() { }

        public AsyncOperation m_aOperation;
        public delegate void OnLoadingProgress(float fProgress);
        public OnLoadingProgress onLoadingProgress;

        public static bool SwitchToFirst = false;

        public void Awake()
        {
            if (SwitchToFirst == false)
            {
                SwitchToFirst = true;
                if (SceneManager.GetActiveScene().buildIndex > 1)
                {
                    SceneManager.LoadScene(1);
                }
            }
        }

        public void SwitchScene(string targetSceneName, bool bNowLoading = false)
        {
            if (bNowLoading)
                StartTransition<NowLoading>(targetSceneName);
            else
                StartTransition<NoTransition>(targetSceneName);
        }

        public void StartTransition<TTransition>(params object[] args)
            where TTransition : ISceneTransition, new()
        {
            GameObject prefab = Resources.Load("Loading/SceneTransitionComposer") as GameObject;

            GameObject prefab_instance = MonoBehaviour.Instantiate(prefab) as GameObject;

            var composer = prefab_instance.GetComponent<SceneTransitionComposer>();

            composer.m_transition = new TTransition();

            composer.m_transition.SetParams(args);


            prefab_instance.transform.SetParent(transform);

        }

        public bool isFall
        {
            get
            {
                return m_aOperation == null;
            }
        }
        public bool isComplete
        {
            get
            {
                return m_aOperation != null && m_aOperation.isDone;
            }
        }

        public bool BeginLoadScene(string targetSceneName, OnLoadingProgress progressCallback)
        {
            onLoadingProgress += progressCallback;

            StartCoroutine(CO_LoadSceneByName(targetSceneName));
            return true;
        }

        public void EndLoadScene(OnLoadingProgress progressCallback)
        {
            onLoadingProgress -= progressCallback;
        }

        public IEnumerator CO_LoadSceneByName(string targetSceneName)
        {
            m_aOperation = SceneManager.LoadSceneAsync(targetSceneName);

            while (m_aOperation != null && !m_aOperation.isDone)
            {
                onLoadingProgress(m_aOperation.progress);
                m_aOperation.allowSceneActivation = m_aOperation.progress > 0.8f;
                yield return null;
            }
            yield return null;
        }
    }
}