using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potato : Monster {

    public GameObject Bullet; // 몬스터 총알.

    private CameraCRTL Camera; // 카메라.

    public Vector2 total_minXAndY; // 모든 맵의 X와 Y값의 최소값.
    public Vector2 total_maxXAndY; // 모든 맵의 X와 Y값의 최대값.

    float Real_Time = 0; // 실제 시간.
    bool ATK_ANIM = false;
    float Delay = 1.05f;

    private void Awake()
    {
        Camera = GameObject.FindWithTag("MainCamera").GetComponent<CameraCRTL>();

        total_minXAndY.x = Camera.total_minXAndY.x - 8f; // 섹터 1의 이동가능한 X와 Y값의 최소값.
        total_minXAndY.y = 0; // 섹터 1의 이동가능한 X와 Y값의 최소값.

        total_maxXAndY.x = Camera.total_maxXAndY.x + 8f; // 섹터 1의 이동가능한 X와 Y값의 최대값.
        total_maxXAndY.y = 0; // 섹터 1의 이동가능한 X와 Y값의 최대값.
    }
    // Update is called once per frame
    void Update()
    {
        MonsterMove();
        MonsterDie();
        Monster_ATK();
    }

    void MonsterMove()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, total_minXAndY.x, total_maxXAndY.x), transform.position.y);

        Vector3 MoveVelocity = Vector3.zero; // 벡터 3 값 움직이는 방향.
        string dist = ""; // 거리값을 문자열로 선언.

        if (isTracing)
        {
            Vector3 playerPos = traceTarget.transform.position; // 플레이어의 위치 = 쫒아가는 타겟의 위치 값.
            if (playerPos.x < transform.position.x) // 플레이어가 몬스터의 왼쪽에 있는 경우.
            {
                transform.localScale = new Vector3(SCALE, SCALE, SCALE);
                transform.rotation = new Quaternion(0, 0, 0, 0);

                if (MovementFlag == 2) // 움직임 상황 '1' 플래그 일때.
                {
                    dist = "Right"; // 좌측으로 이동.                        
                }
                else// 움직임 상황 '2' 플래그 일때.
                {
                    dist = "Left"; // 우측으로 이동.          
                }
            }
            else if (playerPos.x > transform.position.x)
            {
                transform.localScale = new Vector3(SCALE, SCALE, SCALE);
                transform.rotation = new Quaternion(0, 180, 0, 0);
                if (MovementFlag == 1) // 움직임 상황 '1' 플래그 일때.
                {
                    dist = "Left"; // 좌측으로 이동.                        
                }
                else // 움직임 상황 '2' 플래그 일때.
                {
                    dist = "Right"; // 우측으로 이동.          
                }
            }
        }
        else
        {
            if (MovementFlag == 1) // 움직임 상황 '1' 플래그 일때.
            {
                transform.localScale = new Vector3(SCALE, SCALE, SCALE);
                transform.rotation = new Quaternion(0, 0, 0, 0);
                dist = "Left"; // 좌측으로 이동.                        
            }
            else if (MovementFlag == 2) // 움직임 상황 '2' 플래그 일때.
            {
                transform.rotation = new Quaternion(0, 180, 0, 0);
                transform.localScale = new Vector3(SCALE, SCALE, SCALE);
                dist = "Right"; // 우측으로 이동.          
            }
        }

        if (dist == "Left") // 위치값이 '왼쪽'인 경우.
        {
            MoveVelocity = Vector3.left; // 움직임 방향 세팅 ' 왼쪽 ' .
        }
        else if (dist == "Right") // 위치값이 '오른쪽' 인 경우.
        {
            MoveVelocity = Vector3.right; // 움직임 방향 세팅 ' 오른쪽 '.
        }


        if (canMove)
        {
            transform.position = transform.position + MoveVelocity * SPEED * Time.deltaTime; // 몬스터의 위치 = 움직이는 방향 x 움직이는 속도 x 실시간 프레임.
        }
        else if (!canMove)
        {
            transform.position = transform.position;
        }
    }

    void Monster_ATK() // 몬스터 공격 함수.
    {
        Vector3 AttackPos = gameObject.transform.position + new Vector3(1f * -(SCALE / 2.5f), 1.4f, 0f);

        if (isAttacking) // 공격 중인 상황일때
        {
            canMove = false;

            if (!ATK_ANIM) { ATK_ANIM = true; Anim.SetTrigger("ATK"); }
            Real_Time += Time.deltaTime;

            if (Real_Time >= Delay && !isDie) // 실제 시간이 공격 딜레이에 맞으면.
            {
                Debug.Log("Bang");

                Instantiate(Bullet, AttackPos, gameObject.transform.rotation);

                Real_Time = 0;
                ATK_ANIM = false;
            }

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
                isAttacking = true;
            }
            else
            {
                isAttacking = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) // 추격 종료.
    {
        if (other.gameObject.tag == "Player" && other is BoxCollider2D) // 오브젝트의 태그가 ' Player ' 일 경우.
        {
            isTracing = false; // 추격 BOOL = false.

        }
    }
}
