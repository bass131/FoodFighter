using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class Hand_Rotation : MonoBehaviour {

    public Player Player; // 플레이어.
    
    private Quaternion None_P; // 각도 플러스
    private Quaternion None_M; // 각도 마이너스
    private Quaternion z_raw_1; // 각도 25도.
    private Quaternion z_raw_2; // 각도 50도.
    private Quaternion z_raw_3; // 각도 -25도.
    private Quaternion z_raw_4; // 각도 -50도.

    private RectTransform JoyStick; // 조이스틱 스프라이트 트랜스폼.

    // Use this for initialization
    void Start () {
        Player = GameObject.FindWithTag("Player").GetComponent<Player>();
        JoyStick = GameObject.FindWithTag("JoyStick").GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        H_Rotate();
	}
    // 하단 :86 , 60 , 상단 : 150 . 190
    void H_Rotate()
    {
        None_P.eulerAngles = new Vector3(0, 0, 0);
        z_raw_1.eulerAngles = new Vector3(0, 0, 32);
        z_raw_2.eulerAngles = new Vector3(0, 0, -32);

        if (Player.ImageVec == 1)
        { 
            if (CrossPlatformInputManager.GetAxisRaw("Vertical") > 0 && JoyStick.position.y > 160 )
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, z_raw_1, Time.deltaTime * 5.0f);
            }
            else if(CrossPlatformInputManager.GetAxisRaw("Vertical") < 0 && JoyStick.position.y < 76 )
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, z_raw_2, Time.deltaTime * 5.0f);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, None_P, Time.deltaTime * 5.0f);
            }
          

        }
        else if(Player.ImageVec == -1)
        {
            if (CrossPlatformInputManager.GetAxisRaw("Vertical") > 0  && JoyStick.position.y > 160)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, z_raw_2, Time.deltaTime * 5.0f);
            }
            else if (CrossPlatformInputManager.GetAxisRaw("Vertical") < 0 && JoyStick.position.y < 76)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, z_raw_1, Time.deltaTime * 5.0f);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, None_P, Time.deltaTime * 5.0f);
            }
        }
    }
}


/*if (CrossPlatformInputManager.GetAxisRaw("Vertical") < 0 && JoyStick.position.y< 70)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, z_raw_4, Time.deltaTime* 5.0f);
            }
            else if (CrossPlatformInputManager.GetAxisRaw("Vertical") > 0 && JoyStick.position.y< 180)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, z_raw_2, Time.deltaTime* 5.0f);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, None_P, Time.deltaTime* 5.0f);
            }


      if (CrossPlatformInputManager.GetAxisRaw("Vertical") < 0 && JoyStick.position.z< 180)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, z_raw_2, Time.deltaTime* 5.0f);
            }
            else if (CrossPlatformInputManager.GetAxisRaw("Vertical") > 0 && JoyStick.position.z< 70)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, z_raw_4, Time.deltaTime* 5.0f);
            }
            else 
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, None_P, Time.deltaTime* 5.0f);
            }
            */