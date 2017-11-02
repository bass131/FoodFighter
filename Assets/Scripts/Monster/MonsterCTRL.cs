using UnityEngine;
using System.Collections;

public class MonsterCTRL : MonoBehaviour {

    public GameObject Drop; // 드롭 할 아이템 오브젝트 선언.
    public GameObject ATK; // 공격할 오브젝트 선언.

    public float movePower = 1f; // 움직이는 속도.
    public float creatureType = 1f; // 몬스터 타입.

    private float ATK_Delay = 3.0f; // 공격 딜레이.

    private float dieSeTiming = 0.1f; // 죽는 타이밍
    private float dietimePlus = 0f; // 죽는 시간 더하기.

    public int Count = 0;  // 카운트.

    public GameObject traceTarget; // 쫒아갈 대상.

    Animator animator; // 애니메이터 선언.

    public AudioClip dieSeClip; // 죽는 소리 클립.
    AudioSource dieSeAudio; // 죽는 소리 오디오.

    public AudioClip attackSeClip; // 공격 소리 클립.
    AudioSource attackSeAudio; // 공격 소리 오디오.

    public float atk_range = 2.0f; // 공격 범위.

    BoxCollider2D BoxCol; // 피격 범위 설정.

    Rigidbody2D rig; // 물리 엔진 설정.

    Vector3 attackedVelocity; // 공격할 방향 벡터값.

    private Score Scores; // 점수.

    private SpriteRenderer sprites; // 이미지 스프라이트 변수 선언.
    public int HP = 2; // 체력값.

    int movementFlag = 0; // 0 = idle, 1 = left, 2 = right
    bool isTracing; // 추적 트리거.
    bool isDie = false; // 사망 트리거(기본셋 - false)

	// Use this for initialization
	void Start () {

        animator = gameObject.GetComponent<Animator>(); // 에니메이터 참조 선언.

        Scores = GameObject.FindWithTag("Score").GetComponent<Score>();  // 점수 오브젝트 참조 선언.

        attackSeAudio = gameObject.AddComponent<AudioSource>(); // 공격음 참조 선언.
        attackSeAudio.clip = attackSeClip;
        attackSeAudio.loop = false;

        dieSeAudio = gameObject.AddComponent<AudioSource>(); // 사망음 참조 선언.
        dieSeAudio.clip = dieSeClip;
        dieSeAudio.loop = false;

        sprites = gameObject.GetComponent<SpriteRenderer>(); // 스프라이트 참조 선언.

        BoxCol = gameObject.GetComponent<BoxCollider2D>(); // 피격 범위 참조 선언.

        rig = gameObject.GetComponent<Rigidbody2D>(); // 물리 엔진 참조 선언.
        
        StartCoroutine("ChangeMovement"); // 코루틴 시작 선언 ( 움직임 )

    }
    void FixedUpdate() // 매 물리 프레임 마다 반복.
    {
        Move (); // Move 함수 선언.
        if (HP == 0) // 만약 몬스터의 체력이 0 일때.
        {
            Dead_Animator(); // 죽는 에니메이터
            Dead_SE(); // 죽는 소리.
        }
    }

