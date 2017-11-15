using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour{

    public GameObject item_0; // 아이템 오브젝트 변수 선언 0.
    public GameObject item_1; // 아이템 오브젝트 변수 선언 1.
    public GameObject item_2; // 아이템 오브젝트 변수 선언 2.
    public GameObject item_3; // 아이템 오브젝트 변수 선언 3.
    public GameObject item_4; // 아이템 오브젝트 변수 선언 4. 
    public GameObject item_5; // 아이템 오브젝트 변수 선언 5.

    int itemDropFlag = 0; // 아이템 드랍 플래그 변수 선언.

    float DropTime = 0;

    int Hp;


    // Use this for initialization
    void Start () {
        Hp = gameObject.GetComponent<Monster>().HP;

    }
	
	// Update is called once per frame
	void Update () { // 반복형 함수
        Drop();
    }

    void Drop()
    {
        itemDropFlag = Random.Range(0, 5); // 아이템 드랍 랜덤 플래그.

        if (Hp <= 0) // 몬스터의 체력이나 아이스크림 몬스터의 체력이 0일 경우.
        {
            Debug.Log("Drop");

            DropTime = DropTime + Time.deltaTime;

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
        }
    }

}
