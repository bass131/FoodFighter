using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Punch : MonoBehaviour {
    public float moveSpeed = 7.0f; // 이동 속도 계수 선언.

    public int PunchVelocity = 0; // 총탄의 방향 0으로 우선 초기화.

    private Transform Player;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Transform>(); // 플레이어의 방향값의 참조 선언.
        Velocitys(); // 방향값을 받는 함수.
    }

    // Update is called once per frame
    void Update()
    {
        bullet(); // 총탄 이동 함수.
    }

    void bullet() // 총탄 이동 함수.
    {
        transform.Translate(new Vector3(PunchVelocity, 0, 0) * moveSpeed * Time.deltaTime);
        // 총탄의 방향값 방향으로 총탄속도 만큼 실시간 이동.
        Destroy(gameObject, 3.0f);
    }

    void Velocitys() // 방향값 조정 함수.
    {
        if (gameObject.transform.position.x > Player.transform.position.x) // PlayerCTRL 스크립트의 ImageVec 값이 -1 일 경우.
        {
            PunchVelocity = -1; // 총탄의 방향값은 -1.

        }
        else if (gameObject.transform.position.x < Player.transform.position.x) // PlayerCTRL 스크립트의 ImageVec 값이 1 일 경우.
        {
            PunchVelocity = 1; // 총탄의 방향값은 1.

        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        // 오브젝트의 태그가 'Player' 일 경우.
        {
            col.gameObject.GetComponent<Player>().Hit(1);
            Destroy(gameObject); // 총탄 삭제.
        }
    }
}
