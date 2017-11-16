using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Croquette.Logic
{
    public class PlayerDeclr : EntityDeclr
    {
        public string prefabPath;
        public float hp;
        public float atk;
        public float atkDelayForSec;
        public float moveSpeed;
        public float jumpSpeed;
    }
}