using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation_Bottom : MonoBehaviour {

    public Animator animator;

    private float StickRate = 1.0f;
    private float cornRate = 0.2f;
    private float jellyRate = 0.2f;

    private float NextAttack = 0.0f;

    public SpriteRenderer spriteRenderer;
    private PlayerCRTL Player;
   

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerCRTL>();
    }
	
	// Update is called once per frame
	void Update () {
        Move_Animation();
		
	}
    void Move_Animation()
    {
        float x = Input.GetAxis("Horizontal");

        if (x > 0.1)
        {
            animator.SetBool("isWalk_Bottom", true);

        }
        else if (x < -0.1)
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
        if(Player.Hp <= 0)
        {
            animator.SetBool("attacked", false);
            animator.SetBool("Die", true);
        }
        else
        {
            animator.SetBool("Die", false);
        }
    }

    IEnumerator HitEffect()
    {
        int CountTime = 0;

        while (CountTime < 6)
        {
            if (CountTime % 2 == 0)
            {
                spriteRenderer.color = new Color32(255, 255, 255, 90);                
            }
            else
            {
                spriteRenderer.color = new Color32(255, 255, 255, 180);                
            }

            yield return new WaitForSeconds(0.2f);

            CountTime++;

        }

        spriteRenderer.color = new Color32(255, 255, 255, 255);
    

        yield return null;
    }
}
