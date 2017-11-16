using UnityEngine;
using System;

namespace Croquette.Logic
{
    public partial class EntityParent
    {
        public long UID
        {
            get; private set;
        }

        public void SetID(long UID)
        {
            this.UID = UID;
        }

        public string Name
        {
            get; private set;
        }

        public void SetName(string Name)
        {
            this.Name = Name;
        }

        public float MaxHp
        {
            get; private set;
        }

        public void SetMaxHp(float maxHp)
        {
            this.MaxHp = maxHp;
        }

        public float Atk
        {
            get; private set;
        }
        public void SetAtk(float atk)
        {
            this.Atk = atk;
        }

        public float AtkDelayForSec
        {
            get; private set;
        }
        public void SetAtkDelayForSec(float atkDelay)
        {
            this.AtkDelayForSec = atkDelay;
        }

        public float MoveSpeed
        {
            get; private set;
        }
        public void SetMoveSpeed(float moveSpeed)
        {
            this.MoveSpeed = moveSpeed;
        }

        public float JumpSpeed
        {
            get; private set;
        }
        public void SetJumpSpeed(float jumpSpeed)
        {
            this.JumpSpeed = jumpSpeed;
        }
        public void SetPosition(float fX, float fY, float fZ)
        {
            if (mTransform != null)
            {
                mTransform.position = new Vector3(fX, fY, fZ);
            }
        }
        public Transform mTransform { get; set; }

        //public Vector3 mPosition { get; set; }

        //public Vector3 mRotation { get; set; }

        public Vector3 mScale = new Vector3(1, 1, 1);

        // Actor GameObject
        public GameObject mGameObject { get; set; }

        // Actor 자체 컴포넌트
        public ActorParent mActor { get; set; }

        // actor에 달린 animator
        public Animator mAnimator { get; set; }


        public int curHp;
        public float percentageHp;

        public float maxHp;
        public float atk;
        public float atkDelayForSec;
        public float moveSpeed;
        public float jumpSpeed;


        public bool canMove = true;

        public bool isHpZero
        {
            get
            {
                return curHp <= 0;
            }
        }

        public void DecreaseHp(int nPts)
        {
            curHp -= nPts;
            if (curHp <= 0)
            {

            }
        }
    }
}