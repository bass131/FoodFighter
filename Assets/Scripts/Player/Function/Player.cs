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
    private PlayerAnimation_Up Head; // 플레이어 상체 - 머리.
    private PlayerAnimation_Up Body; // 플레이어 상체 - 몸.
    private PlayerAnimation_Up Hand; // 플레이어 상체 - 손.
    private PlayerAnimation_Bottom Player_Down; // 플레이어 하체.

    private Monster Monsters; // 몬스터 클래스.

    public SpriteRenderer Sprites_Head; // 이미지 상체 스프라이트 렌더러.
    public SpriteRenderer Sprites_Body; // 이미지 상체 스프라이트 렌더러.
    public SpriteRenderer Sprites_Hand; // 이미지 상체 스프라이트 렌더러.
    public SpriteRenderer Sprites_Down; // 이미지 하체 스프라이트 렌더러.

    CircleCollider2D circle; // 원형 콜라이더.

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

    public float Damage; // UI 데미지 값.
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


    //==========점프 관련 변수=============//
    private float JumpPower = 130.0f; // 점프 파워.
    bool isjump; // 점프 트리거.
    public AudioClip jumpedSeClip; // 점프 사운드.
    AudioSource jumpedSeAudio;


    private void Awake()
    {
        Camera = GameObject.FindWithTag("MainCamera").GetComponent<CameraCRTL>();

        //================================================================//
        Sec_01_minXAndY.x = Camera.Sec_01_minXAndY.x - 12f; // 섹터 1의 이동가능한 X와 Y값의 최소값.
        Sec_01_minXAndY.y = 0; // 섹터 1의 이동가능한 X와 Y값의 최소값.

        Sec_01_maxXAndY.x = Camera.Sec_01_maxXAndY.x + 12f; // 섹터 1의 이동가능한 X와 Y값의 최대값.
        Sec_01_maxXAndY.y = 0; // 섹터 1의 이동가능한 X와 Y값의 최대값.

        //-------------------------
        Sec_02_minXAndY.x = Camera.Sec_02_minXAndY.x - 12f; // 섹터 2의 이동가능한 X와 Y값의 최소값.
        Sec_02_minXAndY.y = 0; // 섹터 2의 이동가능한 X와 Y값의 최소값.

        Sec_02_maxXAndY.x = Camera.Sec_02_maxXAndY.x + 12f; // 섹터 2의 이동가능한 X와 Y값의 최대값.
        Sec_02_maxXAndY.y = 0; // 섹터 2의 이동가능한 X와 Y값의 최대값.

        //-------------------------
        Sec_03_minXAndY.x = Camera.Sec_03_minXAndY.x - 12f; // 섹터 3의 이동가능한 X와 Y값의 최소값.
        Sec_03_minXAndY.y = 0; // 섹터 3의 이동가능한 X와 Y값의 최소값.

        Sec_03_maxXAndY.x = Camera.Sec_03_maxXAndY.x + 12f; // 섹터 3의 이동가능한 X와 Y값의 최대값.
        Sec_03_maxXAndY.y = 0; // 섹터 3의 이동가능한 X와 Y값의 최대값.

        //-------------------------
        Boss_minXAndY.x = Camera.Boss_minXAndY.x - 12f; // 보스 섹터의 이동가능한 X와 Y값의 최소값.
        Boss_minXAndY.y = 0; // 보스 섹터의 이동가능한 X와 Y값의 최소값.

        Boss_maxXAndY.x = Camera.Boss_maxXAndY.x + 12f; // 보스 섹터의 이동가능한 X와 Y값의 최대값.
        Boss_maxXAndY.y = 0; // 보스 섹터의 이동가능한 X와 Y값의 최대값.

        //-------------------------
        Boss_minXAndY.x = Camera.Boss_minXAndY.x - 12f; // 섹터 1의 이동가능한 X와 Y값의 최소값.
        Boss_minXAndY.y = 0; // 섹터 1의 이동가능한 X와 Y값의 최소값.

        Boss_maxXAndY.x = Camera.Boss_maxXAndY.x + 12f; // 섹터 1의 이동가능한 X와 Y값의 최대값.
        Boss_maxXAndY.y = 0; // 섹터 1의 이동가능한 X와 Y값의 최대값.

        //-------------------------
        total_minXAndY.x = Camera.total_minXAndY.x - 12f; // 섹터 1의 이동가능한 X와 Y값의 최소값.
        total_minXAndY.y = 0; // 섹터 1의 이동가능한 X와 Y값의 최소값.

        total_maxXAndY.x = Camera.total_maxXAndY.x + 12f; // 섹터 1의 이동가능한 X와 Y값의 최대값.
        total_maxXAndY.y = 0; // 섹터 1의 이동가능한 X와 Y값의 최대값.
    }
    // Use this for initialization
    void Start() {

        rigid = gameObject.GetComponent<Rigidbody2D>(); // 중력 컴포넌트 초기화.

        fireCtrl = GameObject.FindWithTag("Player").GetComponent<FireCtrl>(); // 발사 컨트롤 스크립트 초기화.

        Head = GameObject.FindWithTag("Player_Head").GetComponent<PlayerAnimation_Up>(); // 상체 스프라이트 렌더 초기화.
        Body = GameObject.FindWithTag("Player_Body").GetComponent<PlayerAnimation_Up>(); // 상체 스프라이트 렌더 초기화.
        Hand = GameObject.FindWithTag("Player_Hand").GetComponent<PlayerAnimation_Up>(); // 상체 스프라이트 렌더 초기화.
        Player_Down = GameObject.FindWithTag("Player_Down").GetComponent<PlayerAnimation_Bottom>(); // 하체 스프라이트 렌더 초기화.

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

        jumpedSeAudio = gameObject.AddComponent<AudioSource>(); // 점프 사운드 컴포넌트 초기화.
        jumpedSeAudio.clip = jumpedSeClip;// 점프 사운드 클립 초기화.
        jumpedSeAudio.loop = false; // 점프 사운드 루프 = false;

        JoyStick = GameObject.FindWithTag("JoyStick").GetComponent<RectTransform>();

        //================================================================//

        hpmaskRect = hpmask.GetComponent<RectTransform>(); // 체력바 스프라이트 초기화.
        maxHpBarWidth = hpmaskRect.sizeDelta.x; // 체력바 스프라이트 최대 가로 길이 초기화.
        HP = maxHP; // 최대 체력 = maxHP 변수의 값.
        hearthp = maxheart; // 하트의 값 = 최대 하트의 값.
    }

    // Update is called once per frame
    void FixedUpdate() {
        Skill();
        Move();
        DIE();
        Jump();
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
            Player_L();
        }
        else if (CrossPlatformInputManager.GetAxisRaw("Horizontal") > 0 && JoyStick.position.y < 175 && JoyStick.position.y > 60)
        {
            Player_R();
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





    void Jump() // 점프 함수.
    {
        if (Input.GetKey(KeyCode.LeftAlt) && !isjump)
        {
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
        ActiveTime = 2.0f;

        Head.animator.SetBool("Dash", true); // 렌더 변경.
        Body.animator.SetBool("Dash", true); // 렌더 변경.
        Hand.animator.SetBool("Dash", true); // 렌더 변경.
        Player_Down.animator.SetBool("Dash", true); // 렌더 변경.

        rigid.AddForce(new Vector2(ImageVec, 0) * 40f, ForceMode2D.Impulse);

        Invoke("invoke_A", 2.0f);
    }
    void invoke_A()
    {
        Head.animator.SetBool("Dash", false); // 렌더 변경.
        Body.animator.SetBool("Dash", false); // 렌더 변경.
        Hand.animator.SetBool("Dash", false); // 렌더 변경.
        Player_Down.animator.SetBool("Dash", false); // 렌더 변경.
    }






    private void Skill_B() // 방어 스킬.
    {
        Head.animator.SetBool("Skill_B", true); // 렌더 변경.
        Body.animator.SetBool("Skill_B", true); // 렌더 변경.
        Hand.animator.SetBool("Skill_B", true); // 렌더 변경.
        Player_Down.animator.SetBool("Skill_B", true); // 렌더 변경.

        Instantiate(Barrior, gameObject.transform.position + new Vector3(0,1.2f), gameObject.transform.rotation);
    }
    

    public void Skill_C() // 이단 점프.
    {
        isjump = true;

        float SkillJump = JumpPower + 50.0f;

        Debug.Log("점프시작");
        jumpedSeAudio.Play();
        rigid.AddForce(Vector3.up * SkillJump, ForceMode2D.Impulse);
    }

    private void Skill_D() // 홀로그램 (미정)
    {

    }





    public void DIE() // 플레이어 사망 함수.
    {
        if (HP <= 0)
        {
            Destroy(circle);

            SPEED = 0;
            Time.timeScale = 0.5f;

            SceneTime = SceneTime + Time.deltaTime;
            if (SceneTime > 1.2f)
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("Stage_1");
            }
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
            
            rigid.AddForce(new Vector2(ImageVec,0) * -1 * 35f, ForceMode2D.Impulse);

            Head.animator.SetBool("attacked", true); // 렌더 변경.
            Body.animator.SetBool("attacked", true); // 렌더 변경.
            Hand.animator.SetBool("attacked", true); // 렌더 변경.
            Player_Down.animator.SetBool("attacked", true); // 렌더 변경.

            Invoke("Hit_Invoke", 0.5f);
        }

    }
    void Hit_Invoke()
    {
        Head.animator.SetBool("attacked", false); // 렌더 변경.
        Body.animator.SetBool("attacked", false); // 렌더 변경.
        Hand.animator.SetBool("attacked", false); // 렌더 변경.
        Player_Down.animator.SetBool("attacked", false); // 렌더 변경.
    }





    IEnumerator HitEffect() // 피격 이펙트 코루틴.
    {
        float CountTime = 0; // 카운트 경과 시간.

        while (CountTime < 0.04f) // 카운트 경과 시간이 0.04f 미만 일때 까지 반복. 
        {

            if (CountTime % 0.04f == 0) // ' 카운트 경과 시간 % 0.04f ' 를 했을때 나머지가 0 인 경우.
            {
                Sprites_Head.color = new Color32(255, 90, 0, 255); // 빨간 음영을 추가.
                Sprites_Body.color = new Color32(255, 90, 0, 255); // 빨간 음영을 추가.
                Sprites_Hand.color = new Color32(255, 90, 0, 255); // 빨간 음영을 추가.
                Sprites_Down.color = new Color32(255, 90, 0, 255); // 빨간 음영을 추가.
            }

            yield return new WaitForSeconds(0.2f); // '0.2f'의 딜레이를 줌.

            CountTime = CountTime + 0.04f; // 카운트 경과 시간 = 경과시간 + 0.04f(매 프레임 마다 증가).

        }

        Sprites_Head.color = new Color32(255, 255, 255, 255); 
        Sprites_Body.color = new Color32(255, 255, 255, 255); 
        Sprites_Hand.color = new Color32(255, 255, 255, 255); 
        Sprites_Down.color = new Color32(255, 255, 255, 255); // 스프라이트 컬러 모두 출력.

        yield return null; // 종료.

    }
}
