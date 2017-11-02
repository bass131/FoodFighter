using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerCRTL : MonoBehaviour
{

    public float speed = 10.0f;
    public float Jump_Power = 5.0f;

    public int ImageVec = 1;

    public float Hp = 5;

    private float SceneTime = 0f;

    private float StickRate = 4.5f;
    private float cornRate = 5.0f;
    private float jellyRate = 4.5f;
    private float IceRate = 8.0f;
    private float GiantRate = 10.0f;
    private float NextAttack = 0.0f;

    private float attackedDelay = 8.0f;

    public Rigidbody2D rigid;
    public Vector3 attackedVelocity;

    private FireCtrl fireCtrl;
    private PlayerAnimation_Up Player_Up;
    private PlayerAnimation_Bottom Player_Down;

    public SpriteRenderer Sprites_Up; // 이미지 스프라이트 변수 선언.
    public SpriteRenderer Sprites_Down;

    CircleCollider2D circle;

    public bool isEnd = false;
    bool isAttacked = false;

    public AudioClip attackedSeClip;
    AudioSource attackedSeAudio;

    public AudioClip itemSeClip;
    AudioSource itemSeAudio;

    private Score Scores;
    //=================================================================================
    public Image hpmask;
    public Image[] heartmask = new Image[3];
    private RectTransform hpmaskRect;
    public float damge;
    public float maxHP;
    public float maxheart;
    public float hearthp;
    private float maxHpBarWidth;
    private float maxheartbarwidth;
    Vector3 moveVelocity = Vector3.zero;
    // Use this for initialization
    void Start()
    {

        rigid = gameObject.GetComponent<Rigidbody2D>();
        fireCtrl = GameObject.FindWithTag("Player").GetComponent<FireCtrl>();
        Player_Up = GameObject.FindWithTag("Player_Up").GetComponent<PlayerAnimation_Up>();
        Player_Down = GameObject.FindWithTag("Player_Down").GetComponent<PlayerAnimation_Bottom>();
        circle = gameObject.GetComponent<CircleCollider2D>();

        Sprites_Down = GameObject.FindWithTag("Player_Down").GetComponent<SpriteRenderer>();
        Sprites_Up = GameObject.FindWithTag("Player_Up").GetComponent<SpriteRenderer>();

        attackedSeAudio = gameObject.AddComponent<AudioSource>();
        attackedSeAudio.clip = attackedSeClip;
        attackedSeAudio.loop = false;

        itemSeAudio = gameObject.AddComponent<AudioSource>();
        itemSeAudio.clip = itemSeClip;
        itemSeAudio.loop = false;

        //============================================================================
        hpmaskRect = hpmask.GetComponent<RectTransform>();
        maxHpBarWidth = hpmaskRect.sizeDelta.x;
        Hp = maxHP;
        hearthp = maxheart;
    }
    //==========================좌우 함수==========================
    public void Player_R() // 오른쪽 버튼
    {
        moveVelocity = Vector3.right;
        ImageVec = 1;
        attackedVelocity = new Vector3(-1, 1.5f, 0);
        transform.localScale = new Vector3(ImageVec, 1, 1);
    }

    public void Player_L() // 왼쪽 버튼.
    {
        moveVelocity = Vector3.left;
        ImageVec = -1;
        attackedVelocity = new Vector3(1, 1.5f, 0);
        transform.localScale = new Vector3(ImageVec, 1, 1);
    }
    //==========================좌우 함수==========================
    public void Player_Move() // 플레이어 이동 통합.
    {
        moveVelocity = Vector3.zero;
        if (Hp == 0)
        {
            moveVelocity = Vector3.zero;
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            Player_L();
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            Player_R();
        }

        transform.position += moveVelocity * speed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        Player_Move();

        Player_Die();

        gameESC();
    }

    private void itai() // 맞을때마다 체력바 줄어듬
    {
        float deltaSize = Hp / maxHP;
        hpmaskRect.sizeDelta = new Vector2(maxHpBarWidth * deltaSize, hpmaskRect.sizeDelta.y);
    }

    /*void OnTriggerStay2D(Collider2D col)
    { 
        if (col.gameObject.tag == "ATK")
        {
            --Hp;
            rigid.AddForce(attackedVelocity * 50, ForceMode2D.Impulse);
            attackedSeAudio.Play();
            isAttacked = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "ATK")
        {
            isAttacked = false;
        }
    }

    */
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Attack_Stick")
        {
            NextAttack = 0;
        }
        else if (other.gameObject.tag == "Attack_Corn")
        {
            NextAttack = 0;
        }
        else if (other.gameObject.tag == "Attack_Jelly")
        {
            NextAttack = 0;
        }
        else if (other.gameObject.tag == "EndGame")
        {
            SceneManager.LoadScene("TItle");
        }
        else if (other.gameObject.tag == "Item")
        {
            itemSeAudio.Play();
            Destroy(other.gameObject);
            Scores.itemScore += 100;
        }
    }



    void OnTriggerStay2D(Collider2D other)
    {
        NextAttack += Time.deltaTime;

        if (other.gameObject.tag == "Attack_Stick")
        {
            speed = 0;
            if (NextAttack > StickRate && NextAttack > attackedDelay)
            {
                --Hp;
                itai();
                attackedSeAudio.Play();
                rigid.AddForce(attackedVelocity * 50, ForceMode2D.Impulse);
                Player_Up.animator.SetBool("attacked", true);
                Player_Down.animator.SetBool("attacked", true);
                Debug.Log("체력 -");
                NextAttack = 0;
              //  StartCoroutine("HitEffect"); // 히트 이펙트 코루틴 시작.
            }
            else
            {
                Player_Up.animator.SetBool("attacked", false);
                Player_Down.animator.SetBool("attacked", false);
               // StopCoroutine("HitEffect"); // 히트 이펙트 코루틴 시작.
            }

        }
        else if (other.gameObject.tag == "Attack_Corn")
        {
            speed = 0;
            if (NextAttack > cornRate && NextAttack > attackedDelay)
            {
                --Hp;
                itai();
                Player_Up.animator.SetBool("attacked", true);
                Player_Down.animator.SetBool("attacked", true);
                attackedSeAudio.Play();
                rigid.AddForce(attackedVelocity * 50, ForceMode2D.Impulse);
                Debug.Log("체력 -");
                NextAttack = 0;
                //StartCoroutine("HitEffect"); // 히트 이펙트 코루틴 시작.
            }
            else
            {
                Player_Up.animator.SetBool("attacked", false);
                Player_Down.animator.SetBool("attacked", false);
               // StopCoroutine("HitEffect"); // 히트 이펙트 코루틴 시작.
            }
        }

        else if (other.gameObject.tag == "Attack_Jelly")
        {
            speed = 0;
            if (NextAttack > jellyRate && NextAttack > attackedDelay)
            {
                --Hp;
                itai();
                attackedSeAudio.Play();
                rigid.AddForce(attackedVelocity * 50, ForceMode2D.Impulse);
                Player_Up.animator.SetBool("attacked", true);
                Player_Down.animator.SetBool("attacked", true);
                Debug.Log("체력 -");
                NextAttack = 0;
                //StartCoroutine("HitEffect"); // 히트 이펙트 코루틴 시작.
            }
            else
            {
                Player_Up.animator.SetBool("attacked", false);
                Player_Down.animator.SetBool("attacked", false);
                //StopCoroutine("HitEffect"); // 히트 이펙트 코루틴 시작.
            }
        }
        else if (other.gameObject.tag == "Attack_Ice")
        {
            speed = 0;
            if (NextAttack > IceRate && NextAttack > attackedDelay)
            {
                --Hp;
                itai();
                attackedSeAudio.Play();
                rigid.AddForce(attackedVelocity * 50, ForceMode2D.Impulse);
                Player_Up.animator.SetBool("attacked", true);
                Player_Down.animator.SetBool("attacked", true);
                Debug.Log("체력 -");
                NextAttack = 0;
                //StartCoroutine("HitEffect"); // 히트 이펙트 코루틴 시작.
            }
            else
            {
                Player_Up.animator.SetBool("attacked", false);
                Player_Down.animator.SetBool("attacked", false);
                //StopCoroutine("HitEffect"); // 히트 이펙트 코루틴 시작.
            }
        }
        else if (other.gameObject.tag == "Attack_Giant")
        {
            speed = 0;
            if (NextAttack > GiantRate && NextAttack > attackedDelay)
            {
                --Hp;
                itai();
                attackedSeAudio.Play();
                rigid.AddForce(attackedVelocity * 50, ForceMode2D.Impulse);
                Player_Up.animator.SetBool("attacked", true);
                Player_Down.animator.SetBool("attacked", true);
                Debug.Log("체력 -");
                NextAttack = 0;
                //StartCoroutine("HitEffect"); // 히트 이펙트 코루틴 시작.
            }
            else
            {
                Player_Up.animator.SetBool("attacked", false);
                Player_Down.animator.SetBool("attacked", false);
                //StopCoroutine("HitEffect"); // 히트 이펙트 코루틴 시작.
            }
        }
        else
        {
            speed = 5;
        }
    }

    void Player_Die()
    {
        int a = 2;
        if (Hp <= 0)
        {
            Destroy(circle);

            speed = 0;
            Time.timeScale = 0.5f;


            SceneTime = SceneTime + Time.deltaTime;
            if (SceneTime > 1.2f)
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("Stage");
            }
        }
    }



    void gameESC()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    IEnumerator HitEffect() // 피격 이펙트 코루틴.
    {
        float CountTime = 0; // 카운트 경과 시간.

        while (CountTime < 0.04f) // 카운트 경과 시간이 0.04f 미만 일때 까지 반복. 
        {
            if (CountTime % 0.04f == 0) // ' 카운트 경과 시간 % 0.04f ' 를 했을때 나머지가 0 인 경우.
            {
                Sprites_Up.color = new Color32(255, 90, 0, 255); // 빨간 음영을 추가.
                Sprites_Down.color = new Color32(255, 90, 0, 255); // 빨간 음영을 추가.
            }

            yield return new WaitForSeconds(0.2f); // '0.2f'의 딜레이를 줌.

            CountTime = CountTime + 0.04f; // 카운트 경과 시간 = 경과시간 + 0.04f(매 프레임 마다 증가).

        }

        Sprites_Up.color = new Color32(255, 255, 255, 255); // 스프라이트 컬러 모두 출력.
        Sprites_Down.color = new Color32(255, 255, 255, 255); // 스프라이트 컬러 모두 출력.


        yield return null; // 종료.

    }
}


// 플레이어 이동 관련 스크립트 입니다.