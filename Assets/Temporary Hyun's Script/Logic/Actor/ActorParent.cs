using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Croquette
{
    using Logic;
    public class ActorParent<T> : ActorParent where T : EntityParent
    {
        private T m_theEntity;
        public T theEntity
        {
            get { return m_theEntity; }
            set { m_theEntity = value; }
        }
        public override EntityParent GetEntity()
        {
            return m_theEntity;
        }
    }
    public abstract class ActorParent : MonoBehaviour
    {
        public abstract EntityParent GetEntity();

        protected Animator m_animator;
        private Action<string, bool> m_animatorStateChanged;
        public Action<string, bool> AnimatorStateChanged
        {
            get
            {
                return m_animatorStateChanged;
            }
            set
            {
                m_animatorStateChanged = value;
            }
        }

        public SpriteRenderer[] sprRenderers;

        protected virtual void Awake()
        {
            sprRenderers = GetComponentsInChildren<SpriteRenderer>();

            m_animator = GetComponentInChildren<Animator>();
        }

        protected virtual void Update()
        {
            var entity = GetEntity();
            if (entity != null)
            {

            }
        }
    }
}