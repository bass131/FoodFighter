using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Croquette
{
    using Logic;

    public class ActorMonster : ActorParent<EntityMonster>
    {
        protected override void Awake()
        {
            base.Awake();
        }
        protected override void Update()
        {
            base.Update();
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            
        }
    }
}