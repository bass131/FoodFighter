using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour {

    public GameObject item_0; // 아이템 오브젝트 변수 선언 0.
    public GameObject item_1; // 아이템 오브젝트 변수 선언 1.
    public GameObject item_2; // 아이템 오브젝트 변수 선언 2.
    public GameObject item_3; // 아이템 오브젝트 변수 선언 3.
    public GameObject item_4; // 아이템 오브젝트 변수 선언 4. 
    public GameObject item_5; // 아이템 오브젝트 변수 선언 5.

    private MonsterCTRL monsters; // 몬스터 컨트롤 스크립트 포함.
    private IceMonsterCTRL Ice_mon; // 아이스크림 몬스터 스크립트 포함.

    int itemDropFlag = 0; // 아이템 드랍 플래그 변수 선언.


    // Use this for initialization
    void Start () { // 오브젝트 초기화 선언.
        monsters = GameObject.FindWithTag("Enemy").GetComponent<MonsterCTRL>();
        //Ice_mon = mother_2.GetComponent<IceMonsterCTRL>(); // 아이스크림 몬스터 오브젝트 선언.
    }
	
	// Update is called once per frame
	void Update () { // 반복형 함수

        /*itemDropFlag = Random.Range(0, 5); // 아이템 드랍 랜덤 플래그.

        if (monsters.HP == 0) // 몬스터의 체력이나 아이스크림 몬스터의 체력이 0일 경우.
        {
            if (itemDropFlag == 0) // 아이템 플래그가 0 일때.
            {
                Instantiate(item_0, transform.position, transform.rotation); // 0번째 아이템 생성.
            }
            else if (itemDropFlag == 1) // 아이템 플래그가 1 일때.
            {
                Instantiate(item_1, transform.position, transform.rotation); // 1번째 아이템 생성.
            }
            else if (itemDropFlag == 2) // 아이템 플래그가 2 일때 .
            {
                Instantiate(item_2, transform.position, transform.rotation); // 2번째 아이템 생성.
            }
            else if (itemDropFlag == 3) // 아이템 플래그가 3 일때.
            {
                Instantiate(item_3, transform.position, transform.rotation); // 3번째 아이템 생성.
            }
            else if (itemDropFlag == 4) // 아이템 플래그가 4 일때.
            {
                Instantiate(item_4, transform.position, transform.rotation); // 4번째 아이템 생성.
            }
            else if (itemDropFlag == 5) // 아이템 플래그가 5 일때.
            {
                Instantiate(item_5, transform.position, transform.rotation); // 5번째 아이템 생성.
            }
        }*/


        Destroy(gameObject, 0.01f); // 아이템 드랍 매개체 제거.
    }

}
