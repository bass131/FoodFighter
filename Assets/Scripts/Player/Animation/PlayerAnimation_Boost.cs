using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation_Boost : MonoBehaviour {

    public Animator anim;
    public SpriteRenderer renderers;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        renderers = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Jump_anim();

    }

    void Jump_anim()
    {
        if (Input.GetKey (KeyCode.LeftAlt))
        {
            anim.SetBool("isBoost", true);
        }

        else
        {
            anim.SetBool("isBoost", false);
        }
    }
}
