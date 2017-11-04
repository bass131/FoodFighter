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
	void FixedUpdate () {
        base.Monster_Move();
        base.Monster_Die();
        Monster_Attack();
    }

    void Monster_Attack()
    {
        float Real_Time = 0; // 실제 시간.
        Attack_Delay = 0.7f; // 공격 판정 선딜레이.

        if (isAttacking)
        {
            Real_Time = Real_Time + Time.time;
            /*if( 실제 시간이 공격 딜레이가 되면 )
            {
                공격 판정 트리거 ON;
                인포 매니저에 전달.
            }*/
        }
        else
        {
            Real_Time = 0;
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
        if (other.gameObject.tag == "Player") // 만약 오브젝트의 태그가 "Player" 일 경우.
        {
            isTracing = true;  // 추격 BOOL = true.
            if((other.gameObject.transform.position.x - gameObject.transform.position.x) <= 2.0f)
            {
                isTracing = false;
                isAttacking = true;
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
