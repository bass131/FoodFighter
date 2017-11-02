using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

    Rigidbody2D rigid;
    public float JumpPower = 500.0f;

    bool isjump;

    public AudioClip jumpedSeClip;
    AudioSource jumpedSeAudio;


    // Use this for initialization
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();

        jumpedSeAudio = gameObject.AddComponent<AudioSource>();
        jumpedSeAudio.clip = jumpedSeClip;
        jumpedSeAudio.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }
    void Jump()
    {
        if (Input.GetKey(KeyCode.LeftAlt) && !isjump)
        {
            isjump = true;
            Debug.Log("점프시작");
            jumpedSeAudio.Play();
            rigid.AddForce(Vector3.up * JumpPower, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Road")
        {
            isjump = false;
        }
    }
}