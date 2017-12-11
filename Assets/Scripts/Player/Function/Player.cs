using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;


interface Actors
{
    void Move();
    void Skill();
    void DIE();
}

public class Player : MonoBehaviour, Actors {

    public float SPEED = 5.0f; // 이동속도.
    public float HP = 5; // 체력.

    public int ImageVec = 1; // 이미지의 벡터값.

    private float SceneTime = 0f; // 씬 시간.

    public Rigidbody2D rigid; // 중력 컴포넌트.

    private CameraCRTL Camera; // 카메라.

    private FireCtrl fireCtrl; // 발사 컨트롤 스크립트.

    private Animator Head; // 플레이어 상체 - 머리.
    private Animator Body; // 플레이어 상체 - 몸.
    private Animator Hand; // 플레이어 상체 - 손.
    private Animator Player_Down; // 플레이어 하체.
    private Animator Booster; // 부스터 애니메이터.
    private Animator Gun; // 총 애니메이터.
    private Animator Rocket; // 로켓 애니메이터.

    private Monster Monsters; // 몬스터 클래스.

    public SpriteRenderer Sprites_Head; // 이미지 상체 스프라이트 렌더러.
    public SpriteRenderer Sprites_Body; // 이미지 상체 스프라이트 렌더러.
    public SpriteRenderer Sprites_Hand; // 이미지 상체 스프라이트 렌더러.
    public SpriteRenderer Sprites_Down; // 이미지 하체 스프라이트 렌더러.

    CircleCollider2D circle; // 원형 콜라이더.

    public bool Die = false; //사망 트리거.

    public bool isEnd = false; // 종료 Bool 트리거.
    bool isAttacked = false; // 피격 Bool 트리거.

    public AudioClip attackedSeClip; // 피격 사운드 클립.
    AudioSource attackedSeAudio; // 피격 사운드 오디오.

    public AudioClip itemSeClip; // 아이템 획득 사운드 클립.
    AudioSource itemSeAudio; // 아이템 획득 사운드 오디오.

    private Score Scores; // 점수.

    private RectTransform JoyStick;

    //==========체력 UI 부분.===============//
    public Image hpmask; // 체력 마스크.
    public Image[] heartmask = new Image[3]; // 하트 마스크 배열.

    private RectTransform hpmaskRect; // UI 이미지 트랜스폼 값. 

    public float maxHP; // 최대 체력.
    public float maxheart; // 최대 하트.
    public float hearthp; // 최대 HP값.

    private float maxHpBarWidth; // 최대 체력바 가로 값.
    private float maxheartbarwidth; // 최대 하트 가로 값.

    private bool isSkill = false; // 스킬 사용 트리거. 

    Vector3 MoveVelocity = Vector3.zero; // 이동 방향 벡터 값.

    public Vector2 Sec_01_minXAndY; // 섹터 1의 이동가능한 X와 Y값의 최소값.
    public Vector2 Sec_01_maxXAndY; // 섹터 1의 이동가능한 X와 Y값의 최대값.

    public Vector2 Sec_02_minXAndY; // 섹터 2의 이동가능한 X와 Y값의 최소값.
    public Vector2 Sec_02_maxXAndY; // 섹터 2의 이동가능한 X와 Y값의 최대값.

    public Vector2 Sec_03_minXAndY; // 섹터 3의 이동가능한 X와 Y값의 최소값.
    public Vector2 Sec_03_maxXAndY; // 섹터 3의 이동가능한 X와 Y값의 최대값.

    public Vector2 Boss_minXAndY; // 보스 섹터의 이동가능한 X와 Y값의 최소값.
    public Vector2 Boss_maxXAndY; // 보스 섹터의 이동가능한 X와 Y값의 최대값.

    public Vector2 total_minXAndY; // 모든 맵의 이동가능한 X와 Y값의 최소값.
    public Vector2 total_maxXAndY; // 모든 맵의 이동가능한 X와 Y값의 최대값.


    //==========스킬 관련 변수=============//
    public GameObject Barrior; // 방어스킬 방어막.

