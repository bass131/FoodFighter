using System;
using System.Collections.Generic;
using UnityEngine;

namespace Croquette.Scenes
{
    using Data;
    //using Logic;

    public abstract partial class scScene
    {
        public readonly List<DT_TYPE> RequiredDataScripts = new List<DT_TYPE>();

        public abstract void OnRequestDataScript();

        public void ReqDataScript(DT_TYPE dt)
        {
            if (!RequiredDataScripts.Contains(dt))
                RequiredDataScripts.Add(dt);
        }

        private void AcquireRequiredDataScripts()
        {
            for (int i = 0; i < RequiredDataScripts.Count; ++i)
            {
                DataScriptHolder.Instance.CallDataHandler(RequiredDataScripts[i], true);
                Hyun.Debug.Log.MessageFormat("AcquireRequiredDataScripts --> {0}", RequiredDataScripts[i].ToString());
            }
        }
    }
}