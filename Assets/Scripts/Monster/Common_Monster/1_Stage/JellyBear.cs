using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
     * S1. 이동.
     *  ㄴ 랜덤하게 (가만히 or 왼쪽 or 오른쪽)
     * S2. 공격.
     *  ㄴ 몬스터마다 패턴과 타이밍이 다름
     *      ㄴ 공격 루틴 (발견 - 준비 - 가격)
     * S3. 사망
     *  ㄴ 몬스터마다 다름
*/

public class JellyBear : Monster {

    // Update is called once per frame
    float Real_Time = 0; // 실제 시간.

    void FixedUpdate()
    {
        base.Monster_Move();
        base.Monster_Die();
        Monster_ATK();
    }

    void Monster_ATK() // 몬스터 공격 함수.
    {
        Attack_Delay = 0.3f; // 공격 판정 선딜레이.

        if (isAttacking) // 공격 중인 상황일때
        {
            Debug.Log("Attack!");
            Anim.SetBool("ATK", true);

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
            Anim.SetBool("ATK", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") // 만약 오브젝트의 태그가 "Player" 일 경우.
        {
            traceTarget = other.gameObject; // 추격 대상 = Player.

            StopCoroutine("ChangeMovement"); // 움직임 변환 코루틴 중지.
        }
    }
    private void OnTriggerStay2D(Collider2D other) // 추격 진행.
    {
        if (other.gameObject.tag == "Player" && (other.gameObject.GetComponent<Player>().HP > 0)) // 만약 오브젝트의 태그가 "Player" 일 경우.
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
        if (other.gameObject.tag == "Player") // 오브젝트의 태그가 ' Player ' 일 경우.
        {
            isTracing = false; // 추격 BOOL = false.

            StartCoroutine("ChangeMovement"); // 움직임 변환 코루틴 시작.
        }
    }
}
