using UnityEngine;
using System.Collections;

public class FireCtrl : MonoBehaviour
{
	public AudioClip attackSeClip;
	AudioSource attackSeAudio;

	public GameObject Bullets; // 총탄 오브젝트 선언.
	public Transform firePos; // 발사 위치 선언.
	private float fireTime; // 실수형 발사 시간 선언.
	private float fireRate = 0.2f;
	private float NextFire = 0.0f;

	// Use this for initialization
	void Start () {
		fireTime = 0.0f;

		attackSeAudio = gameObject.AddComponent<AudioSource> ();
		attackSeAudio.clip = attackSeClip;
		attackSeAudio.loop = false;
    }

	// Update is called once per frame
	void Update () {
		shootbullet();
	}

	void shootbullet()
	{
		if (Input.GetKey (KeyCode.LeftControl) && Time.time > NextFire) 
		{ // 좌 컨트롤을 누르면.
			
			NextFire = Time.time + fireRate;

			MakeBullet ();
            attackSeAudio.Play ();
		}
	}

	void MakeBullet()
	{
		GameObject instance = Instantiate (Bullets, firePos.position, firePos.rotation) as GameObject; 
		// 총탄 오브젝트가 위치에 생성됨


		Destroy (instance, 0.4f); // 거리값이 2.0 f 이면 사라짐.
	}
}
	
// 발사체 관련 스크립트 입니다.