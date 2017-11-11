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

public class Player :MonoBehaviour, Actors {

    public float SPEED = 10.0f; // 이동속도.
    public float HP = 5; // 체력.

    public int ImageVec = 1; // 이미지의 벡터값.

    private float SceneTime = 0f; // 씬 시간.

    public Rigidbody2D rigid; // 중력 컴포넌트.

    private FireCtrl fireCtrl; // 발사 컨트롤 스크립트.
    private PlayerAnimation_Up Player_Up; // 플레이어 상체.
    private PlayerAnimation_Bottom Player_Down; // 플레이어 하체.

    private Monster Monsters; // 몬스터 클래스.

    public SpriteRenderer Sprites_Up; // 이미지 상체 스프라이트 렌더러.
    public SpriteRenderer Sprites_Down; // 이미지 하체 스프라이트 렌더러.

    CircleCollider2D circle; // 원형 콜라이더.
   
    public bool isEnd = false; // 종료 Bool 트리거.
    bool isAttacked = false; // 피격 Bool 트리거.
     
    public AudioClip attackedSeClip; // 피격 사운드 클립.
    AudioSource attackedSeAudio; // 피격 사운드 오디오.
     
    public AudioClip itemSeClip; // 아이템 획득 사운드 클립.
    AudioSource itemSeAudio; // 아이템 획득 사운드 오디오.

    private Score Scores; // 점수.
    private float Jump_Power = 5.0f; // 점프 파워.

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


    Vector3 MoveVelocity = Vector3.zero; // 이동 방향 벡터 값.


    // Use this for initialization
    void Start () {
        rigid = gameObject.GetComponent<Rigidbody2D>(); // 중력 컴포넌트 초기화.

        fireCtrl = GameObject.FindWithTag("Player").GetComponent<FireCtrl>(); // 발사 컨트롤 스크립트 초기화.

        Player_Up = GameObject.FindWithTag("Player_Up").GetComponent<PlayerAnimation_Up>(); // 상체 스프라이트 렌더 초기화.
        Player_Down = GameObject.FindWithTag("Player_Down").GetComponent<PlayerAnimation_Bottom>(); // 하체 스프라이트 렌더 초기화.

        circle = gameObject.GetComponent<CircleCollider2D>(); // 원형 콜라이터 초기화.

        Sprites_Down = GameObject.FindWithTag("Player_Down").GetComponent<SpriteRenderer>(); // 상체 스프라이트 렌더러 초기화.
        Sprites_Up = GameObject.FindWithTag("Player_Up").GetComponent<SpriteRenderer>(); // 하체 스프라이트 렌더러 초기화.

        attackedSeAudio = gameObject.AddComponent<AudioSource>(); // 피격 사운드 컴포넌트 초기화.
        attackedSeAudio.clip = attackedSeClip; // 피격 사운드 클립 초기화.
        attackedSeAudio.loop = false; // 피격 사운드 루프  = false. 

        itemSeAudio = gameObject.AddComponent<AudioSource>(); // 아이템 획특 사운드 컴포넌트 초기화.
        itemSeAudio.clip = itemSeClip; // 아이템 획득 사운드 클립 초기화.
        itemSeAudio.loop = false; // 아이템 획득 사운드 루프  = false.

        JoyStick = GameObject.FindWithTag("JoyStick").GetComponent<RectTransform>();

        Monsters = GameObject.FindWithTag("Enemy").GetComponent<Monster>();

        //================================================================//

        hpmaskRect = hpmask.GetComponent<RectTransform>(); // 체력바 스프라이트 초기화.
        maxHpBarWidth = hpmaskRect.sizeDelta.x; // 체력바 스프라이트 최대 가로 길이 초기화.
        HP = maxHP; // 최대 체력 = maxHP 변수의 값.
        hearthp = maxheart; // 하트의 값 = 최대 하트의 값.
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Move();
        Skill();
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
            Player_L();
        }
        else if (CrossPlatformInputManager.GetAxisRaw("Horizontal") > 0 && JoyStick.position.y < 175 && JoyStick.position.y > 60)
        {
            Player_R();
        }

        transform.position += MoveVelocity * SPEED * Time.deltaTime;
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









    /*public void ATK() // 플레이어 공격 함수.
    {
        FireCrtl 클래스로 구현 완료.
    }
    */












    public void Skill() // 플레이어 스킬 함수.
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
                SceneManager.LoadScene("Stage");
            }
        }
    }

    public void Hit(int atk)
    {
        Debug.Log("GetDMG");
        HP = HP - atk; // 체력 소모.
        attackedSeAudio.Play(); // 피격 사운드 출력.
        Player_Up.animator.SetBool("attacked", true); // 렌더 변경.
        Player_Down.animator.SetBool("attacked", true); // 렌더 변경.
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("1111111111111 : " + col);
        Debug.Log("2222222222222 : " + col.gameObject.GetComponent<Monster>());
        Monster mon = col.gameObject.GetComponent<Monster>(); // 몬스터의 컴포넌트를 가져옴.

        if (col.gameObject.tag == "Enemy" && mon.isAttack == true)
        {         
        }
        else
        {
            Player_Up.animator.SetBool("attacked", false); // 렌더 변경.
            Player_Down.animator.SetBool("attacked", false); // 렌더 변경.
        }
    }
}
