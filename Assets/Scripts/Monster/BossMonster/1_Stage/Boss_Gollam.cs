﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Gollam : MonoBehaviour {

    public int HP = 50; // 체력.
    public float Speed = 2; // 이동속도.
    public float Scale = 1.0f; // 크기.

    public Transform trPunch; //펀치스킬 발사위치.

    public GameObject sPunch; // 펀치투사체 오브젝트.

    public GameObject sStone_A; //파편투사체_A
    public GameObject sStone_B; //파편투사체_B
    public GameObject sStone_C; //파편투사체_C

    private Animator Anim; // 애니메이터.

    private int Phase = 0; // 행동 페이즈.
    private int Flag = 0; // Move 플래그.

    public AudioClip attackSeClip; // 피격 소리 클립.
    AudioSource attackSeAudio; // 피격 소리 오디오.

    private bool isMoving = false; // 무브 트리거.
    private bool isAttack = false; // 공격 트리거.
    private bool isSkill = false; // 스킬 트리거.
    private bool isDie = false; // 사망 트리거.

    bool isFire = true;


    // Use this for initialization
    void Start () {
        Anim = gameObject.GetComponent<Animator>();
        StartCoroutine("pStats");

        attackSeAudio = gameObject.AddComponent<AudioSource>(); // 공격음 참조 선언.
        attackSeAudio.clip = attackSeClip;
        attackSeAudio.loop = false;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        bMove();
        bAttack();
        bSkill();
        bAnim();
        bDie();
    }

    void bMove()
    {
        Vector3 moveVelocity = Vector3.zero; // 벡터 3 값 움직이는 방향.
        string dist = ""; // 거리값을 문자열로 선언.
        StartCoroutine("Movement");

        if (isMoving)
        { 
            StartCoroutine("Rand_Move");

            if (Flag == 1)
            {
                dist = "Left";
            }
            else if (Flag == 2)
            {
                dist = "Right";
            }

            if (dist == "Left")
            {
                moveVelocity = Vector3.left; // 움직임 방향 세팅 ' 왼쪽 ' .
                transform.localScale = new Vector3(Scale, Scale, Scale); // 오브젝트의 스프라이트 반전 X .
            }
            else if (dist == "Right")
            {
                moveVelocity = Vector3.right; // 움직임 방향 세팅 ' 왼쪽 ' .
                transform.localScale = new Vector3(Scale * -1, Scale, Scale); // 오브젝트의 스프라이트 반전 X .
            }

            transform.position += moveVelocity * Speed * Time.deltaTime; // 몬스터의 위치 = 움직이는 방향 x 움직이는 속도 x 실시간 프레임.
        }

    }

    void bAttack()
    {
        StopCoroutine("Movement");
        if(isAttack)
        {
            //일반공격 시전.
        }
    }
    
    void bSkill()
    {
        StopCoroutine("Movement");
        if (isSkill && Phase == 2) { }
        //무적스킬 시전.


        else if (isSkill && Phase == 3 && !isFire)
        {
            isFire = true;
            Instantiate(sPunch, trPunch);
        }
        //주먹투사체 시전.


        else if (isSkill && Phase == 4 && !isFire)
        {
            isFire = true;
            Instantiate(sStone_A, trPunch);
            Instantiate(sStone_B, trPunch);
            Instantiate(sStone_C, trPunch);
        }
        //파편투사체 시전.
    }
    
    void bDie()
    {
        if(HP <= 0)
        {
            StopCoroutine("pStats");
            StopCoroutine("Movement");
            isMoving = false;
            isAttack = false;
            isSkill = false;
            isDie = true;

            Destroy(gameObject, 12.0f);
        }
    }

    void bAnim()
    {
        /*
         bool 세팅
         isMoving = 무브.
         isAttack = 공격.
         isSkill = 스킬.
         isDie = 죽음.  
         */
         switch (Phase)
        {
            case 0: // 이동.
                Anim.SetBool("isMoving", true);
                Anim.SetBool("isAttack", false);
                Anim.SetBool("isSkill_A", false);
                Anim.SetBool("isSkill_B", false);
                Anim.SetBool("isSkill_C", false);
                Anim.SetBool("isDie", false);
                break;
            case 1: // 공격.
                Anim.SetBool("isMoving", false);
                Anim.SetBool("isAttack", true);
                Anim.SetBool("isSkill_A", false);
                Anim.SetBool("isSkill_B", false);
                Anim.SetBool("isSkill_C", false);
                Anim.SetBool("isDie", false);
                break;
            case 2: // 스킬_1(무적)
                Anim.SetBool("isMoving", false);
                Anim.SetBool("isAttack", false);
                Anim.SetBool("isSkill_A", true);
                Anim.SetBool("isSkill_B", false);
                Anim.SetBool("isSkill_C", false);
                Anim.SetBool("isDie", false);
                break;
            case 3: // 스킬_2(주먹투사체)
                Anim.SetBool("isMoving", false);
                Anim.SetBool("isAttack", false);
                Anim.SetBool("isSkill_A", false);
                Anim.SetBool("isSkill_B", true);
                Anim.SetBool("isSkill_C", false);
                Anim.SetBool("isDie", false);
                break;
            case 4: // 스킬_3(돌투사체)
                Anim.SetBool("isMoving", false);
                Anim.SetBool("isAttack", false);
                Anim.SetBool("isSkill_A", false);
                Anim.SetBool("isSkill_B", false);
                Anim.SetBool("isSkill_C", true);
                Anim.SetBool("isDie", false);
                break;
            case 5: // 사망.
                Anim.SetBool("isMoving", false);
                Anim.SetBool("isAttack", false);
                Anim.SetBool("isSkill_A", false);
                Anim.SetBool("isSkill_B", false);
                Anim.SetBool("isSkill_C", false);
                Anim.SetBool("isDie", true);
                break;
        }

    }

    IEnumerator pStats() // 행동 상태 코루틴.
    {
        Phase = Random.Range(0, 5);
        switch (Phase)
        {
            case 0: // 이동.
                isMoving = true;
                isAttack = false;
                isSkill = false;
                isDie = false;
                isFire = false;
                Debug.Log("Move Phase.");
                break;
            case 1: // 공격.
                isMoving = false;
                isAttack = true;
                isSkill = false;
                isDie = false;
                isFire = false;
                Debug.Log("ATK Phase.");
                break;
            case 2: // 스킬_1(무적)
                isMoving = false;
                isAttack = false;
                isSkill = true;
                isDie = false;
                isFire = false;
                Debug.Log("Skill A Phase.");
                break;
            case 3: // 스킬_2(주먹투사체)
                isMoving = false;
                isAttack = false;
                isSkill = true;
                isDie = false;
                Debug.Log("Skill B Phase.");
                break;
            case 4: // 스킬_3(돌투사체)
                isMoving = false;
                isAttack = false;
                isSkill = true;
                isDie = false;
                Debug.Log("Skill C Phase.");
                break;
        }

        yield return new WaitForSeconds(1.4f); // '3f' 딜레이 간격으로 실행.

        StartCoroutine("pStats");
    }
    IEnumerator Movement() // 움직임 변환 코루틴.
    {
        Flag = Random.Range(1, 2);

        yield return new WaitForSeconds(3f); // '3f' 딜레이 간격으로 실행.

        StartCoroutine("ChangeMovement"); // 움직임 변환 코루틴 시작.
    }

    void OnCollisionEnter2D(Collision2D col) // 충돌판정 피격시.
    {
        if (col.gameObject.tag == "Bullet" && isDie == false) // 만약 "Bullet"태그의 오브젝트가 닿고 && 사망 플래그가 false 인 경우.
        {
            StartCoroutine("HitEffect"); // 히트 이펙트 코루틴 시작.
            --HP; // 체력 감소.
            attackSeAudio.Play(); // 총탄 발사 오디오 출력.
        }
    }

}
