using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliAnim : MonoBehaviour {

    public Animator anim; // 애니메이터 선언.
    public GameObject Players; // Players 오브젝트 변수 선언.
    public Transform tr; // 위치값 변수 tr 선언.

    public AudioClip HeliSeClip; // 헬리콥터 사운드 클립 변수 선언.
    AudioSource HeliSeAudio; // 헬리콥터 사운드 오디오 소스 변수 선언.

    public bool isPlayer = false; // 'isPlayer' BOOL값 = false 로 우선 초기화.

	// Use this for initialization
	void Start () { // 초기화 선언.

        HeliSeAudio = gameObject.AddComponent<AudioSource>(); // 헬리콥터의 오디오 소스 초기화 선언.
        HeliSeAudio.clip = HeliSeClip; // 헬리콥터 오디어 클립 초기화 선언.
        HeliSeAudio.loop = false; // 헬리콥터 오디오 루프 = false.

        anim = gameObject.GetComponent<Animator>(); // 게임 오브젝트의 애니메이터 초기화.

        HeliSeAudio.Play(); // 헬리콥터 사운드 한번 실행.
        Invoke("Heli_Idle", 1.7f); // "Heli_Idle" 함수를 1.7f의 딜레이 이후에 작동.

    }

    void Heli_Idle() // 헬리콥터 이동 애니메이션 작동 함수.
    {
        anim.SetTrigger("Idle"); // 헬기 애니메이터의 "움직임" 트리거 작동.
    }
}
