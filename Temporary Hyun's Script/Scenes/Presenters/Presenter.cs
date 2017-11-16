using System;
using UnityEngine;

namespace Croquette
{
    [ExecuteInEditMode]
    public abstract class Presenter : scBehaviour
    {
        public Presenter() { }
        public abstract bool Init();

        public override void OnPostEnable()
        {

        }

        public override void OnPreDisable()
        {

        }
    }
}