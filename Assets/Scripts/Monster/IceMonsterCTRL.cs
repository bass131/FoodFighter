using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMonsterCTRL : MonoBehaviour // 아이스크림 몬스터 개별 스크립트.
{
    public GameObject Drop; // 드랍 오브젝트 변수 선언.
    public GameObject ATK; // 공격 범위 오브젝트 선언.

    public float movePower = 1f; // 이동속도 선언.
    public float creatureType = 1f; // 몬스터 타입 선언.

    private float onTime = 0; // 추격 대상이 들어온후 흐르는 시간.
    private float ATK_Delay = 3.0f; // 공격 딜레이.
    public float atk_range = 2.0f; // 공격 범위.

    public GameObject traceTarget; // 추적하는 대상.

    Animator animator; // 애니메이터 선언.

    public AudioClip dieSeClip; // 사망 사운드 클립 선언.
    AudioSource dieSeAudio;

    public AudioClip attackSeClip; // 공격 사운드 클립 선언.
    AudioSource attackSeAudio;

    public AudioClip attackedSeClip; // 피격 사운드 클립 선언.
    AudioSource attackedAudio;

    Rigidbody2D rig; // Rigidbody2D 물리 효과 선언.
    BoxCollider2D BoxCol; // 판정 박스 선언.

    public int HP = 5; // 체력 선언.

    private Score Scores; // 'Score' 클래스의 Scores 선언.
    private SpriteRenderer spriteRen; // 스프라이트 렌더러 선언.

    int movementFlag = 0; // 0 = idle, 1 = left, 2 = right
    bool isTracing; // 추격트리거 BOOL값 선언.
    bool isDie = false; //사망트리거 BOOL값 선언.

    // Use this for initialization
    void Start() // 초기화 함수.
    {
        animator = gameObject.GetComponent<Animator>(); // 오브젝트 애니메이터 참조.

        rig = gameObject.GetComponent<Rigidbody2D>(); // RigidBody2D 참조.

        attackSeAudio = gameObject.AddComponent<AudioSource>(); // 공격 사운드 초기화.
        attackSeAudio.clip = attackSeClip;
        attackSeAudio.loop = false;

        dieSeAudio = gameObject.AddComponent<AudioSource>(); // 사망 사운드 초기화.
        dieSeAudio.clip = dieSeClip;
        dieSeAudio.loop = false;

        attackedAudio = gameObject.AddComponent<AudioSource>(); // 피격 사운드 초기화
        attackedAudio.clip = attackedSeClip;
        attackedAudio.loop = true;

        spriteRen = gameObject.GetComponent<SpriteRenderer>(); // 스프라이트 렌더러 초기화

        BoxCol = gameObject.GetComponent<BoxCollider2D>(); // 판정 박스 초기화


        StartCoroutine("ChangeMovement"); // 움직임 변환 코루틴 시작.
    }
    void FixedUpdate()
    {
        Move(); // 움직임 함수.

        if (HP == 0) //체력이 0 이 되면.
        {
            Dead_Animator(); // 사망 애니메이터 함수 사용.
            Dead_SE(); // 사망 사운드 함수 사용.
        }
    }

    void Move() // 움직임 관련 함수.
    {
        Vector3 moveVelocity = Vector3.zero; // 기본 방향 벡터값 .
        string dist = ""; // 방향 문자열 값.


        if (isTracing) // 추적 상황일때.
        {
            onTime += Time.deltaTime; // 추적에 들어간 시간 = 실시간.

            Vector3 playerPos = traceTarget.transform.position; // 플레이어의 위치값 = 추적할 대상의 위치값.

            if (playerPos.x < transform.position.x) // 플레이어가 몬스터의 왼쪽에 있는 경우.
            {
                dist = "Left"; // 방향 문자열 값 = 왼쪽.

                if (((transform.position.x - playerPos.x) < atk_range) && (HP > 0) && (transform.position.y - playerPos.y) < 0.2f && (transform.position.y - playerPos.y) > -0.2f && isDie == false)
                /* 공격 범위 안에 들어 왔고 , 체력이 0 이상이며,둘 사이의 거리가 0.2f 일때.*/
                {
                    attackedAudio.Play(); // 공격 사운드 출력.
                    attackedAudio.loop = true; // 공격 사운트 루프 = true.
                    animator.SetBool("ATK", true); // 공격 애니메이션 BOOL 값 = true.
                    StopCoroutine("ChangeMovement"); // 움직임 변환 코루틴 중지.
                    dist = ""; // 방향값 x.
                }
                else // 위의 조건에 불충족시.
                {
                    animator.SetBool("ATK", false); // 공격 애니메이션 BOOL 값 = false.
                    StartCoroutine("ChangeMovement"); // 움직임 변환 코루틴 시작.
                }
            }
            else if (playerPos.x > transform.position.x) // 플레이어가 몬스터의 오른쪽에 있는 경우.
            {
                dist = "Right"; // 방향 문자열 값  = 오른쪽.

                if (((playerPos.x - transform.position.x) < atk_range) && (HP > 0) && (playerPos.y - transform.position.y) < 0.2f && (playerPos.y - transform.position.y) > -0.2f && isDie == false)
                    /* 공격 범위 안에 들어 왔고 , 체력이 0 이상이며,둘 사이의 거리가 0.2f 일때.*/                 
                {                
                    attackedAudio.Play(); // 공격 사운드 실행.
                    attackedAudio.loop = true; // 공격 사운드 루프.
                    animator.SetBool("ATK", true); // 애니메이션 BOOL값 = true.
                    StopCoroutine("ChangeMovement"); // 움직임 변환 코루틴 중지.
                    dist = ""; // 거리값 x
                }
                else // 아닐경우.
                {
                    animator.SetBool("ATK", false); // 애니메이션 BOOL값 = false.
                    StartCoroutine("ChangeMovement"); // 움직임 변환 코루틴 시작.
                }
            }
        }
        else // 위의 모든 조건이 아닐경우.
        {
            if (movementFlag == 1) // 몬스터 움직임 플래그가 '1' 이면.
            {
                dist = "Left"; // 문자열 방향값 = 왼쪽.
            }
            else if (movementFlag == 2) // 몬스터 움직임 플래그가 '2' 이면.
            {
                dist = "Right"; // 문자열 방향값 = 오른쪽.
            }
        }



        /*문자열 방향값 관련 소스*/
        if (dist == "Left") // 문자열 방향값 = left 이면.
        {
            moveVelocity = Vector3.left; // 오브젝트의 벡터값 = left.
            transform.localScale = new Vector3(3f, 3f, 1f); // 몬스터의 이미지 플립 x.
        }
        else if (dist == "Right") // 문자열 방향값 = right 이면.
        {
            moveVelocity = Vector3.right; // 오브젝트의 벡터값 = right.
            transform.localScale = new Vector3(-3f, 3f, 1f); // 몬스터의 이미지 플립 o.
        }
        transform.position += moveVelocity * movePower * Time.deltaTime; // 몬스터의 위치 = 방향값 x 이동속도 x FPS.
    }

    void OnCollisionEnter2D(Collision2D col) // 총탄 피격.
    {
        if (col.gameObject.tag == "Bullet" && isDie == false) // "Bullet" 태그가 붙은 오브젝트가 충돌하고 몬스터가 죽지 않은 상태라면.
        {
            StartCoroutine("HitEffect"); // 피격 이펙트 코루틴 시작.
            --HP; // 체력 감소.
            Scores.HitScore += 10; // 타격 점수 +10
            attackSeAudio.Play(); // 피격 사운드 플레이.
        }
        /*else if ((col.gameObject.tag == "Enemy" || col.gameObject.tag == "Enemy_Ice") ) // 같은 몬스터 끼리 부딛혔을 경우.
        {
            rig.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            /*Rigidbody X,Y 축 고정.
        }*/
    }

   /* void OnCollisionExit2D(Collision2D col) // 
    {
        if ((col.gameObject.tag == "Enemy" || col.gameObject.tag == "Enemy_Ice"))
        {
            rig.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
    }*/

    void OnTriggerEnter2D(Collider2D other) // 추격 시작.
    {
        if (creatureType == 0)
            return;
        if (other.gameObject.tag == "Player")
        {
            traceTarget = other.gameObject;

            StopCoroutine("ChangeMovement");
        }

    }

    void OnTriggerStay2D(Collider2D other) // 추격 진행.
    {
        if (creatureType == 0)
        {
            return;
        }

        if (other.gameObject.tag == "Player")
        {
            isTracing = true;
        }

    }

    void OnTriggerExit2D(Collider2D other) // 추격 종료.
    {
        if (creatureType == 0)
        {
            return;
        }

        if (other.gameObject.tag == "Player")
        {
            isTracing = false;

            StartCoroutine("ChangeMovement");
        }
    }

    void Dead_Animator()
    {
        Debug.Log("isdie = true");
        StopCoroutine("ChangeMovement");
        rig.isKinematic = true;
        rig.Sleep();
        Destroy(BoxCol);
        Destroy(Drop,0.01f);
        Destroy(ATK);

        animator.SetBool("Die", true);
        animator.SetBool("Move", false);
        animator.SetBool("ATK", false);
        isTracing = false;

        movementFlag = 0;
        movePower = 0;
        Destroy(this.gameObject, 4.0f);
    }

    void Dead_SE()
    {
        dieSeAudio.Play();
    }

    IEnumerator ChangeMovement()
    {
        movementFlag = Random.Range(0, 3);

        if (movementFlag == 0)
        {
            animator.SetBool("Move", false);
        }
        else
        {
            animator.SetBool("Move", true);
        }

        yield return new WaitForSeconds(3f);

        StartCoroutine("ChangeMovement");
    }

    IEnumerator HitEffect()
    {
        float CountTime = 0;

        while (CountTime < 0.04f)
        {
            if (CountTime % 0.04f == 0)
            {
                spriteRen.color = new Color32(255, 90, 0, 255);
            }

            yield return new WaitForSeconds(0.2f);

            CountTime = CountTime + 0.04f;

        }

        spriteRen.color = new Color32(255, 255, 255, 255);


        yield return null;
    }
}