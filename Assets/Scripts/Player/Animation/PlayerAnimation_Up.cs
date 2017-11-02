using UnityEngine;
using System.Collections;

public class PlayerAnimation_Up: MonoBehaviour {

	public Animator animator;

    private float StickRate = 1.0f;
    private float cornRate = 0.2f;
    private float jellyRate = 0.2f;

    private float NextAttack = 0.0f;

    private PlayerCRTL Player;
    //public SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start () 
	{
        animator = GetComponent<Animator>();
       // spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerCRTL>();
    }

    // Update is called once per frame
    void Update()
    {
        Move_Animation();
        Die();

    }

    void Move_Animation()
    {
        float x = Input.GetAxis("Horizontal");

        if (x > 0.1)
        {
            animator.SetBool("isWalk_Up", true);
            animator.SetBool("isFire_Up", false);
            if (Input.GetKey(KeyCode.LeftControl))
            {
                animator.SetBool("isWalk_Up", false);
                animator.SetBool("isFire_Up", true);
            }

        }
        else if (x < -0.1)
        {
            animator.SetBool("isWalk_Up", true);
            animator.SetBool("isFire_Up", false);
            if(Input.GetKey(KeyCode.LeftControl))
            {
                animator.SetBool("isWalk_Up", false);
                animator.SetBool("isFire_Up", true);
            }
        }

        else if(Input.GetKey(KeyCode.LeftControl))
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

    void Die()
    {
        if(Player.Hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    /*IEnumerator HitEffect()
    {
        int CountTime = 0;

        while (CountTime < 10)
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
    }*/
}



