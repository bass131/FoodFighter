using UnityEngine;
using System.Collections;

/*
GameObject prefab = Resources.Load("Prefabs/Bullet") as GameObject;
// Resources/Prefabs/Bullet.prefab 로드
GameObject bullet = MonoBehaviour.Instantiate(prefab) as GameObject;
// 실제 인스턴스 생성. GameObject name의 기본값은 Bullet (clone)
bullet.name = "bullet"; // name을 변경
bullet.transform.parent = player.transform;
// bullet을 player에 입양하는등 초기화작업 수행
*/

public class FireCtrl : MonoBehaviour
{
	public AudioClip attackSeClip;
	AudioSource attackSeAudio;

	public GameObject Bullets; // 총탄 오브젝트 선언.
	public Transform firePos; // 발사 위치 선언.
	private float fireTime; // 실수형 발사 시간 선언.
	private float fireRate = 0.2f;
	private float NextFire = 0.0f;

    GameObject Bullet_01;
    /*
    GameObject Bullet_02 = Resources.Load("Prefabs/Bullet/Bullet") as GameObject;
    GameObject Bullet_03 = Resources.Load("Prefabs/Bullet/Bullet") as GameObject;
    */

    private int bullet_Type = 0; // 0 = 일반 , 1 = 1번째 아이템 , 2 = 2번째 아이템.

	// Use this for initialization
	void Start () {
		fireTime = 0.0f;

        Bullet_01 = Resources.Load("Prefabs/Bullet/Bullet") as GameObject;
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

            GetObject(bullet_Type);
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

    void GetObject(int _type) // 총탄의 타입을 받아오는 함수.
    {
        if (_type == 1)
        {
            Bullets = Bullet_01;
        }
        /*else if(_type == 2)
        {
            Bullets = Bullet_02;
        }*/
    }
}
	
// 발사체 관련 스크립트 입니다.