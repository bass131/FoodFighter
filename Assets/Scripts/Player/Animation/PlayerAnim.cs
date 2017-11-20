using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerAnim : MonoBehaviour {
    public Animator animator;

    private float StickRate = 1.0f;
    private float cornRate = 0.2f;
    private float jellyRate = 0.2f;

    private float NextAttack = 0.0f;

    private Player Player;

    private RectTransform JoyStick;

    //public SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        Player = GameObject.FindWithTag("Player").GetComponent<Player>();
        JoyStick = GameObject.FindWithTag("JoyStick").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update () {
		
	}


    void Move_UpSide() // 상체 애니메이션.
    {
        float x = CrossPlatformInputManager.GetAxis("Horizontal");

        if (x > 0.1 && JoyStick.position.y < 175 && JoyStick.position.y > 60)
        {
            animator.SetBool("isWalk_Up", true);
            animator.SetBool("isFire_Up", false);
            if (Input.GetKey(KeyCode.LeftControl))
            {
                animator.SetBool("isWalk_Up", false);
                animator.SetBool("isFire_Up", true);
            }

        }
        else if (x < -0.1 && JoyStick.position.y < 175 && JoyStick.position.y > 60)
        {
            animator.SetBool("isWalk_Up", true);
            animator.SetBool("isFire_Up", false);
            if (Input.GetKey(KeyCode.LeftControl))
            {
                animator.SetBool("isWalk_Up", false);
                animator.SetBool("isFire_Up", true);
            }
        }

        else if (Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetBool("isWalk_Up", false);
            animator.SetBool("isFire_Up", true);
        }
        else
        {
            animator.SetBool("isWalk_Up", false);
            animator.SetBool("isFire_Up", false);
        }

    }

    void Move_DownSide() // 하체 애니메이션.
    {
        float x = CrossPlatformInputManager.GetAxis("Horizontal");

        if (x > 0.1 && JoyStick.position.y < 175 && JoyStick.position.y > 60)
        {
            animator.SetBool("isWalk_Bottom", true);

        }
        else if (x < -0.1 && JoyStick.position.y < 175 && JoyStick.position.y > 60)
        {
            animator.SetBool("isWalk_Bottom", true);
        }
        else
        {
            animator.SetBool("isWalk_Bottom", false);
        }

        Die_anim();
    }

    void Die_anim()
    {
        if (Player.HP <= 0)
        {
            animator.SetBool("attacked", false);
            animator.SetBool("Die", true);
        }
        else
        {
            animator.SetBool("Die", false);
        }
    }
}
