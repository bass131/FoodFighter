using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Go_sc : MonoBehaviour {

    public Animator anim;

    bool isPlay;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(isPlay)
        {
            anim.SetBool("isPlaying", true);
        }
        else
        {
            anim.SetBool("isPlaying", false);
        }
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlay = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlay = false;
        }
    }
}
