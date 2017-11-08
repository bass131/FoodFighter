using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_Des : MonoBehaviour {

    private Player Player;

	// Use this for initialization
	void Start () {
        Player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update () {
        Die();
	}

    void Die()
    {
        if (Player.HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
