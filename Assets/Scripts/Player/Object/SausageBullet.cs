﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SausageBullet : MonoBehaviour {

    public GameObject effect; // 이펙트 오브젝트 선언.

    public float moveSpeed = 10.0f; // 이동 속도 계수 선언.

    public int bulletVelocity = 1; // 총탄의 방향 0으로 우선 초기화.
    //public int effectVelo = 0; // 이펙트의 방향 0으로 우선 초기화.

    //public int direction; // 방향값 초기화.

    //public Transform Player; // 플레이어 클래스의 방향값 선언.

    private void Awake()
    {
        //Player = GameObject.FindWithTag("Player").transform; // 플레이어의 방향값의 참조 선언.
        //Velocitys();
    }
    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
        bullet(); // 총탄 이동 함수.
    }

    void bullet() // 총탄 이동 함수.
    {
        transform.Translate(new Vector3(bulletVelocity, 0, 0) * moveSpeed * Time.deltaTime);
        // 총탄의 방향값 방향으로 총탄속도 만큼 실시간 이동.
        Destroy(gameObject, 70.0f);
    }

   /* void Velocitys() // 방향값 조정 함수.
    {
        Debug.Log("GetVelo");
        if (gameObject.transform.position.x > Player.position.x) // PlayerCTRL 스크립트의 ImageVec 값이 -1 일 경우.
        {
            Debug.Log("-");
            bulletVelocity = -1; // 총탄의 방향값은 -1.
            effectVelo = 5; // 이펙트의 x값은 음수.

        }
        else if (gameObject.transform.position.x < Player.position.x) // PlayerCTRL 스크립트의 ImageVec 값이 1 일 경우.
        {
            Debug.Log("+");
            bulletVelocity = 1; // 총탄의 방향값은 1.
            effectVelo = -5; // 이펙트의 x값은 양수.

        }
    }
    */

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        // 오브젝트의 태그가 'Enemy' or 'Enemy_Ice(아이스크림)' 일 경우.
        {
            Instantiate(effect, transform.position, transform.rotation);
            effect.transform.localScale = new Vector3(7, 7, 1); // 이펙트의 이미지값.
            col.gameObject.GetComponent<Player>().Hit(1);
            Destroy(gameObject);
        }
    }

}