using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 스테이지 매니저.
/*
 * 기능 : 캐릭터가 스테이지를 플레이 중인지 or 스테이지를 클리어 했는지 체크 유무. 
 */

public class StageManager : MonoBehaviour {

    public static StageManager instance = null;  //Static instance of GameManager which allows it to be accessed by any other script.

    public AudioSource Music;

    public AudioClip AlertSeClip; // 피격 사운드 클립.
    AudioSource AlertSeAudio; // 피격 사운드 오디오.

    public AudioClip bossSeClip; // 피격 사운드 클립.
    AudioSource bossSeAudio; // 피격 사운드 오디오.

    public AudioClip ClearSeClip;
    AudioSource ClearSeAudio;

    private CameraCRTL Camera; // 카메라.
    private Sector_Image Sector_UI; // 섹터 진행도 UI;

    private GameObject Player; // 플레이어.

    public Transform trPlayer; // 플레이어의 좌표.

    public Transform Cheak_Point_1; // 체크 포인트 1.
    public Transform Cheak_Point_2; // 체크 포인트 2.
    public Transform Cheak_Point_3; // 체크 포인트 3.

    public Transform Boss_Pos; // 보스몹 소환 포지션.
    public GameObject Boss; // 보스몹.

    public int StageNumber;

    public bool gameover = false;

    public int Score; // 점수.

    public int DeathCount = 14; // 사살 카운트 .

    bool tSector_1 = false; // 섹터1 클리어 트리거.
    bool tSector_2 = false; // 섹터2 클리어 트리거.
    bool tSector_3 = false; // 섹터3 클리어 트리거.

    public bool setBoss = false; // 보스 등장 트리거.

    public bool Stage_All_Clear = false; // 스테이지 올 클리어 트리거.
    public bool Clear_Sound = false;

    public enum Stage
    {
        Move,
        Engage,
    };

    public Stage Player_State = Stage.Engage;

    public enum Sector
    {
        Sector_1,
        Sector_2,
        Sector_3,
        Boss
    };

    public Sector Sec = Sector.Sector_1;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);



        Screen.SetResolution(1280, 720, true);

        Music = GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>(); // 메인 카메라의 오디오 소스 컴포넌트.

        Player = GameObject.FindWithTag("Player");
        Camera = GameObject.FindWithTag("MainCamera").GetComponent<CameraCRTL>();

        trPlayer = GameObject.FindWithTag("Player").transform;

        bossSeAudio = gameObject.AddComponent<AudioSource>();
        bossSeAudio.clip = bossSeClip; // 아이템 획득 사운드 클립 초기화.
        bossSeAudio.loop = false; // 아이템 획득 사운드 루프  = false.


        ClearSeAudio = gameObject.AddComponent<AudioSource>();
        ClearSeAudio.clip = ClearSeClip; // 아이템 획득 사운드 클립 초기화.
        ClearSeAudio.loop = false; // 아이템 획득 사운드 루프  = false.


        AlertSeAudio = gameObject.AddComponent<AudioSource>();
        AlertSeAudio.clip = AlertSeClip; // 경고 사운드 클립 초기화.
        AlertSeAudio.loop = false; // 경고 사운드 루프  = false.

        StagePointSet();

    }




    // Use this for initialization
    void Start () {
    }

    private void Update()
    {
        StageSet();
        is_All_Clear();
    }







    void StageSet() // 스테이지 체크 세팅.
    {
        switch(StageNumber)
        {
            case 1:
                Clear1();
                SectorCHK_1();
                Arrive_Cheak_1();
                break;

            case 2:
                Clear2();
                SectorCHK_2();
                Arrive_Cheak_2();
                break;
        }
    }

    void StagePointSet() // 스테이지 체크포인트 세팅.
    {
        switch (StageNumber)
        {
            case 1:
                SetCheckPoint_1();
                break;
            case 2:
                SetCheckPoint_2();
                break;
        }
    }



    void Boss_Appear() // 보스 등장 함수.
    {
        UI_Manager.instance.Caution.SetActive(false);

        Music.clip = bossSeClip;
        Music.loop = true;
        Music.Play();
        Music.volume = 0.8f;

        if (!setBoss)
        {
            setBoss = true;
            Instantiate(Boss, Boss_Pos);
        }
    }









    public void Clear_Cheak(int Count) // 섹터의 몬스터 제거 수 충족 체크.
    {
        if (Player_State == Stage.Engage)
        {
            DeathCount += Count; // 사망 카운트 연산.

            if (DeathCount >= 30) // 만약 몬스터를 15마리 이상 잡았다면
            {
                Player_State = Stage.Move;
                DeathCount = 28; // 데스카운트 0으로 다시 초기화.
            }
        }
    }




