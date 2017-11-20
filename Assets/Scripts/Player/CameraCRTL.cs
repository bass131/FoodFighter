using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 섹터 뷰 카메라(전환)
 * 1. 스테이지 매니저랑 연동.
 * 2. 플레이어의 위치와 섹터 클리어 Bool 값을 같이 체크.
 */

public class CameraCRTL : MonoBehaviour {


    private float xMargin = 1f;      // Distance in the x axis the player can move before the camera follows.
    private float yMargin = 1f;      // Distance in the y axis the player can move before the camera follows.

    public float xSmooth = 8f;      // How smoothly the camera catches up with it's target movement in the x axis.
    public float ySmooth = 8f;      // How smoothly the camera catches up with it's target movement in the y axis.

    public Transform player; // 플레이어의 위치.


    public Vector2 Sec_01_minXAndY; // 섹터 1의 X와 Y값의 최소값.
    public Vector2 Sec_01_maxXAndY; // 섹터 1의 X와 Y값의 최대값.

    public Vector2 Sec_02_minXAndY; // 섹터 2의 X와 Y값의 최소값.
    public Vector2 Sec_02_maxXAndY; // 섹터 2의 X와 Y값의 최대값.

    public Vector2 Sec_03_minXAndY; // 섹터 3의 X와 Y값의 최소값.
    public Vector2 Sec_03_maxXAndY; // 섹터 3의 X와 Y값의 최대값.

    public Vector2 Boss_minXAndY; // 보스 섹터의 X와 Y값의 최소값.
    public Vector2 Boss_maxXAndY; // 보스 섹터의 X와 Y값의 최대값.

    public Vector2 total_minXAndY; // 모든 맵의 X와 Y값의 최소값.
    public Vector2 total_maxXAndY; // 모든 맵의 X와 Y값의 최대값.


    // Use this for initialization
    void Awake () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    bool CheckXMargin()
    {
        return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
        //'만약 카메라의 x좌표 와 플레이어의 x좌표의 오차가 1 보다 크면' 이라는 조건을 리턴.
    }


    bool CheckYMargin()
    {
        return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
        //'만약 카메라의 y좌표 와 플레이어의 y좌표의 오차가 1 보다 크면' 이라는 조건을 리턴
    }


    // Update is called once per frame
    void FixedUpdate () {
        SetCamera();
        player = GameObject.FindWithTag("Player").transform;
    }

    void SetCamera()
    {
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        //만약 카메라의 x좌표 와 플레이어의 x좌표의 오차가 1 보다 크면
        if (CheckXMargin())
            // 플레이어에게 xSmooth의 감도만큼 카메라가 따라감.
            targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.deltaTime);

        //만약 카메라의 y좌표 와 플레이어의 y좌표의 오차가 1 보다 크면
        if (CheckYMargin())
            // 플레이어에게 ySmooth의 감도만큼 카메라가 따라감.
            targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.deltaTime);



        if(StageManager.instance.Player_State == StageManager.Stage.Move) 
            //만약 플레이어의 상태가 이동중이라면
        {
            targetX = Mathf.Clamp(targetX, total_minXAndY.x, total_maxXAndY.x); 
            // 카메라의 x 좌표 이동의 제한은 전체 최소 및 최대
            targetY = Mathf.Clamp(targetY, total_minXAndY.y, total_maxXAndY.y);
            // 카메라의 y 좌표 이동의 제한은 전체 최소 및 최대        }
        }

        else if(StageManager.instance.Player_State == StageManager.Stage.Engage)
            // 만약 플레이어의 상태가 교전중이고.
        {
            switch(StageManager.instance.Sec)
                //섹터의 상태를 체크.
            {
                case StageManager.Sector.Sector_1:
                    //첫번째 섹터에 있는 상태라면.
                    targetX = Mathf.Clamp(targetX, Sec_01_minXAndY.x, Sec_01_maxXAndY.x);
                    targetY = Mathf.Clamp(targetY, Sec_01_minXAndY.y, Sec_01_maxXAndY.y);
                    //카메라의 x 좌표 이동의 제한은 섹터 1의 최소 및 최대.
                    break;
                case StageManager.Sector.Sector_2:
                    //두번째 섹터에 있는 상태라면.
                    targetX = Mathf.Clamp(targetX, Sec_02_minXAndY.x, Sec_02_maxXAndY.x);
                    targetY = Mathf.Clamp(targetY, Sec_02_minXAndY.y, Sec_02_maxXAndY.y);
                    //카메라의 x 좌표 이동의 제한은 섹터 2의 최소 및 최대.
                    break;
                case StageManager.Sector.Sector_3:
                    //세번째 섹터에 있는 상태라면.
                    targetX = Mathf.Clamp(targetX, Sec_03_minXAndY.x, Sec_03_maxXAndY.x);
                    targetY = Mathf.Clamp(targetY, Sec_03_minXAndY.y, Sec_03_maxXAndY.y);
                    //카메라의 x 좌표 이동의 제한은 섹터 3의 최소 및 최대.
                    break;
                case StageManager.Sector.Boss:
                    //보스 섹터에 있는 상태라면.
                    targetX = Mathf.Clamp(targetX, Boss_minXAndY.x, Boss_maxXAndY.x);
                    targetY = Mathf.Clamp(targetY, Boss_minXAndY.y, Boss_maxXAndY.y);
                    //카메라의 x 좌표 이동의 제한은 보스 섹터의 최소 및 최대.
                    break;
            }
        }

        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
} 
