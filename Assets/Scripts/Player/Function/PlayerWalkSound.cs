using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkSound : MonoBehaviour {

    public AudioClip walkSeClip;

    AudioSource walkSeAudio;


   

    // Use this for initialization
    void Start () {
        walkSeAudio = gameObject.AddComponent<AudioSource>();
        walkSeAudio.clip = walkSeClip;
        walkSeAudio.loop = true;

   
    }
	
	// Update is called once per frame
	void Update () {
        WalkSoundON();
       
	}

    void WalkSoundON()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            walkSeAudio.Play();
            walkSeAudio.loop = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            walkSeAudio.Play();
            walkSeAudio.loop = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            walkSeAudio.loop = false;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            walkSeAudio.loop = false;
        }
    }
}

