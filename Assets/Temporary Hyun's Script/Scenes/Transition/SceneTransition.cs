using System;
using UnityEngine;

namespace Croquette.Scenes
{

    public enum TransitionState
    {
        TS_INIT,
        TS_ENTER,
        TS_UPDATE,
        TS_LEAVE,
        TS_DESTROY,
    }


    public interface ISceneTransition
    {
        void SetParams(params object[] args);
        TransitionState GetState();
        void SetState(TransitionState state);
        bool OnTransitionInit(GameObject owner);
        bool OnTransitionEnter();
        bool OnTransitionUpdate();
        void OnTransitionLeave();
        void OnTransitionDestroy();

    }

    public abstract class SceneTransition : ISceneTransition
    {
        public TransitionState mState = TransitionState.TS_INIT;
        public object[] mParams;
        public GameObject mpOwner;

        public SceneTransition() { }

        public TransitionState GetState()
        {
            return mState;
        }
        public void SetState(TransitionState state)
        {
            mState = state;
        }
        public virtual bool OnTransitionInit(GameObject owner)
        {
            mpOwner = owner;
            return true;
        }
        public abstract bool OnTransitionEnter();
        public abstract bool OnTransitionUpdate();
        public abstract void OnTransitionLeave();
        public abstract void OnTransitionDestroy();

        public abstract void SetParams(params object[] args);
    }
}