    public UI_CoolTime JumpSkill; // 점프스킬 버튼.
    public UI_CoolTime DashSkill; // 대쉬스킬 버튼.


    //==========점프 관련 변수=============//
    private float JumpPower = 130.0f; // 점프 파워.
    bool isjump; // 점프 트리거.

    public AudioClip jumpedSeClip; // 점프 사운드.
    AudioSource jumpedSeAudio;

    public AudioClip DashSeClip; // 점프 사운드.
    AudioSource DashSeAudio;


    private void Awake()
    {
        Camera = GameObject.FindWithTag("MainCamera").GetComponent<CameraCRTL>();

        #region Sector
        //================================================================//
        Sec_01_minXAndY.x = Camera.Sec_01_minXAndY.x - 8f; // 섹터 1의 이동가능한 X와 Y값의 최소값.
        Sec_01_minXAndY.y = 0; // 섹터 1의 이동가능한 X와 Y값의 최소값.

        Sec_01_maxXAndY.x = Camera.Sec_01_maxXAndY.x + 8f; // 섹터 1의 이동가능한 X와 Y값의 최대값.
        Sec_01_maxXAndY.y = 0; // 섹터 1의 이동가능한 X와 Y값의 최대값.

        //-------------------------
        Sec_02_minXAndY.x = Camera.Sec_02_minXAndY.x - 8f; // 섹터 2의 이동가능한 X와 Y값의 최소값.
        Sec_02_minXAndY.y = 0; // 섹터 2의 이동가능한 X와 Y값의 최소값.

        Sec_02_maxXAndY.x = Camera.Sec_02_maxXAndY.x + 8f; // 섹터 2의 이동가능한 X와 Y값의 최대값.
        Sec_02_maxXAndY.y = 0; // 섹터 2의 이동가능한 X와 Y값의 최대값.

        //-------------------------
        Sec_03_minXAndY.x = Camera.Sec_03_minXAndY.x - 8f; // 섹터 3의 이동가능한 X와 Y값의 최소값.
        Sec_03_minXAndY.y = 0; // 섹터 3의 이동가능한 X와 Y값의 최소값.

        Sec_03_maxXAndY.x = Camera.Sec_03_maxXAndY.x + 8f; // 섹터 3의 이동가능한 X와 Y값의 최대값.
        Sec_03_maxXAndY.y = 0; // 섹터 3의 이동가능한 X와 Y값의 최대값.

        //-------------------------
        Boss_minXAndY.x = Camera.Boss_minXAndY.x - 8f; // 보스 섹터의 이동가능한 X와 Y값의 최소값.
        Boss_minXAndY.y = 0; // 보스 섹터의 이동가능한 X와 Y값의 최소값.

        Boss_maxXAndY.x = Camera.Boss_maxXAndY.x + 8f; // 보스 섹터의 이동가능한 X와 Y값의 최대값.
        Boss_maxXAndY.y = 0; // 보스 섹터의 이동가능한 X와 Y값의 최대값.

        //-------------------------
        Boss_minXAndY.x = Camera.Boss_minXAndY.x - 8f; // 섹터 1의 이동가능한 X와 Y값의 최소값.
        Boss_minXAndY.y = 0; // 섹터 1의 이동가능한 X와 Y값의 최소값.

        Boss_maxXAndY.x = Camera.Boss_maxXAndY.x + 8f; // 섹터 1의 이동가능한 X와 Y값의 최대값.
        Boss_maxXAndY.y = 0; // 섹터 1의 이동가능한 X와 Y값의 최대값.

        //-------------------------
        total_minXAndY.x = Camera.total_minXAndY.x - 8f; // 섹터 1의 이동가능한 X와 Y값의 최소값.
        total_minXAndY.y = 0; // 섹터 1의 이동가능한 X와 Y값의 최소값.

        total_maxXAndY.x = Camera.total_maxXAndY.x + 8f; // 섹터 1의 이동가능한 X와 Y값의 최대값.
        total_maxXAndY.y = 0; // 섹터 1의 이동가능한 X와 Y값의 최대값.
#endregion

    }
    // Use this for initialization
    void Start() {

        rigid = gameObject.GetComponent<Rigidbody2D>(); // 중력 컴포넌트 초기화.

        fireCtrl = GameObject.FindWithTag("Player").GetComponent<FireCtrl>(); // 발사 컨트롤 스크립트 초기화.

        Head = GameObject.FindWithTag("Player_Head").GetComponent<Animator>(); // 상체 스프라이트 렌더 초기화.
        Body = GameObject.FindWithTag("Player_Body").GetComponent<Animator>(); // 상체 스프라이트 렌더 초기화.
        Hand = GameObject.FindWithTag("Player_Hand").GetComponent<Animator>(); // 상체 스프라이트 렌더 초기화.
        Player_Down = GameObject.FindWithTag("Player_Down").GetComponent<Animator>(); // 하체 스프라이트 렌더 초기화.
        Booster = GameObject.FindWithTag("Booster").GetComponent<Animator>(); // 부스터 애니메이터.
        Gun = GameObject.FindWithTag("Gun").GetComponent<Animator>(); // 총 애니메이터.
        Rocket = GameObject.FindWithTag("Rocket").GetComponent<Animator>(); // 로켓 애니메이터.

        circle = gameObject.GetComponent<CircleCollider2D>(); // 원형 콜라이터 초기화.

        Sprites_Down = GameObject.FindWithTag("Player_Down").GetComponent<SpriteRenderer>(); // 상체 스프라이트 렌더러 초기화.
        Sprites_Head = GameObject.FindWithTag("Player_Head").GetComponent<SpriteRenderer>(); // 하체 스프라이트 렌더러 초기화.
        Sprites_Body = GameObject.FindWithTag("Player_Body").GetComponent<SpriteRenderer>(); // 하체 스프라이트 렌더러 초기화.
        Sprites_Hand = GameObject.FindWithTag("Player_Hand").GetComponent<SpriteRenderer>(); // 하체 스프라이트 렌더러 초기화.


        attackedSeAudio = gameObject.AddComponent<AudioSource>(); // 피격 사운드 컴포넌트 초기화.
        attackedSeAudio.clip = attackedSeClip; // 피격 사운드 클립 초기화.
        attackedSeAudio.loop = false; // 피격 사운드 루프  = false. 

        itemSeAudio = gameObject.AddComponent<AudioSource>(); // 아이템 획특 사운드 컴포넌트 초기화.
        itemSeAudio.clip = itemSeClip; // 아이템 획득 사운드 클립 초기화.
        itemSeAudio.loop = false; // 아이템 획득 사운드 루프  = false.

        DashSeAudio = gameObject.AddComponent<AudioSource>(); // 아이템 획특 사운드 컴포넌트 초기화.
        DashSeAudio.clip = DashSeClip; // 아이템 획득 사운드 클립 초기화.
        DashSeAudio.loop = false; // 아이템 획득 사운드 루프  = false.

        jumpedSeAudio = gameObject.AddComponent<AudioSource>(); // 점프 사운드 컴포넌트 초기화.
        jumpedSeAudio.clip = jumpedSeClip;// 점프 사운드 클립 초기화.
        jumpedSeAudio.loop = false; // 점프 사운드 루프 = false;

        JoyStick = GameObject.FindWithTag("JoyStick").GetComponent<RectTransform>();

        //================================================================//

        hpmaskRect = hpmask.GetComponent<RectTransform>(); // 체력바 스프라이트 초기화.
        maxHpBarWidth = hpmaskRect.sizeDelta.x; // 체력바 스프라이트 최대 가로 길이 초기화.

        HP = maxHP; // 최대 체력 = maxHP 변수의 값.
        hearthp = maxheart; // 하트의 값 = 최대 하트의 값.

        //================================================================//
        JumpSkill = GameObject.FindWithTag("Jump_Skill").GetComponent<UI_CoolTime>();
        DashSkill = GameObject.FindWithTag("Dash_Skill").GetComponent<UI_CoolTime>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        Skill();
        Move();
        DIE();
    }

    
    public void Move() // 플레이어 이동 함수.
    {

        MoveVelocity = Vector3.zero;

        if (HP == 0)
        {
            MoveVelocity = Vector3.zero;
        }

        if (CrossPlatformInputManager.GetAxisRaw("Horizontal") < 0 && JoyStick.position.y < 175 && JoyStick.position.y > 60)
        {
            Anim_Move();
            Player_L();
        }
        else if (CrossPlatformInputManager.GetAxisRaw("Horizontal") > 0 && JoyStick.position.y < 175 && JoyStick.position.y > 60)
        {
            Anim_Move();
            Player_R();
        }
        else
        {
            Anim_Idle();
        }

        if (StageManager.instance.Player_State == StageManager.Stage.Move)
        //만약 플레이어의 상태가 이동중이라면
        {
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, total_minXAndY.x, total_maxXAndY.x), transform.position.y);
            // 플레이어의 x 좌표 이동의 제한은 전체 최소 및 최대
        }
        else if (StageManager.instance.Player_State == StageManager.Stage.Engage)
        // 만약 플레이어의 상태가 교전중이고.
        {
            switch (StageManager.instance.Sec)
            //섹터의 상태를 체크.
            {
                case StageManager.Sector.Sector_1:
                    //첫번째 섹터에 있는 상태라면.
                    transform.position = new Vector2(Mathf.Clamp(transform.position.x, Sec_01_minXAndY.x, Sec_01_maxXAndY.x), transform.position.y);                    
                    //카메라의 x 좌표 이동의 제한은 섹터 1의 최소 및 최대.
                    break;
                case StageManager.Sector.Sector_2:
                    //두번째 섹터에 있는 상태라면.
                    transform.position = new Vector2(Mathf.Clamp(transform.position.x, Sec_02_minXAndY.x, Sec_02_maxXAndY.x), transform.position.y);
                    //카메라의 x 좌표 이동의 제한은 섹터 2의 최소 및 최대.
                    break;
                case StageManager.Sector.Sector_3:
                    //세번째 섹터에 있는 상태라면.
                    transform.position = new Vector2(Mathf.Clamp(transform.position.x, Sec_03_minXAndY.x, Sec_03_maxXAndY.x), transform.position.y);           
                    //카메라의 x 좌표 이동의 제한은 섹터 3의 최소 및 최대.
                    break;
                case StageManager.Sector.Boss:
                    //보스 섹터에 있는 상태라면.
                    transform.position = new Vector2(Mathf.Clamp(transform.position.x, Boss_minXAndY.x, Boss_maxXAndY.x), transform.position.y);             
                    //카메라의 x 좌표 이동의 제한은 보스 섹터의 최소 및 최대.
                    break;
            }
        }

        transform.position = transform.position + (MoveVelocity * SPEED * Time.deltaTime);
    }
    //==========================좌우 함수==========================//
    public void Player_R() // 오른쪽 버튼
    {
        MoveVelocity = Vector3.right;
        ImageVec = 1;
        transform.localScale = new Vector3(ImageVec, 1, 1);
    }

    public void Player_L() // 왼쪽 버튼.
    {
        MoveVelocity = Vector3.left;
        ImageVec = -1;
        transform.localScale = new Vector3(ImageVec, 1, 1);
    }




    public void Jump() // 점프 함수.
    {
        if (!isjump)
        {
            Anim_Jump();
            isjump = true;
            Debug.Log("점프시작");
            jumpedSeAudio.Play();
            rigid.AddForce(Vector3.up * JumpPower, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D col) // 착지 관련 함수.
    {
        if (col.gameObject.tag == "Road")
        {
            isjump = false; 
        }
    }









    /*public void ATK() // 플레이어 공격 함수.
    {
        FireCrtl 클래스로 구현 완료.
    }
    */



    float ActiveTime = 0;

    public void Skill() // 플레이어 스킬 함수.
    {
        float SkillTime = Time.time + ActiveTime;

        if (Input.GetKey(KeyCode.Z))
        {
            ActiveTime = 2.0f;

            isSkill = true;
            Skill_A();
        }    
        else if(Input.GetKey(KeyCode.X))
        {
            ActiveTime = 5.0f;

            isSkill = true;
            Skill_B();
        }
        else if(Input.GetKey(KeyCode.C) && !isjump)
        {
            ActiveTime = 8.0f;

            isSkill = true;
            Skill_C();
        }


        if (Time.time > SkillTime)
        {
            isSkill = false;
        }
    }

    public void Skill_A() // 대쉬 스킬.
    {
        if (DashSkill.canUseSkill == true)
        {
            Anim_Dash();
            DashSeAudio.Play();
            rigid.AddForce(new Vector2(ImageVec, 0) * 70f, ForceMode2D.Impulse);

            Invoke("invoke_A", 2.0f);
        }
    }





    private void Skill_B() // 방어 스킬.
    {
        Instantiate(Barrior, gameObject.transform.position + new Vector3(0,1.2f), gameObject.transform.rotation);
    }
    

    public void Skill_C() // 이단 점프.
    {
        if (JumpSkill.canUseSkill == true)
        {
            Anim_Jump();
            isjump = true;

            float SkillJump = JumpPower + 50.0f;

            Debug.Log("점프시작");
            jumpedSeAudio.Play();
            rigid.AddForce(Vector3.up * SkillJump, ForceMode2D.Impulse);
        }
    }

    private void Skill_D() // 홀로그램 (미정)
    {

    }





    public void DIE() // 플레이어 사망 함수.
    {
        if (HP <= 0)
        { 
            if (!Die)
            {
                Anim_Die();
            }
            Die = true;
            Destroy(circle);

            SPEED = 0;
            Time.timeScale = 0.5f;
        }
    }





    private void hpLower() // 맞을때마다 체력바 줄어듬
    {
        float deltaSize = HP / maxHP;
        hpmaskRect.sizeDelta = new Vector2(maxHpBarWidth * deltaSize, hpmaskRect.sizeDelta.y);
    }





    public void Hit(int atk) // 피격함수 (미완성.)
    {
        if (HP > 0)
        {
            Debug.Log("GetDMG");
            HP = HP - atk; // 체력 소모.
            attackedSeAudio.Play(); // 피격 사운드 출력.

            hpLower();
            rigid.AddForce(new Vector2(ImageVec, 0) * -1 * 25f, ForceMode2D.Impulse);

            Anim_Hit();
        }
    }


    // 애니메이션 렌더 함수. 지역 처리로 숨겨놓음.
#region anim
    public void Anim_Idle()
    {
        Head.SetTrigger("Idle");
        Body.SetTrigger("Idle");
        Hand.SetTrigger("Idle");
        Player_Down.SetTrigger("Idle");
    }

    public void Anim_Move()
    {
        Head.SetTrigger("Move");
        Body.SetTrigger("Move");
        Hand.SetTrigger("Move");
        Player_Down.SetTrigger("Move");
    }

    public void Anim_Shoot()
    {
        Head.SetTrigger("Shoot");
        Body.SetTrigger("Shoot");
        Hand.SetTrigger("Shoot");
    }

    public void Anim_Hit()
    {
        Head.SetTrigger("Hit");
        Body.SetTrigger("Hit");
        Hand.SetTrigger("Hit");
        Player_Down.SetTrigger("Hit");
    }

    public void Anim_Jump()
    {
        Head.SetTrigger("Jump");
        Body.SetTrigger("Jump");
        Hand.SetTrigger("Jump");
        Player_Down.SetTrigger("Jump");
        Booster.SetTrigger("Jump");
    }

    public void Anim_Die()
    {
        Head.SetTrigger("Die");
        Body.SetTrigger("Die");
        Hand.SetTrigger("Die");
        Player_Down.SetTrigger("Die");
    }

    public void Anim_Dash()
    {
        Head.SetTrigger("Dash");
        Body.SetTrigger("Dash");
        Hand.SetTrigger("Dash");
        Player_Down.SetTrigger("Dash");
        Booster.SetTrigger("Dash");
        Gun.SetTrigger("Dash");
        Rocket.SetTrigger("Dash");
    }
#endregion
}


