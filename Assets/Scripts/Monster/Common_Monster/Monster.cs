using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
     * S1. 이동.
     *  ㄴ 랜덤하게 (가만히 or 왼쪽 or 오른쪽).
     * S2. 공격.
     *  ㄴ 몬스터마다 패턴과 타이밍이 다름.
     *      ㄴ 공격 루틴 (발견 - 준비 - 가격).
     * S3. 사망
     *  ㄴ 몬스터마다 다름.
*/

public class Monster : MonoBehaviour {

    public int HP = 0; // 체력.
    public int DAMAGE = 0; // 공격력.
    public float SPEED = 0; // 이동속도.
    public float SCALE = 1; // 몬스터 크기.

    public GameObject traceTarget; // 쫒아갈 대상.
    public Animator Anim; // 애니메이터.

    private Rigidbody2D rig; // RigidBody2D.
    private BoxCollider2D BoxCol; // 박스 콜라이더.
    private CircleCollider2D CirCol; // 서클 콜라이더.

    private int MovementFlag = 0; // 0 = 대기, 1 = 왼쪽, 2 = 오른쪽.

    protected float Attack_Delay; // 공격 대기 시간.

    protected bool isDie; // 사망스위치.
    protected bool isTracing; // 추적스위치.
    protected bool isAttacking; // 공격스위치.
                   
    // Use this for initialization
    void Start () {
        Anim = gameObject.GetComponent<Animator>();
        rig = gameObject.GetComponent<Rigidbody2D>();
        BoxCol = gameObject.GetComponent<BoxCollider2D>();
        CirCol = gameObject.GetComponent<CircleCollider2D>();

        StartCoroutine("ChangeMovement"); // 코루틴 시작 선언 ( 움직임 )
    }
	
	// Update is called once per frame
	void FixedUpdate () { }

    protected void Monster_Move()
    {
        Vector3 MoveVelocity = Vector3.zero; // 벡터 3 값 움직이는 방향.
        string dist = ""; // 거리값을 문자열로 선언.

        if (isTracing)
        {
            Vector3 playerPos = traceTarget.transform.position; // 플레이어의 위치 = 쫒아가는 타겟의 위치 값.
            if (playerPos.x < transform.position.x) // 플레이어가 몬스터의 왼쪽에 있는 경우.
            {
                dist = "Left"; // 방향 ' 왼쪽 '
            }
            else if (playerPos.x > transform.position.x)
            {
                dist = "Right"; // 방향 ' 오른쪽 '
            }
        }
        else // 추적 상태가 아닐경우.
        {
            if (MovementFlag == 1) // 움직임 상황 '1' 플래그 일때.
            {
                dist = "Left"; // 좌측으로 이동.                        
            }
            else if (MovementFlag == 2) // 움직임 상황 '2' 플래그 일때.
            {
                dist = "Right"; // 우측으로 이동.          
            }
        }

        if (dist == "Left") // 위치값이 '왼쪽'인 경우.
        { 
            MoveVelocity = Vector3.left; // 움직임 방향 세팅 ' 왼쪽 ' .
            transform.localScale = new Vector3(SCALE, SCALE, SCALE); // 오브젝트의 스프라이트 반전 X .

        }
        else if (dist == "Right") // 위치값이 '오른쪽' 인 경우.
        {
            MoveVelocity = Vector3.right; // 움직임 방향 세팅 ' 오른쪽 '.
            transform.localScale = new Vector3(-SCALE, SCALE, SCALE); // 오브젝트의 스프라이트 반전 O.
        }
        transform.position += MoveVelocity * SPEED * Time.deltaTime; // 몬스터의 위치 = 움직이는 방향 x 움직이는 속도 x 실시간 프레임.
    }

    protected void Monster_Attack() { } // 몬스터 공격 함수.



    protected void Monster_Die() // 몬스터 사망 함수.
    {
        if (HP <= 0)
        {
            StopCoroutine("ChangeMovement"); // 움직임 변환 코루틴 중지.

            rig.isKinematic = true; // 물리 효과의 영향 제거.
            rig.Sleep(); // 물리 효과 제거.

            Destroy(BoxCol); // 판정 박스 제거.
            Destroy(CirCol); // 추적 범위 제거.

            Anim.SetBool("Die", true); // 사망 애니메이터 = true.
            Anim.SetBool("Move", false); // 움직임 애니메이터 = false.
            Anim.SetBool("ATK", false); // 공격 애니메이터  = false.

            isTracing = false; // 추격 BOOL = false.

            MovementFlag = 0; // 움직임 플래그 = 0.
            SPEED = 0; // 이동속도 = 0.
            Destroy(this.gameObject, 4.0f); // 이 오브젝트를 4.0f 뒤에 제거.
        }
    } 

    IEnumerator ChangeMovement() // 움직임 변환 코루틴.
    {
        MovementFlag = Random.Range(0, 3); // 움직임 플래그 0 ~ 3 까지의 난수.

        if (MovementFlag == 0) // 움직임 플래그가 ' 0 ' 일 경우.
        {
            Anim.SetBool("Move", false); // "Move" 애니메이터 = false.
        }
        else if(MovementFlag == 1 || MovementFlag == 2)// 그 외의 값 일경우.
        {
            Anim.SetBool("Move", true); // "Move" 애니메이터 = true.
        }

        yield return new WaitForSeconds(3f); // '3f' 딜레이 간격으로 실행.

        StartCoroutine("ChangeMovement"); // 움직임 변환 코루틴 시작.
    }


    void OnCollisionEnter2D(Collision2D col) // 몬스터 피격 함수.
    {
        if (col.gameObject.tag == "Bullet" && isDie == false) // 만약 "Bullet"태그의 오브젝트가 닿고 && 사망 플래그가 false 인 경우.
        {
            StartCoroutine("HitEffect"); // 히트 이펙트 코루틴 시작.
            --HP; // 체력 감소.
        }
    }

}