#region Stage_1
    void SetCheckPoint_1()
    {
        Cheak_Point_1 = GameObject.FindWithTag("Sector_1").GetComponent<Transform>();
        Cheak_Point_2 = GameObject.FindWithTag("Sector_2").GetComponent<Transform>();
        Cheak_Point_3 = GameObject.FindWithTag("Sector_3").GetComponent<Transform>();

        Cheak_Point_1.transform.position = new Vector2((Camera.Sec_02_minXAndY.x + Camera.Sec_02_maxXAndY.x) / 1.5f, 0);
        // 체크포인트 1의 위치 = 섹터2의 최소 최대 제한점의 중간.
        Cheak_Point_2.transform.position = new Vector2((Camera.Sec_03_minXAndY.x + Camera.Sec_03_maxXAndY.x) / 1.5f, 0);
        // 체크포인트 2의 위치 = 섹터3의 최소 최대 제한점의 중간.
        Cheak_Point_3.transform.position = new Vector2((Camera.Boss_minXAndY.x + Camera.Boss_maxXAndY.x) / 1.5f, 0);
        // 체크포인트 3의 위치 = 보스 섹터의 최소 최대 제한점의 중간.
    }
    void SectorCHK_1() // 구역 클리어 체크.
    {
        switch (Sec)
        {
            case Sector.Sector_1:
                break;
            case Sector.Sector_2:
                tSector_1 = true;
                break;
            case Sector.Sector_3:
                tSector_2 = true;
                break;
            case Sector.Boss:
                tSector_3 = true;
                break;
        }
    }
    void Clear1() // 모든 섹터가 클리어 된다면.
    {
        if (tSector_1 && tSector_2 && tSector_3 && setBoss == false)
        {
            UI_Manager.instance.Caution.SetActive(true);
            Invoke("Boss_Appear", 3.0f); // 4초후에 보스 등장.
        }
    }
    public void Arrive_Cheak_1() // 플레이어 섹터 도달 체크.
    {
        if(Player_State == Stage.Move) // 플레이어가 이동해야하는 상태에서
        {
            switch(Sec)
            {
                case Sector.Sector_1: 
                    //섹터값이 1이고.
                    if(trPlayer.position.x >= Cheak_Point_1.position.x)
                        //플레이어의 위치값이 체크포인트_1에 도달 했을경우.
                    {
                        Sec++; // Sector_1 -> Sector_2.
                        Player_State = Stage.Engage; // 플레이어의 상태는 -> 교전중.
                    }
                    break;

                case Sector.Sector_2:
                    //섹터값이 2이고.
                    if(trPlayer.position.x >= Cheak_Point_2.position.x)
                        //플레이어의 위치값이 체크포인트_2 에 도달 했을경우
                    {
                        Sec++; // Sector_2 -> Sector_3.
                        Player_State = Stage.Engage; // 플레이어의 상태는 -> 교전중.            
                    }
                    break;

                case Sector.Sector_3:
                    //섹터값이 3이고.
                    if (trPlayer.position.x >= Cheak_Point_3.position.x)
                        //플레이어의 위치값이 체크포인트_3에 도달 했을경우.
                    {
                        Sec++; // Sector_3 -> Boss_Sector.
                        Player_State = Stage.Engage; // 플레이어의 상태는 -> 교전중.
                        AlertSeAudio.PlayOneShot(AlertSeClip);
                    }
                    break;
            }
        }
    }
    #endregion
    #region Stage_2
    void SetCheckPoint_2()
    {
        Cheak_Point_1 = GameObject.FindWithTag("Sector_1").GetComponent<Transform>();
        Cheak_Point_2 = GameObject.FindWithTag("Sector_2").GetComponent<Transform>();
    }
    void SectorCHK_2() // 구역 클리어 체크.
    {
        switch (Sec)
        {
            case Sector.Sector_1:
                break;
            case Sector.Sector_2:
                tSector_1 = true;
                break;
            case Sector.Boss:
                //tSector_2 = true;
                break;
        }
    }
    void Clear2() // 모든 섹터가 클리어 된다면.
    {
        if (tSector_1 && tSector_2 && setBoss == false)
        {
            UI_Manager.instance.Caution.SetActive(true);
            Invoke("Boss_Appear", 3.0f); // 4초후에 보스 등장.
        }
    }
    public void Arrive_Cheak_2() // 플레이어 섹터 도달 체크.
    {
        if (Player_State == Stage.Move) // 플레이어가 이동해야하는 상태에서
        {
            switch (Sec)
            {
                case Sector.Sector_1:
                    //섹터값이 1이고.
                    if (trPlayer.position.x >= Cheak_Point_1.position.x)
                    //플레이어의 위치값이 체크포인트_1에 도달 했을경우.
                    {
                        Sec++; // Sector_1 -> Sector_2.
                        Player_State = Stage.Engage; // 플레이어의 상태는 -> 교전중.
                    }
                    break;

                case Sector.Sector_2:
                    //섹터값이 2이고.
                    if (trPlayer.position.x >= Cheak_Point_2.position.x)
                    //플레이어의 위치값이 체크포인트_2 에 도달 했을경우
                    {
                        Sec = Sector.Boss; // Sector_2 -> Boss_Sector.
                        Player_State = Stage.Engage; // 플레이어의 상태는 -> 교전중.
                        //AlertSeAudio.PlayOneShot(AlertSeClip);
                    }
                    break;
            }
        }
    }
    #endregion






    public void is_All_Clear() // 스테이지 클리어 완수 함수.
    {
        if(Stage_All_Clear && !Clear_Sound)
        {
            Clear_Sound = true;

            Music.clip = ClearSeClip;
            Music.Play();
            Music.loop = false;
        }
    }

    public void StageReset()
    {
        setBoss = false; // 보스 등장 트리거.
        Stage_All_Clear = false; // 스테이지 올 클리어 트리거.
        Clear_Sound = false;
        gameover = false;
        Sec = Sector.Sector_1;
        Score = 0;
        tSector_1 = false; // 섹터1 클리어 트리거.
        tSector_2 = false; // 섹터2 클리어 트리거.
        tSector_3 = false; // 섹터3 클리어 트리거.
    }


}