    void Move() // 움직임 함수.
    {
        Vector3 moveVelocity = Vector3.zero; // 벡터 3 값 움직이는 방향.
        string dist = ""; // 거리값을 문자열로 선언.


        if(isTracing) // 추적하는 상황일때.
        {
            Vector3 playerPos = traceTarget.transform.position; // 플레이어의 위치 = 쫒아가는 타겟의 위치 값.

            if (playerPos.x < transform.position.x) // 플레이어가 몬스터의 왼쪽에 있는 경우.
            {
                dist = "Left"; // 거리값 ' 왼쪽 '

                if (((transform.position.x - playerPos.x) < atk_range) && (HP > 0) && (transform.position.y - playerPos.y) < 0.2f && (transform.position.y - playerPos.y) > -0.2f && isDie == false)
                    /* 공격 범위 안에 들어 왔고 , 체력이 0 이상이며,둘 사이의 거리가 0.2f 일때.*/
                {
                    animator.SetBool("ATK", true); // 공격 에니메이션 시작.
                    StopCoroutine("ChangeMovement"); // 움직임 변환 코루틴 중지.
                    dist = ""; // 거리값 x
                }
                else // 위의 조건에 불충족시.
                {
                    animator.SetBool("ATK", false); // 공격 애니메이션을 중지.
                    StartCoroutine("ChangeMovement"); // 움직임 변환 코루틴 시작.
                }
            }
            else if (playerPos.x > transform.position.x) // 플레이어가 몬스터의 오른쪽에 있는 경우.
            {
                dist = "Right"; // 거리값 ' 오른쪽 '
                
                if (((playerPos.x - transform.position.x) < atk_range) && (HP > 0) && (playerPos.y - transform.position.y) < 0.2f && (playerPos.y - transform.position.y) > -0.2f && isDie == false)
                /* 공격 범위 안에 들어 왔고 , 체력이 0 이상이며,둘 사이의 거리가 0.2f 일때.*/
                { 
                    animator.SetBool("ATK", true); // 공격 애니메이션을 시작.
                    StopCoroutine("ChangeMovement"); // 움직임 변환 코루틴 중지.
                    dist = ""; // 거리값 x
                }
                else // 위의 조건에 불충족시.
                {
                    animator.SetBool("ATK", false); // 공격 애니메이션을 중지.
                    StartCoroutine("ChangeMovement"); // 움직임 변환 코루틴 시작.
                }

            }

        }
        else // 추적 상태가 아닐경우.
        {
            if (movementFlag == 1) // 움직임 상황 '1' 플래그 일때.
            {
                dist = "Left"; // 좌측으로 이동.                        
            }
            else if (movementFlag == 2) // 움직임 상황 '2' 플래그 일때.
            {
                dist = "Right"; // 우측으로 이동.          
            }
        }

        if (dist == "Left") // 위치값이 '왼쪽'인 경우.
        {
            attackedVelocity = Vector3.right; // 공격 당하는 방향 세팅 ' 오른쪽 ' .
            moveVelocity = Vector3.left; // 움직임 방향 세팅 ' 왼쪽 ' .
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f); // 오브젝트의 스프라이트 반전 X .

        }
        else if (dist == "Right") // 위치값이 '오른쪽' 인 경우.
        {
            attackedVelocity = Vector3.left; // 공격 당하는 방향 세팅 ' 왼쪽 ' .
            moveVelocity = Vector3.right; // 움직임 방향 세팅 ' 오른쪽 '.
            transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f); // 오브젝트의 스프라이트 반전 O.
        }
        transform.position += moveVelocity * movePower * Time.deltaTime; // 몬스터의 위치 = 움직이는 방향 x 움직이는 속도 x 실시간 프레임.
    }

    void OnCollisionEnter2D(Collision2D col) // 충돌판정 피격시.
    {
        if (col.gameObject.tag == "Bullet" && isDie == false) // 만약 "Bullet"태그의 오브젝트가 닿고 && 사망 플래그가 false 인 경우.
        {
            StartCoroutine("HitEffect"); // 히트 이펙트 코루틴 시작.
            --HP; // 체력 감소.
            Count++; // 카운트 증가.
            Scores.HitScore += 10; // 점수 클래스의 타격점수 10점 증가.
            attackSeAudio.Play(); // 총탄 발사 오디오 출력.
            
        }
        /*else if((col.gameObject.tag == "Enemy" || col.gameObject.tag == "Enemy_Ice")) // 만약 태그가 "적군" 일 경우.
        {
                rig.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
             회전과 X,Y 움직임에 대한 제한 설정. 
        }*/
    }

   /* void OnCollisionExit2D(Collision2D col) // 충돌판정 중지시.
    {
        if ((col.gameObject.tag == "Enemy" || col.gameObject.tag == "Enemy_Ice")) // 만약 태그가 "적군" 일 경우.
        {
            rig.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation; 
            /*  X,Y 움직임에 대한 제한 해제. 
        }
    }*/

    void OnTriggerEnter2D(Collider2D other) // 추격 시작.
    {
        if (creatureType == 0) // 몬스터 타입 ' 0 ' 일 경우.
            return; // 리턴해서 추격 탈출.
        if (other.gameObject.tag =="Player") // 만약 오브젝트의 태그가 "Player" 일 경우.
        {
            traceTarget = other.gameObject; // 추격 대상 = Player.

            StopCoroutine("ChangeMovement"); // 움직임 변환 코루틴 중지.
        }

    }

    void OnTriggerStay2D(Collider2D other) // 추격 진행.
    {
        if (creatureType == 0) // 몬스터 타입 ' 0 ' 일 경우.
        {
            return; // 리턴해서 추격 탈출.
        }

        if (other.gameObject.tag == "Player") // 만약 오브젝트의 태그가 "Player" 일 경우.
        {
            isTracing = true;  // 추격 BOOL = true.
        }
    
    }

    void OnTriggerExit2D(Collider2D other) // 추격 종료.
    {
        if (creatureType == 0) // 몬스터 타입이 ' 0 ' 일 경우.
        {
            return; // 리턴으로 탈출.
        }

        if (other.gameObject.tag == "Player") // 오브젝트의 태그가 ' Player ' 일 경우.
        {
            isTracing = false; // 추격 BOOL = false.

            StartCoroutine("ChangeMovement"); // 움직임 변환 코루틴 시작.
        }
    }

    void Dead_SE() // 사망 사운트 이펙트.
    {
        dieSeAudio.Play(); 
    }

    void Dead_Animator() // 사망 애니메이터.
    {
        StopCoroutine("ChangeMovement"); // 움직임 변환 코루틴 중지.

        rig.isKinematic = true; // 물리 효과의 영향 제거.
        rig.Sleep(); // 물리 효과 제거.

        Destroy(BoxCol); // 판정 박스 제거.
        Destroy(Drop,0.01f); // 아이템 드랍 (0.01f의 딜레이 이후에 실행)
        Destroy(ATK); // 공격범위 박스 제거.

        animator.SetBool("Die", true); // 사망 애니메이터 = true.
        animator.SetBool("Move", false); // 움직임 애니메이터 = false.
        animator.SetBool("ATK", false); // 공격 애니메이터  = false.

        isTracing = false; // 추격 BOOL = false.
        movementFlag = 0; // 움직임 플래그 = 0.
        movePower = 0; // 이동속도 = 0.
        Destroy(this.gameObject, 4.0f); // 이 오브젝트를 4.0f 뒤에 제거.
    }


    IEnumerator ChangeMovement() // 움직임 변환 코루틴.
    {
        movementFlag = Random.Range(0, 3); // 움직임 플래그 0 ~ 3 까지의 난수.

        if (movementFlag == 0) // 움직임 플래그가 ' 0 ' 일 경우.
        {
            animator.SetBool("Move", false); // "Move" 애니메이터 = false.
        }
        else // 그 외의 값 일경우.
        {
            animator.SetBool("Move", true); // "Move" 애니메이터 = true.
        }

        yield return new WaitForSeconds(3f); // '3f' 딜레이 간격으로 실행.

        StartCoroutine("ChangeMovement"); // 움직임 변환 코루틴 시작.
    }

    IEnumerator HitEffect() // 피격 이펙트 코루틴.
    {
        float CountTime = 0; // 카운트 경과 시간.

        while (CountTime < 0.04f) // 카운트 경과 시간이 0.04f 미만 일때 까지 반복. 
        {
            if (CountTime % 0.04f == 0) // ' 카운트 경과 시간 % 0.04f ' 를 했을때 나머지가 0 인 경우.
            {
                sprites.color = new Color32(255, 90, 0, 255); // 빨간 음영을 추가.
            }

            yield return new WaitForSeconds(0.2f); // '0.2f'의 딜레이를 줌.

            CountTime = CountTime + 0.04f; // 카운트 경과 시간 = 경과시간 + 0.04f(매 프레임 마다 증가).

        }

        sprites.color = new Color32(255, 255, 255, 255); // 스프라이트 컬러 모두 출력.


        yield return null; // 종료.
    }

}
