using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{

    public GameObject Monster_1;
    public GameObject Monster_2;
    public GameObject Monster_3;
    public GameObject Monster_4;

    public Transform SpawnPos;

    public float SpawnRate = 6.5f;
    private float RealTime = 0.0f;
    private int MonsterFlag;

    bool isSpawn = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isSpawn)
        {
            MonsterFlag = Random.Range(1, 5);
            RealTime += Time.deltaTime;
            if (RealTime > SpawnRate)
            {
                RealTime = 0;
                if (MonsterFlag == 1)
                {
                    GameObject MonsterReSpawn = Instantiate(Monster_1, SpawnPos.position, SpawnPos.rotation) as GameObject;
                }
                else if (MonsterFlag == 2)
                {
                    GameObject MonsterReSpawn = Instantiate(Monster_2, SpawnPos.position, SpawnPos.rotation) as GameObject;
                }
                else if (MonsterFlag == 3)
                {
                    GameObject MonsterReSpawn = Instantiate(Monster_3, SpawnPos.position, SpawnPos.rotation) as GameObject;
                }
                else if (MonsterFlag == 4)
                {
                    GameObject MonsterReSpawn = Instantiate(Monster_4, SpawnPos.position, SpawnPos.rotation) as GameObject;
                }
            }
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            isSpawn = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            isSpawn = false;
        }
    }
}