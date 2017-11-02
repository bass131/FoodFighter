using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_Des : MonoBehaviour {

    private PlayerCRTL Player;

	// Use this for initialization
	void Start () {
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerCRTL>();
    }
	
	// Update is called once per frame
	void Update () {
        Die();
	}

    void Die()
    {
        if (Player.Hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
