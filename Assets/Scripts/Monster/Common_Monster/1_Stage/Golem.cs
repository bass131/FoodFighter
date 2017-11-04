using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Monster {
    void FixedUpdate()
    {
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
            if ((other.gameObject.transform.position.x - gameObject.transform.position.x) <= 2.0f)
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
