using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect_Anim : MonoBehaviour { // 총탄 화염 이펙트 스크립트.

    public Animator animator; // 애니메이터 선언.

    // Use this for initialization
    void Start () { // 한번 실행되는 함수.

        animator = GetComponent<Animator>(); // 애니메이터 참조 초기화.

    }
	
	// Update is called once per frame
	void Update () { // 반복 실행되는 함수.

        fire_effect(); // 총탄 이펙트 생성 함수.
		
	}

    void fire_effect()
    {
        if(Input.GetKey (KeyCode.LeftControl)) // 왼쪽 컨트롤을 눌렀을때.
        {
            animator.SetBool("isGun", true); // 발사 애니메이션 시작.
        }
        else // 아닐경우.
        {
            animator.SetBool("isGun", false); // 발사 애니메이션 중단.
        }
    }
}
