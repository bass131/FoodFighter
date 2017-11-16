using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hyun;
namespace Croquette
{
    [DisallowMultipleComponent]
    [ScriptOrder(ScriptOrders.monoManager)]
    public abstract class scMonoManager : MonoBehaviour
    {
        public abstract bool LoadData();
        public abstract bool Initialize();

        public abstract void OnAwake();
        public abstract void OnStart();

        public void Awake()
        {
            if (LoadData())
            {
                OnAwake();
                Hyun.Debug.Log.MessageFormat("MonoManager : {0}, Awake Complete", name);
            }
            else
            {
                Hyun.Debug.Log.MessageFormat("MonoManager : {0}, LoadData Failed", name);
            }
        }

        public void Start()
        {
            if (Initialize())
            {
                OnStart();
                Hyun.Debug.Log.MessageFormat("MonoManager : {0}, Start Complete", name);
            }
            else
            {
                Hyun.Debug.Log.ErrorFormat("MonoManager : {0}, Initialize Failed", name);
            }
        }


    }
}