using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Go_sc : MonoBehaviour {

    public Animator anim;

    float Real_Time = 0;

    float delay = 7.0f;

    public AudioClip noticeSeClip; // 피격 사운드 클립.
    AudioSource noticeSeAudio; // 피격 사운드 오디오.

    bool isPlay = false;

	// Use this for initialization
	void Awake () {
        noticeSeAudio = gameObject.AddComponent<AudioSource>();
        noticeSeAudio.clip = noticeSeClip; // 아이템 획득 사운드 클립 초기화.
        noticeSeAudio.loop = true; // 아이템 획득 사운드 루프  = false.
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Real_Time += Time.deltaTime;

        if (StageManager.instance.Player_State == StageManager.Stage.Move && Real_Time >= delay)
        {
            noticeSeAudio.PlayOneShot(noticeSeClip);
            anim.SetBool("isPlaying", true);
            Real_Time = 0;
        }
        else
        {
            anim.SetBool("isPlaying", false);
        }
		
	}
}
