using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donut : Monster {

    float Real_Time = 0; // 실제 시간.
    bool ATK_ANIM = false;

    // Update is called once per frame
    void Update () {
        base.Monster_Move();
        MonsterDie();
        Monster_ATK();
	}

    void Monster_ATK() // 몬스터 공격 함수.
    {
        Attack_Delay = 1f; // 공격 판정 선딜레이.

        if (isAttacking) // 공격 중인 상황일때
        {
            Debug.Log("Attack!");
            if (!ATK_ANIM) { ATK_ANIM = true; Anim.SetTrigger("ATK"); }
            Real_Time += Time.deltaTime;

            if (Real_Time >= Attack_Delay && !isDie) // 실제 시간이 공격 딜레이에 맞으면.
            {
                Debug.Log("Bang");
                Real_Time = 0;

                Collider2D[] cols;
                cols = Physics2D.OverlapCircleAll(this.gameObject.transform.position, 3);

                foreach (var col in cols)
                {
                    if (col.gameObject.CompareTag("Player") && col.gameObject.GetComponent<Player>().HP > 0)
                    {
                        col.gameObject.GetComponent<Player>().Hit(DAMAGE);
                    }
                }
            }

        }
        else
        {
            Real_Time = 0;
            ATK_ANIM = false ;
        }
    }

    void MonsterDie() // 몬스터 사망 함수.
    {
        if (HP <= 0)
        {
            canMove = false;

            StopCoroutine("ChangeMovement"); // 움직임 변환 코루틴 중지.

            rig.isKinematic = true; // 물리 효과의 영향 제거.
            rig.Sleep(); // 물리 효과 제거.

            Destroy(BoxCol); // 판정 박스 제거.
            Destroy(CirCol); // 추적 범위 제거.


            isTracing = false; // 추격 BOOL = false.

            if (!isDie)
            {
                StageManager.instance.Clear_Cheak(1);
                UI_Manager.instance.AddScore(10);
                isDie = true;
                Anim.SetTrigger("Die"); // 사망 애니메이터 = true.

            }


            MovementFlag = 0; // 움직임 플래그 = 0.
            SPEED = 0; // 이동속도 = 0.
            Destroy(this.gameObject, 4.0f); // 이 오브젝트를 4.0f 뒤에 제거.
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && other is BoxCollider2D) // 만약 오브젝트의 태그가 "Player" 일 경우.
        {
            traceTarget = other.gameObject; // 추격 대상 = Player.

            StopCoroutine("ChangeMovement"); // 움직임 변환 코루틴 중지.
        }
    }
    private void OnTriggerStay2D(Collider2D other) // 추격 진행.
    {
        if (other.gameObject.tag == "Player" && (other.gameObject.GetComponent<Player>().HP > 0) && other is BoxCollider2D) // 만약 오브젝트의 태그가 "Player" 일 경우.
        {
            float distance = (gameObject.transform.position - other.gameObject.transform.position).sqrMagnitude;

            isTracing = true;  // 추격 BOOL = true.
            if (distance <= RANGE)
            {
                canMove = false;
                isAttacking = true;
            }
            else
            {
                canMove = true;
                isAttacking = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) // 추격 종료.
    {
        if (other.gameObject.tag == "Player" && other is BoxCollider2D) // 오브젝트의 태그가 ' Player ' 일 경우.
        {
            isTracing = false; // 추격 BOOL = false.

            StartCoroutine("ChangeMovement"); // 움직임 변환 코루틴 시작.
        }
    }
}
