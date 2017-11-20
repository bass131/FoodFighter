using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 스테이지 매니저.
/*
 * 기능 : 캐릭터가 스테이지를 플레이 중인지 or 스테이지를 클리어 했는지 체크 유무. 
 */

public class StageManager : MonoBehaviour {

    public static StageManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private CameraCRTL Camera; // 카메라.
    private Sector_Image Sector_UI; // 섹터 진행도 UI;

    private GameObject Player; // 플레이어.

    public Transform trPlayer; // 플레이어의 좌표.

    public Transform Cheak_Point_1; // 체크 포인트 1.
    public Transform Cheak_Point_2; // 체크 포인트 2.
    public Transform Cheak_Point_3; // 체크 포인트 3.

    public Transform Boss_Pos; // 보스몹 소환 포지션.
    public GameObject Boss; // 보스몹.

    public int DeathCount = 14; // 사살 카운트.

    bool tSector_1 = false; // 섹터1 클리어 트리거.
    bool tSector_2 = false; // 섹터2 클리어 트리거.
    bool tSector_3 = false; // 섹터3 클리어 트리거.

    bool setBoss = false; // 보스 등장 트리거.

    public bool Stage_All_Clear = false; // 스테이지 올 클리어 트리거.

    public enum Stage
    {
        Move,
        Engage,
    };

    public Stage Player_State = Stage.Engage;

    public enum Sector {Sector_1,Sector_2,Sector_3,Boss};
    public Sector Sec = Sector.Sector_1;

    // Use this for initialization
    void Start () {
        Player = GameObject.FindWithTag("Player");
        Camera = GameObject.FindWithTag("MainCamera").GetComponent<CameraCRTL>();

        Sector_UI = GameObject.FindWithTag("Sector").GetComponent<Sector_Image>();

        trPlayer = GameObject.FindWithTag("Player").transform;

        Cheak_Point_1.transform.position = (Camera.Sec_02_minXAndY + Camera.Sec_02_maxXAndY) / 2;
        // 체크포인트 1의 위치 = 섹터2의 최소 최대 제한점의 중간.
        Cheak_Point_2.transform.position = (Camera.Sec_03_minXAndY + Camera.Sec_03_maxXAndY) / 2;
        // 체크포인트 2의 위치 = 섹터3의 최소 최대 제한점의 중간.
        Cheak_Point_3.transform.position = (Camera.Boss_minXAndY + Camera.Boss_maxXAndY) / 2;
        // 체크포인트 3의 위치 = 보스 섹터의 최소 최대 제한점의 중간.
    }

    private void Update()
    {
        SectorCHK();
        isStage_Clear();
        Arrive_Cheak();
        is_All_Clear();
    }


    void SectorCHK() // 구역 클리어 체크.
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

    void isStage_Clear() // 모든 섹터가 클리어 된다면.
    {
        Invoke("Boss_Appear", 4.0f); // 4초후에 보스 등장.
    }

    void Boss_Appear() // 보스 등장 함수.
    {
        if (tSector_1 && tSector_2 && tSector_3 && setBoss == false)
        {
            Sector_UI.Boss_Sector();
            Instantiate(Boss,Boss_Pos);
            setBoss = true;
        }
    }


    public void Clear_Cheak(int Count) // 섹터의 몬스터 제거 수 충족 체크.
    {
        DeathCount += Count; // 사망 카운트 연산.

        if (DeathCount >= 15) // 만약 몬스터를 15마리 이상 잡았다면
        {
            Player_State = Stage.Move;
            Sector_UI.Clear();
            DeathCount = 14; // 데스카운트 0으로 다시 초기화.
        }
    }


    public void Arrive_Cheak() // 플레이어 섹터 도달 체크.
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
                        Sector_UI.Sector_2();
                        Sec++; // Sector_1 -> Sector_2.
                        Player_State = Stage.Engage; // 플레이어의 상태는 -> 교전중.
                    }
                    break;

                case Sector.Sector_2:
                    //섹터값이 2이고.
                    if(trPlayer.position.x >= Cheak_Point_2.position.x)
                        //플레이어의 위치값이 체크포인트_2 에 도달 했을경우
                    {
                        Sector_UI.Sector_3();
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
                    }
                    break;
            }
        }
    }

    public void is_All_Clear() // 스테이지 클리어 완수 함수.
    {
        if(Stage_All_Clear == true)
        {
            Sector_UI.All_Clear();
            Time.timeScale = 0.5f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }


}
