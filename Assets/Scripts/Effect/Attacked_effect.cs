using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacked_effect : MonoBehaviour {

    private Bullet bulletVelo;
    private SpriteRenderer SpriteRen;

    // Use this for initialization
    void Start () {
        bulletVelo = GameObject.FindWithTag("Bullet").GetComponent<Bullet>();
        imageFlipX();
        Destroy(gameObject, 2.0f);
    }
	
	// Update is called once per frame
	void Update () {
    }

    void imageFlipX()
    {
        if (bulletVelo.bulletVelocity == -1)
        {
            SpriteRen.flipX = true;
        }
        else if(bulletVelo.bulletVelocity == 1)
        {
            SpriteRen.flipX = false;
        }
    }
}
